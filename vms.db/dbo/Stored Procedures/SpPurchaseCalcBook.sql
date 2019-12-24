-- =============================================
-- Author:		Sabbir Ahmed Osmani
-- Create date: July 14, 2019
-- Description:	Returns Purchase Calculation Book for Mushak 6.1
-- SpPurchaseCalcBook 16, NULL, NULL, 0, 0
-- =============================================
CREATE PROCEDURE [dbo].[SpPurchaseCalcBook]
    @OrganizationId INT,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @VendorId INT,
    @ProductId INT
AS
BEGIN
    SELECT SlNo = ROW_NUMBER() OVER (ORDER BY si.StockInId),
           si.StockInId,
           si.OrganizationId,
		   org.Name AS OrganizationName,
		   org.Address AS OrganaizationAddress,
		   org.BIN AS OrganizationBin,
           purcDtl.PurchaseDetailId,
           purc.PurchaseId,
           purc.InvoiceNo,
           purc.VendorInvoiceNo,
           PurchaseDate = purc.PurchaseDate,
		   ISNULL(si.InitialQuantity, 0) AS InitialQuantity,
		   ISNULL(si.InitialQuantity, 0) * ISNULL(si.InitUnitPriceWithoutVat, 0) AS InitPriceWithoutVat,
		   ISNULL(purc.VatChallanNo, purc.BillOfEntry) AS VatChallanOrBillOfEntry,
		   ISNULL(purc.VatChallanIssueDate, purc.BillOfEntryDate) AS VatChallanOrBillOfEntryDate,
		   vndr.Name AS VendorName,
		   vndr.Address AS VendorAddress,
		   ISNULL(vndr.BinNo, vndr.NationalIdNo) AS VendorBinOrNid,
           prod.ProductId,
           prod.Name AS ProductName,
           PurchaseQty = si.InQuantity,
		   purcDtl.UnitPrice * si.InQuantity - purcDtl.DiscountPerItem * si.InQuantity AS PriceWithoutVat,
		   (purcDtl.UnitPrice * si.InQuantity * purcDtl.SupplementaryDutyPercent) / 100 AS SupplimentaryDuty,
		   (purcDtl.UnitPrice * si.InQuantity * purcDtl.VATPercent) / 100 AS VATAmount,
		   ISNULL(si.InitialQuantity, 0) + si.InQuantity AS TotalProdQty,
		   (ISNULL(si.InitialQuantity, 0) * ISNULL(si.InitUnitPriceWithoutVat, 0)) + (purcDtl.UnitPrice * si.InQuantity) - (purcDtl.DiscountPerItem * si.InQuantity) AS TotalProdPrice,
		   0 AS UsedInProduction,
		   0 AS PriceWithoutVatForUsedInProduction,
		   ISNULL(si.InitialQuantity, 0) + si.InQuantity AS ClosingProdQty,
		   (ISNULL(si.InitialQuantity, 0) * ISNULL(si.InitUnitPriceWithoutVat, 0)) + (purcDtl.UnitPrice * si.InQuantity) - (purcDtl.DiscountPerItem * si.InQuantity) AS ClosingTotalPrice,
           mu.MeasurementUnitId,
           mu.Name AS MeasurementUnitName
    FROM dbo.StockIn si
	INNER JOIN dbo.Organizations org ON org.OrganizationId = si.OrganizationId
        INNER JOIN dbo.Products prod
            ON prod.ProductId = si.ProductId
        INNER JOIN dbo.PurchaseDetails purcDtl
            ON purcDtl.PurchaseDetailId = si.PurchaseDetailId
        INNER JOIN dbo.Purchase purc
            ON purc.PurchaseId = purcDtl.PurchaseId
        INNER JOIN dbo.Vendor vndr
            ON vndr.VendorId = purc.VendorId
        INNER JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = si.MeasurementUnitId
    WHERE si.IsActive = 1
          AND si.OrganizationId = @OrganizationId
          AND
          (
              @FromDate IS NULL
              OR purc.PurchaseDate >= @FromDate
          )
          AND
          (
              @ToDate IS NULL
              OR purc.PurchaseDate <= @ToDate
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
          );
END;