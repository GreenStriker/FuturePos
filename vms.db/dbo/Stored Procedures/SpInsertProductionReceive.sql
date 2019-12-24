-- =============================================
-- Author:		Sabbir Ahmed Osmani
-- Create date: July 29, 2019
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertProductionReceive]
    @BatchNo NVARCHAR(50),
    @OrganizationId INT,
    @ProductionId INT,
    @ProductId INT,
    @ReceiveQuantity DECIMAL(18, 2),
    @MeasurementUnitId INT,
    @ReceiveTime DATETIME,
    @CreatedBy INT,
    @CreatedTime DATETIME,
    @BomJson NVARCHAR(MAX),
    @ContentJson NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @NoOfIteamsUsedInBom INT,
                @ErrorMsg NVARCHAR(100) = N'';

        DECLARE @BomMain TABLE
        (
            [BomMainSl] [INT] NOT NULL,
            [RawMaterialId] [INT] NOT NULL,
            [UsedQuantity] [DECIMAL](18, 2) NOT NULL,
            [MeasurementUnitId] [INT] NOT NULL
        );


        DECLARE @Bom TABLE
        (
            [BomSl] [INT] NOT NULL,
            [RawMaterialId] [INT] NOT NULL,
            [StockInId] [INT] NOT NULL,
            [UnitPrice] [DECIMAL](18, 2) NOT NULL,
            [UsedQuantity] [DECIMAL](18, 2) NOT NULL,
            [MeasurementUnitId] [INT] NOT NULL
        );

        DECLARE @StockIn TABLE
        (
            [StockInId] [INT] NOT NULL,
            [CurrentStock] [DECIMAL](18, 2) NOT NULL,
            [MeasurementUnitId] INT NOT NULL,
            [UnitPrice] [DECIMAL](18, 2) NOT NULL
        );

        SET @CreatedTime = ISNULL(@CreatedTime, GETDATE());
        SET @ReceiveTime = ISNULL(@ReceiveTime, @CreatedTime);

        --Insert Data into Bom Main
        INSERT INTO @BomMain
        (
            BomMainSl,
            RawMaterialId,
            UsedQuantity,
            MeasurementUnitId
        )
        SELECT ROW_NUMBER() OVER (ORDER BY jd.RawMaterialId) AS BomMainSl, -- BomMainSl - int
               jd.RawMaterialId,                                           -- RawMaterialId - int
               jd.UsedQuantity,                                            -- UsedQuantity - decimal(18, 2)
               jd.MeasurementUnitId                                        -- MeasurementUnitId - int 
        FROM
            OPENJSON(@BomJson)
            WITH
            (
                [RawMaterialId] [INT],
                [UsedQuantity] [DECIMAL](18, 2),
                [MeasurementUnitId] [INT]
            ) jd;
        --End Insert Data into Bom Main

        DECLARE @ProcessingBomRow INT = 0,
                @ProdTotalStock DECIMAL(18, 2) = 0;
        SELECT @NoOfIteamsUsedInBom = COUNT(bm.BomMainSl)
        FROM @BomMain bm;

        --Variables for sales Bom Loop
        DECLARE @ProductQtyUsedInBom DECIMAL(18, 2),
                @RawMaterialId INT,
                @RawMaterialMeasurementUnitId INT,
                @StockInId INT,
                @RawMaterialUnitPrice DECIMAL(18, 2),
                @MaxUsageQty DECIMAL(18, 2),
                @ProdUsageQty DECIMAL(18, 2),
                @BomSl INT = 1;
        --End Variables for sales Detail loop

        --Insert sales using while loop
        WHILE @ProcessingBomRow < @NoOfIteamsUsedInBom
        BEGIN
            SET @ProcessingBomRow += 1;

            SELECT @ProductQtyUsedInBom = bm.UsedQuantity,
                   @RawMaterialId = bm.RawMaterialId,
                   @RawMaterialMeasurementUnitId = bm.MeasurementUnitId
            FROM @BomMain bm
            WHERE bm.BomMainSl = @ProcessingBomRow;

            DELETE FROM @StockIn;
            INSERT INTO @StockIn
            (
                StockInId,
                CurrentStock,
                MeasurementUnitId,
                UnitPrice
            )
            SELECT si.StockInId,                                                      -- StockInId - int
                   si.CurrentStock,                                                   -- CurrentStock - decimal(18, 2)
                   si.MeasurementUnitId,                                              -- MeasurementUnitId - int
                   COALESCE(si.EndUnitPriceWithoutVat, si.InitUnitPriceWithoutVat, 0) -- UnitPrice - decimal(18, 2)
            FROM dbo.StockIn si
            WHERE si.CurrentStock > 0
                  AND si.OrganizationId = @OrganizationId
                  AND si.ProductId = @RawMaterialId
                  AND si.IsActive = 1;

            SELECT @ProdTotalStock = SUM(si.CurrentStock)
            FROM @StockIn si;

            IF @ProductQtyUsedInBom > @ProdTotalStock
            BEGIN
                SET @ErrorMsg = N'Used quantity exceeds stock!!';
                RAISERROR(   @ErrorMsg, -- Message text.  
                             20,        -- Severity.  
                             -1         -- State.  
                         );
            END;

            WHILE @ProductQtyUsedInBom > 0
            BEGIN

                SELECT TOP (1)
                       @MaxUsageQty = si.CurrentStock,
                       @RawMaterialMeasurementUnitId = si.MeasurementUnitId,
                       @StockInId = si.StockInId,
                       @RawMaterialUnitPrice = si.UnitPrice
                FROM @StockIn si
                ORDER BY si.StockInId;



                IF @ProductQtyUsedInBom > @MaxUsageQty
                BEGIN
                    SET @ProdUsageQty = @MaxUsageQty;
                END;
                ELSE
                BEGIN
                    SET @ProdUsageQty = @ProductQtyUsedInBom;
                END;
                INSERT INTO @Bom
                (
                    BomSl,
                    RawMaterialId,
                    StockInId,
                    UnitPrice,
                    UsedQuantity,
                    MeasurementUnitId
                )
                VALUES
                (   @BomSl,                       -- BomSl - int
                    @RawMaterialId,               -- RawMaterialId - int
                    @StockInId,                   -- StockInId - int
                    @RawMaterialUnitPrice,        -- UnitPrice - decimal(18, 2)
                    @ProdUsageQty,                -- UsedQuantity - decimal(18, 2)
                    @RawMaterialMeasurementUnitId -- MeasurementUnitId - int
                    );
                SET @BomSl += 1;

                SET @ProductQtyUsedInBom = @ProductQtyUsedInBom - @ProdUsageQty;
                DELETE FROM @StockIn
                WHERE StockInId = @StockInId;

                UPDATE dbo.StockIn
                SET UsedInProductionQuantity = ISNULL(UsedInProductionQuantity, 0) + @ProdUsageQty
                WHERE StockInId = @StockInId;
            END;

        END;



        --End Insert sales using while loop

        DECLARE @MaterialCost DECIMAL(18, 2),
                @ProductionReceiveId INT;

        SELECT @MaterialCost = ISNULL(SUM(bm.UnitPrice * bm.UsedQuantity), 0)
        FROM @Bom bm;


        --Insert Production Receive
        INSERT INTO dbo.ProductionReceive
        (
            BatchNo,
            OrganizationId,
            ProductionId,
            ProductId,
            ReceiveQuantity,
            MeasurementUnitId,
            ReceiveTime,
            MaterialCost,
            IsActive,
            CreatedBy,
            CreatedTime
        )
        VALUES
        (   @BatchNo,           -- BatchNo - nvarchar(50)
            @OrganizationId,    -- OrganizationId - int
            @ProductionId,      -- ProductionId - int
            @ProductId,         -- ProductId - int
            @ReceiveQuantity,   -- ReceiveQuantity - decimal(18, 2)
            @MeasurementUnitId, -- MeasurementUnitId - int
            @ReceiveTime,       -- ReceiveTime - datetime
            @MaterialCost,      -- MaterialCost - decimal(18, 2)
            1,                  -- IsActive - bit
            @CreatedBy,         -- CreatedBy - int
            @CreatedTime        -- CreatedTime - datetime
            );
        --End Insert Production Receive
        -- Set Procuction Receive Id
        SET @ProductionReceiveId = SCOPE_IDENTITY();


        --Insert BOM
        INSERT INTO dbo.BillOfMaterial
        (
            ProductionReceiveId,
            RawMaterialId,
            UsedQuantity,
            MeasurementUnitId,
            StockInId,
            IsActive,
            CreatedBy,
            CreatedTime
        )
        SELECT @ProductionReceiveId, -- ProductionReceiveId - bigint
               bm.RawMaterialId,     -- RawMaterialId - int
               bm.UsedQuantity,      -- UsedQuantity - decimal(18, 2)
               bm.MeasurementUnitId, -- MeasurementUnitId - int
               bm.StockInId,         -- StockInId - bigint
               1,                    -- IsActive - bit
               @CreatedBy,           -- CreatedBy - int
               @CreatedTime          -- CreatedTime - datetime
        FROM @Bom bm;
        --Insert BOM


        DECLARE @InitStock DECIMAL(18, 2),
                @InitUnitPrice DECIMAL(18, 2) = 0;
        SELECT @InitStock = ISNULL(SUM(si.CurrentStock), 0)
        FROM dbo.StockIn si
        WHERE si.CurrentStock > 0
              AND si.ProductId = @ProductId;
        SELECT TOP (1)
               @InitUnitPrice = ISNULL(si.EndUnitPriceWithoutVat, 0)
        FROM dbo.StockIn si
        WHERE si.ProductId = @ProductId
        ORDER BY si.StockInId DESC;



        --Insert StockIn
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
        (   @OrganizationId,                                                                                        -- OrganizationId - int
            @ProductId,                                                                                             -- ProductId - int
            @ProductionReceiveId,                                                                                   -- ProductionReceiveId - bigint
            NULL,                                                                                                   -- PurchaseDetailId - int
            @InitStock,                                                                                             -- InitialQuantity - decimal(18, 2)
            @ReceiveQuantity,                                                                                       -- InQuantity - decimal(18, 2)
            @InitUnitPrice,                                                                                         -- InitUnitPriceWithoutVat - decimal(18, 2)
            ((@InitUnitPrice * @InitStock) + (@ReceiveQuantity * @MaterialCost)) / (@InitStock + @ReceiveQuantity), -- EndUnitPriceWithoutVat - decimal(18, 2)
            @MeasurementUnitId,                                                                                     -- MeasurementUnitId - int
            0,                                                                                                      -- SaleQuantity - decimal(18, 2)
            0,                                                                                                      -- DamageQuantity - decimal(18, 2)
            0,                                                                                                      -- UsedInProductionQuantity - decimal(18, 2)
            0,                                                                                                      -- PurchaseReturnQty - decimal(18, 2)
            0,                                                                                                      -- SalesReturnQty - decimal(18, 2)
            1,                                                                                                      -- IsActive - bit
            @CreatedBy,                                                                                             -- CreatedBy - int
            @CreatedTime                                                                                            -- CreatedTime - datetime
            );
        --Insert StockIn

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
                   35,
                   @ProductionReceiveId,
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