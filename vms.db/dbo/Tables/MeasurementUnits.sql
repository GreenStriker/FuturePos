CREATE TABLE [dbo].[MeasurementUnits] (
    [MeasurementUnitId] INT           IDENTITY (1023, 1) NOT NULL,
    [Name]              NVARCHAR (50) NOT NULL,
    [OrganizationId]    INT           NOT NULL,
    [IsActive]          BIT           NOT NULL,
    [CreatedBy]         INT           NULL,
    [CreatedTime]       DATETIME      NULL,
    CONSTRAINT [PK_MeasurementUnits] PRIMARY KEY CLUSTERED ([MeasurementUnitId] ASC)
);

