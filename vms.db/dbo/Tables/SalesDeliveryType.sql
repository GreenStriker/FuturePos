CREATE TABLE [dbo].[SalesDeliveryType] (
    [SalesDeliveryTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (50)  NOT NULL,
    [Description]         NVARCHAR (200) NULL,
    [CreatedBy]           INT            NULL,
    [CreatedTime]         DATETIME       NULL,
    CONSTRAINT [PK_SalesDeliveryType] PRIMARY KEY CLUSTERED ([SalesDeliveryTypeId] ASC)
);

