-- =============================================
-- Author:		Sabbir Ahmed Osmani
-- Create date: July 14, 2019
-- Description:	Returns Purchase Calculation Book for Mushak 6.2.1
-- SpPurchaseSaleCalcBook 16, NULL, NULL, 0, 0
-- =============================================
CREATE PROCEDURE [dbo].[SpPurchaseSaleCalcBook]
    @OrganizationId INT,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @VendorId INT,
    @ProductId INT
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    SELECT ROW_NUMBER() OVER (ORDER BY purchSaleComb.StockInId, purchSaleComb.SlNoByStockIn) AS SlNo,
           purchSaleComb.OrganizationId,
           purchSaleComb.OrganizationName,
           purchSaleComb.OrganizationAddress,
           purchSaleComb.OrganizationBin,
           purchSaleComb.OperationDate,
           purchSaleComb.InitialQty,
           purchSaleComb.InitialPriceWithoutVatInStockIn,
           purchSaleComb.ProductId,
           purchSaleComb.ProductName,
           purchSaleComb.ProductCode,
           purchSaleComb.ProductHsCode,
           purchSaleComb.ProductModel,
           purchSaleComb.PurchaseDetailId,
           purchSaleComb.PurchaseQty,
           purchSaleComb.PurchasePriceWithoutVat,
           purchSaleComb.QtyAfterPurchase,
           purchSaleComb.ProductPriceWithoutVatAfterPurchase,
           purchSaleComb.VendorId,
           purchSaleComb.VendorName,
           purchSaleComb.VendorBinOrNidNo,
           purchSaleComb.VendorAddress,
           purchSaleComb.PurcVatChallanOrBillOfEntryNo,
           purchSaleComb.PurcVatChallanOrBillOfEntryDate,
           purchSaleComb.SalesDetailId,
           purchSaleComb.SoldQty,
           purchSaleComb.SalesPriceWithoutVat,
           purchSaleComb.SalesSupplimentaryDuty,
           purchSaleComb.SalesVat,
           purchSaleComb.CustomerName,
           purchSaleComb.CustomerAddress,
           purchSaleComb.CustomerBinOrNidNo,
           purchSaleComb.SalesVatChallanNo,
           purchSaleComb.SalesVatChallanDate,
           purchSaleComb.EndQty,
           purchSaleComb.EndProductPriceWithoutVat,
           purchSaleComb.TransMeasurementUnitId,
           purchSaleComb.TransMeasurementUnitName
    FROM
    (
        --Purchase Part
        (SELECT ROW_NUMBER() OVER (PARTITION BY prod.ProductId ORDER BY si.StockInId) AS SlNoByStockIn,
                si.StockInId,
                si.OrganizationId,
                org.Name AS OrganizationName,
                org.Address AS OrganizationAddress,
                org.BIN AS OrganizationBin,
                purch.PurchaseDate AS OperationDate,
                si.InitialQuantity AS InitialQty,
                si.InitialQuantity * si.InitUnitPriceWithoutVat AS InitialPriceWithoutVatInStockIn,
                si.ProductId AS ProductId,
                prod.Name AS ProductName,
                prod.Code AS ProductCode,
                prod.HSCode AS ProductHsCode,
                prod.ModelNo AS ProductModel,
                --Purchase Information
                si.PurchaseDetailId AS PurchaseDetailId,
                si.InQuantity AS PurchaseQty,
                si.InQuantity * si.InitUnitPriceWithoutVat AS PurchasePriceWithoutVat,
                si.InitialQuantity + si.InQuantity AS QtyAfterPurchase,
                ISNULL((si.InitialQuantity * si.InitUnitPriceWithoutVat), 0)
                + (si.InQuantity * si.InitUnitPriceWithoutVat) AS ProductPriceWithoutVatAfterPurchase,
                vndr.VendorId AS VendorId,
                vndr.Name AS VendorName,
                CAST(ISNULL(vndr.BinNo, vndr.NationalIdNo) AS NVARCHAR(50)) AS VendorBinOrNidNo,
                vndr.Address AS VendorAddress,
                ISNULL(purch.VatChallanNo, purch.BillOfEntry) AS PurcVatChallanOrBillOfEntryNo,
                ISNULL(purch.VatChallanIssueDate, purch.BillOfEntryDate) AS PurcVatChallanOrBillOfEntryDate,
                --Sales information
                CAST(0 AS INT) AS SalesDetailId,
                CAST(0 AS DECIMAL(18, 2)) SoldQty,
                CAST(0 AS DECIMAL(18, 2)) AS SalesPriceWithoutVat,
                CAST(0 AS DECIMAL(18, 2)) AS SalesSupplimentaryDuty,
                CAST(0 AS DECIMAL(18, 2)) AS SalesVat,
                CAST(N'' AS NVARCHAR(200)) AS CustomerName,
                CAST(N'' AS NVARCHAR(200)) AS CustomerAddress,
                CAST(N'' AS NVARCHAR(50)) AS CustomerBinOrNidNo,
                CAST(N'' AS NVARCHAR(50)) AS SalesVatChallanNo,
                CAST(NULL AS DATETIME) AS SalesVatChallanDate,
                si.InitialQuantity + si.InQuantity AS EndQty,
                ISNULL((si.InitialQuantity * si.InitUnitPriceWithoutVat), 0)
                + (si.InQuantity * si.InitUnitPriceWithoutVat) AS EndProductPriceWithoutVat,
                si.MeasurementUnitId AS TransMeasurementUnitId,
                mu.Name AS TransMeasurementUnitName
         FROM dbo.StockIn si
             INNER JOIN dbo.Organizations org
                 ON org.OrganizationId = si.OrganizationId
             INNER JOIN dbo.Products prod
                 ON prod.ProductId = si.ProductId
             INNER JOIN dbo.PurchaseDetails purchDtl
                 ON purchDtl.PurchaseDetailId = si.PurchaseDetailId
             INNER JOIN dbo.Purchase purch
                 ON purch.PurchaseId = purchDtl.PurchaseId
             INNER JOIN dbo.Vendor vndr
                 ON vndr.VendorId = purch.VendorId
             INNER JOIN dbo.MeasurementUnits mu
                 ON mu.MeasurementUnitId = si.MeasurementUnitId
         WHERE si.PurchaseDetailId IS NOT NULL
               AND si.OrganizationId = @OrganizationId
               AND
               (
                   @FromDate IS NULL
                   OR purch.PurchaseDate >= @FromDate
               )
               AND
               (
                   @ToDate IS NULL
                   OR purch.PurchaseDate <= @ToDate
               )
               AND
               (
                   @VendorId = 0
                   OR @VendorId IS NULL
                   OR vndr.VendorId = @VendorId
               )
               AND
               (
                   @ProductId = 0
                   OR @ProductId IS NULL
                   OR prod.ProductId = @ProductId
               ))
        UNION
        --Sales Part
        (SELECT ROW_NUMBER() OVER (PARTITION BY prod.ProductId
                                   ORDER BY si.StockInId,
                                            slsDtl.SalesDetailId
                                  ) + 1 AS SlNoByStockIn,
                si.StockInId,
                si.OrganizationId,
                org.Name AS OrganizationName,
                org.Address AS OrganizationAddress,
                org.BIN AS OrganizationBin,
                sls.SalesDate AS OperationDate,
                ComputedColumn.TotalQtyAfterPurchase AS InitialQty,
                ComputedColumn.ProductPriceWithoutVatAfterPurchase AS InitialPriceWithoutVatInStockIn,
                si.ProductId AS ProductId,
                prod.Name AS ProductName,
                prod.Code AS ProductCode,
                prod.HSCode AS ProductHsCode,
                prod.ModelNo AS ProductModel,
                --Purchase Information
                CAST(0 AS INT) AS PurchaseDetailId,
                CAST(0 AS DECIMAL(18, 2)) AS PurchaseQty,
                CAST(0 AS DECIMAL(18, 2)) AS PurchasePriceWithoutVat,
                ComputedColumn.TotalQtyAfterPurchase AS QtyAfterPurchase,
                ComputedColumn.ProductPriceWithoutVatAfterPurchase AS ProductPriceWithoutVatAfterPurchase,
                CAST(0 AS INT) AS VendorId,
                CAST(N'' AS NVARCHAR(200)) AS VendorName,
                CAST(N'' AS NVARCHAR(50)) AS VendorBinOrNidNo,
                CAST(N'' AS NVARCHAR(500)) AS VendorAddress,
                CAST(N'' AS NVARCHAR(50)) AS PurcVatChallanOrBillOfEntryNo,
                CAST(NULL AS DATETIME) AS PurcVatChallanOrBillOfEntryDate,
                --Sales information
                slsDtl.SalesDetailId AS SalesDetailId,
                slsDtl.Quantity AS SoldQty,
                slsDtl.Quantity * (slsDtl.UnitPrice - slsDtl.DiscountPerItem) AS SalesPriceWithoutVat,
                slsDtl.Quantity * slsDtl.UnitPrice * slsDtl.SupplementaryDutyPercent / 100 AS SalesSupplimentaryDuty,
                ((slsDtl.Quantity * slsDtl.UnitPrice)
                 + (slsDtl.Quantity * slsDtl.UnitPrice * slsDtl.SupplementaryDutyPercent / 100)
                ) * slsDtl.VATPercent
                / 100 AS SalesVat,
                cust.Name AS CustomerName,
                cust.Address AS CustomerAddress,
                CAST(ISNULL(cust.BIN, cust.NIDNo) AS NVARCHAR(50)) AS CustomerBinOrNidNo,
                sls.VatChallanNo AS SalesVatChallanNo,
                sls.TaxInvoicePrintedTime AS SalesVatChallanDate,
                ComputedColumn.TotalQtyAfterPurchase - ComputedColumn.TotalSalesQty AS EndQty,
                ((ComputedColumn.TotalQtyAfterPurchase - ComputedColumn.TotalSalesQty)
                 / (ComputedColumn.TotalQtyAfterPurchase / ComputedColumn.ProductPriceWithoutVatAfterPurchase)
                ) AS EndProductPriceWithoutVat,
                si.MeasurementUnitId AS TransMeasurementUnitId,
                mu.Name AS TransMeasurementUnitName
         FROM dbo.StockIn si
             INNER JOIN dbo.Organizations org
                 ON org.OrganizationId = si.OrganizationId
             INNER JOIN dbo.Products prod
                 ON prod.ProductId = si.ProductId
             INNER JOIN dbo.PurchaseDetails purchDtl
                 ON purchDtl.PurchaseDetailId = si.PurchaseDetailId
             INNER JOIN dbo.Purchase purch
                 ON purch.PurchaseId = purchDtl.PurchaseId
             INNER JOIN dbo.Vendor vndr
                 ON vndr.VendorId = purch.VendorId
             INNER JOIN dbo.SalesDetails slsDtl
                 ON slsDtl.StockInId = si.StockInId
             INNER JOIN dbo.Sales sls
                 ON sls.SalesId = slsDtl.SalesId
             LEFT JOIN dbo.Customer cust
                 ON cust.CustomerId = sls.CustomerId
             INNER JOIN dbo.MeasurementUnits mu
                 ON mu.MeasurementUnitId = si.MeasurementUnitId
             CROSS APPLY
         (
             SELECT (si.InitialQuantity + si.InQuantity) AS TotalQtyAfterPurchase,
                    ISNULL((si.InitialQuantity * si.InitUnitPriceWithoutVat), 0)
                    + (si.InQuantity * si.InitUnitPriceWithoutVat) AS ProductPriceWithoutVatAfterPurchase,
                    (
                        SELECT SUM(sd.Quantity)
                        FROM dbo.SalesDetails sd
                        WHERE sd.StockInId = si.StockInId
                              AND sd.SalesDetailId <= slsDtl.SalesDetailId
                    ) AS TotalSalesQty
         ) AS ComputedColumn
         WHERE si.PurchaseDetailId IS NOT NULL
               AND si.OrganizationId = @OrganizationId
               AND
               (
                   @FromDate IS NULL
                   OR sls.SalesDate >= @FromDate
               )
               AND
               (
                   @ToDate IS NULL
                   OR sls.SalesDate <= @ToDate
               )
               AND
               (
                   @VendorId = 0
                   OR @VendorId IS NULL
                   OR vndr.VendorId = @VendorId
               )
               AND
               (
                   @ProductId = 0
                   OR @ProductId IS NULL
                   OR prod.ProductId = @ProductId
               ))
    ) AS purchSaleComb;
END;
