 CREATE Procedure [dbo].[USP_AuditRestore]  
  (  
  @TableName VARCHAR(100),  
  @PrimaryKeyName VARCHAR(50),  
  @PrimaryKey VARCHAR(50) ,
  @AuditLogId VARCHAR(50) 
  )  
  As   
  BEGIN  
     Declare @value NVARCHAR(500)  
       SET @value = 'Update '+@TableName+'  SET  ISActive =1 where '+@PrimaryKeyName+'='+ @PrimaryKey  
       --Print(@value)   
      Update AuditLog SET IsActive=0 where AuditLogId=@AuditLogId
	   EXec(@value)  
    END  