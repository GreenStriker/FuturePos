using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
   public class vmUsers
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
