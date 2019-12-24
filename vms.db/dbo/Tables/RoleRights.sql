CREATE TABLE [dbo].[RoleRights] (
    [RoleRightId] INT      IDENTITY (1, 1) NOT NULL,
    [RoleId]      INT      NOT NULL,
    [RightId]     INT      NOT NULL,
    [CreatedBy]   INT      NULL,
    [CreatedTime] DATETIME NULL,
    CONSTRAINT [PK_RoleFeatures] PRIMARY KEY CLUSTERED ([RoleRightId] ASC),
    CONSTRAINT [FK_dbo_RoleFeatures_dbo_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [FK_RoleRights_Rights] FOREIGN KEY ([RightId]) REFERENCES [dbo].[Rights] ([RightId])
);

