using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace vms.Models
{
    public class FileSaveDto
    {
        public string FileSaveDirectory { get; set; }
        public IList<IFormFile> FormFiles { get; set; }
    }
}