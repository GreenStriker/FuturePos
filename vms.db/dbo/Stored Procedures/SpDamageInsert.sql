-- =============================================
-- Author:		<MD.Mustafizur Rahman>
-- Create date: <20/08/2019>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpDamageInsert]
    @OrganizationId INT,
	@ProductId INT,
	@StockInId BIGINT,
	@DamageQty DECIMAL(18,2),
	@DamageTypeId INT,
	@Description NVARCHAR(1000),
	@CreatedBy INT
	
AS
BEGIN
      DECLARE @CreatedTime DATETIME;

      SET @CreatedTime = ISNULL(@CreatedTime, GETDATE());


 
    IF @StockInId > 0
    BEGIN
        INSERT INTO dbo.Damage
              (
      OrganizationId,
      ProductId,
      StockInId,
	  DamageQty,
	  DamageTypeId,
	  Description,
	  IsActive,
	  CreatedBy,
      CreatedTime
    )
     VALUES
     (@OrganizationId,
	 @ProductId,
	 @StockInId,
	 @DamageQty,
 	 @DamageTypeId ,
	 @Description,
	 1 ,
	 @CreatedBy ,
	 @CreatedTime );


	-- Update statements for StockIn table here
     UPDATE dbo.StockIn 
	 SET 
	 DamageQuantity=DamageQuantity+@DamageQty 
	 WHERE StockInId=@StockInId;

   
    END;

	ELSE
	 BEGIN
	  DECLARE @Temp DECIMAL(18,2);
	  DECLARE @TempStocId INT;
	  DECLARE @TempCurrentStock DECIMAL(18,2);
	  SET @Temp = @DamageQty;
	 
	  WHILE @Temp!=0
	  BEGIN

	  SELECT TOP 1 
	  @TempCurrentStock=st.CurrentStock,
	  @TempStocId=st.StockInId
	  
	  FROM dbo.StockIn st
	    
		WHERE 
		st.OrganizationId = @OrganizationId
		AND st.ProductId = @ProductId
		AND st.CurrentStock>0
		
		ORDER BY st.StockInId;


      IF @Temp > @TempCurrentStock
        BEGIN
		
	INSERT INTO dbo.Damage
              (
      OrganizationId,
      ProductId,
      StockInId,
	  DamageQty,
	  DamageTypeId,
	  Description,
	  IsActive,
	  CreatedBy,
      CreatedTime
    )
     VALUES
     (@OrganizationId,
	 @ProductId,
	 @TempStocId,
	 @TempCurrentStock,
 	 @DamageTypeId ,
	 @Description,
	 1 ,
	 @CreatedBy ,
	 @CreatedTime );


	-- Update statements for StockIn table here
     UPDATE dbo.StockIn 
	 SET 
	 DamageQuantity=DamageQuantity+@TempCurrentStock 
	 WHERE 
	 StockInId=@TempStocId;


	 SET @Temp = @Temp - @TempCurrentStock;

	    END;

      ELSE
	  BEGIN
	  INSERT INTO dbo.Damage
              (
      OrganizationId,
      ProductId,
      StockInId,
	  DamageQty,
	  DamageTypeId,
	  Description,
	  IsActive,
	  CreatedBy,
      CreatedTime
    )
     VALUES
     (@OrganizationId,
	 @ProductId,
	 @TempStocId,
	 @TempCurrentStock,
 	 @DamageTypeId ,
	 @Description,
	 1 ,
	 @CreatedBy ,
	 @CreatedTime );

	-- Update statements for StockIn table here
     UPDATE dbo.StockIn 
	 SET 
	 DamageQuantity=DamageQuantity+@Temp 
	 WHERE 
	 StockInId=@TempStocId;

	 SET @Temp = @Temp - @Temp;

	  END;
	  END;


-- Insert statements for Damage table here
      
  END;
  END;