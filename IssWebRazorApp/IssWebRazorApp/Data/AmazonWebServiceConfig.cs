using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    public class AmazonWebServiceConfig
    {
        public string S3Directory { get; set; }
        public string S3BucketName { get; set; }
        public string S3AccessKey { get; set; }
        public string S3SecretKey { get; set; }        
    }
}
