
CREATE PROCEDURE [dbo].[SpPurchaseReport]
    @PurchaseReasonId INT,
    @VendorId INT,
    @OrganizationId INT,
    @InvoiceNo NVARCHAR(50),
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SELECT purc.PurchaseId,
           purc.VendorId,
           vndr.Name AS VendorName,
           vndr.BinNo AS VendorBIN,
           vndr.Address AS VendorAddress,
           purc.VendorInvoiceNo,
           purc.InvoiceNo,
           purc.OrganizationId,
           purc.PurchaseDate AS PurchaseDate,
           purc.PurchaseTypeId,
           pt.Name AS PurchaseTypeName,
           purc.PurchaseReasonId,
           pr.Reason AS PurchaseReason,
           prod.Name AS ProductName,
           purcDtl.Quantity AS PurchaseQty,
           mu.Name AS MeasurementUnitName,
           purcDtl.UnitPrice AS UnitPrice,
           purcDtl.Quantity * purcDtl.UnitPrice AS ProductPrice,
           purcDtl.VATPercent AS VatPercent,
           purcDtl.Quantity * purcDtl.UnitPrice * purcDtl.VATPercent / 100 AS ProductVAt,
           (purcDtl.Quantity * purcDtl.UnitPrice) + (purcDtl.Quantity * purcDtl.UnitPrice * purcDtl.VATPercent / 100) AS ProductPriceWithVat
    FROM dbo.Purchase purc
        LEFT JOIN dbo.Vendor vndr
            ON vndr.VendorId = purc.VendorId
        INNER JOIN dbo.PurchaseDetails purcDtl
            ON purcDtl.PurchaseId = purc.PurchaseId
        INNER JOIN dbo.Products prod
            ON prod.ProductId = purcDtl.ProductId
        INNER JOIN dbo.PurchaseTypes pt
            ON pt.PurchaseTypeId = purc.PurchaseTypeId
        INNER JOIN dbo.PurchaseReason pr
            ON pr.PurchaseReasonId = purc.PurchaseReasonId
        INNER JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = purcDtl.MeasurementUnitId
    WHERE (
              @FromDate IS NULL
              OR purc.PurchaseDate >= @FromDate
          )
          AND
          (
              @ToDate IS NULL
              OR purc.PurchaseDate < DATEADD(DAY, 1, @ToDate)
          )
          AND
          (
              @PurchaseReasonId IS NULL
              OR @PurchaseReasonId = 0
              OR pr.PurchaseReasonId = @PurchaseReasonId
          )
          AND
          (
              @VendorId IS NULL
              OR @VendorId = 0
              OR vndr.VendorId = @VendorId
          )
          AND
          (
              @OrganizationId IS NULL
              OR @OrganizationId = 0
              OR purc.OrganizationId = @OrganizationId
          )
          AND
          (
              @InvoiceNo IS NULL
              OR purc.InvoiceNo = @InvoiceNo
          )
          --AND purc.IsCanceled = 0;
END;
