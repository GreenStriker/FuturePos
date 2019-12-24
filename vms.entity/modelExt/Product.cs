using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.viewModels;

namespace vms.entity.models
{
    public partial class Product : URF.Core.EF.Trackable.Entity
    {
        //[NotMapped]
        ////public string EncryptedId { get; set; }
        ////public IEnumerable<SelectListItems> Roles;
        ////public IEnumerable<SelectListItems> UserTypes;
        //[NotMapped]
        //public string jsonobj { get; set; }
        public IEnumerable<SelectListItems> MeasurementUnits;
        public IEnumerable<SelectListItems> ProductGroups;
        public IEnumerable<SelectListItems> ProductVattypes;
        public IEnumerable<SelectListItems> ProductCategories;

    }
}
