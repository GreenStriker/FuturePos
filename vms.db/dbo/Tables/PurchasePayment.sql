CREATE TABLE [dbo].[PurchasePayment] (
    [PurchasePaymentId] INT             IDENTITY (1, 1) NOT NULL,
    [PurchaseId]        INT             NOT NULL,
    [PaymentMethodId]   INT             NOT NULL,
    [PaidAmount]        DECIMAL (18, 2) NOT NULL,
    [PaymentDate]       DATETIME        NOT NULL,
    [CreatedBy]         INT             NULL,
    [CreatedTime]       DATETIME        NULL,
    CONSTRAINT [PK_PurchasePayment] PRIMARY KEY CLUSTERED ([PurchasePaymentId] ASC),
    CONSTRAINT [FK_PurchasePayment_PaymentMethod] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[PaymentMethod] ([PaymentMethodId]),
    CONSTRAINT [FK_PurchasePayment_Purchase] FOREIGN KEY ([PurchaseId]) REFERENCES [dbo].[Purchase] ([PurchaseId])
);

