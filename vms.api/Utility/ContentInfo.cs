using Microsoft.AspNetCore.Http;


namespace vms.api.Utility
{
    public class ContentInfo
    {
        public int DocumentTypeId { get; set; }
        public IFormFile UploadFile { get; set; }
    }
}
