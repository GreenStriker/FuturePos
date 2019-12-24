CREATE TABLE [dbo].[SupplimentaryDuty] (
    [SupplimentaryDutyId] INT             IDENTITY (1, 1) NOT NULL,
    [OrganizationId]      INT             NOT NULL,
    [ProductId]           INT             NOT NULL,
    [SdPercent]           DECIMAL (18, 2) NOT NULL,
    [IsActive]            BIT             NOT NULL,
    [EffectiveFrom]       DATETIME        NOT NULL,
    [EffectiveTo]         DATETIME        NULL,
    [CreatedBy]           INT             NULL,
    [CreatedTime]         DATETIME        NULL,
    CONSTRAINT [PK_SupplimentaryDuty] PRIMARY KEY CLUSTERED ([SupplimentaryDutyId] ASC),
    CONSTRAINT [FK_SupplimentaryDuty_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId]),
    CONSTRAINT [FK_SupplimentaryDuty_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
);

