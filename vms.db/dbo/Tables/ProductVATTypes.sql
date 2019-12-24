CREATE TABLE [dbo].[ProductVATTypes] (
    [ProductVATTypeId]         INT             IDENTITY (1, 1) NOT NULL,
    [Name]                     NVARCHAR (50)   NOT NULL,
    [DefaultVatPercent]        DECIMAL (18, 2) CONSTRAINT [DF__tmp_ms_xx__Defau__72C60C4A] DEFAULT ((0)) NOT NULL,
    [SupplementaryDutyPercent] DECIMAL (18)    NULL,
    [DisplayName]              NVARCHAR (50)   NOT NULL,
    [EffectiveFrom]            NVARCHAR (50)   NOT NULL,
    [EffectiveTo]              NVARCHAR (50)   NULL,
    [TransactionTypeId]        INT             NOT NULL,
    [PurchaseTypeId]           INT             NULL,
    [SalesTypeId]              INT             NULL,
    [IsActive]                 BIT             NULL,
    [CreatedBy]                INT             NULL,
    [CreatedTime]              DATETIME        NULL,
    CONSTRAINT [PK_ProductVATType] PRIMARY KEY CLUSTERED ([ProductVATTypeId] ASC),
    CONSTRAINT [FK_ProductVATTypes_PurchaseTypes] FOREIGN KEY ([PurchaseTypeId]) REFERENCES [dbo].[PurchaseTypes] ([PurchaseTypeId]),
    CONSTRAINT [FK_ProductVATTypes_SalesType] FOREIGN KEY ([SalesTypeId]) REFERENCES [dbo].[SalesType] ([SalesTypeId]),
    CONSTRAINT [FK_ProductVATTypes_TransectionTypes] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[TransectionTypes] ([TransectionTypeId])
);

