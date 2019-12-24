CREATE TABLE [dbo].[Vendor] (
    [VendorId]             INT            IDENTITY (1, 1) NOT NULL,
    [OrganizationId]       INT            NULL,
    [VendorOrganizationId] INT            NULL,
    [Name]                 NVARCHAR (200) NOT NULL,
    [BinNo]                NVARCHAR (20)  NULL,
    [NationalIdNo]         NVARCHAR (50)  NULL,
    [Address]              NVARCHAR (500) NOT NULL,
    [ContactNo]            VARCHAR (20)   NULL,
    [CreatedBy]            INT            NULL,
    [CreatedTime]          DATETIME       NULL,
    CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED ([VendorId] ASC),
    CONSTRAINT [FK_Vendor_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId])
);

