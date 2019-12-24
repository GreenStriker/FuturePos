using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;

namespace vms.entity.viewModels
{
    public class VmPurchase:Purchase
    {
        public List<PurchaseDetail> PurchaseOrderDetailList { get; set; }
        public List<Content> ContentInfoJson { get; set; }
        public List<PurchasePayment> PurchasePaymenJson { get; set; }
        public string FileName { get; set; }
       
    }
}