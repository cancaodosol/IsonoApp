using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly AmazonWebServiceConfig _awsconfig;

        public PlaybookRepository(IssWebRazorApp.Data.IssWebRazorAppContext context, IWebHostEnvironment env, IOptions<AmazonWebServiceConfig> awsconfig)
        {
            _context = context;
            _environment = env;
            _awsconfig = awsconfig.Value;

            s3Directory = _awsconfig.S3Directory;
            bucketName = _awsconfig.S3BucketName;
            accesskey = _awsconfig.S3AccessKey;
            secretkey = _awsconfig.S3SecretKey;
        }
        public Playbook Find(int id)
        {
            PlaybookData data = _context.PlaybookData.AsNoTracking().FirstOrDefault(m => m.PlaybookSystemId == id);
            IList<Category> categories = GetCategoryList("Offense");
            return data.ToModel(categories);
        }

        public IList<Playbook> FindAll() 
        {
            IList<PlaybookData> datas = _context.PlaybookData.OrderBy(m => m.Category).ToList();
            IList<Category> categories = GetCategoryList("Offense");
            IList<Playbook> playbooks = new List<Playbook>();

            foreach (var data in datas) 
            {
                playbooks.Add(data.ToModel(categories));
            }

            return playbooks;
        }

        /// <summary>
        /// プレイブックの新規登録
        /// </summary>
        /// <remarks>プレイデザイン画像があった場合、AWS S3に保存される。</remarks>
        /// <param name="playbook"></param>
        /// <param name="bucketPath"></param>
        public async void Add(Playbook playbook, string bucketPath)
        {
            var data = playbook.ToData();

            try
            {
                if (playbook.PlayDesign.File != null)
                {
                    UploadFileToS3Bucket(playbook.PlayDesign, bucketPath);
                    data.PlayDesignUrl = s3Directory + bucketPath + "/" + playbook.PlayDesign.FileName;
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
        private readonly string s3Directory;
        private readonly string bucketName;
        private static readonly RegionEndpoint bucketRegin = RegionEndpoint.APNortheast1;
        private readonly string accesskey;
        private readonly string secretkey;

        private void UploadFileToS3Bucket(PlayDesign playDesign, string buckectPath)
        {
            var file = playDesign.File;
            var s3Cliant = new AmazonS3Client(accesskey, secretkey, bucketRegin);
            var fileTransferUtility = new TransferUtility(s3Cliant);
            try
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, "Upload", playDesign.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName + "/" + buckectPath,
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

        public async void Edit(Playbook playbook, string bucketPath)
        {
            var data = playbook.ToData();
            _context.Attach(data).State = EntityState.Modified;

            try
            {
                if (playbook.PlayDesign.File != null)
                {
                    UploadFileToS3Bucket(playbook.PlayDesign, bucketPath);
                    data.PlayDesignUrl = s3Directory + bucketPath + "/" + playbook.PlayDesign.FileName;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IList<Category> GetCategoryList(String session){
            IList<CategoryData> data = _context.CategoryData.Where(m => m.Session.Equals(session)).OrderBy(m => m.Code).ToList();
            IList<Category> categories = new List<Category>();

            foreach (var item in data) 
            {
                categories.Add(new Category(item.Code,item.Session,item.Name));
            }

            return categories;
        }
    }
}
