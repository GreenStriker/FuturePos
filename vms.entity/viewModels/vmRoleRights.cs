using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
   public class vmRoleRights
    {
     
        public int RoleId { get; set; }

        public List<RoleRightModel> RoleFeatures { get; set; }
    }
    public class RoleRightModel
    {
        public int RightId { get; set; }
        public string RightName { get; set; }
        public bool IsAssigned { get; set; }
    }
    public class vmRoles
    {

        public int RoleId { get; set; }

       public int RightId { get; set; }
    }
    //public class vmRoleRights
    //{

    //    public int RoleId { get; set; }
    //    public List<RoleRightModel> RoleFeatures { get; set; }
      
    //}

}
