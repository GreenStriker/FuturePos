using System.ComponentModel.DataAnnotations;

namespace vms.entity.StoredProcedureModel
{
    public class SpGetProductAutocompleteForSale
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ModelNo { get; set; }
        public string Code { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal VatPercent { get; set; }
        public decimal MaxSaleQty { get; set; }
        public int MUnitId { get; set; }
        public string MeasurementUnitName { get; set; }
    }
}