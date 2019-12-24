using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.StoredProcedureModel
{
   public class SpAddMushakReturnPaymentInfo
    {
        public int MushakGenerationId { get; set; }
        public string VatPaymentChallanNo { get; set; }
        public string SuppDutyChallanNo { get; set; }
        public string InterestForDueVatChallanNo { get; set; }
        public string InterestForDueSuppDutyChallanNo { get; set; }
        public string FinancialPenaltyChallanNo { get; set; }
        public string ExciseDutyChallanNo { get; set; }
        public string DevelopmentSurchargeChallanNo { get; set; }
        public string ItDevelopmentSurchargeChallanNo { get; set; }
        public string HealthDevelopmentSurchargeChallanNo { get; set; }
        public string EnvironmentProtectSurchargeChallanNo { get; set; }
    }
}
