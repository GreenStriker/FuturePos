-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- SpPurcSalesChallanForHighValSale 2011, 50
-- =============================================
CREATE PROCEDURE SpPurcSalesChallanForHighValSalePurchase
    @SalesId INT,
    @LowerLimitOfHighValSale DECIMAL(18, 2)
AS
BEGIN
    SET @LowerLimitOfHighValSale = ISNULL(@LowerLimitOfHighValSale, 200000);

    SELECT ROW_NUMBER() OVER (ORDER BY slsDtl.SalesDetailId) AS Sl,
           purc.PurchaseId,
           purc.VendorInvoiceNo,
           purc.PurchaseDate,
           purc.VatChallanNo,
           purc.VatChallanIssueDate,
           vndr.Name AS VendorName,
           vndr.Address AS VendorAddress,
           ISNULL(vndr.BinNo, vndr.NationalIdNo) AS VendorBinOrNid,
           -------------------------------
           (purcDtl.UnitPrice * purcDtl.Quantity)
           + (purcDtl.UnitPrice * purcDtl.Quantity * purcDtl.SupplementaryDutyPercent / 100)
           + (purcDtl.UnitPrice * purcDtl.Quantity * purcDtl.VATPercent / 100) AS ProdPriceInclVATAndDuty
    -------------------------------
    FROM dbo.Sales sls
        INNER JOIN dbo.SalesDetails slsDtl
            ON slsDtl.SalesId = sls.SalesId
        INNER JOIN dbo.StockIn stkIn
            ON stkIn.StockInId = slsDtl.StockInId
        INNER JOIN dbo.PurchaseDetails purcDtl
            ON purcDtl.PurchaseDetailId = stkIn.PurchaseDetailId
        INNER JOIN dbo.Purchase purc
            ON purc.PurchaseId = purcDtl.PurchaseId
        INNER JOIN dbo.Vendor vndr
            ON vndr.VendorId = purc.VendorId
    WHERE sls.SalesId = @SalesId
          AND sls.TotalPriceWithoutVat >= @LowerLimitOfHighValSale;
END;
