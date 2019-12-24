CREATE TABLE [dbo].[Organizations] (
    [OrganizationId]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                            NVARCHAR (50)  NOT NULL,
    [ParentOrganizationId]            INT            NULL,
    [Code]                            NVARCHAR (50)  NULL,
    [VATRegNo]                        NVARCHAR (50)  NULL,
    [BIN]                             NVARCHAR (50)  NULL,
    [IsDeductVatInSource]             BIT            NOT NULL,
    [CertificateNo]                   NVARCHAR (50)  NULL,
    [Address]                         NVARCHAR (200) NOT NULL,
    [CountryId]                       INT            NOT NULL,
    [CityId]                          INT            NOT NULL,
    [VatResponsiblePersonName]        NVARCHAR (100) NOT NULL,
    [VatResponsiblePersonDesignation] NVARCHAR (50)  NOT NULL,
    [IsActive]                        BIT            NOT NULL,
    [EnlistedNo]                      INT            NULL,
    [PostalCode]                      INT            NULL,
    [CreatedBy]                       INT            NULL,
    [CreatedTime]                     DATETIME       NULL,
    CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED ([OrganizationId] ASC)
);

