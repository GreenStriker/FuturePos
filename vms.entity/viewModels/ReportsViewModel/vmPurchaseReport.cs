using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels.ReportsViewModel
{
   public class VmPurchaseReport : ReoportOption
    {

        public int PurchaseId { get; set; }
        public string InvoiceNo { get; set; }
        public int vendorId { get; set; }
        public int reason { get; set; }
        public int organizationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}