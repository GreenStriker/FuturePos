-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Test Execution: SpGetMushakNinePointOne 16, 2019, 1
-- =============================================
CREATE PROCEDURE SpGetMushakNinePointOne
    @OrganizationId INT,
    @Year INT,
    @Month INT
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;


    -- Insert statements for procedure here
    DECLARE @firstDayOfMonth DATETIME = CAST(@Year AS VARCHAR(4)) + '-' + CAST(@Month AS VARCHAR(2)) + '-01',
            @firstDayOfNextMonth DATETIME = CAST(@Year AS VARCHAR(4)) + '-' + CAST(@Month + 1 AS VARCHAR(2)) + '-01';

			DECLARE @totalSalesAmountInTerm DECIMAL(18, 2)

    DECLARE @MushakReturn TABLE
    (
        MushakReturnId INT NOT NULL,
        --Part-1: Tax-payers information
        OrganizationId INT NOT NULL,
        OrganizationName NVARCHAR(100) NOT NULL,
        OrganizationVatRegNo NVARCHAR(20) NOT NULL,
        OrganizationBin NVARCHAR(20) NOT NULL,
        OrganizationAddress NVARCHAR(200) NOT NULL,
        OrganizationTypeOfBusiness NVARCHAR(50) NOT NULL,
        OrganizationTypeOfFinancialActivity NVARCHAR(50) NOT NULL,
        --Part-2: Submission Information
        TermOfTax NVARCHAR(20) NOT NULL,
        TypeOfSubmission NVARCHAR(100) NOT NULL,
        IsOperatedInLastTerm BIT NOT NULL,
        IsOperatedInLastTermInWords NVARCHAR(10) NOT NULL,
        DateOfSubmission DATETIME NOT NULL,
        --Part-3: Sell/Supply - Payable Tax
        DirectExportAmount DECIMAL(18, 2) NOT NULL,
        IndirectExportAmount DECIMAL(18, 2) NOT NULL,
        VatExemptedProdSellAmount DECIMAL(18, 2) NOT NULL,
        StandardVatRateProdSellAmount DECIMAL(18, 2) NOT NULL,
        StandardVatRateProdSellSdAmount DECIMAL(18, 2) NOT NULL,
        StandardVatRateProdSellVatAmount DECIMAL(18, 2) NOT NULL,
        MrpProdSellAmount DECIMAL(18, 2) NOT NULL,
        MrpProdSellSdAmount DECIMAL(18, 2) NOT NULL,
        MrpProdSellVatAmount DECIMAL(18, 2) NOT NULL,
        FixedVatProdSellAmount DECIMAL(18, 2) NOT NULL,
        FixedVatProdSellSdAmount DECIMAL(18, 2) NOT NULL,
        FixedVatProdSellVatAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatRateProdSellAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatRateProdSellSdAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatRateProdSellVatAmount DECIMAL(18, 2) NOT NULL,
        TradingSellAmount DECIMAL(18, 2) NOT NULL,
        TradingSellSdAmount DECIMAL(18, 2) NOT NULL,
        TradingSellVatAmount DECIMAL(18, 2) NOT NULL,
        TotalAmount DECIMAL(18, 2) NOT NULL,
        TotalSdAmount DECIMAL(18, 2) NOT NULL,
        TotalVatAmount DECIMAL(18, 2) NOT NULL,
        --Part-4: Purchase - Raw-Material Tax
        ZeroVatProdLocalPurchaseAmount DECIMAL(18, 2) NOT NULL,
        ZeroVatProdImportAmount DECIMAL(18, 2) NOT NULL,
        VatExemptedProdLocalPurchaseAmount DECIMAL(18, 2) NOT NULL,
        VatExemptedProdImportAmount DECIMAL(18, 2) NOT NULL,
        StandardVatProdLocalPurchaseAmount DECIMAL(18, 2) NOT NULL,
        StandardVatProdLocalPurchaseVatAmount DECIMAL(18, 2) NOT NULL,
        StandardVatProdImportAmount DECIMAL(18, 2) NOT NULL,
        StandardVatProdImportVatAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatProdLocalPurchaseAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatProdLocalPurchaseVatAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatProdImportAmount DECIMAL(18, 2) NOT NULL,
        OtherThanStandardVatProdImportVatAmount DECIMAL(18, 2) NOT NULL,
        FixedVatProdLocalPurchaseAmount DECIMAL(18, 2) NOT NULL,
        FixedVatProdLocalPurchaseVatAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdLocalPurchaseFromTurnOverOrgAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdLocalPurchaseFromTurnOverOrgVatAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdLocalPurchaseFromNonRegOrgAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdLocalPurchaseFromNonRegOrgVatAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdVatAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdAmount DECIMAL(18, 2) NOT NULL,
        NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdVatAmount DECIMAL(18, 2) NOT NULL,
        TotalRawMaterialRebateAmount DECIMAL(18, 2) NOT NULL,
        --Part-5: Incremental Adjustment (VAT)
        IncrementalAdjustmentAmountForVdsSale DECIMAL(18, 2) NOT NULL,
        IncrementalAdjustmentAmountForNotPaidInBankingChannel DECIMAL(18, 2) NOT NULL,
        IncrementalAdjustmentAmountForDebitNote DECIMAL(18, 2) NOT NULL,
        IncrementalAdjustmentAmountForOtherReason DECIMAL(18, 2) NOT NULL,
        IncrementalAdjustmentDescriptionForOtherReason NVARCHAR(200) NOT NULL,
        TotalIncrementalAdjustmentAmount DECIMAL(18, 2) NOT NULL,
        --Part-6: Decremental Adjustment
        DecrementalAdjustmentAmountForVdsPurchase DECIMAL(18, 2) NOT NULL,
        DecrementalAdjustmentAmountForAdvanceTaxInImport DECIMAL(18, 2) NOT NULL,
        DecrementalAdjustmentAmountForRawMaterialPurchaseToProduceExportProd DECIMAL(18, 2) NOT NULL,
        DecrementalAdjustmentAmountForCreditNote DECIMAL(18, 2) NOT NULL,
        DecrementalAdjustmentAmountForOtherReason DECIMAL(18, 2) NOT NULL,
        DecrementalAdjustmentDescriptionForOtherReason NVARCHAR(200) NOT NULL,
        TotalDecrementalAdjustmentAmount DECIMAL(18, 2) NOT NULL,
        --Part-7: Net Tax Calculation
        NetTaxTotalPayableVatAmountInCurrentTaxTerm DECIMAL(18, 2) NOT NULL,
        NetTaxTotalPayableVatAmountAfterAdjustmentWithEndingBalance DECIMAL(18, 2) NOT NULL,
        NetTaxTotalPayableSdAmountInCurrentTaxTerm DECIMAL(18, 2) NOT NULL,
        NetTaxTotalPayableSdAmountAfterAdjustmentWithEndingBalance DECIMAL(18, 2) NOT NULL,
        NetTaxVatAmountForDebitNote DECIMAL(18, 2) NOT NULL,
        NetTaxVatAmountForCreditNote DECIMAL(18, 2) NOT NULL,
        NetTaxPaidVatAmountForRawMaterialPurchaseToProduceExportProd DECIMAL(18, 2) NOT NULL,
        NetTaxInterstAmountForDeuVat DECIMAL(18, 2) NOT NULL,
        NetTaxInterstAmountForDeuSd DECIMAL(18, 2) NOT NULL,
        NetTaxFineAndPenaltyAmount DECIMAL(18, 2) NOT NULL,
        NetTaxExciseDuty DECIMAL(18, 2) NOT NULL,
        NetTaxDevelopmentSurcharge DECIMAL(18, 2) NOT NULL,
        NetTaxInformationTechnologyDevelopmentSurcharge DECIMAL(18, 2) NOT NULL,
        NetTaxHealthProtectionSurcharge DECIMAL(18, 2) NOT NULL,
        NetTaxEnvironmentProtectionSurcharge DECIMAL(18, 2) NOT NULL,
        NetTaxVatEndingBalanceOfLastTerm DECIMAL(18, 2) NOT NULL,
        NetTaxSdEndingBalanceOfLastTerm DECIMAL(18, 2) NOT NULL,
        --Part-8: Tax Payment Schedule (Treasury Submission)
        TreasurySubmissionAmountForCurrentTermVat DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForCurrentTermSd DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForInterestOfDueVat DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForInterestOfDueSd DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForFineAndPenalty DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForExciseDuty DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForDevelopmentSurcharge DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForInformationTechnologyDevelopmentSurcharge DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForHealthProtectionSurcharge DECIMAL(18, 2) NOT NULL,
        TreasurySubmissionAmountForEnvironmentProtectionSurcharge DECIMAL(18, 2) NOT NULL,
        --Part-9: Ending Balance (Initial Balance for next Term of Tax)
        VatEndingBalance DECIMAL(18, 2) NOT NULL,
        SdEndingBalance DECIMAL(18, 2) NOT NULL,
        --Part-10: Return
        IsWantToGetReturnAmountInEndingBalance BIT NOT NULL,
        IsWantToGetReturnAmountInEndingBalanceInWords NVARCHAR(10) NOT NULL,
        --Part-10: Declaration
        VatResponsiblePersonName NVARCHAR(100) NOT NULL,
        VatResponsiblePersonDesignation NVARCHAR(100) NOT NULL,
        DateOfSignatureInSubmission DATETIME NOT NULL,
        VatResponsiblePersonMobileNo NVARCHAR(20) NOT NULL,
        VatResponsiblePersonEmail NVARCHAR(100) NOT NULL
    );

    INSERT INTO @MushakReturn
    (
        MushakReturnId,
        OrganizationId,
        OrganizationName,
        OrganizationVatRegNo,
        OrganizationBin,
        OrganizationAddress,
        OrganizationTypeOfBusiness,
        OrganizationTypeOfFinancialActivity,
        TermOfTax,
        TypeOfSubmission,
        IsOperatedInLastTerm,
        IsOperatedInLastTermInWords,
        DateOfSubmission,
        DirectExportAmount,
        IndirectExportAmount,
        VatExemptedProdSellAmount,
        StandardVatRateProdSellAmount,
        StandardVatRateProdSellSdAmount,
        StandardVatRateProdSellVatAmount,
        MrpProdSellAmount,
        MrpProdSellSdAmount,
        MrpProdSellVatAmount,
        FixedVatProdSellAmount,
        FixedVatProdSellSdAmount,
        FixedVatProdSellVatAmount,
        OtherThanStandardVatRateProdSellAmount,
        OtherThanStandardVatRateProdSellSdAmount,
        OtherThanStandardVatRateProdSellVatAmount,
        TradingSellAmount,
        TradingSellSdAmount,
        TradingSellVatAmount,
        TotalAmount,
        TotalSdAmount,
        TotalVatAmount,
        ZeroVatProdLocalPurchaseAmount,
        ZeroVatProdImportAmount,
        VatExemptedProdLocalPurchaseAmount,
        VatExemptedProdImportAmount,
        StandardVatProdLocalPurchaseAmount,
        StandardVatProdLocalPurchaseVatAmount,
        StandardVatProdImportAmount,
        StandardVatProdImportVatAmount,
        OtherThanStandardVatProdLocalPurchaseAmount,
        OtherThanStandardVatProdLocalPurchaseVatAmount,
        OtherThanStandardVatProdImportAmount,
        OtherThanStandardVatProdImportVatAmount,
        FixedVatProdLocalPurchaseAmount,
        FixedVatProdLocalPurchaseVatAmount,
        NonRebatableProdLocalPurchaseFromTurnOverOrgAmount,
        NonRebatableProdLocalPurchaseFromTurnOverOrgVatAmount,
        NonRebatableProdLocalPurchaseFromNonRegOrgAmount,
        NonRebatableProdLocalPurchaseFromNonRegOrgVatAmount,
        NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdAmount,
        NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdVatAmount,
        NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdAmount,
        NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdVatAmount,
        TotalRawMaterialRebateAmount,
        IncrementalAdjustmentAmountForVdsSale,
        IncrementalAdjustmentAmountForNotPaidInBankingChannel,
        IncrementalAdjustmentAmountForDebitNote,
        IncrementalAdjustmentAmountForOtherReason,
        IncrementalAdjustmentDescriptionForOtherReason,
        TotalIncrementalAdjustmentAmount,
        DecrementalAdjustmentAmountForVdsPurchase,
        DecrementalAdjustmentAmountForAdvanceTaxInImport,
        DecrementalAdjustmentAmountForRawMaterialPurchaseToProduceExportProd,
        DecrementalAdjustmentAmountForCreditNote,
        DecrementalAdjustmentAmountForOtherReason,
        DecrementalAdjustmentDescriptionForOtherReason,
        TotalDecrementalAdjustmentAmount,
        NetTaxTotalPayableVatAmountInCurrentTaxTerm,
        NetTaxTotalPayableVatAmountAfterAdjustmentWithEndingBalance,
        NetTaxTotalPayableSdAmountInCurrentTaxTerm,
        NetTaxTotalPayableSdAmountAfterAdjustmentWithEndingBalance,
        NetTaxVatAmountForDebitNote,
        NetTaxVatAmountForCreditNote,
        NetTaxPaidVatAmountForRawMaterialPurchaseToProduceExportProd,
        NetTaxInterstAmountForDeuVat,
        NetTaxInterstAmountForDeuSd,
        NetTaxFineAndPenaltyAmount,
        NetTaxExciseDuty,
        NetTaxDevelopmentSurcharge,
        NetTaxInformationTechnologyDevelopmentSurcharge,
        NetTaxHealthProtectionSurcharge,
        NetTaxEnvironmentProtectionSurcharge,
        NetTaxVatEndingBalanceOfLastTerm,
        NetTaxSdEndingBalanceOfLastTerm,
        TreasurySubmissionAmountForCurrentTermVat,
        TreasurySubmissionAmountForCurrentTermSd,
        TreasurySubmissionAmountForInterestOfDueVat,
        TreasurySubmissionAmountForInterestOfDueSd,
        TreasurySubmissionAmountForFineAndPenalty,
        TreasurySubmissionAmountForExciseDuty,
        TreasurySubmissionAmountForDevelopmentSurcharge,
        TreasurySubmissionAmountForInformationTechnologyDevelopmentSurcharge,
        TreasurySubmissionAmountForHealthProtectionSurcharge,
        TreasurySubmissionAmountForEnvironmentProtectionSurcharge,
        VatEndingBalance,
        SdEndingBalance,
        IsWantToGetReturnAmountInEndingBalance,
        IsWantToGetReturnAmountInEndingBalanceInWords,
        VatResponsiblePersonName,
        VatResponsiblePersonDesignation,
        DateOfSignatureInSubmission,
        VatResponsiblePersonMobileNo,
        VatResponsiblePersonEmail
    )
    SELECT 0,                                   -- MushakReturnId - int
           org.OrganizationId,                  -- OrganizationId - int
           org.Name,                            -- OrganizationName - nvarchar(100)
           org.VATRegNo,                        -- OrganizationVatRegNo - nvarchar(20)
           org.BIN,                             -- OrganizationBin - nvarchar(20)
           org.Address,                         -- OrganizationAddress - nvarchar(200)
           N'',                                 -- OrganizationTypeOfBusiness - nvarchar(50)
           N'',                                 -- OrganizationTypeOfFinancialActivity - nvarchar(50)
           N'',                                 -- TermOfTax - nvarchar(20)
           N'',                                 -- TypeOfSubmission - nvarchar(100)
           0,                                   -- IsOperatedInLastTerm - bit
           N'',                                 -- IsOperatedInLastTermInWords - nvarchar(10)
           GETDATE(),                           -- DateOfSubmission - datetime
           0,                                   -- DirectExportAmount - decimal(18, 2)
           0,                                   -- IndirectExportAmount - decimal(18, 2)
           0,                                   -- VatExemptedProdSellAmount - decimal(18, 2)
           0,                                   -- StandardVatRateProdSellAmount - decimal(18, 2)
           0,                                   -- StandardVatRateProdSellSdAmount - decimal(18, 2)
           0,                                   -- StandardVatRateProdSellVatAmount - decimal(18, 2)
           0,                                   -- MrpProdSellAmount - decimal(18, 2)
           0,                                   -- MrpProdSellSdAmount - decimal(18, 2)
           0,                                   -- MrpProdSellVatAmount - decimal(18, 2)
           0,                                   -- FixedVatProdSellAmount - decimal(18, 2)
           0,                                   -- FixedVatProdSellSdAmount - decimal(18, 2)
           0,                                   -- FixedVatProdSellVatAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatRateProdSellAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatRateProdSellSdAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatRateProdSellVatAmount - decimal(18, 2)
           0,                                   -- TradingSellAmount - decimal(18, 2)
           0,                                   -- TradingSellSdAmount - decimal(18, 2)
           0,                                   -- TradingSellVatAmount - decimal(18, 2)
           0,                                   -- TotalAmount - decimal(18, 2)
           0,                                   -- TotalSdAmount - decimal(18, 2)
           0,                                   -- TotalVatAmount - decimal(18, 2)
           0,                                   -- ZeroVatProdLocalPurchaseAmount - decimal(18, 2)
           0,                                   -- ZeroVatProdImportAmount - decimal(18, 2)
           0,                                   -- VatExemptedProdLocalPurchaseAmount - decimal(18, 2)
           0,                                   -- VatExemptedProdImportAmount - decimal(18, 2)
           0,                                   -- StandardVatProdLocalPurchaseAmount - decimal(18, 2)
           0,                                   -- StandardVatProdLocalPurchaseVatAmount - decimal(18, 2)
           0,                                   -- StandardVatProdImportAmount - decimal(18, 2)
           0,                                   -- StandardVatProdImportVatAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatProdLocalPurchaseAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatProdLocalPurchaseVatAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatProdImportAmount - decimal(18, 2)
           0,                                   -- OtherThanStandardVatProdImportVatAmount - decimal(18, 2)
           0,                                   -- FixedVatProdLocalPurchaseAmount - decimal(18, 2)
           0,                                   -- FixedVatProdLocalPurchaseVatAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdLocalPurchaseFromTurnOverOrgAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdLocalPurchaseFromTurnOverOrgVatAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdLocalPurchaseFromNonRegOrgAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdLocalPurchaseFromNonRegOrgVatAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdVatAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdAmount - decimal(18, 2)
           0,                                   -- NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdVatAmount - decimal(18, 2)
           0,                                   -- TotalRawMaterialRebateAmount - decimal(18, 2)
           0,                                   -- IncrementalAdjustmentAmountForVdsSale - decimal(18, 2)
           0,                                   -- IncrementalAdjustmentAmountForNotPaidInBankingChannel - decimal(18, 2)
           0,                                   -- IncrementalAdjustmentAmountForDebitNote - decimal(18, 2)
           0,                                   -- IncrementalAdjustmentAmountForOtherReason - decimal(18, 2)
           N'',                                 -- IncrementalAdjustmentDescriptionForOtherReason - nvarchar(200)
           0,                                   -- TotalIncrementalAdjustmentAmount - decimal(18, 2)
           0,                                   -- DecrementalAdjustmentAmountForVdsPurchase - decimal(18, 2)
           0,                                   -- DecrementalAdjustmentAmountForAdvanceTaxInImport - decimal(18, 2)
           0,                                   -- DecrementalAdjustmentAmountForRawMaterialPurchaseToProduceExportProd - decimal(18, 2)
           0,                                   -- DecrementalAdjustmentAmountForCreditNote - decimal(18, 2)
           0,                                   -- DecrementalAdjustmentAmountForOtherReason - decimal(18, 2)
           N'',                                 -- DecrementalAdjustmentDescriptionForOtherReason - nvarchar(200)
           0,                                   -- TotalDecrementalAdjustmentAmount - decimal(18, 2)
           0,                                   -- NetTaxTotalPayableVatAmountInCurrentTaxTerm - decimal(18, 2)
           0,                                   -- NetTaxTotalPayableVatAmountAfterAdjustmentWithEndingBalance - decimal(18, 2)
           0,                                   -- NetTaxTotalPayableSdAmountInCurrentTaxTerm - decimal(18, 2)
           0,                                   -- NetTaxTotalPayableSdAmountAfterAdjustmentWithEndingBalance - decimal(18, 2)
           0,                                   -- NetTaxVatAmountForDebitNote - decimal(18, 2)
           0,                                   -- NetTaxVatAmountForCreditNote - decimal(18, 2)
           0,                                   -- NetTaxPaidVatAmountForRawMaterialPurchaseToProduceExportProd - decimal(18, 2)
           0,                                   -- NetTaxInterstAmountForDeuVat - decimal(18, 2)
           0,                                   -- NetTaxInterstAmountForDeuSd - decimal(18, 2)
           0,                                   -- NetTaxFineAndPenaltyAmount - decimal(18, 2)
           0,                                   -- NetTaxExciseDuty - decimal(18, 2)
           0,                                   -- NetTaxDevelopmentSurcharge - decimal(18, 2)
           0,                                   -- NetTaxInformationTechnologyDevelopmentSurcharge - decimal(18, 2)
           0,                                   -- NetTaxHealthProtectionSurcharge - decimal(18, 2)
           0,                                   -- NetTaxEnvironmentProtectionSurcharge - decimal(18, 2)
           0,                                   -- NetTaxVatEndingBalanceOfLastTerm - decimal(18, 2)
           0,                                   -- NetTaxSdEndingBalanceOfLastTerm - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForCurrentTermVat - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForCurrentTermSd - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForInterestOfDueVat - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForInterestOfDueSd - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForFineAndPenalty - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForExciseDuty - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForDevelopmentSurcharge - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForInformationTechnologyDevelopmentSurcharge - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForHealthProtectionSurcharge - decimal(18, 2)
           0,                                   -- TreasurySubmissionAmountForEnvironmentProtectionSurcharge - decimal(18, 2)
           0,                                   -- VatEndingBalance - decimal(18, 2)
           0,                                   -- SdEndingBalance - decimal(18, 2)
           0,                                   -- IsWantToGetReturnAmountInEndingBalance - bit
           N'',                                 -- IsWantToGetReturnAmountInEndingBalanceInWords - nvarchar(10)
           org.VatResponsiblePersonName,        -- VatResponsiblePersonName - nvarchar(100)
           org.VatResponsiblePersonDesignation, -- VatResponsiblePersonDesignation - nvarchar(100)
           GETDATE(),                           -- DateOfSignatureInSubmission - datetime
           N'',                                 -- VatResponsiblePersonMobileNo - nvarchar(20)
           N''                                  -- VatResponsiblePersonEmail - nvarchar(100)

    FROM dbo.Organizations org
    WHERE org.OrganizationId = @OrganizationId;

    --Return Summerized Information
    SELECT mr.MushakReturnId,
           mr.OrganizationId,
           mr.OrganizationName,
           mr.OrganizationVatRegNo,
           mr.OrganizationBin,
           mr.OrganizationAddress,
           mr.OrganizationTypeOfBusiness,
           mr.OrganizationTypeOfFinancialActivity,
           mr.TermOfTax,
           mr.TypeOfSubmission,
           mr.IsOperatedInLastTerm,
           mr.IsOperatedInLastTermInWords,
           mr.DateOfSubmission,
           mr.DirectExportAmount,
           mr.IndirectExportAmount,
           mr.VatExemptedProdSellAmount,
           mr.StandardVatRateProdSellAmount,
           mr.StandardVatRateProdSellSdAmount,
           mr.StandardVatRateProdSellVatAmount,
           mr.MrpProdSellAmount,
           mr.MrpProdSellSdAmount,
           mr.MrpProdSellVatAmount,
           mr.FixedVatProdSellAmount,
           mr.FixedVatProdSellSdAmount,
           mr.FixedVatProdSellVatAmount,
           mr.OtherThanStandardVatRateProdSellAmount,
           mr.OtherThanStandardVatRateProdSellSdAmount,
           mr.OtherThanStandardVatRateProdSellVatAmount,
           mr.TradingSellAmount,
           mr.TradingSellSdAmount,
           mr.TradingSellVatAmount,
           mr.TotalAmount,
           mr.TotalSdAmount,
           mr.TotalVatAmount,
           mr.ZeroVatProdLocalPurchaseAmount,
           mr.ZeroVatProdImportAmount,
           mr.VatExemptedProdLocalPurchaseAmount,
           mr.VatExemptedProdImportAmount,
           mr.StandardVatProdLocalPurchaseAmount,
           mr.StandardVatProdLocalPurchaseVatAmount,
           mr.StandardVatProdImportAmount,
           mr.StandardVatProdImportVatAmount,
           mr.OtherThanStandardVatProdLocalPurchaseAmount,
           mr.OtherThanStandardVatProdLocalPurchaseVatAmount,
           mr.OtherThanStandardVatProdImportAmount,
           mr.OtherThanStandardVatProdImportVatAmount,
           mr.FixedVatProdLocalPurchaseAmount,
           mr.FixedVatProdLocalPurchaseVatAmount,
           mr.NonRebatableProdLocalPurchaseFromTurnOverOrgAmount,
           mr.NonRebatableProdLocalPurchaseFromTurnOverOrgVatAmount,
           mr.NonRebatableProdLocalPurchaseFromNonRegOrgAmount,
           mr.NonRebatableProdLocalPurchaseFromNonRegOrgVatAmount,
           mr.NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdAmount,
           mr.NonRebatableProdLocalPurchaseByOrgWhoSellOtherThanStandardVatProdVatAmount,
           mr.NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdAmount,
           mr.NonRebatableProdImportByOrgWhoSellOtherThanStandardVatProdVatAmount,
           mr.TotalRawMaterialRebateAmount,
           mr.IncrementalAdjustmentAmountForVdsSale,
           mr.IncrementalAdjustmentAmountForNotPaidInBankingChannel,
           mr.IncrementalAdjustmentAmountForDebitNote,
           mr.IncrementalAdjustmentAmountForOtherReason,
           mr.IncrementalAdjustmentDescriptionForOtherReason,
           mr.TotalIncrementalAdjustmentAmount,
           mr.DecrementalAdjustmentAmountForVdsPurchase,
           mr.DecrementalAdjustmentAmountForAdvanceTaxInImport,
           mr.DecrementalAdjustmentAmountForRawMaterialPurchaseToProduceExportProd,
           mr.DecrementalAdjustmentAmountForCreditNote,
           mr.DecrementalAdjustmentAmountForOtherReason,
           mr.DecrementalAdjustmentDescriptionForOtherReason,
           mr.TotalDecrementalAdjustmentAmount,
           mr.NetTaxTotalPayableVatAmountInCurrentTaxTerm,
           mr.NetTaxTotalPayableVatAmountAfterAdjustmentWithEndingBalance,
           mr.NetTaxTotalPayableSdAmountInCurrentTaxTerm,
           mr.NetTaxTotalPayableSdAmountAfterAdjustmentWithEndingBalance,
           mr.NetTaxVatAmountForDebitNote,
           mr.NetTaxVatAmountForCreditNote,
           mr.NetTaxPaidVatAmountForRawMaterialPurchaseToProduceExportProd,
           mr.NetTaxInterstAmountForDeuVat,
           mr.NetTaxInterstAmountForDeuSd,
           mr.NetTaxFineAndPenaltyAmount,
           mr.NetTaxExciseDuty,
           mr.NetTaxDevelopmentSurcharge,
           mr.NetTaxInformationTechnologyDevelopmentSurcharge,
           mr.NetTaxHealthProtectionSurcharge,
           mr.NetTaxEnvironmentProtectionSurcharge,
           mr.NetTaxVatEndingBalanceOfLastTerm,
           mr.NetTaxSdEndingBalanceOfLastTerm,
           mr.TreasurySubmissionAmountForCurrentTermVat,
           mr.TreasurySubmissionAmountForCurrentTermSd,
           mr.TreasurySubmissionAmountForInterestOfDueVat,
           mr.TreasurySubmissionAmountForInterestOfDueSd,
           mr.TreasurySubmissionAmountForFineAndPenalty,
           mr.TreasurySubmissionAmountForExciseDuty,
           mr.TreasurySubmissionAmountForDevelopmentSurcharge,
           mr.TreasurySubmissionAmountForInformationTechnologyDevelopmentSurcharge,
           mr.TreasurySubmissionAmountForHealthProtectionSurcharge,
           mr.TreasurySubmissionAmountForEnvironmentProtectionSurcharge,
           mr.VatEndingBalance,
           mr.SdEndingBalance,
           mr.IsWantToGetReturnAmountInEndingBalance,
           mr.IsWantToGetReturnAmountInEndingBalanceInWords,
           mr.VatResponsiblePersonName,
           mr.VatResponsiblePersonDesignation,
           mr.DateOfSignatureInSubmission,
           mr.VatResponsiblePersonMobileNo,
           mr.VatResponsiblePersonEmail
    FROM @MushakReturn mr;

END;
