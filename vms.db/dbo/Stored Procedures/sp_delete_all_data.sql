
CREATE PROCEDURE [dbo].[sp_delete_all_data]
AS
BEGIN
    -- disable referential integrity
    EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

    -- delete data
    EXEC sp_MSforeachtable 'DELETE FROM ?';

    -- enable referential integrity again 
    EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';

    -- Reseed
    EXEC sp_MSforeachtable '
 Print ''?''
 IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1
  DBCC CHECKIDENT (''?'', RESEED, 0)';
END;
