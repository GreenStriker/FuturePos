-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- SpPurcSalesChallanForHighValSale 2011, 50
-- =============================================
CREATE PROCEDURE SpPurcSalesChallanForHighValSaleSale
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
END;
