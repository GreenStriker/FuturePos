CREATE TABLE [dbo].[PurchaseTypes] (
    [PurchaseTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [CreatedBy]      INT           NULL,
    [CreatedTime]    DATETIME      NULL,
    CONSTRAINT [PK_PurchaseType] PRIMARY KEY CLUSTERED ([PurchaseTypeId] ASC)
);

