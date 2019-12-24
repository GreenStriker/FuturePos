using System.Collections.Generic;
using vms.entity.models;

namespace vms.entity.viewModels
{
    public class VmSalesDetail
    {
        public Sale Sale { get; set; }
        public IEnumerable<SalesDetail> SalesDetails { get; set; }
        public string ReportUrl { get; set; }
    }

    public class VmPurchaseDetail
    {
        public Purchase Purchase { get; set; }
        public IEnumerable<PurchaseDetail> PurchaseDetails { get; set; }
    }
}