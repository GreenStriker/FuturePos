using System;
using System.Collections.Generic;
using System.Text;
namespace vms.entity.viewModels.ReportsViewModel
{
    public class VmSalesCalcBook : ReoportOption
    {

        public int organizationId { get; set; }
        public int productID { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}