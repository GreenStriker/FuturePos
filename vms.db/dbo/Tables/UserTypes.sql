CREATE TABLE [dbo].[UserTypes] (
    [UserTypeId]    INT           IDENTITY (1, 1) NOT NULL,
    [UserTypeName]  NVARCHAR (50) NOT NULL,
    [IsActive]      BIT           NOT NULL,
    [EffectiveFrom] DATETIME      NOT NULL,
    [EffectiveTo]   DATETIME      NULL,
    [CreatedBy]     INT           NULL,
    [CreatedTime]   DATETIME      NULL,
    CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED ([UserTypeId] ASC)
);

