-- =============================================
-- Author:		<MD SABBIR REZA>
-- Create date: <2019-09-22>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGet6P3View]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@InvoiceNo NVARCHAR(50)= '',
	@CustomerName NVARCHAR(200)= '',
	@FromDate DATETIME= NULL,
	@ToDate DATETIME= NULL

AS
BEGIN
SELECT InvoiceNo,Isnull(cus.Name,'Not Found') as CustomerName FROM Sales sl
left join Customer cus on sl.CustomerId=cus.CustomerId
where 
(
@InvoiceNo=NULL OR
 sl.InvoiceNo like '%'+@InvoiceNo+'%'
 )
OR 
(
@CustomerName=NULL OR cus.Name LIKE '%'+@CustomerName+'%'
)
OR 
( 
SL.SalesDate >= @FromDate AND SL.SalesDate< DATEADD(DAY, 1, @ToDate)
)

END
