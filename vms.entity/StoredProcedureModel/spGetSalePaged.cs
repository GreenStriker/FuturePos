using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace vms.entity.StoredProcedureModel
{
    public class spGetSalePaged
    {
        public long Serial { get; set; }
        [Key]
        public int SalesId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime SalesDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        
    }
}
