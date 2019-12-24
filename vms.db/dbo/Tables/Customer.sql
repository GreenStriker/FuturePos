CREATE TABLE [dbo].[Customer] (
    [CustomerId]             INT            IDENTITY (1, 1) NOT NULL,
    [OrganizationId]         INT            NULL,
    [CustomerOrganizationId] INT            NULL,
    [Name]                   NVARCHAR (200) NOT NULL,
    [BIN]                    NVARCHAR (20)  NULL,
    [NIDNo]                  NVARCHAR (50)  NULL,
    [CountryId]              INT            NULL,
    [Address]                NVARCHAR (200) NULL,
    [PhoneNo]                NVARCHAR (20)  NULL,
    [EmailAddress]           NVARCHAR (100) NULL,
    [IsActive]               BIT            NOT NULL,
    [CreatedBy]              INT            NULL,
    [CreatedTime]            DATETIME       NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [FK_Customer_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId])
);

