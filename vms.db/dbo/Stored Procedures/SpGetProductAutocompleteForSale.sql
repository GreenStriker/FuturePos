-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC SpGetProductAutocompleteForSale 16, 'o'
-- =============================================
CREATE PROCEDURE [dbo].[SpGetProductAutocompleteForSale]
    @OrganizationId INT,
    @ProductSearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT prod.ProductId,
           prod.Name AS ProductName,
           prod.ModelNo,
           prod.Code,
           prodPrice.SalesUnitPrice,
           pvt.DefaultVatPercent,
           ISNULL(sd.SdPercent, 0) AS SupplimentaryDutyPercent,
           ISNULL(SUM(ISNULL(stk.CurrentStock, 0)), 0) AS MaxSaleQty,
           prod.MeasurementUnitId,
           mu.Name AS MeasurementUnitName
    FROM dbo.Products prod
        INNER JOIN dbo.PriceSetup prodPrice
            ON prodPrice.ProductId = prod.ProductId
               AND prodPrice.IsActive = 1
        LEFT JOIN dbo.StockIn stk
            ON stk.ProductId = prod.ProductId
               AND stk.CurrentStock > 0
        INNER JOIN dbo.ProductVATs pv
            ON pv.ProductId = prod.ProductId
               AND pv.IsActive = 1
        INNER JOIN dbo.ProductVATTypes pvt
            ON pvt.ProductVATTypeId = pv.ProductVATTypeId
               AND pvt.IsActive = 1
        LEFT JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = prod.MeasurementUnitId
        LEFT JOIN dbo.SupplimentaryDuty sd
            ON sd.ProductId = prod.ProductId
               AND sd.IsActive = 1
    WHERE prod.OrganizationId = @OrganizationId
          AND prod.IsSellable = 1
          AND prod.Name LIKE N'%' + @ProductSearchTerm + '%'
    GROUP BY prod.ProductId,
             prod.Name,
             prod.ModelNo,
             prod.Code,
             prodPrice.SalesUnitPrice,
             pvt.DefaultVatPercent,
             sd.SdPercent,
             prod.MeasurementUnitId,
             mu.Name;
END;
