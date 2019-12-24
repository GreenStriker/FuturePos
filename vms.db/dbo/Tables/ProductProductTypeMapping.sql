CREATE TABLE [dbo].[ProductProductTypeMapping] (
    [ProductProductTypeMappingId] INT IDENTITY (1, 1) NOT NULL,
    [ProductId]                   INT NOT NULL,
    [ProductTypeId]               INT NOT NULL,
    CONSTRAINT [PK_ProductProductTypeMapping] PRIMARY KEY CLUSTERED ([ProductProductTypeMappingId] ASC),
    CONSTRAINT [FK_ProductProductTypeMapping_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    CONSTRAINT [FK_ProductProductTypeMapping_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([ProductTypeId])
);

