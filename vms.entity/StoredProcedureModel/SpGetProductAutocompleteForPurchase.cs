using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace vms.entity.StoredProcedureModel
{
    public class SpGetProductAutocompleteForPurchase
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string ModelNo { get; set; }
        public string Code { get; set; }
        public decimal PurchaseUnitPrice { get; set; }
        public decimal DefaultImportDutyPercent { get; set; }
        public decimal DefaultRegulatoryDutyPercent { get; set; }
        public decimal DefaultSupplimentaryDutyPercent { get; set; }
        public decimal DefaultVatPercent { get; set; }
        public decimal DefaultAdvanceTaxPercent { get; set; }
        public decimal DefaultAdvanceIncomeTaxPercent { get; set; }
        public int MeasurementUnitId { get; set; }
        public string MeasurementUnitName { get; set; }
    }
}