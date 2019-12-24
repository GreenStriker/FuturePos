CREATE TABLE [dbo].[CreditNoteDetail] (
    [CreditNoteDetailId] INT             IDENTITY (1, 1) NOT NULL,
    [CreditNoteId]       INT             NOT NULL,
    [SalesDetailId]      INT             NOT NULL,
    [ReturnQuantity]     DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]  INT             NOT NULL,
    [CreatedBy]          INT             NULL,
    [CreatedTime]        DATETIME        NULL,
    CONSTRAINT [PK_CreditNoteDetail] PRIMARY KEY CLUSTERED ([CreditNoteDetailId] ASC),
    CONSTRAINT [FK_CreditNoteDetail_CreditNote] FOREIGN KEY ([CreditNoteId]) REFERENCES [dbo].[CreditNote] ([CreditNoteId]),
    CONSTRAINT [FK_CreditNoteDetail_MeasurementUnits] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [dbo].[MeasurementUnits] ([MeasurementUnitId]),
    CONSTRAINT [FK_CreditNoteDetail_SalesDetails] FOREIGN KEY ([SalesDetailId]) REFERENCES [dbo].[SalesDetails] ([SalesDetailId])
);

