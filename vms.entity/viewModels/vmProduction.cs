using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;

namespace vms.entity.viewModels
{
    public class vmProduction
    {
        public int ProductionId { get; set; }
        public int? WorkOrderId { get; set; }
        public int? OrganizationId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       // public List<ProductionDetail> ProductionOrderDetailList { get; set; }
    }
}
