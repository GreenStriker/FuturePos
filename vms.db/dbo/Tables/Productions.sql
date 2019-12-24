CREATE TABLE [dbo].[Productions] (
    [ProductionId]   INT      IDENTITY (1, 1) NOT NULL,
    [WorkOrderId]    INT      NULL,
    [OrganizationId] INT      NULL,
    [CreatedBy]      INT      NOT NULL,
    [CreatedTime]    DATETIME NOT NULL,
    [ExpectedDate]   DATETIME NOT NULL,
    [StartDate]      DATETIME NOT NULL,
    [EndDate]        DATETIME NOT NULL,
    CONSTRAINT [PK_Productions] PRIMARY KEY CLUSTERED ([ProductionId] ASC),
    CONSTRAINT [FK_Productions_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId])
);

