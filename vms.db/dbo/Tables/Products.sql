﻿CREATE TABLE [dbo].[Products] (
    [ProductId]         INT             IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)   NOT NULL,
    [ModelNo]           NVARCHAR (50)   NULL,
    [Code]              NVARCHAR (50)   NULL,
    [HSCode]            NVARCHAR (50)   NULL,
    [ProductCategoryId] INT             NULL,
    [ProductGroupId]    INT             NOT NULL,
    [OrganizationId]    INT             NOT NULL,
    [TotalQuantity]     DECIMAL (18, 2) CONSTRAINT [DF_Products_TotalQuantity] DEFAULT ((0)) NOT NULL,
    [MeasurementUnitId] INT             NOT NULL,
    [EffectiveFrom]     DATETIME        NOT NULL,
    [EffectiveTo]       DATETIME        NULL,
    [IsActive]          BIT             NOT NULL,
    [CreatedBy]         INT             NULL,
    [CreatedTime]       DATETIME        NULL,
    [IsSellable]        BIT             NOT NULL,
    [IsRawMaterial]     BIT             NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_Products_MeasurementUnits] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [dbo].[MeasurementUnits] ([MeasurementUnitId]),
    CONSTRAINT [FK_Products_Organizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([OrganizationId]),
    CONSTRAINT [FK_Products_ProductCategory] FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryId]),
    CONSTRAINT [FK_Products_ProductGroups] FOREIGN KEY ([ProductGroupId]) REFERENCES [dbo].[ProductGroups] ([ProductGroupId])
);

