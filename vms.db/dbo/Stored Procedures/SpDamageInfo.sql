-- =============================================
-- Author:		<MD.Mustafizur Rahman>
-- Create date: <18/08/2019>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpDamageInfo]
    @OrganizationId INT,
	@ProductId INT
AS
BEGIN

SELECT si.StockInId,
purc.InvoiceNo,
pr.BatchNo,
si.CurrentStock
FROM dbo.StockIn si
LEFT JOIN dbo.ProductionReceive pr
ON pr.ProductionReceiveId = si.ProductionReceiveId
LEFT JOIN dbo.PurchaseDetails purcDtl
ON purcDtl.PurchaseDetailId = si.PurchaseDetailId
LEFT JOIN dbo.Purchase purc
ON purc.PurchaseId = purcDtl.PurchaseId
WHERE si.CurrentStock > 0
AND si.OrganizationId=@OrganizationId
AND si.ProductId=@ProductId;
END

