

-- =============================================
-- Author:		Sabbir Ahmed Osmani
-- Create date: July 9, 2019
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertPurchase]
    @OrganizationId INT,
    @VendorId INT,
    @VatChallanNo NVARCHAR(50),
    @VatChallanIssueDate DATETIME,
    @VendorInvoiceNo NVARCHAR(50),
    @InvoiceNo NVARCHAR(50),
    @PurchaseDate DATETIME,
    @PurchaseTypeId INT,
    @PurchaseReasonId INT,
    @DiscountOnTotalPrice DECIMAL(18, 2),
    @IsVatDeductedInSource BIT,
    @ExpectedDeliveryDate DATETIME,
    @DeliveryDate DATETIME,
    @LcNo NVARCHAR(50),
    @LcDate DATETIME,
    @BillOfEntry NVARCHAR(50),
    @BillOfEntryDate DATETIME,
    @DueDate DATETIME,
    @TermsOfLc NVARCHAR(500),
    @PoNumber NVARCHAR(50),
    @MushakGenerationId INT,
    @IsComplete BIT,
    @CreatedBy INT,
    @CreatedTime DATETIME,
    @PurchaseOrderDetailsJson NVARCHAR(MAX),
    @PurchasePaymentJson NVARCHAR(MAX),
    @ContentJson NVARCHAR(MAX)
