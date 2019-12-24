CREATE TABLE [dbo].[PurchaseDetails] (
    [PurchaseDetailId]         INT             IDENTITY (1, 1) NOT NULL,
    [PurchaseId]               INT             NOT NULL,
    [ProductId]                INT             NOT NULL,
    [ProductVATTypeId]         INT             NOT NULL,
    [Quantity]                 DECIMAL (18, 2) NOT NULL,
    [UnitPrice]                DECIMAL (18, 2) NOT NULL,
    [DiscountPerItem]          DECIMAL (18, 2) NOT NULL,
    [ImportDutyPercent]        DECIMAL (18, 2) NOT NULL,
    [RegulatoryDutyPercent]    DECIMAL (18, 2) NOT NULL,
    [SupplementaryDutyPercent] DECIMAL (18, 2) NOT NULL,
    [VATPercent]               DECIMAL (18, 2) NOT NULL,
    [AdvanceTaxPercent]        DECIMAL (18, 2) NOT NULL,
    [AdvanceIncomeTaxPercent]  DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]        INT             NOT NULL,
    [CreatedBy]                INT             NULL,
    [CreatedTime]              DATETIME        NULL,
    CONSTRAINT [PK_PurchaseDetails] PRIMARY KEY CLUSTERED ([PurchaseDetailId] ASC),
    CONSTRAINT [FK_PurchaseDetails_MeasurementUnits] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [dbo].[MeasurementUnits] ([MeasurementUnitId]),
    CONSTRAINT [FK_PurchaseDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    CONSTRAINT [FK_PurchaseDetails_ProductVATTypes] FOREIGN KEY ([ProductVATTypeId]) REFERENCES [dbo].[ProductVATTypes] ([ProductVATTypeId]),
    CONSTRAINT [FK_PurchaseDetails_Purchase] FOREIGN KEY ([PurchaseId]) REFERENCES [dbo].[Purchase] ([PurchaseId])
);

