-- Author:		<MD. Sabbir Reza>
CREATE PROCEDURE [dbo].[SpCreditNote]
	-- Add the parameters for the stored procedure here
	@SalesId INT,
	@ReasonOfReturn NVARCHAR(50),
	@ReturnDate DATETIME,
	@CreatedBy INT,
    @CreatedTime DATETIME,
	@CreditNoteDetails NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET @CreatedTime = ISNULL(@CreatedTime, GETDATE());
	DECLARE @CreditNoteDetail TABLE
    (
        [SalesDetailId] [INT] NOT NULL,
        [ReturnQuantity] [DECIMAL] NOT NULL,
        [MeasurementUnitId] [INT] NOT NULL
    );

	DECLARE @CreditNoteId INT;
	INSERT INTO @CreditNoteDetail(
		SalesDetailId,
		ReturnQuantity,
        MeasurementUnitId
	)
	    SELECT jd.[SalesDetailId],
           jd.[ReturnQuantity],
           jd.[MeasurementUnitId]
    FROM
        OPENJSON(@CreditNoteDetails)
        WITH
        (
            [SalesDetailId] [INT],
            [ReturnQuantity] [DECIMAL],
            [MeasurementUnitId][INT]
          
        ) jd


	DECLARE @StockIn TABLE
    (
        [SalesDetailId] [INT] NOT NULL,
        [ReturnQuantity] [DECIMAL] NOT NULL,
		[StockInId] [INT] NOT NULL
    );
	INSERT INTO @StockIn(
		SalesDetailId,
		ReturnQuantity,
		StockInId
	)
	 SELECT jd.[SalesDetailId],
           jd.[ReturnQuantity],
		   sd.StockInId
    FROM
        OPENJSON(@CreditNoteDetails)
        WITH
        (
            [SalesDetailId] [INT],
            [ReturnQuantity] [DECIMAL]
          
        ) jd INNER JOIN SalesDetails sd on jd.SalesDetailId=sd.SalesDetailId
    -- Update statements for update saleReturnQty in stockin using saleDetailsId
		UPDATE dbo.StockIn
		SET SalesReturnQty=SalesReturnQty+ReturnQuantity
		FROM @StockIn tempsi
		where dbo.StockIn.StockInId=tempsi.StockInId
    -- Insert statements for procedure here
  INSERT INTO dbo.CreditNote
    (
      SalesId,
      ReasonOfReturn,
      ReturnDate,
      CreatedBy,
      CreatedTime
    )
    VALUES
    (@SalesId,@ReasonOfReturn,@ReturnDate, @CreatedBy,@CreatedTime);

	  SET @CreditNoteId = SCOPE_IDENTITY();
    --Insert CreditNote Details
    INSERT INTO dbo.CreditNoteDetail
    (
        CreditNoteId,
		SalesDetailId,
		ReturnQuantity,
        MeasurementUnitId,
        CreatedBy,
        CreatedTime
    )
    SELECT @CreditNoteId,             
           cd.SalesDetailId,          
           cd.ReturnQuantity,         
           cd.MeasurementUnitId,             
           @CreatedBy,                  
           @CreatedTime                 
    FROM @CreditNoteDetail cd;
END
