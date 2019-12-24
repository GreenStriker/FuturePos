using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
     public  class vmRestore
     {
        public string TableName { get; set; }
        public string PrimaryKeyName { get; set; }
        public string PrimaryKey { get; set; }
        public string AuditLogId { get; set; }
    }
}
