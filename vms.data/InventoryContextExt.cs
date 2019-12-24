using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using vms.entity.StoredProcedureModel;

namespace vms.entity.models
{
    public partial class InventoryContext
    {
        [NotMapped]
        public DbSet<SpGetProductAutocompleteForSale> SpGetProductAutocompleteForSales { get; set; }
    }
}
