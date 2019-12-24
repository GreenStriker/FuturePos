-- =============================================
-- Author:		<MD.Mustafizur Rahman>
-- Create date: <22/08/2019>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpDashboard]
   @OrganizationId INT,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN

DECLARE @PurchaseAmount DECIMAL(18,2);
DECLARE @SalesAmount DECIMAL(18,2);
DECLARE @PurchaseDueAmount DECIMAL(18,2);
DECLARE @SalesDueAmount DECIMAL(18,2);
DECLARE @ProductionCost DECIMAL(18,2);
DECLARE @PaidTax DECIMAL(18,2);
DECLARE @TotalRawMat DECIMAL(18,2);
DECLARE @TotalFinishGd DECIMAL(18,2);

SELECT @PurchaseAmount=SUM(ps.PaidAmount), @PurchaseDueAmount=SUM(ps.DueAmount) FROM dbo.Purchase ps WHERE ps.OrganizationId = @OrganizationId AND ps.CreatedTime >= @FromDate AND ps.CreatedTime <= @ToDate ;

SELECT @SalesAmount=SUM(sl.PaymentReceiveAmount),@SalesDueAmount=SUM(sl.PaymentDueAmount) FROM dbo.Sales sl WHERE sl.OrganizationId = @OrganizationId AND sl.CreatedTime >= @FromDate AND sl.CreatedTime <= @ToDate ;

SELECT @ProductionCost = SUM(pr.MaterialCost) FROM dbo.ProductionReceive pr WHERE pr.OrganizationId = @OrganizationId AND pr.CreatedTime >= @FromDate AND pr.CreatedTime <= @ToDate;

SELECT @TotalRawMat = SUM(st.CurrentStock) FROM dbo.StockIn st LEFT JOIN dbo.Products pd ON pd.ProductId = st.ProductId WHERE pd.IsRawMaterial = 1 AND pd.IsActive=1; 
SELECT @TotalFinishGd = SUM(st.CurrentStock) FROM dbo.StockIn st LEFT JOIN dbo.Products pd ON pd.ProductId = st.ProductId WHERE pd.IsSellable = 1 AND pd.IsActive=1; 


SELECT @PurchaseAmount AS PurchaseAmount
       ,@PurchaseDueAmount AS PurchaseDue
       ,@SalesAmount AS SalesAmount
	   ,@SalesDueAmount AS SalesDue
	   ,@ProductionCost AS ProductionCost
	  ,@TotalRawMat AS TotalRawMaterial
	  ,@TotalFinishGd AS TotalFinishedGood
	   -- EXEC SpDashboard @OrganizationId=16 ,@FromDate='2019/01/01' ,@ToDate = '2019/09/01'






END

