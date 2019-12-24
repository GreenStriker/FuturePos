using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vms.entity.models;

namespace Inventory.Utility
{
    public static class UserRole
    {
        public static bool Check(string featureName, IList<Right> roleData)
        {




            foreach (var item in roleData)
            {
                if (item.RightName.Equals(featureName))
                {
                    return true;
                }
            }

            return false;
        }


    }
}