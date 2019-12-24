CREATE TABLE [dbo].[Damage] (
    [DamageId]       INT             IDENTITY (1, 1) NOT NULL,
    [OrganizationId] INT             NULL,
    [ProductId]      INT             NOT NULL,
    [StockInId]      BIGINT          NOT NULL,
    [DamageQty]      DECIMAL (18, 2) NOT NULL,
    [DamageTypeId]   INT             NOT NULL,
    [Description]    NVARCHAR (1000) NOT NULL,
    [IsActive]       BIT             NOT NULL,
    [CreatedBy]      INT             NULL,
    [CreatedTime]    DATETIME        NULL,
    CONSTRAINT [PK_Damage] PRIMARY KEY CLUSTERED ([DamageId] ASC),
    CONSTRAINT [FK_Damage_DamageType] FOREIGN KEY ([DamageTypeId]) REFERENCES [dbo].[DamageType] ([DamageTypeId]),
    CONSTRAINT [FK_Damage_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId]),
    CONSTRAINT [FK_Damage_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    CONSTRAINT [FK_Damage_StockIn] FOREIGN KEY ([StockInId]) REFERENCES [dbo].[StockIn] ([StockInId])
);

