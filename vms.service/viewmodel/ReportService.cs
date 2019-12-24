//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using vms.entity.attr;
//using vms.entity.viewModels;

//namespace vms.service.viewmodel
//{
//    public static class ReportService
//    {
//        public static Report GetReportAttributes<T>() 
//        {
//            object instance = Activator.CreateInstance(typeof(T));
//            var properties = instance.GetType().GetProperties();
//            var report = new Report();
//            var columns = new List<Clolumn>();
//            foreach (var property in properties)
//            {
//                var items = property.GetCustomAttributes(typeof(ReportAttribute), true).FirstOrDefault();
//                if (items != null)
//                {
//                    Clolumn column = new Clolumn();
//                    var attr = (ReportAttribute)items;
//                    column.Name = property.Name;
//                    column.CanDisplay = attr.Display;
//                    column.CanSearh = attr.SearchAble;
//                    column.IsNavigation = attr.NavigationTable;
//                    columns.Add(column);
//                }
//            }
//            report.Columns = columns;
//            return report;
//        }

//    }
//}
