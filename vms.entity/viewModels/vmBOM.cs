using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
   public class vmBOM
    {
        public int BOMId { get; set; }
        public int ProductId { get; set; }
        public  int ProducttionId { get; set; }
        public int PurchaseDetailsId { get; set; }
        public int Quantity { get; set; }
    }
}
