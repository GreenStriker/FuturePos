using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;
namespace vms.entity.StoredProcedureModel
{
    public class SpDamage
    {
        [Key]
        public int DamageId { get; set; }
        public string Org_Name { get; set; }
        public string Pr_Name { get; set; }
        public decimal Qty { get; set; }
        public string D_Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string U_Name { get; set; }
        public DateTime CreatedTime { get; set; }
    
    }
}