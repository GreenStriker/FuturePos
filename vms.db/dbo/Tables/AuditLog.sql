CREATE TABLE [dbo].[AuditLog] (
    [AuditLogId]       INT             IDENTITY (1, 1) NOT NULL,
    [ObjectTypeId]     INT             NOT NULL,
    [PrimaryKey]       INT             NULL,
    [AuditOperationId] INT             NOT NULL,
    [UserId]           INT             NOT NULL,
    [Datetime]         DATETIME2 (7)   NOT NULL,
    [Descriptions]     NVARCHAR (4000) NOT NULL,
    [IsActive]         BIT             NOT NULL,
    [CreatedBy]        INT             NULL,
    [CreatedTime]      DATETIME        NULL,
    [OrganizationId]   INT             NULL,
    CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED ([AuditLogId] ASC),
    CONSTRAINT [FK_AuditLog_AuditLog] FOREIGN KEY ([AuditLogId]) REFERENCES [dbo].[AuditLog] ([AuditLogId]),
    CONSTRAINT [FK_AuditLog_AuditOperation] FOREIGN KEY ([AuditOperationId]) REFERENCES [dbo].[AuditOperation] ([AuditOperationID]),
    CONSTRAINT [FK_AuditLog_ObjectType] FOREIGN KEY ([ObjectTypeId]) REFERENCES [dbo].[ObjectType] ([ObjectTypeId]),
    CONSTRAINT [FK_AuditLog_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId]),
    CONSTRAINT [FK_AuditLog_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

