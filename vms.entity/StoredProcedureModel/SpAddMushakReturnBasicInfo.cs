using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.StoredProcedureModel
{
    public class SpAddMushakReturnBasicInfo
    {
        public int? OrganizationId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public DateTime? GenerateDate { get; set; }
        public decimal? InterestForDueVat { get; set; }
        public decimal? InterestForDueSd { get; set; }
        public decimal? FinancialPenalty { get; set; }
        public decimal? ExciseDuty { get; set; }
        public decimal? DevelopmentSurcharge { get; set; }
        public decimal? ItDevelopmentSurcharge { get; set; }
        public decimal? HealthDevelopmentSurcharge { get; set; }
        public decimal? EnvironmentProtectSurcharge { get; set; }
        public bool? IsWantToGetBackClosingAmount { get; set; }
    }
}
