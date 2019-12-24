using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;

namespace vms.entity.viewModels
{
    public class vmCreditNote
    {
        public int CreditNoteId { get; set; }
        public int SalesId { get; set; }
        public string ReasonOfReturn { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }

       // public List<CreditNoteDetail> CreditNoteDetails { get; set; }
    }
}
