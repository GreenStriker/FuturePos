using System;
using System.Collections.Generic;
using System.Text;
using vms.entity.models;

namespace vms.entity.viewModels
{
    public class vmSession
    {
        //public vmSession()
        //{
        //    Rights = new List<Right>();
        //}
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FristThe { get; set; }
        public string secThe { get; set; }
        public string thirdThe { get; set; }
        public string FroreThe { get; set; }
        //public int RoleId { get; set; }
        //public string RoleName { get; set; }
        //public string PreviousData { get; set; }
        //   public List<Right> Rights { get; set; }
    }
    
}
