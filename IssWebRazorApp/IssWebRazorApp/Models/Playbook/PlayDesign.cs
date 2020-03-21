using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class PlayDesign
    {
        public string Url { get; set; }
        public string FileName { get; set; }

        public IFormFile File { get; set; }

        public PlayDesign(IFormFile file, PlayName playName)
        {
            if (file == null) return;

            var fileExtension = Path.GetExtension(file.FileName).ToLower();


            if (CanUseFileExtension(fileExtension) == false)
            {
                File = null;
                return;
            }

            FileName = CreateFileName(playName, fileExtension);
            File = file;
        }
        /// <summary>
        /// プレイブックデザイン画像に使用可能なファイル拡張子か判別する。
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private bool CanUseFileExtension(string fileExtension)
        {
            var canUseFileExtension = new string[] { ".jpeg", ".jpg", ".png" };
            var result = false;

            foreach (var item in canUseFileExtension)
            {
                if (fileExtension.Equals(item))
                {
                    result = true;
                    break;
                }
            }
            if (result == false) 
            {
                string message = "ファイル形式が正しくありません。保存できるファイル形式は、「";
                foreach (var item in canUseFileExtension) 
                {
                    message += " " + item + " ";
                }
                message += "」です。";
                throw new Exception(message);
            }

            return result;
        }

        /// <summary>
        /// プレイブックデザイン画像の保存ファイル名を作成する。
        /// </summary>
        /// <param name="playName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private string CreateFileName(PlayName playName, string fileExtension)
        {
            var cantUseChars = Path.GetInvalidFileNameChars();
            Array.Resize(ref cantUseChars, cantUseChars.Length + 1);
            cantUseChars[cantUseChars.Length - 1] = ' ';
            var playNameforFileName = string.Concat(playName.ShortName.Select(c => cantUseChars.Contains(c) ? '_' : c));
            var sysDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            return sysDate + "_" + playNameforFileName + fileExtension;
        }

        public PlayDesign(string url)
        {
            Url = url;
        }
    }
}
