using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BlobSasTokenResponse
    {
        public string SasToken { get; set; }
        public string BlobUrl { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
