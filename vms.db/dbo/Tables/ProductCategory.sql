CREATE TABLE [dbo].[ProductCategory] (
    [ProductCategoryId] INT            IDENTITY (1, 1) NOT NULL,
    [OrganizationId]    INT            NULL,
    [Name]              NVARCHAR (100) NOT NULL,
    [IsActive]          BIT            NOT NULL,
    [CreatedBy]         INT            NULL,
    [CreatedTime]       DATETIME       NULL,
    CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([ProductCategoryId] ASC),
    CONSTRAINT [FK_ProductCategory_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId])
);

