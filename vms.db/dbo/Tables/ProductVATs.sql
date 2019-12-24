CREATE TABLE [dbo].[ProductVATs] (
    [ProductVATId]     INT      IDENTITY (1, 1) NOT NULL,
    [ProductId]        INT      NOT NULL,
    [ProductVATTypeId] INT      NOT NULL,
    [EffectiveFrom]    DATETIME NOT NULL,
    [EffectiveTo]      DATETIME NULL,
    [IsActive]         INT      NOT NULL,
    [CreatedBy]        INT      NOT NULL,
    [CreatedTime]      DATETIME NOT NULL,
    CONSTRAINT [PK_ProductVAT] PRIMARY KEY CLUSTERED ([ProductVATId] ASC),
    CONSTRAINT [FK_ProductVATs_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    CONSTRAINT [FK_ProductVATs_ProductVATTypes] FOREIGN KEY ([ProductVATTypeId]) REFERENCES [dbo].[ProductVATTypes] ([ProductVATTypeId])
);

