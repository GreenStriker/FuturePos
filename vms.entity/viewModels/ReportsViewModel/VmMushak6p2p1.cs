using System;
using System.Collections.Generic;
using System.Text;
namespace vms.entity.viewModels.ReportsViewModel
{
    public class VmMushak6p2p1 : ReoportOption
    {

        public int organizationId { get; set; }
        public int vendorId { get; set; }
        public int customerId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}