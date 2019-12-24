CREATE TABLE [dbo].[Users] (
    [UserId]                  INT            IDENTITY (1, 1) NOT NULL,
    [FullName]                NVARCHAR (200) NULL,
    [UserName]                NVARCHAR (64)  NOT NULL,
    [EmailAddress]            NVARCHAR (64)  NULL,
    [Password]                NVARCHAR (64)  NULL,
    [UserTypeId]              INT            NOT NULL,
    [RoleId]                  INT            NOT NULL,
    [OrganizationId]          INT            NULL,
    [Mobile]                  NVARCHAR (50)  NULL,
    [IsActive]                BIT            NOT NULL,
    [LastLoginTime]           DATETIME       NULL,
    [IsDefaultPassword]       BIT            NOT NULL,
    [IsCompanyRepresentative] BIT            NOT NULL,
    [CreatedBy]               INT            NULL,
    [CreatedTime]             DATETIME       NULL,
    CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_dbo_UserProfiles_dbo_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [FK_Users_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId]),
    CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserTypes] ([UserTypeId])
);

