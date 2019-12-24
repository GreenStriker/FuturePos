-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- [dbo].[SpSalesTaxInvoice] 8
-- =============================================
CREATE PROCEDURE [dbo].[SpSalesTaxInvoice] @SalesId INT
AS
BEGIN
    DECLARE @IsTaxInvoicePrined BIT,
            @TaxInvoicePrintedTime DATETIME;
    SELECT @IsTaxInvoicePrined = sls.IsTaxInvoicePrined,
           @TaxInvoicePrintedTime = sls.TaxInvoicePrintedTime
    FROM dbo.Sales sls
    WHERE sls.SalesId = @SalesId;

    IF @IsTaxInvoicePrined = 0
    BEGIN
        SET @TaxInvoicePrintedTime = GETDATE();
        UPDATE dbo.Sales
        SET IsTaxInvoicePrined = 1,
            TaxInvoicePrintedTime = @TaxInvoicePrintedTime
        WHERE SalesId = @SalesId;
    END;
    SELECT ROW_NUMBER() OVER (ORDER BY slsDtl.SalesDetailId) AS Sl,
           sls.SalesId,
           sls.InvoiceNo,
           sls.VatChallanNo,
		   @TaxInvoicePrintedTime AS TaxInvoiceIssueTime,
		   sls.SalesDate,
           sls.OrganizationId,
		   org.Name AS TaxRegisteredName,
		   org.BIN AS TaxRegisteredBIN,
		   org.Address AS TaxInvoiceIssueAddress,
		   org.VatResponsiblePersonName,
		   org.VatResponsiblePersonDesignation,
           sls.CustomerId,
           cust.Name AS CustomerName,
           cust.BIN AS CustomerBIN,
           sls.ReceiverName,
           sls.ReceiverContactNo,
           sls.ShippingAddress,
           sls.ShippingCountryId,
           sls.IsTaxInvoicePrined,
           sls.TaxInvoicePrintedTime,
           CASE
               WHEN @IsTaxInvoicePrined = 1 THEN
                   ''
               ELSE
                   '(Copy)'
           END AS InvoiceStatus,
           -------------------------------
           prod.Name AS ProductName,
           prod.ModelNo,
           mu.Name AS MeasurementUnitName,
           slsDtl.Quantity,
           slsDtl.UnitPrice,
           slsDtl.UnitPrice * slsDtl.Quantity AS ProductPrice,
           slsDtl.SupplementaryDutyPercent,
           slsDtl.UnitPrice * slsDtl.Quantity * slsDtl.SupplementaryDutyPercent / 100 AS ProdSupplementaryDutyAmount,
           slsDtl.VATPercent,
           slsDtl.UnitPrice * slsDtl.Quantity * slsDtl.VATPercent / 100 AS ProdVATAmount,
           (slsDtl.UnitPrice * slsDtl.Quantity)
           + (slsDtl.UnitPrice * slsDtl.Quantity * slsDtl.SupplementaryDutyPercent / 100)
           + (slsDtl.UnitPrice * slsDtl.Quantity * slsDtl.VATPercent / 100) AS ProdPriceInclVATAndDuty
    -------------------------------
    FROM dbo.Sales sls
        INNER JOIN dbo.Organizations org
            ON org.OrganizationId = sls.OrganizationId
        INNER JOIN dbo.SalesDetails slsDtl
            ON slsDtl.SalesId = sls.SalesId
        INNER JOIN dbo.Products prod
            ON prod.ProductId = slsDtl.ProductId
        INNER JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = slsDtl.MeasurementUnitId
        LEFT JOIN dbo.Customer cust
            ON cust.CustomerId = sls.CustomerId
    WHERE sls.SalesId = @SalesId;
END;
