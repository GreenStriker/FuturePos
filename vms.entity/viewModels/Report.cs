using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
    public class Report
    {
        public Report() {
            Columns = new List<Clolumn>();
        }
        public string TableName { get; set; }
        public List<Clolumn> Columns { get; set; }
    }
    public class Clolumn
    {
        public string Name { get; set; }
        public bool CanSearh { get; set; }
        public bool CanDisplay { get; set; }
        public bool IsNavigation { get; set; }
    }
    public class NavigationTable
    {
        public string Tablename { get; set; }
    }
}