AS
BEGIN
    SET @CreatedTime = ISNULL(@CreatedTime, GETDATE());
    DECLARE @PurchaseDetails TABLE
    (
        [ProductId] [INT] NOT NULL,
        [ProductVATTypeId] [INT] NOT NULL,
        [Quantity] [DECIMAL](18, 2) NOT NULL,
        [UnitPrice] [DECIMAL](18, 2) NOT NULL,
        [DiscountPerItem] [DECIMAL](18, 2) NOT NULL,
        [ImportDutyPercent] [DECIMAL](18, 2) NOT NULL,
        [RegulatoryDutyPercent] [DECIMAL](18, 2) NOT NULL,
        [SupplementaryDutyPercent] [DECIMAL](18, 2) NOT NULL,
        [VATPercent] [DECIMAL](18, 2) NOT NULL,
        [AdvanceTaxPercent] [DECIMAL](18, 2) NOT NULL,
        [AdvanceIncomeTaxPercent] [DECIMAL](18, 2) NOT NULL,
        [MeasurementUnitId] [INT] NOT NULL
    );
    DECLARE @PurchasePayment TABLE
    (
        [PaymentMethodId] [INT] NOT NULL,
        [PaidAmount] [DECIMAL](18, 2) NOT NULL,
        [PaymentDate] [DATETIME] NOT NULL
    );
    DECLARE @NoOfIteams INT,
            @TotalDiscountOnIndividualProduct DECIMAL(18, 2),
            @TotalImportDuty DECIMAL(18, 2),
            @TotalRegulatoryDuty DECIMAL(18, 2),
            @TotalSupplementaryDuty DECIMAL(18, 2),
            @TotalVAT DECIMAL(18, 2),
            @TotalAdvanceTax DECIMAL(18, 2),
            @TotalAdvanceIncomeTax DECIMAL(18, 2),
            @TotalPriceWithoutVat DECIMAL(18, 2),
            @PaidAmount DECIMAL(18, 2),
            @PurchaseId INT;


    INSERT INTO @PurchaseDetails
    (
        ProductId,
        ProductVATTypeId,
        Quantity,
        UnitPrice,
        DiscountPerItem,
        ImportDutyPercent,
        RegulatoryDutyPercent,
        SupplementaryDutyPercent,
        VATPercent,
        AdvanceTaxPercent,
        AdvanceIncomeTaxPercent,
        MeasurementUnitId
    )
    SELECT jd.[ProductId],
           pv.[ProductVATTypeId],
           jd.[Quantity],
           jd.[UnitPrice],
           jd.[DiscountPerItem],
           jd.[ImportDutyPercent],
           jd.[RegulatoryDutyPercent],
           jd.[SupplementaryDutyPercent],
           jd.[Vatpercent],
           jd.[AdvanceTaxPercent],
           jd.[AdvanceIncomeTaxPercent],
           jd.[MeasurementUnitId]
    FROM
        OPENJSON(@PurchaseOrderDetailsJson)
        WITH
        (
            [ProductId] [INT],
            [Quantity] [DECIMAL](18, 2),
            [UnitPrice] [DECIMAL](18, 2),
            [DiscountPerItem] [DECIMAL](18, 2),
            [ImportDutyPercent] [DECIMAL](18, 2),
            [RegulatoryDutyPercent] [DECIMAL](18, 2),
            [SupplementaryDutyPercent] [DECIMAL](18, 2),
            [Vatpercent] [DECIMAL](18, 2),
            [AdvanceTaxPercent] [DECIMAL](18, 2),
            [AdvanceIncomeTaxPercent] [DECIMAL](18, 2),
            [MeasurementUnitId] [INT]
        ) jd
        INNER JOIN dbo.ProductVATs pv
            ON pv.ProductId = jd.ProductId
    WHERE pv.IsActive = 1;

    IF @PurchasePaymentJson IS NOT NULL
       AND LEN(@PurchasePaymentJson) > 0
    BEGIN
        INSERT INTO @PurchasePayment
        (
            PaymentMethodId,
            PaidAmount,
            PaymentDate
        )
        SELECT jd.PaymentMethodId,
               jd.PaidAmount,
               jd.PaymentDate
        FROM
            OPENJSON(@PurchasePaymentJson)
            WITH
            (
                [PaymentMethodId] [INT],
                [PaidAmount] [DECIMAL](18, 2),
                [PaymentDate] [DATETIME]
            ) jd;

    END;


    SELECT @NoOfIteams = COUNT(pod.ProductId),
           @TotalDiscountOnIndividualProduct = SUM(pod.Quantity * pod.DiscountPerItem),
           @TotalImportDuty = SUM((pod.ImportDutyPercent * pod.Quantity * pod.UnitPrice) / 100),
           @TotalRegulatoryDuty = SUM((pod.RegulatoryDutyPercent * pod.Quantity * pod.UnitPrice) / 100),
           @TotalSupplementaryDuty = SUM((pod.SupplementaryDutyPercent * pod.Quantity * pod.UnitPrice) / 100),
           @TotalVAT = SUM((pod.VATPercent * pod.Quantity * pod.UnitPrice) / 100),
           @TotalAdvanceTax = SUM((pod.VATPercent * pod.Quantity * pod.UnitPrice) / 100),
           @TotalAdvanceIncomeTax = SUM((pod.AdvanceIncomeTaxPercent * pod.Quantity * pod.UnitPrice) / 100),
           @TotalPriceWithoutVat = SUM(pod.Quantity * pod.UnitPrice)
    FROM @PurchaseDetails pod;

    SELECT @PaidAmount = ISNULL(SUM(pp.PaidAmount), 0)
    FROM @PurchasePayment pp;

    --Insert Purchase Information
    INSERT INTO dbo.Purchase
    (
        OrganizationId,
        VendorId,
        VatChallanNo,
        VatChallanIssueDate,
        VendorInvoiceNo,
        InvoiceNo,
        PurchaseDate,
        PurchaseTypeId,
        PurchaseReasonId,
        NoOfIteams,
        TotalPriceWithoutVat,
        DiscountOnTotalPrice,
        TotalDiscountOnIndividualProduct,
        TotalImportDuty,
        TotalRegulatoryDuty,
        TotalSupplementaryDuty,
        TotalVAT,
        TotalAdvanceTax,
        TotalAdvanceIncomeTax,
        IsVatDeductedInSource,
        PaidAmount,
        ExpectedDeliveryDate,
        DeliveryDate,
        LcNo,
        LcDate,
        BillOfEntry,
        BillOfEntryDate,
        DueDate,
        TermsOfLc,
        PoNumber,
        MushakGenerationId,
        IsComplete,
        CreatedBy,
        CreatedTime
    )
    VALUES
    (@OrganizationId, @VendorId, @VatChallanNo, @VatChallanIssueDate, @VendorInvoiceNo, @InvoiceNo, @PurchaseDate,
     @PurchaseTypeId, @PurchaseReasonId, @NoOfIteams, @TotalPriceWithoutVat, @DiscountOnTotalPrice,
     @TotalDiscountOnIndividualProduct, @TotalImportDuty, @TotalRegulatoryDuty, @TotalSupplementaryDuty, @TotalVAT,
     @TotalAdvanceTax, @TotalAdvanceIncomeTax, @IsVatDeductedInSource, @PaidAmount, @ExpectedDeliveryDate,
     @DeliveryDate, @LcNo, @LcDate, @BillOfEntry, @BillOfEntryDate, @DueDate, @TermsOfLc, @PoNumber,
     @MushakGenerationId, @IsComplete, @CreatedBy, @CreatedTime);




    --Get PurchaseId
    SET @PurchaseId = SCOPE_IDENTITY();


    --Insert Purchase Details
    INSERT INTO dbo.PurchaseDetails
    (
        PurchaseId,
        ProductId,
        ProductVATTypeId,
        Quantity,
        UnitPrice,
        DiscountPerItem,
        ImportDutyPercent,
        RegulatoryDutyPercent,
        SupplementaryDutyPercent,
        VATPercent,
        AdvanceTaxPercent,
        AdvanceIncomeTaxPercent,
        MeasurementUnitId,
        CreatedBy,
        CreatedTime
    )
    SELECT @PurchaseId,                 -- PurchaseId - int
           pd.ProductId,                -- ProductId - int
           pd.ProductVATTypeId,         -- ProductVATTypeId - int
           pd.Quantity,                 -- Quantity - decimal(18, 2)
           pd.UnitPrice,                -- UnitPrice - decimal(18, 2)
           pd.DiscountPerItem,          -- DiscountPerItem - decimal(18, 2)
           pd.ImportDutyPercent,        -- ImportDutyPercent - decimal(18, 2)
           pd.RegulatoryDutyPercent,    -- RegulatoryDutyPercent - decimal(18, 2)
           pd.SupplementaryDutyPercent, -- SupplementaryDutyPercent - decimal(18, 2)
           pd.VATPercent,               -- VATPercent - decimal(18, 2)
           pd.AdvanceTaxPercent,        -- AdvanceTaxPercent - decimal(18, 2)
           pd.AdvanceIncomeTaxPercent,  -- AdvanceIncomeTaxPercent - decimal(18, 2)
           pd.MeasurementUnitId,        -- MeasurementUnitId - int
           @CreatedBy,                  -- CreatedBy - int
           @CreatedTime                 -- CreatedTime - datetime
    FROM @PurchaseDetails pd;


    --Insert into Stockin
    --Variables for Insert into Stockin
    DECLARE @PurchaseDetailId INT,
            @PurchaseProductId INT,
            @PurchaseProductQuantity DECIMAL(18, 2),
            @PurchaseProdMeasurementUnitId INT,
            @PurchaseUnitPrice DECIMAL(18, 2),
            @InitStock DECIMAL(18, 2),
            @InitUnitPrice DECIMAL(18, 2);
    --Variables for Insert into Stockin InitialQuantity
    DECLARE PURCH_DTL_CURSOR CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY FOR
    SELECT purchDtl.PurchaseDetailId,
           purchDtl.ProductId,
           purchDtl.Quantity,
           purchDtl.MeasurementUnitId,
           purchDtl.UnitPrice - purchDtl.DiscountPerItem AS PurchaseUnitPrice
    FROM dbo.PurchaseDetails purchDtl
    WHERE purchDtl.PurchaseId = @PurchaseId;

    OPEN PURCH_DTL_CURSOR;
    FETCH NEXT FROM PURCH_DTL_CURSOR
    INTO @PurchaseDetailId,
         @PurchaseProductId,
         @PurchaseProductQuantity,
         @PurchaseProdMeasurementUnitId,
         @PurchaseUnitPrice;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @InitStock = ISNULL(SUM(si.CurrentStock), 0)
        FROM dbo.StockIn si
        WHERE si.CurrentStock > 0
              AND si.ProductId = @PurchaseProductId;
        SELECT TOP (1)
               @InitUnitPrice = ISNULL(si.EndUnitPriceWithoutVat, 0)
        FROM dbo.StockIn si
        WHERE si.ProductId = @PurchaseProductId
        ORDER BY si.StockInId DESC;
        --PRINT (@PurchaseDetailId);
        INSERT INTO dbo.StockIn
        (
            OrganizationId,
            ProductId,
            ProductionReceiveId,
            PurchaseDetailId,
            InitialQuantity,
            InQuantity,
            InitUnitPriceWithoutVat,
            EndUnitPriceWithoutVat,
            MeasurementUnitId,
            SaleQuantity,
            DamageQuantity,
            UsedInProductionQuantity,
            PurchaseReturnQty,
            SalesReturnQty,
            IsActive,
            CreatedBy,
            CreatedTime
        )
        VALUES
        (   @OrganizationId,                                      -- OrganizationId - int
            @PurchaseProductId,                                   -- ProductId - int
            NULL,                                                 -- ProductionReceiveId - bigint
            @PurchaseDetailId,                                    -- PurchaseDetailId - int
            @InitStock,                                           -- InitialQuantity - decimal(18, 2)
            @PurchaseProductQuantity,                             -- InQuantity - decimal(18, 2)
            @InitUnitPrice,                                       -- InitUnitPriceWithoutVat - decimal(18, 2)
            (ISNULL(@InitStock * @InitUnitPrice, 0) + @PurchaseProductQuantity * @PurchaseUnitPrice)
            / (ISNULL(@InitStock, 0) + @PurchaseProductQuantity), -- EndUnitPriceWithoutVat - decimal(18, 2)
            @PurchaseProdMeasurementUnitId,                       -- MeasurementUnitId - int
            0,                                                    -- SaleQuantity - decimal(18, 2)
            0,                                                    -- DamageQuantity - decimal(18, 2)
            0,                                                    -- UsedInProductionQuantity - decimal(18, 2)
            0,                                                    -- PurchaseReturnQty - decimal(18, 2)
            0,                                                    -- SalesReturnQty - decimal(18, 2)
            1,                                                    -- IsActive - bit
            @CreatedBy,                                           -- CreatedBy - int
            @CreatedTime                                          -- CreatedTime - datetime
            );
        FETCH NEXT FROM PURCH_DTL_CURSOR
        INTO @PurchaseDetailId,
             @PurchaseProductId,
             @PurchaseProductQuantity,
             @PurchaseProdMeasurementUnitId,
             @PurchaseUnitPrice;
    END;
    CLOSE PURCH_DTL_CURSOR;
    DEALLOCATE PURCH_DTL_CURSOR;

    INSERT INTO dbo.PurchasePayment
    (
        PurchaseId,
        PaymentMethodId,
        PaidAmount,
        PaymentDate,
        CreatedBy,
        CreatedTime
    )
    SELECT @PurchaseId,        -- PurchaseId - int
           pp.PaymentMethodId, -- PaymentMethodId - int
           pp.PaidAmount,      -- PaidAmount - decimal(18, 2)
           pp.PaymentDate,     -- PaymentDate - datetime
           @CreatedBy,         -- CreatedBy - int
           @CreatedTime        -- CreatedTime - datetime
    FROM @PurchasePayment pp;


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
               21,
               @PurchaseId,
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
END;
