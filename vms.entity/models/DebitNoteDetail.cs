﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace vms.entity.models
{
    public partial class DebitNoteDetail
    {
        public int DebitNoteDetailId { get; set; }
        public int? DebitNoteId { get; set; }
        public int? PurchaseDetailId { get; set; }
        public decimal? ReturnQuantity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }

        public virtual DebitNote DebitNote { get; set; }
        public virtual PurchaseDetail PurchaseDetail { get; set; }
    }
}