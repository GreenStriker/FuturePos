-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- SpPurcSalesChallanForHighValSale 2011, 50
-- =============================================
CREATE PROCEDURE SpPurcSalesChallanForHighValSale
    @SalesId INT,
    @LowerLimitOfHighValSale DECIMAL(18, 2) 
AS
BEGIN
    SET @LowerLimitOfHighValSale = ISNULL(@LowerLimitOfHighValSale, 200000);
    SELECT ROW_NUMBER() OVER (ORDER BY slsDtl.SalesDetailId) AS Sl,
           sls.SalesId,
           sls.InvoiceNo,
           sls.VatChallanNo,
           sls.TaxInvoicePrintedTime,
           sls.SalesDate,
           sls.OrganizationId,
		   org.Name AS TaxRegisteredName,
		   org.BIN AS TaxRegisteredBIN,
		   org.Address AS TaxInvoiceIssueAddress,
		   org.VatResponsiblePersonName,
		   org.VatResponsiblePersonDesignation,
           sls.CustomerId,
           cust.Name AS CustomerName,
           cust.Address AS CustomerAddress,
           cust.BIN AS CustomerBIN,
           -------------------------------
           (slsDtl.UnitPrice * slsDtl.Quantity)
           + (slsDtl.UnitPrice * slsDtl.Quantity * slsDtl.SupplementaryDutyPercent / 100)
           + (slsDtl.UnitPrice * slsDtl.Quantity * slsDtl.VATPercent / 100) AS ProdPriceInclVATAndDuty
    -------------------------------
    FROM dbo.Sales sls
        INNER JOIN dbo.Organizations org
            ON org.OrganizationId = sls.OrganizationId
        INNER JOIN dbo.SalesDetails slsDtl
            ON slsDtl.SalesId = sls.SalesId
        LEFT JOIN dbo.Customer cust
            ON cust.CustomerId = sls.CustomerId
    WHERE sls.SalesId = @SalesId
          AND sls.TotalPriceWithoutVat >= @LowerLimitOfHighValSale;

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
