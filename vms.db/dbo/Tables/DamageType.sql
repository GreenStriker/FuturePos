CREATE TABLE [dbo].[DamageType] (
    [DamageTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId] INT           NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [IsActive]       BIT           NOT NULL,
    [CreatedBy]      INT           NULL,
    [CreatedTime]    DATETIME      NULL,
    CONSTRAINT [PK_DamageType] PRIMARY KEY CLUSTERED ([DamageTypeId] ASC),
    CONSTRAINT [FK_DamageType_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId])
);

