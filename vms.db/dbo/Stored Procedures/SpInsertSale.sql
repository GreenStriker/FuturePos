-- =============================================
-- Author:		Sabbir Ahmed Osmani
-- Create date: July 29, 2019
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertSale]
    @InvoiceNo NVARCHAR(50),
    @VatChallanNo NVARCHAR(50),
    @OrganizationId INT,
    @DiscountOnTotalPrice DECIMAL(18, 2),
    @IsVatDeductedInSource BIT,
    @CustomerId INT,
    @ReceiverName NVARCHAR(200),
    @ReceiverContactNo VARCHAR(20),
    @ShippingAddress NVARCHAR(200),
    @ShippingCountryId INT,
    @SalesTypeId INT,
    @SalesDeliveryTypeId INT,
    @WorkOrderNo NVARCHAR(50),
    @SalesDate DATETIME,
    @ExpectedDeliveryDate DATETIME,
    @DeliveryDate DATETIME,
    @DeliveryMethodId INT,
    @ExportTypeId INT,
    @LcNo NVARCHAR(50),
    @LcDate DATETIME,
    @BillOfEntry NVARCHAR(50),
    @BillOfEntryDate DATETIME,
    @DueDate DATETIME,
    @TermsOfLc NVARCHAR(500),
    @CustomerPoNumber NVARCHAR(50),
    @IsComplete BIT,
    @IsTaxInvoicePrined BIT,
    @CreatedBy INT,
    @CreatedTime DATETIME,
    @SalesDetailsJson NVARCHAR(MAX),
    @PaymentReceiveJson NVARCHAR(MAX),
    @ContentJson NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(100) = N'';
        DECLARE @NoOfIteams INT,
                @TotalPriceWithoutVat DECIMAL(18, 2),
                @TotalDiscountOnIndividualProduct DECIMAL(18, 2),
                @TotalVAT DECIMAL(18, 2),
                @TotalSupplimentaryDuty DECIMAL(18, 2),
                @PaymentReceiveAmount DECIMAL(18, 2),
                @SalesId INT,
                @TaxInvoicePrintedTime DATETIME = NULL;

        IF @IsTaxInvoicePrined = 1
        BEGIN
            SET @TaxInvoicePrintedTime = GETDATE();
        END;


        DECLARE @SlsDtlMain TABLE
        (
            [SlsDtlMainSl] [INT] NOT NULL,
            [ProductId] [INT] NOT NULL,
            [ProductVATTypeId] [INT] NOT NULL,
            [Quantity] [DECIMAL](18, 2) NOT NULL,
            [UnitPrice] [DECIMAL](18, 2) NOT NULL,
            [DiscountPerItem] [DECIMAL](18, 2) NOT NULL,
            [VATPercent] [DECIMAL](18, 2) NOT NULL,
            [SupplementaryDutyPercent] [DECIMAL](18, 2) NOT NULL,
            [MeasurementUnitId] [INT] NOT NULL
        );


        DECLARE @SlsDtl TABLE
        (
            [ProductId] [INT] NOT NULL,
            [ProductVATTypeId] [INT] NOT NULL,
            [StockInId] [INT] NOT NULL,
            [Quantity] [DECIMAL](18, 2) NOT NULL,
            [UnitPrice] [DECIMAL](18, 2) NOT NULL,
            [DiscountPerItem] [DECIMAL](18, 2) NOT NULL,
            [VATPercent] [DECIMAL](18, 2) NOT NULL,
            [SupplementaryDutyPercent] [DECIMAL](18, 2) NOT NULL,
            [MeasurementUnitId] [INT] NOT NULL
        );

        DECLARE @StockIn TABLE
        (
            [StockInId] [INT] NOT NULL,
            [CurrentStock] [DECIMAL](18, 2) NOT NULL
        );
        DECLARE @PaymentReceive TABLE
        (
            [ReceivedPaymentMethodId] [INT] NOT NULL,
            [ReceiveAmount] [DECIMAL](18, 2) NOT NULL,
            [ReceiveDate] [DATETIME] NOT NULL
        );

        SET @CreatedTime = ISNULL(@CreatedTime, GETDATE());
        SET @SalesDate = ISNULL(@SalesDate, @CreatedTime);

        --Need to add logic 
        --SET @RemainingAmount = ISNULL(@InvoiceNo,COUNT(s.ProductId));--For new invoice	
        DECLARE @MaxSalseId INT;
        SELECT @MaxSalseId = MAX(SalesId)
        FROM dbo.Sales;
        SET @InvoiceNo = ISNULL(@InvoiceNo, 'INVOICE:' + CAST(ISNULL(@MaxSalseId, 0) + 1 AS VARCHAR(8)));


        IF @PaymentReceiveJson IS NOT NULL
           AND LEN(@PaymentReceiveJson) > 0
        BEGIN
            INSERT INTO @PaymentReceive
            (
                ReceivedPaymentMethodId,
                ReceiveAmount,
                ReceiveDate
            )
            SELECT jd.ReceivedPaymentMethodId,
                   jd.ReceiveAmount,
                   jd.ReceiveDate
            FROM
                OPENJSON(@PaymentReceiveJson)
                WITH
                (
                    [ReceivedPaymentMethodId] [INT],
                    [ReceiveAmount] [DECIMAL](18, 2),
                    [ReceiveDate] [DATETIME]
                ) jd;

        END;


        INSERT INTO @SlsDtlMain
        (
            SlsDtlMainSl,
            ProductId,
            ProductVATTypeId,
            Quantity,
            UnitPrice,
            DiscountPerItem,
            VATPercent,
            SupplementaryDutyPercent,
            MeasurementUnitId
        )
        SELECT ROW_NUMBER() OVER (ORDER BY jd.ProductId) AS SlsDtlMainSl, -- SlsDtlMainSl - int
               jd.ProductId,                                              -- ProductId - int
               pv.ProductVATTypeId,                                       -- ProductVATTypeId - int
               jd.Quantity,                                               -- Quantity - decimal(18, 2)
               jd.UnitPrice,                                              -- UnitPrice - decimal(18, 2)
               jd.DiscountPerItem,                                        -- DiscountPerItem - decimal(18, 2)
               jd.Vatpercent,                                             -- VATPercent - decimal(18, 2)
               jd.SupplementaryDutyPercent,                               -- SupplementaryDutyPercent - decimal(18, 2)
               jd.MeasurementUnitId                                       -- MeasurementUnitId - int
        FROM
            OPENJSON(@SalesDetailsJson)
            WITH
            (
                [ProductId] [INT],
                [Quantity] [DECIMAL](18, 2),
                [UnitPrice] [DECIMAL](18, 2),
                [DiscountPerItem] [DECIMAL](18, 2),
                [Vatpercent] [DECIMAL](18, 2),
                [SupplementaryDutyPercent] [DECIMAL](18, 2),
                [MeasurementUnitId] [INT]
            ) jd
            INNER JOIN dbo.ProductVATs pv
                ON pv.ProductId = jd.ProductId
                   AND pv.IsActive = 1;

        SELECT @PaymentReceiveAmount = ISNULL(SUM(pr.ReceiveAmount), 0)
        FROM @PaymentReceive pr;
        SELECT @NoOfIteams = MAX(sdm.SlsDtlMainSl),
               @TotalPriceWithoutVat = SUM(sdm.UnitPrice * sdm.Quantity),
               @TotalDiscountOnIndividualProduct = SUM(sdm.DiscountPerItem * sdm.Quantity),
               @TotalVAT = SUM(sdm.UnitPrice * sdm.Quantity * sdm.VATPercent / 100),
               @TotalSupplimentaryDuty = SUM(sdm.UnitPrice * sdm.Quantity * sdm.SupplementaryDutyPercent / 100)
        FROM @SlsDtlMain sdm;


        DECLARE @ProcessingOrderDetailRow INT = 0,
                @ProdTotalStock DECIMAL(18, 2) = 0;
        IF @NoOfIteams < 1
        BEGIN
            SET @ErrorMsg = N'Sale is not possible without products!!';
            RAISERROR(   'Sale is not possible without products!!', -- Message text.  
                         20,                                        -- Severity.  
                         -1                                         -- State.  
                     );
        END;

        --Insert Sales
        INSERT INTO dbo.Sales
        (
            InvoiceNo,
            VatChallanNo,
            OrganizationId,
            NoOfIteams,
            TotalPriceWithoutVat,
            DiscountOnTotalPrice,
            TotalDiscountOnIndividualProduct,
            TotalVAT,
            TotalSupplimentaryDuty,
            IsVatDeductedInSource,
            PaymentReceiveAmount,
            CustomerId,
            ReceiverName,
            ReceiverContactNo,
            ShippingAddress,
            ShippingCountryId,
            SalesTypeId,
            SalesDeliveryTypeId,
            WorkOrderNo,
            SalesDate,
            ExpectedDeliveryDate,
            DeliveryDate,
            DeliveryMethodId,
            ExportTypeId,
            LcNo,
            LcDate,
            BillOfEntry,
            BillOfEntryDate,
            DueDate,
            TermsOfLc,
            CustomerPoNumber,
            IsComplete,
            IsTaxInvoicePrined,
            TaxInvoicePrintedTime,
            MushakGenerationId,
            CreatedBy,
            CreatedTime
        )
        VALUES
        (   @InvoiceNo,                        -- InvoiceNo - nvarchar(50)
            @VatChallanNo, @OrganizationId,    -- OrganizationId - int
            @NoOfIteams,                       -- NoOfIteams - int
            @TotalPriceWithoutVat,             -- TotalPriceWithoutVat - decimal(18, 2)
            @DiscountOnTotalPrice,             -- DiscountOnTotalPrice - decimal(18, 2)
            @TotalDiscountOnIndividualProduct, -- TotalDiscountOnIndividualProduct - decimal(18, 2)
            @TotalVAT,                         -- TotalVAT - decimal(18, 2)
            @TotalSupplimentaryDuty,           -- TotalSupplimentaryDuty - decimal(18, 2)
            @IsVatDeductedInSource,            -- IsVatDeductedInSource - bit
            @PaymentReceiveAmount,             -- PaymentReceiveAmount - decimal(18, 2)
            @CustomerId,                       -- CustomerId - int
            @ReceiverName,                     -- ReceiverName - nvarchar(200)
            @ReceiverContactNo,                -- ReceiverContactNo - varchar(20)
            @ShippingAddress,                  -- ShippingAddress - nvarchar(200)
            @ShippingCountryId,                -- ShippingCountryId - int
            @SalesTypeId,                      -- SalesTypeId - int
            @SalesDeliveryTypeId,              -- SalesDeliveryTypeId - int
            @WorkOrderNo,                      -- WorkOrderNo - nvarchar(50)
            @SalesDate,                        -- SalesDate - datetime
            @ExpectedDeliveryDate,             -- ExpectedDeleveryDate - datetime
            @DeliveryDate,                     -- DeliveryDate - datetime
            @DeliveryMethodId,                 -- DeliveryMethodId - int
            @ExportTypeId,                     -- ExportTypeId - int
            @LcNo,                             -- LcNo - nvarchar(50)
            @LcDate,                           -- LcDate - datetime
            @BillOfEntry,                      -- BillOfEntry - nvarchar(50)
            @BillOfEntryDate,                  -- BillOfEntryDate - datetime
            @DueDate,                          -- DueDate - datetime
            @TermsOfLc,                        -- TermsOfLc - nvarchar(500)
            @CustomerPoNumber,                 -- CustomerPoNumber - nvarchar(50)
            @IsComplete,                       -- IsComplete - bit
            @IsTaxInvoicePrined,               -- IsTaxInvoicePrined - bit
            @TaxInvoicePrintedTime, NULL,      -- MushakGenerationId - int
            @CreatedBy,                        -- CreatedBy - int
            @CreatedTime                       -- CreatedTime - datetime
            );


        --Get SalesId
        SET @SalesId = SCOPE_IDENTITY();

        --Variables for sales Detail Loop
        DECLARE @ProductId INT,
                @ProductQty DECIMAL(18, 2),
                @ProductVATTypeId INT,
                @StockInId INT,
                @UnitPrice DECIMAL(18, 2),
                @DiscountPerItem DECIMAL(18, 2),
                @VATPercent DECIMAL(18, 2),
                @SupplementaryDutyPercent DECIMAL(18, 2),
                @MeasurementUnitId INT,
                @MaxSaleQty DECIMAL(18, 2),
                @ProdSaleQty DECIMAL(18, 2);
        --End Variables for sales Detail loop

        --Insert sales using while loop
        WHILE @ProcessingOrderDetailRow < @NoOfIteams
        BEGIN
            SET @ProcessingOrderDetailRow += 1;

            SELECT @ProductId = sdm.ProductId,
                   @ProductVATTypeId = sdm.ProductVATTypeId,
                   @ProductQty = sdm.Quantity,
                   @UnitPrice = sdm.UnitPrice,
                   @DiscountPerItem = sdm.DiscountPerItem,
                   @VATPercent = sdm.VATPercent,
                   @SupplementaryDutyPercent = sdm.SupplementaryDutyPercent,
                   @MeasurementUnitId = sdm.MeasurementUnitId
            FROM @SlsDtlMain sdm
            WHERE sdm.SlsDtlMainSl = @ProcessingOrderDetailRow;

            DELETE FROM @StockIn;
            INSERT INTO @StockIn
            (
                StockInId,
                CurrentStock
            )
            SELECT si.StockInId,   -- StockInId - int
                   si.CurrentStock -- CurrentStock - decimal(18, 2)
            FROM dbo.StockIn si
            WHERE si.CurrentStock > 0
                  AND si.OrganizationId = @OrganizationId
                  AND si.ProductId = @ProductId
                  AND si.IsActive = 1;
            SELECT @ProdTotalStock = SUM(si.CurrentStock)
            FROM @StockIn si;

            IF @ProductQty > @ProdTotalStock
            BEGIN
                SET @ErrorMsg = N'Sales quantity exceeds stock!!';
                RAISERROR(   'Sales quantity exceeds stock!!', -- Message text.  
                             20,                               -- Severity.  
                             -1                                -- State.  
                         );
            END;

            WHILE @ProductQty > 0
            BEGIN

                SELECT TOP (1)
                       @MaxSaleQty = si.CurrentStock,
                       @StockInId = si.StockInId
                FROM @StockIn si
                --WHERE si.StockInId = MIN(si.StockInId);
                ORDER BY si.StockInId;



                IF @ProductQty > @MaxSaleQty
                BEGIN
                    SET @ProdSaleQty = @MaxSaleQty;
                END;
                ELSE
                BEGIN
                    SET @ProdSaleQty = @ProductQty;
                END;

                INSERT INTO @SlsDtl
                (
                    ProductId,
                    ProductVATTypeId,
                    StockInId,
                    Quantity,
                    UnitPrice,
                    DiscountPerItem,
                    VATPercent,
                    SupplementaryDutyPercent,
                    MeasurementUnitId
                )
                VALUES
                (   @ProductId,                -- ProductId - int
                    @ProductVATTypeId,         -- ProductVATTypeId - int
                    @StockInId,                -- StockInId - int
                    @ProdSaleQty,              -- Quantity - decimal(18, 2)
                    @UnitPrice,                -- UnitPrice - decimal(18, 2)
                    @DiscountPerItem,          -- DiscountPerItem - decimal(18, 2)
                    @VATPercent,               -- VATPercent - decimal(18, 2)
                    @SupplementaryDutyPercent, -- SupplementaryDutyPercent - decimal(18, 2)
                    @MeasurementUnitId         -- MeasurementUnitId - int
                    );

                SET @ProductQty = @ProductQty - @ProdSaleQty;
                DELETE FROM @StockIn
                WHERE StockInId = @StockInId;

                UPDATE dbo.StockIn
                SET SaleQuantity = ISNULL(SaleQuantity, 0) + @ProdSaleQty
                WHERE StockInId = @StockInId;
            END;

        END;



        --End Insert sales using while loop

        --Insert Sales Detail
        INSERT INTO dbo.SalesDetails
        (
            SalesId,
            ProductId,
            ProductVATTypeId,
            StockInId,
            Quantity,
            UnitPrice,
            DiscountPerItem,
            VATPercent,
            SupplementaryDutyPercent,
            MeasurementUnitId,
            CreatedBy,
            CreatedTime
        )
        SELECT @SalesId,                        -- SalesId - int
               slsDtl.ProductId,                -- ProductId - int
               slsDtl.ProductVATTypeId,         -- ProductVATTypeId - int
               slsDtl.StockInId,                -- StockInId - int
               slsDtl.Quantity,                 -- Quantity - decimal(18, 2)
               slsDtl.UnitPrice,                -- UnitPrice - decimal(18, 2)
               slsDtl.DiscountPerItem,          -- DiscountPerItem - decimal(18, 2)
               slsDtl.VATPercent,               -- VATPercent - decimal(18, 2)
               slsDtl.SupplementaryDutyPercent, -- SupplementaryDutyPercent - decimal(18, 2)
               slsDtl.MeasurementUnitId,        -- MeasurementUnitId - int
               @CreatedBy,                      -- CreatedBy - int
               @CreatedTime                     -- CreatedTime - datetime
        FROM @SlsDtl slsDtl;
        --End Insert Sales Detail

        --Insert Payment

        INSERT INTO dbo.SalesPaymentReceive
        (
            SalesId,
            ReceivedPaymentMethodId,
            ReceiveAmount,
            ReceiveDate,
            CreatedBy,
            CreatedTime
        )
        SELECT @SalesId,                   -- SalesId - int
               pr.ReceivedPaymentMethodId, -- ReceivedPaymentMethodId - int
               pr.ReceiveAmount,           -- ReceiveAmount - decimal(18, 2)
               pr.ReceiveDate,             -- ReceiveDate - datetime
               @CreatedBy,                 -- CreatedBy - int
               @CreatedTime                -- CreatedTime - datetime
        FROM @PaymentReceive pr;
        --Insert Payment

        --Insert content
        IF @ContentJson IS NOT NULL
           AND LEN(@ContentJson) > 0
        BEGIN
            INSERT INTO dbo.Content
            (
                DocumentTypeId,
                OrganizationId,
                FileUrl,
                MimeType,
                Node,
                ObjectId,
                ObjectPrimaryKey,
                IsActive,
                CreatedBy,
                CreatedTime
            )
            SELECT jd.DocumentTypeId,
                   @OrganizationId,
                   jd.FileUrl,
                   jd.MimeType,
                   NULL,
                   24,
                   @SalesId,
                   1,
                   @CreatedBy,
                   @CreatedTime
            FROM
                OPENJSON(@ContentJson)
                WITH
                (
                    [DocumentTypeId] [INT],
                    [FileUrl] [NVARCHAR](500),
                    [MimeType] [NVARCHAR](50)
                ) jd;
        END;
        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH;

END;