CREATE TABLE [dbo].[Roles] (
    [RoleId]            INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]          NVARCHAR (64) NOT NULL,
    [OrganizationId]    INT           NULL,
    [RoleDefaultPageId] INT           NULL,
    [IsActive]          BIT           NOT NULL,
    [CreatedBy]         INT           NULL,
    [CreatedTime]       DATETIME      NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC),
    CONSTRAINT [FK_Roles_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId])
);

