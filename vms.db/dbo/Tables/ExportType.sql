CREATE TABLE [dbo].[ExportType] (
    [ExportTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [ExportTypeName] NVARCHAR (50) NOT NULL,
    [CreatedBy]      INT           NULL,
    [CreatedTime]    DATETIME      NULL,
    CONSTRAINT [PK_ExportType] PRIMARY KEY CLUSTERED ([ExportTypeId] ASC)
);

