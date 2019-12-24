CREATE TABLE [dbo].[DebitNoteDetail] (
    [DebitNoteDetailId] INT             IDENTITY (1, 1) NOT NULL,
    [DebitNoteId]       INT             NOT NULL,
    [PurchaseDetailId]  INT             NOT NULL,
    [ReturnQuantity]    DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId] INT             NOT NULL,
    [CreatedBy]         INT             NULL,
    [CreatedTime]       DATETIME        NULL,
    CONSTRAINT [PK_DebitNoteDetail] PRIMARY KEY CLUSTERED ([DebitNoteDetailId] ASC),
    CONSTRAINT [FK_DebitNoteDetail_DebitNote] FOREIGN KEY ([DebitNoteId]) REFERENCES [dbo].[DebitNote] ([DebitNoteId]),
    CONSTRAINT [FK_DebitNoteDetail_MeasurementUnits] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [dbo].[MeasurementUnits] ([MeasurementUnitId]),
    CONSTRAINT [FK_DebitNoteDetail_PurchaseDetails] FOREIGN KEY ([PurchaseDetailId]) REFERENCES [dbo].[PurchaseDetails] ([PurchaseDetailId])
);

