using IssWebRazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    public interface IPlaybookRepository
    {
        public PlaybookData Find(int id);
        public IList<PlaybookData> FindAll();
        public Task Add(PlaybookData data);
        public Task Edit(PlaybookData data);
        public void UploadFileToS3Bucket(PlayDesign playDesign, string bucketPath, out string uploadFilePath);
        public List<CategoryData> GetCategoryDataList(string session);
    }
}
