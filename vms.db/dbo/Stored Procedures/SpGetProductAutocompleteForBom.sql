-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC SpGetProductAutocompleteForSale 16, 'o'
-- =============================================
CREATE PROCEDURE [dbo].[SpGetProductAutocompleteForBom]
    @OrganizationId INT,
    @ProductSearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT prod.ProductId,
           prod.Name AS ProductName,
           prod.ModelNo,
           prod.Code,
           ISNULL(SUM(ISNULL(stk.CurrentStock, 0)), 0) AS MaxUseQty,
           prod.MeasurementUnitId,
           mu.Name AS MeasurementUnitName
    FROM dbo.Products prod
        LEFT JOIN dbo.StockIn stk
            ON stk.ProductId = prod.ProductId
               AND stk.CurrentStock > 0
        LEFT JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = prod.MeasurementUnitId
    WHERE prod.OrganizationId = @OrganizationId
          AND prod.IsRawMaterial = 1
          AND prod.Name LIKE N'%' + @ProductSearchTerm + '%'
    GROUP BY prod.ProductId,
             prod.Name,
             prod.ModelNo,
             prod.Code,
             prod.MeasurementUnitId,
             mu.Name;
END;
