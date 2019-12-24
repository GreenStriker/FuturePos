using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.viewModels;

namespace vms.entity.models
{
    public partial class AdvancedSalary : URF.Core.EF.Trackable.Entity
    {
        public IEnumerable<SelectListItems> Employes;


       // public bool? Active { get; set; }
    }
}
