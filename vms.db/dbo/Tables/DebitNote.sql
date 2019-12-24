CREATE TABLE [dbo].[DebitNote] (
    [DebitNoteId]    INT            IDENTITY (1, 1) NOT NULL,
    [PurchaseId]     INT            NOT NULL,
    [ReasonOfReturn] NVARCHAR (500) NULL,
    [ReturnDate]     DATETIME       NOT NULL,
    [CreatedBy]      INT            NULL,
    [CreatedTime]    DATETIME       NULL,
    CONSTRAINT [PK_DebitNote] PRIMARY KEY CLUSTERED ([DebitNoteId] ASC),
    CONSTRAINT [FK_DebitNote_Purchase] FOREIGN KEY ([PurchaseId]) REFERENCES [dbo].[Purchase] ([PurchaseId])
);

