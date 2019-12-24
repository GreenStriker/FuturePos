using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;

namespace vms.entity.viewModels
{
   public class VmDuePayment
    {
        public int PurchaseId { get; set; }
        public int SalesId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? PayableAmount { get; set; }
        public decimal? PrevPaidAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public IEnumerable<SelectListItems> PaymentMethods;
    }
}
