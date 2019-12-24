CREATE TABLE [dbo].[TransectionTypes] (
    [TransectionTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NULL,
    [CreatedBy]         INT           NULL,
    [CreatedTime]       DATETIME      NULL,
    CONSTRAINT [PK_TransectionTypes] PRIMARY KEY CLUSTERED ([TransectionTypeId] ASC)
);

