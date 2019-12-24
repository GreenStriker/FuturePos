-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC SpGetProductAutocompleteForSale 16, 'o'
-- =============================================
CREATE PROCEDURE [dbo].[SpGetProductAutocompleteForProductionReceive]
    @OrganizationId INT,
    @ProductSearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT prod.ProductId,
           prod.Name AS ProductName,
           prod.ModelNo,
           prod.Code,
           prod.MeasurementUnitId,
           mu.Name AS MeasurementUnitName
    FROM dbo.Products prod
        LEFT JOIN dbo.MeasurementUnits mu
            ON mu.MeasurementUnitId = prod.MeasurementUnitId
    WHERE prod.OrganizationId = @OrganizationId
          AND prod.IsSellable = 1
          AND prod.Name LIKE N'%' + @ProductSearchTerm + '%'
    GROUP BY prod.ProductId,
             prod.Name,
             prod.ModelNo,
             prod.Code,
             prod.MeasurementUnitId,
             mu.Name;
END;
