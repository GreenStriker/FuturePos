using System;
using System.Collections.Generic;
using X.PagedList;
using vms.entity.StoredProcedureModel;

namespace vms.entity.viewModels.ReportsViewModel
{
    public class VmTaxInvoice : ReoportOption
    {
        public string InvoiceNo { get; set; }
    
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<spGet6P3View> ListOfItem { get; set; }
        public IPagedList<spGet6P3View> PagedList { get; set; }
    }
   
}