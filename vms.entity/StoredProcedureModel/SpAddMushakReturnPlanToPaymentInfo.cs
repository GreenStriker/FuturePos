using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.StoredProcedureModel
{
    public class SpAddMushakReturnPlanToPaymentInfo
    {
        public int MushakGenerationId { get; set; }
        public decimal? PaidVatAmount { get; set; }
        public int? VatEconomicCodeId { get; set; }
        public DateTime? VatPaymentDate { get; set; }
        public int? VatPaymentBankBranchId { get; set; }
        public decimal? PaidSuppDutyAmount { get; set; }
        public int? SuppDutyEconomicCodeId { get; set; }
        public DateTime? SuppDutyPaymentDate { get; set; }
        public int? SuppDutyBankBranchId { get; set; }
        public decimal? PaidInterestAmountForDueVat { get; set; }
        public int? InterestForDueVatEconomicCodeId { get; set; }
        public DateTime? InterestForDueVatPaymentDate { get; set; }
        public int? InterestForDueVatBankBranchId { get; set; }
        public decimal? PaidInterestAmountForDueSuppDuty { get; set; }
        public int? InterestForDueSuppDutyEconomicCodeId { get; set; }
        public DateTime? InterestForDueSuppDutyPaymentDate { get; set; }
        public int? InterestForDueSuppDutyBankBranchId { get; set; }
        public decimal? PaidFinancialPenalty { get; set; }
        public int? FinancialPenaltyEconomicCodeId { get; set; }
        public DateTime? FinancialPenaltyPaymentDate { get; set; }
        public int? FinancialPenaltyBankBranchId { get; set; }
        public decimal? PaidExciseDuty { get; set; }
        public int? ExciseDutyEconomicCodeId { get; set; }
        public DateTime? ExciseDutyPaymentDate { get; set; }
        public int? ExciseDutyBankBranchId { get; set; }
        public decimal? PaidDevelopmentSurcharge { get; set; }
        public int? DevelopmentSurchargeEconomicCodeId { get; set; }
        public DateTime? DevelopmentSurchargePaymentDate { get; set; }
        public int? DevelopmentSurchargeBankBranchId { get; set; }
        public decimal? PaidItDevelopmentSurcharge { get; set; }
        public int? ItDevelopmentSurchargeEconomicCodeId { get; set; }
        public DateTime? ItDevelopmentSurchargePaymentDate { get; set; }
        public int? ItDevelopmentSurchargeBankBranchId { get; set; }
        public decimal? PaidHealthDevelopmentSurcharge { get; set; }
        public int? HealthDevelopmentSurchargeEconomicCodeId { get; set; }
        public DateTime? HealthDevelopmentSurchargePaymentDate { get; set; }
        public int? HealthDevelopmentSurchargeBankBranchId { get; set; }
        public decimal? PaidEnvironmentProtectSurcharge { get; set; }
        public int? EnvironmentProtectSurchargeEconomicCodeId { get; set; }
        public DateTime? EnvironmentProtectSurchargePaymentDate { get; set; }
        public int? EnvironmentProtectSurchargeBankBranchId { get; set; }
    }
}
