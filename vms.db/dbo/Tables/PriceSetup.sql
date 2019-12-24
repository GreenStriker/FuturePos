CREATE TABLE [dbo].[PriceSetup] (
    [PriceSetupId]      INT             IDENTITY (1, 1) NOT NULL,
    [OrganizationId]    INT             NOT NULL,
    [ProductId]         INT             NOT NULL,
    [MeasurementUnitId] INT             NOT NULL,
    [BaseTP]            DECIMAL (18, 2) NULL,
    [MRP]               DECIMAL (18, 2) NULL,
    [PurchaseUnitPrice] DECIMAL (18, 2) NOT NULL,
    [SalesUnitPrice]    DECIMAL (18, 2) NOT NULL,
    [EffectiveFrom]     DATETIME        NOT NULL,
    [EffectiveTo]       DATETIME        NULL,
    [IsActive]          BIT             NOT NULL,
    [CreatedBy]         INT             NULL,
    [CreatedTime]       DATETIME        NULL,
    CONSTRAINT [PK_PriceSetup] PRIMARY KEY CLUSTERED ([PriceSetupId] ASC),
    CONSTRAINT [FK_PriceSetup_MeasurementUnits] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [dbo].[MeasurementUnits] ([MeasurementUnitId]),
    CONSTRAINT [FK_PriceSetup_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId]),
    CONSTRAINT [FK_PriceSetup_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
);

