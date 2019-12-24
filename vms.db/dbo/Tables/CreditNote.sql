CREATE TABLE [dbo].[CreditNote] (
    [CreditNoteId]   INT            IDENTITY (1, 1) NOT NULL,
    [SalesId]        INT            NOT NULL,
    [ReasonOfReturn] NVARCHAR (500) NULL,
    [ReturnDate]     DATETIME       NOT NULL,
    [CreatedBy]      INT            NULL,
    [CreatedTime]    DATETIME       NULL,
    CONSTRAINT [PK_CreditNote] PRIMARY KEY CLUSTERED ([CreditNoteId] ASC),
    CONSTRAINT [FK_CreditNote_Sales] FOREIGN KEY ([SalesId]) REFERENCES [dbo].[Sales] ([SalesId])
);

