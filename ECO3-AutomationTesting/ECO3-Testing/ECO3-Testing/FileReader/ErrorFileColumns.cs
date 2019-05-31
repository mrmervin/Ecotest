using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO3_Testing.FileReader
{
    public class ErrorFileColumns
    {
        public string Id { get; set; }
        public string DataFileType { get; set; }
        public string UploadedBy { get; set; }
        public string UploadedAt { get; set; }
        public string DocumentId { get; set; }
        public string Version { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public string ValidationErrorCode { get; set; }
        public string ValidationError { get; set; }
        public string LineNo { get; set; }
    }
}
