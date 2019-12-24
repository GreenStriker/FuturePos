CREATE TABLE [dbo].[SalesPaymentReceive] (
    [SalesPaymentReceiveId]   INT             IDENTITY (1, 1) NOT NULL,
    [SalesId]                 INT             NOT NULL,
    [ReceivedPaymentMethodId] INT             NOT NULL,
    [ReceiveAmount]           DECIMAL (18, 2) NOT NULL,
    [ReceiveDate]             DATETIME        NOT NULL,
    [CreatedBy]               INT             NULL,
    [CreatedTime]             DATETIME        NULL,
    CONSTRAINT [PK_SalesPaymentReceive] PRIMARY KEY CLUSTERED ([SalesPaymentReceiveId] ASC),
    CONSTRAINT [FK_SalesPaymentReceive_PaymentMethod] FOREIGN KEY ([ReceivedPaymentMethodId]) REFERENCES [dbo].[PaymentMethod] ([PaymentMethodId]),
    CONSTRAINT [FK_SalesPaymentReceive_Sales] FOREIGN KEY ([SalesId]) REFERENCES [dbo].[Sales] ([SalesId])
);

