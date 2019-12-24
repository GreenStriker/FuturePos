using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vms.Models
{
    public class ContentInfo
    {
       public int DocumentTypeId { get; set; }
        public IFormFile UploadFile { get; set; }
    }
}
