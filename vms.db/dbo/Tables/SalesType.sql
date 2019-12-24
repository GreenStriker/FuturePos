CREATE TABLE [dbo].[SalesType] (
    [SalesTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [SalesTypeName] NVARCHAR (50)  NOT NULL,
    [Description]   NVARCHAR (200) NULL,
    [CreatedBy]     INT            NULL,
    [CreatedTime]   DATETIME       NULL,
    CONSTRAINT [PK_SalesType] PRIMARY KEY CLUSTERED ([SalesTypeId] ASC)
);

