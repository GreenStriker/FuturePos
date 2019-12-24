-- =============================================
-- Author:		<MD.Mustafizur Rahman>
-- Create date: <20/08/2019>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpDamageList]
@OrganizationId INT
AS
BEGIN

SELECT dm.DamageId,
         og.Name AS Org_Name,
         pd.Name AS Pr_Name,
         dm.DamageQty AS Qty,
         dt.Name AS D_Type,
         dm.Description ,
         dm.IsActive ,
         us.UserName AS U_Name,
         dm.CreatedTime 
   FROM dbo.Damage dm 
       LEFT JOIN dbo.Organizations og
   ON 
       dm.OrganizationId=og.OrganizationId 
       LEFT JOIN dbo.Products pd 
   ON 
       dm.ProductId = pd.ProductId 
       LEFT JOIN dbo.DamageType dt 
   ON 
       dm.DamageTypeId=dt.DamageTypeId
       LEFT JOIN dbo.Users us 
   ON 
       dm.CreatedBy = us.UserId
	   
	   WHERE dm.OrganizationId=@OrganizationId;
END

