using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
   public class SelectListItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Present { get; set; }
        public decimal? Amount { get; set; }
    }
}
