using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
   public class vmPurchasePayment
    {
        public int PurchaseId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int CreatedBy { get; set; }
    }
    public class VmSalesPaymentReceive
    {
        public int SalesId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int CreatedBy { get; set; }
    }
}
