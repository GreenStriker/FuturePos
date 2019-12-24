

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC [SpGetProductAutocompleteForPurchase] 16, 'o'
-- =============================================
CREATE PROCEDURE [dbo].[SpGetProductAutocompleteForPurchase]
    @OrganizationId INT,
    @ProductSearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT prod.ProductId,
           prod.Name AS ProductName,
           prod.ModelNo,
           prod.Code,
           ISNULL(prodPrice.PurchaseUnitPrice, 0) AS PurchaseUnitPrice,
           25.00 AS DefaultImportDutyPercent,
           3.00 AS DefaultRegulatoryDutyPercent,
           ISNULL(sd.SdPercent, 0) AS DefaultSupplimentaryDutyPercent,
           ISNULL(pvt.DefaultVatPercent, 0) AS DefaultVatPercent,
           3.00 AS DefaultAdvanceTaxPercent,
           5.00 AS DefaultAdvanceIncomeTaxPercent,
           prod.MeasurementUnitId,
           mu.Name AS MeasurementUnitName
    FROM dbo.Products prod
        INNER JOIN dbo.PriceSetup prodPrice
            ON prodPrice.ProductId = prod.ProductId
               AND prodPrice.IsActive = 1
        INNER JOIN dbo.ProductVATs pv
            ON pv.ProductId = prod.ProductId
               AND pv.IsActive = 1
        INNER JOIN dbo.ProductVATTypes pvt
            ON pvt.ProductVATTypeId = pv.ProductVATTypeId
               AND pvt.IsActive = 1
        INNER JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = prod.MeasurementUnitId
        LEFT JOIN dbo.SupplimentaryDuty sd
            ON sd.ProductId = prod.ProductId
               AND sd.IsActive = 1
    WHERE prod.OrganizationId = @OrganizationId
          AND prod.Name LIKE N'%' + @ProductSearchTerm + '%';
END;
