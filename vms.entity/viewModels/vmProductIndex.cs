using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
    public class vmProductIndex
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? VatId { get; set; }
        public string ModelNo { get; set; }
        public int? MunitId { get; set; }
        public string Code { get; set; }
        public DateTime? EfectiveFrom { get; set; }
        public DateTime? EfectiveTo { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Url { get; set; }
        public string MunitName { get; set; }
        public decimal? VatPercentage { get; set; }
    }
}
