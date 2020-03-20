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
        public async void Add(Playbook playbook,string bucketPath) 
        {
            var data = playbook.ToData();

            try
            {
                if (playbook.PlayDesign.File.Length > 0)
                {
                    UploadFileToS3Bucket(playbook.PlayDesign.File, bucketPath);
                }
                _context.PlaybookData.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private const string keyName = "";
        private const string filePath = null;
        private static readonly string s3Directory = "https://iss-web-app-storage.s3-ap-northeast-1.amazonaws.com/";
        private static readonly string bucketName = "iss-web-app-storage";
        private static readonly RegionEndpoint bucketRegin = RegionEndpoint.APNortheast1;
        private static readonly string accesskey = "AKIAQI24TSMT2CMJFLVM";
        private static readonly string secretkey = "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";


        private void UploadFileToS3Bucket(IFormFile file,string buckectPath) 
        {
            var s3Cliant = new AmazonS3Client(accesskey,secretkey,bucketRegin);
            var fileTransferUtility = new TransferUtility(s3Cliant);
            try
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, "Upload", file.Name);
                    using (var fileStream = new FileStream(filePath,FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);
                    }

                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName +"/" + buckectPath,
                        FilePath = filePath,
                        StorageClass = S3StorageClass.Standard,
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
