using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace vms.entity.StoredProcedureModel
{
    public class SpDamageInsert
    {
        

        public int OrganizationId { get; set; }
        public int ProductId { get; set; }
        public long StockInId { get; set; }
        public decimal DamageQty { get; set; }
        public int DamageTypeId { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }



    }
}