using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.StoredProcedureModel
{
    public class SpAddMushakReturnReturnedAmountInfo
    {
        public int MushakGenerationId { get; set; }
        public decimal? ReturnAmountFromClosingVat { get; set; }
        public int? ReturnFromClosingVatChequeBankId { get; set; }
        public string ReturnFromClosingVatChequeNo { get; set; }
        public DateTime? ReturnFromClosingVatChequeDate { get; set; }
        public decimal? ReturnAmountFromClosingSd { get; set; }
        public int? ReturnFromClosingSdChequeBankId { get; set; }
        public string ReturnFromClosingSdChequeNo { get; set; }
        public DateTime? ReturnFromClosingSdChequeDate { get; set; }
    }
}
