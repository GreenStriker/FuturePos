﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace vms.entity.models
{
    public partial class ExpenceType
    {
        public ExpenceType()
        {
            Expences = new HashSet<Expence>();
        }

        public int ExpenceTypeId { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }

        public virtual ICollection<Expence> Expences { get; set; }
    }
}