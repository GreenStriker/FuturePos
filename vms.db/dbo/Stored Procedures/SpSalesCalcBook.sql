-- =============================================
-- Author:		Sabbir Ahmed Osmani
-- Create date: July 14, 2019
-- Description:	Returns Purchase Calculation Book for Mushak 6.1
-- SpPurchaseCalcBook 16, NULL, NULL, 0, 0
-- =============================================
CREATE PROCEDURE [dbo].[SpSalesCalcBook]
    @OrganizationId INT,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @ProductId INT
AS
BEGIN
    SELECT SlNo = ROW_NUMBER() OVER (ORDER BY slsDtl.SalesDetailId),
           si.StockInId,
           si.OrganizationId,
           org.Name AS OrganizationName,
           org.Address AS OrganizationAddress,
           org.BIN AS OrganizationBin,
           sls.SalesId,
           slsDtl.SalesDetailId,
           sls.SalesDate,
           ISNULL(si.InitialQuantity, 0) + si.InQuantity AS InitialQty,
           (ISNULL(si.InitialQuantity, 0) + si.InQuantity) * ISNULL(prcstup.SalesUnitPrice, 0) AS InitPriceWithoutVat,
		   0 AS ProductionQty,
		   0 AS PriceOfProdFromProduction,
           ISNULL(si.InitialQuantity, 0) + si.InQuantity AS TotalProductionQty,
           (ISNULL(si.InitialQuantity, 0) + si.InQuantity) * ISNULL(prcstup.SalesUnitPrice, 0) AS TotalProductionPrice,
		   cust.Name AS CustomerName,
		   cust.Address AS CustomerAddress,
		   ISNULL(cust.BIN, cust.NIDNo) AS CustomerAddressOrNid,
		   sls.VatChallanNo,
		   sls.TaxInvoicePrintedTime,
           prod.ProductId,
           prod.Name AS ProductName,
           slsDtl.Quantity AS SalesQty,
		   slsDtl.Quantity * slsDtl.UnitPrice AS TaxablePrice,
		   slsDtl.Quantity * slsDtl.UnitPrice * slsDtl.SupplementaryDutyPercent / 100 AS SupplementaryDuty,
		   slsDtl.Quantity * slsDtl.UnitPrice * slsDtl.VATPercent / 100 AS ProdVatAmount,
           ISNULL(si.InitialQuantity, 0) + si.InQuantity AS ClosingProdQty,
           (ISNULL(si.InitialQuantity, 0) + si.InQuantity) * prcstup.SalesUnitPrice AS ClosingProdPrice,
           mu.MeasurementUnitId,
           mu.Name AS MeasurementUnitName
    FROM dbo.StockIn si
        INNER JOIN dbo.Organizations org
            ON org.OrganizationId = si.OrganizationId
        INNER JOIN dbo.Products prod
            ON prod.ProductId = si.ProductId
        INNER JOIN dbo.PriceSetup prcstup
            ON prcstup.ProductId = prod.ProductId
        INNER JOIN dbo.SalesDetails slsDtl
            ON slsDtl.StockInId = si.StockInId
        INNER JOIN dbo.Sales sls
            ON sls.SalesId = slsDtl.SalesId
			INNER JOIN dbo.Customer cust ON cust.CustomerId = sls.CustomerId
        INNER JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = si.MeasurementUnitId
    WHERE si.IsActive = 1
          AND si.OrganizationId = @OrganizationId
          AND prcstup.IsActive = 1
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
              @ProductId = 0
              OR @ProductId IS NULL
              OR prod.ProductId = @ProductId
          );
END;