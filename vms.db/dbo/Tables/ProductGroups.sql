CREATE TABLE [dbo].[ProductGroups] (
    [ProductGroupId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [ParentGroupId]  INT           NULL,
    [Node]           NVARCHAR (50) NULL,
    [IsActive]       BIT           NOT NULL,
    [CreatedBy]      INT           NULL,
    [CreatedTime]    DATETIME      NULL,
    CONSTRAINT [PK_ProductGroups] PRIMARY KEY CLUSTERED ([ProductGroupId] ASC)
);

