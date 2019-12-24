CREATE TABLE [dbo].[ProductionDetails] (
    [ProductionDetailsId] INT             IDENTITY (1, 1) NOT NULL,
    [ProductionId]        INT             NOT NULL,
    [ProductId]           INT             NOT NULL,
    [Quantity]            DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_ProductionDetails] PRIMARY KEY CLUSTERED ([ProductionDetailsId] ASC),
    CONSTRAINT [FK_ProductionDetails_Productions] FOREIGN KEY ([ProductionId]) REFERENCES [dbo].[Productions] ([ProductionId]),
    CONSTRAINT [FK_ProductionDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
);

