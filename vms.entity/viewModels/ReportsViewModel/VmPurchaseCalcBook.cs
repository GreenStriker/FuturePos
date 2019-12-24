using System;
using System.Collections.Generic;
using System.Text;
namespace vms.entity.viewModels.ReportsViewModel
{
    public class VmPurchaseCalcBook : ReoportOption
    {

        public int vendorId { get; set; }
        public int ogrId { get; set; }
        public int productId { get; set; }
        public int? PriceDeclarId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}