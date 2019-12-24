using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vms.entity.models;

namespace vms.api.Utility
{
    public class vmPurchase
    {
        public List<PurchaseDetail> PurchaseOrderDetailList { get; set; }
        public List<Content> ContentInfoJsonTest { get; set; }
        public List<ContentInfo> ContentInfoJson { get; set; }
        public List<PurchasePayment> PurchasePaymenJson { get; set; }
        public int PurchaseId { get; set; }
        public int OrganizationId { get; set; }
        public string VatChallanNo { get; set; }
        public string VatChallanIssueDate { get; set; }
        public int VendorId { get; set; }
        public string VendorInvoiceNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PurchaseTypeId { get; set; }
        public int PurchaseReasonId { get; set; }
        public int NoOfIteams { get; set; }
        public decimal TotalPriceWithoutVat { get; set; }
        public decimal DiscountOnTotalPrice { get; set; }
        public decimal TotalDiscountOnIndividualProduct { get; set; }
        public decimal TotalImportDuty { get; set; }
        public decimal TotalRegulatoryDuty { get; set; }
        public decimal TotalSupplementaryDuty { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAdvanceTax { get; set; }
        public decimal TotalAdvanceIncomeTax { get; set; }
        public bool IsVatDeductedInSource { get; set; }
        public decimal? PayableAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string LcNo { get; set; }
        public DateTime? LcDate { get; set; }
        public string BillOfEntry { get; set; }
        public DateTime? BillOfEntryDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string TermsOfLc { get; set; }
        public string PoNumber { get; set; }
        public int? MushakGenerationId { get; set; }
        public bool IsComplete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int Flag { get; set; }
        public IFormFile UploadFile { get; set; }
        public string FileName { get; set; }
    }
}
