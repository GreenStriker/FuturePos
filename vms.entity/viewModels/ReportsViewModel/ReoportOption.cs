
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace vms.entity.viewModels.ReportsViewModel
{
    public class ReoportOption
    {
        public List<SelectListItem> PreviousYearList { get; set; }
        public string monthPicker { get; set; }
       
        public bool IsReportVisible { get; set; }
        public string ReportUrl { get; set; }
    }
}