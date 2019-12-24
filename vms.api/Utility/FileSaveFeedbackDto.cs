using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vms.api.Utility
{
    public class FileSaveFeedbackDto
    {
        public int DocumentTypeId { get; set; }
        public string FileUrl { get; set; }
        public string MimeType { get; set; }
    }
}
