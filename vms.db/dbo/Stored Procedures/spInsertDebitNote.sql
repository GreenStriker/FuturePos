-- =============================================
-- Author:		<Md. Sabbir Reza>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertDebitNote]
	-- Add the parameters for the stored procedure here
	@PurchaseId INT,
	@ReasonOfReturn NVARCHAR(50),
	@ReturnDate DATETIME,
	@CreatedBy INT,
    @CreatedTime DATETIME,
	@DebitNoteDetails NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(100) = N'';
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET @CreatedTime = ISNULL(@CreatedTime, GETDATE());
	DECLARE @DebitNoteDetail TABLE
    (
        [PurchaseDetailId] [INT] NOT NULL,
        [ReturnQuantity] [DECIMAL] NOT NULL,
        [MeasurementUnitId] [INT] NOT NULL
    );

	DECLARE @DebitNoteId INT;
	INSERT INTO @DebitNoteDetail(
		PurchaseDetailId,
		ReturnQuantity,
        MeasurementUnitId
	)
	    SELECT jd.[PurchaseDetailId],
           jd.[ReturnQuantity],
           jd.[MeasurementUnitId]
    FROM
        OPENJSON(@DebitNoteDetails)
        WITH
        (
            [PurchaseDetailId] [INT],
            [ReturnQuantity] [DECIMAL],
            [MeasurementUnitId][INT]
          
        ) jd

	DECLARE @StockIn TABLE
    (
        [PurchaseDetailId] [INT] NOT NULL,
        [ReturnQuantity] [DECIMAL] NOT NULL

    );
	INSERT INTO @StockIn(
		PurchaseDetailId,
		ReturnQuantity
	)
	 SELECT jd.[PurchaseDetailId],
           jd.[ReturnQuantity]

    FROM
        OPENJSON(@DebitNoteDetails)
        WITH
        (
            [PurchaseDetailId] [INT],
            [ReturnQuantity] [DECIMAL]
          
        ) jd 
    -- Update statements for update saleReturnQty in stockin using saleDetailsId
		UPDATE dbo.StockIn
		SET PurchaseReturnQty=PurchaseReturnQty+ReturnQuantity
		FROM @StockIn tempsi
		where dbo.StockIn.PurchaseDetailId=tempsi.PurchaseDetailId
    -- Insert statements for procedure here
  INSERT INTO dbo.DebitNote
    (
      PurchaseId,
      ReasonOfReturn,
      ReturnDate,
      CreatedBy,
      CreatedTime
    )
    VALUES
    (@PurchaseId,@ReasonOfReturn,@ReturnDate, @CreatedBy,@CreatedTime);

	  SET @DebitNoteId = SCOPE_IDENTITY();
    --Insert CreditNote Details
    INSERT INTO dbo.DebitNoteDetail
    (
        DebitNoteId,
		PurchaseDetailId,
		ReturnQuantity,
        MeasurementUnitId,
        CreatedBy,
        CreatedTime
    )
    SELECT @DebitNoteId,             
           dn.PurchaseDetailId,          
           dn.ReturnQuantity,         
           dn.MeasurementUnitId,             
           @CreatedBy,                  
           @CreatedTime                 
    FROM @DebitNoteDetail dn;
	COMMIT TRANSACTION;
	    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH;
END
