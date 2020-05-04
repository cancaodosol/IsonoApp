using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IssWebRazorApp.Models.Exceptions;

namespace IssWebRazorApp.Models.Common
{
    public class UploadFile
    {
        public string Url { get; set; }
        public string FileName { get; set; }

        public IFormFile File { get; set; }

        private static readonly string[] CanUseFileExtensions = new string[] { ".jpeg", ".jpg", ".png" };

    public UploadFile(IFormFile file)
        {
            ChangeFile(file);
        }
        public UploadFile(string url)
        {
            Url = url;
        }

        public void ChangeFile(IFormFile file)
        {
            if (file == null) return;

            var fileExtension = Path.GetExtension(file.FileName).ToLower();


            if (!CanUseFileExtension(fileExtension))
            {
                string message = "ファイル形式が正しくありません。保存できるファイル形式は、「";
                foreach (var item in CanUseFileExtensions)
                {
                    message += " " + item + " ";
                }
                message += "」です。";
                throw new ISSModelException(nameof(UploadFile), nameof(File), "UFULFL001", message);
            }

            FileName = CreateAndGetFileName(fileExtension);
            File = file;
        }

        /// <summary>
        /// アップロード画像に使用可能なファイル拡張子か判別する。
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private bool CanUseFileExtension(string fileExtension)
        {
            var result = false;

            foreach (var item in CanUseFileExtensions)
            {
                if (fileExtension.Equals(item))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// アップロード画像の保存ファイル名を作成する。
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private string CreateAndGetFileName(string fileExtension)
        {
            var cantUseChars = Path.GetInvalidFileNameChars();
            Array.Resize(ref cantUseChars, cantUseChars.Length + 1);
            cantUseChars[cantUseChars.Length - 1] = ' ';
            var playNameforFileName = "";
            var sysDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            return sysDate + "_" + playNameforFileName + fileExtension;
        }

    }
}
