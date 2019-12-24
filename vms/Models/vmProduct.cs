using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vms.entity.models;

namespace Inventory.Models
{
    public class vmProduct:Product
    {
        public IFormFile UploadFile { get; set; }
        public string FileName { get; set; }
    }
}
