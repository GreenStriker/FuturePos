CREATE TABLE [dbo].[Content] (
    [ContentId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [DocumentTypeId]   INT            NOT NULL,
    [OrganizationId]   INT            NULL,
    [FileUrl]          NVARCHAR (500) NOT NULL,
    [MimeType]         NVARCHAR (50)  NULL,
    [Node]             NVARCHAR (500) NULL,
    [ObjectId]         INT            NOT NULL,
    [ObjectPrimaryKey] INT            NOT NULL,
    [IsActive]         BIT            NOT NULL,
    [CreatedBy]        INT            NULL,
    [CreatedTime]      DATETIME       NULL,
    CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED ([ContentId] ASC),
    CONSTRAINT [FK_Content_DocumentType] FOREIGN KEY ([DocumentTypeId]) REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
);

