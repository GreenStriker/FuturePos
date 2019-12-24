CREATE TABLE [acc].[COAGroups] (
    [COAGroupId]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NULL,
    [ParentGroupId] INT           NULL,
    [Node]          NVARCHAR (50) NULL,
    [IsActive]      BIT           NULL,
    [CreatedBy]     INT           NULL,
    [CreatedTime]   DATETIME      NULL,
    CONSTRAINT [PK_COAGroups] PRIMARY KEY CLUSTERED ([COAGroupId] ASC)
);

