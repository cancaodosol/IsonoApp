using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models.Exceptions;
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
        public PlaybookData Find(int id)
        {
            PlaybookData data = _context.PlaybookData.Include(_ => _.CreateUserData).Include(__ => __.LastUpdateUserData).AsNoTracking().FirstOrDefault(m => m.PlaybookSystemId == id);
            data.CategoryData = _context.CategoryData.FirstOrDefault(_ => _.Code == data.Category);
            return data;
        }

        public IList<PlaybookData> FindAll() 
        {
            //Userが未登録の時、そのレコードは除去される。inner joinと同じっぽい。
            IList<PlaybookData> datas = _context.PlaybookData.Include(_ => _.CreateUserData).Include(__ => __.LastUpdateUserData).AsNoTracking().ToList<PlaybookData>();
            IList<CategoryData> categories = _context.CategoryData.ToList<CategoryData>(); ;

            foreach (var data in datas) 
            {
                data.CategoryData = categories.FirstOrDefault(_ => _.Code == data.Category);
            }

            return datas;
        }

        /// <summary>
        /// プレイブックの新規登録
        /// </summary>
        /// <remarks>プレイデザイン画像があった場合、AWS S3に保存される。</remarks>
        /// <param name="playbook"></param>
        /// <param name="bucketPath"></param>
        public async Task Add(PlaybookData data)
        {
            _context.PlaybookData.Add(data);
            await _context.SaveChangesAsync();
        }

        private const string keyName = "";
        private const string filePath = null;
        private readonly string s3Directory;
        private readonly string bucketName;
        private static readonly RegionEndpoint bucketRegin = RegionEndpoint.APNortheast1;
        private readonly string accesskey;
        private readonly string secretkey;

        public void UploadFileToS3Bucket(PlayDesign playDesign, string bucketPath , out string uploadFilePath)
        {
            uploadFilePath = "";
            var file = playDesign.File;
            try
            {
                var s3Cliant = new AmazonS3Client(accesskey, secretkey, bucketRegin);
                var fileTransferUtility = new TransferUtility(s3Cliant);
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, playDesign.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName + "/" + bucketPath,
                        FilePath = filePath,
                        StorageClass = S3StorageClass.Standard,
                        PartSize = 6291456,// 6 MB
                        CannedACL = S3CannedACL.PublicRead
                    };
                    fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                    fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                    fileTransferUtility.Upload(fileTransferUtilityRequest);
                    fileTransferUtility.Dispose();
                    File.Delete(filePath);
                    uploadFilePath = s3Directory + bucketPath;
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw new ISSRepositoryException("AmazonS3への画像アップロードに失敗しました。",amazonS3Exception);
            }
        }

        public async Task Edit(PlaybookData data)
        {
            _context.Attach(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ISSRepositoryException("Playbookのデータベースへの更新処理でエラーが発生しました。", ex);
            }
        }
        public List<CategoryData> GetCategoryDataList(string session) 
        {
            var datas = _context.CategoryData.Where(_ => _.Session.Equals(session)).ToList<CategoryData>();
            return datas;
        }
    }
}
