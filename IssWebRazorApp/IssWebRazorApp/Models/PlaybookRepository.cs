using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class PlaybookRepository : IPlaybookRepository
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        private readonly IWebHostEnvironment _environment;

        public PlaybookRepository(IssWebRazorApp.Data.IssWebRazorAppContext context, IWebHostEnvironment env) 
        {
            _context = context;
            _environment = env;
        }
        public async void Add(Playbook playbook) 
        {
            var data = playbook.ToData();
            //UploadFileToS3Bucket(playbook.PlayDesign.File);
            _context.PlaybookData.Add(data);
            await _context.SaveChangesAsync();
        }

        private const string keyName = "";
        private const string falePath = null;
        private static readonly string bucketName = "iss-web-app-storage";
        private static readonly RegionEndpoint bucketRegin = RegionEndpoint.APNortheast1;
        private static readonly string accsskey = "";
        private static readonly string secretkey = "";


        private void UploadFileToS3Bucket(IFormFile file) 
        {
            var s3Cliant = new AmazonS3Client(bucketRegin);
            var fileTransferUtility = new TransferUtility(s3Cliant);
            try
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, "Upload", file.FileName);
                    using (var fileStream = new FileStream(filePath,FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);
                    }

                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        FilePath = filePath,
                        StorageClass = S3StorageClass.StandardInfrequentAccess,
                        PartSize = 6291456,// 6 MB
                        CannedACL = S3CannedACL.PublicRead
                    };
                    fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                    fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                    fileTransferUtility.Upload(fileTransferUtilityRequest);
                    fileTransferUtility.Dispose();
                }
                Console.WriteLine("File Uploaded Successfully!!");
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                Console.WriteLine(amazonS3Exception);
            }
        }
    }
}
