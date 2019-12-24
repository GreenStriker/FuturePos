 CREATE Procedure [dbo].[SPManagePurchaseDue]   
 (  
  @PurchaseId INT,  
  @PaymentMethodId INT,  
  @PaidAmount Decimal,  
  @CreatedBy INT  
 )  
 AS   
 Begin   
    Update Purchase Set PaidAmount= @PaidAmount where PurchaseId= @PurchaseId  
    SET NOCOUNT ON  
   Insert Into PurchasePayment   
   (PurchaseId, PaymentMethodId,PaidAmount,PaymentDate,CreatedBy,CreatedTime)  
   values   
   (@PurchaseId,@PaymentMethodId,@PaidAmount,GETDATE(),@CreatedBy,GETDATE())  
   SET NOCOUNT OFF  
 END  
   