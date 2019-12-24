CREATE TABLE [dbo].[ProductType] (
    [ProductTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED ([ProductTypeId] ASC)
);

