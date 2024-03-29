﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace vms.entity.models
{
    public partial class Sale
    {
        public Sale()
        {
            Incentives = new HashSet<Incentive>();
            RewardPoints = new HashSet<RewardPoint>();
            SaleContents = new HashSet<SaleContent>();
            SalePayments = new HashSet<SalePayment>();
            SalesDetails = new HashSet<SalesDetail>();
        }

        public int SalesId { get; set; }
        public string SaleInvoiceNo { get; set; }
        public string VoucherNo { get; set; }
        public decimal? NoOfIteams { get; set; }
        public int? CustomerId { get; set; }
        public decimal? TotalVat { get; set; }
        public int? BranchId { get; set; }
        public decimal? TotalPriceWithoutVat { get; set; }
        public decimal? PaymentReceiveAmount { get; set; }
        public decimal? ReceivableAmount { get; set; }
        public decimal? TotalDiscountOnIndividualProduct { get; set; }
        public decimal? DiscountOnTotalPrice { get; set; }
        public decimal? PaymentDueAmount { get; set; }
        public int? SoldBy { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Incentive> Incentives { get; set; }
        public virtual ICollection<RewardPoint> RewardPoints { get; set; }
        public virtual ICollection<SaleContent> SaleContents { get; set; }
        public virtual ICollection<SalePayment> SalePayments { get; set; }
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
    }
}