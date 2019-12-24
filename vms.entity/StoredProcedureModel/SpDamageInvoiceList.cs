using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace vms.entity.StoredProcedureModel
{
    public class SpDamageInvoiceList
    {
        [Key]
        public long StockInId { get; set; }

        public string InvoiceNo { get; set; }
        public string BatchNo { get; set; }
        public decimal CurrentStock { get; set; }
       


    }
}