using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;

namespace vms.entity.viewModels
{
   public class vmDebitNote
    {
        public int DebitNoteId { get; set; }
        public int PurchaseId { get; set; }
        public string ReasonOfReturn { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
       // public List<DebitNoteDetail> DebitNoteDetails { get; set; }
    }
}
