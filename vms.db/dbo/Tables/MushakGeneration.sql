﻿CREATE TABLE [dbo].[MushakGeneration] (
    [MushakGenerationId]               INT             IDENTITY (1, 1) NOT NULL,
    [MushakForYear]                    INT             NOT NULL,
    [MushakForMonth]                   INT             NOT NULL,
    [GenerateDate]                     DATETIME        NOT NULL,
    [IsSubmitted]                      BIT             NULL,
    [SubmissionDate]                   DATETIME        NULL,
    [PaidAmountForVat]                 DECIMAL (18, 2) NULL,
    [PaidAmountForSuppDuty]            DECIMAL (18, 2) NULL,
    [InterestForDueVat]                DECIMAL (18, 2) NULL,
    [InterestForDueSuppDuty]           DECIMAL (18, 2) NULL,
    [FinancialPenalty]                 DECIMAL (18, 2) NULL,
    [ExciseDuty]                       DECIMAL (18, 2) NULL,
    [DevelopmentSurcharge]             DECIMAL (18, 2) NULL,
    [ItDevelopmentSurcharge]           DECIMAL (18, 2) NULL,
    [HealthDevelopmentSurcharge]       DECIMAL (18, 2) NULL,
    [EnvironmentProtectSurcharge]      DECIMAL (18, 2) NULL,
    [LastClosingVatAmount]             DECIMAL (18, 2) NULL,
    [LastClosingSuppDutyAmount]        DECIMAL (18, 2) NULL,
    [PaidVatAmount]                    DECIMAL (18, 2) NULL,
    [PaidSuppDutyAmount]               DECIMAL (18, 2) NULL,
    [PaidInterestAmountForDueVat]      DECIMAL (18, 2) NULL,
    [PaidInterestAmountForDueSuppDuty] DECIMAL (18, 2) NULL,
    [PaidFinancialPenalty]             DECIMAL (18, 2) NULL,
    [PaidExciseDuty]                   DECIMAL (18, 2) NULL,
    [PaidDevelopmentSurcharge]         DECIMAL (18, 2) NULL,
    [PaidItDevelopmentSurcharge]       DECIMAL (18, 2) NULL,
    [PaidHealthDevelopmentSurcharge]   DECIMAL (18, 2) NULL,
    [PaidEnvironmentProtectSurcharge]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_MushakGeneration] PRIMARY KEY CLUSTERED ([MushakGenerationId] ASC)
);

