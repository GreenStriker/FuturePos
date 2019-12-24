using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using vms.entity.StoredProcedureModel;

namespace vms.entity.models
{
    public partial class VmsContext
    {
        [NotMapped]
        public DbSet<SpGetProductAutocompleteForPurchase> SpGetProductAutocompleteForPurchases { get; set; }

        [NotMapped]
        public DbSet<SpGetProductAutocompleteForSale> SpGetProductAutocompleteForSales { get; set; }
        [NotMapped]
        public DbSet<SpGetProductAutocompleteForProductionReceive> SpGetProductAutocompleteForProductionReceive { get; set; }

        [NotMapped]
        public DbSet<SpDamageInvoiceList> SpDamageInvoiceList { get; set; }
        [NotMapped]
        public DbSet<SpGetProductAutocompleteForBom> SpGetProductAutocompleteForBom { get; set; }

        [NotMapped]
        public DbSet<SpDamage> SpDamage { get; set; }
        [NotMapped]
        public DbSet<spGet6P3View> spGet6P3View { get; set; }
        [NotMapped]
        public DbSet<spGetSalePaged> spGetSalePaged { get; set; }

    }
}