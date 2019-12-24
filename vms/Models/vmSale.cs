using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vms.entity.models;

namespace vms.Models
{
    public class vmSale:Sale
    {
        public List<SalesDetail> SalesDetailList { get; set; }
        public List<Content> ContentInfoJsonTest { get; set; }
        public List<ContentInfo> ContentInfoJson { get; set; }
        public List<SalePayment> SalesPaymentReceiveJson { get; set; }
        public string CustomerMobile { get; set; }
        public IFormFile UploadFile { get; set; }
        public string FileName { get; set; }

    }
}
