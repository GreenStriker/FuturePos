CREATE TABLE [dbo].[Rights] (
    [RightId]     INT            IDENTITY (1, 1) NOT NULL,
    [RightName]   NVARCHAR (64)  NOT NULL,
    [Description] NVARCHAR (128) NULL,
    [IsActive]    BIT            NOT NULL,
    [CreatedBy]   INT            NULL,
    [CreatedTime] DATETIME       NULL,
    CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED ([RightId] ASC)
);

