Create Procedure [dbo].[SPManageSalesDue]   
 ( 
  @SalesId INT,  
  @PaymentMethodId INT,  
  @PaidAmount Decimal,  
  @CreatedBy INT 
 )  
 AS   
 Begin   
    Update Sales Set PaymentReceiveAmount= @PaidAmount where SalesId= @SalesId  
    SET NOCOUNT ON  
   Insert Into SalesPaymentReceive   
   (SalesId, ReceivedPaymentMethodId,ReceiveAmount,ReceiveDate,CreatedBy,CreatedTime)  
   values   
   (@SalesId,@PaymentMethodId,@PaidAmount,GETDATE(),@CreatedBy,GETDATE())  
   SET NOCOUNT OFF  
 END  
   