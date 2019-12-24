CREATE TABLE [dbo].[ProductionReceive] (
    [ProductionReceiveId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [BatchNo]             NVARCHAR (50)   NULL,
    [OrganizationId]      INT             NOT NULL,
    [ProductionId]        INT             NULL,
    [ProductId]           INT             NOT NULL,
    [ReceiveQuantity]     DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]   INT             NOT NULL,
    [ReceiveTime]         DATETIME        NOT NULL,
    [MaterialCost]        DECIMAL (18, 2) NOT NULL,
    [IsActive]            BIT             NOT NULL,
    [CreatedBy]           INT             NULL,
    [CreatedTime]         DATETIME        NULL,
    CONSTRAINT [PK_ProductionReceive] PRIMARY KEY CLUSTERED ([ProductionReceiveId] ASC)
);

