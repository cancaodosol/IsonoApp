using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class Playbook
    {
        private readonly IWebHostEnvironment _environment;
        public int PlaybookSystemId { get; private set; }
        public int PlaybookId { get; private set; }
        public PlayName PlayName { get; private set; }
        public string Cotegory { get; private set; }
        public string InstollStatus { get; private set; }
        public PlayFormation OffenseFormation { get; private set; }
        public PlayFormation DefenceFormation { get; private set; }
        public PlayDesign PlayDesign { get; private set; }
        public Context Context { get; private set; }
        public User CreateUser { get; private set; }
        public DateTime CreateDate { get; private set; }
        public User LastUpdateUser { get; private set; }
        public DateTime LastUpdateDate { get; private set; }

        public Playbook(IWebHostEnvironment env)
        {
            _environment = env;
        }

        //TODO : Updateメソッドをどこにセットしようか悩み中。最終更新日・ユーザーを取得したいがchangeメソッドの中に毎回書くのは嫌だから。
        public Playbook(int id, PlayName playName, string instollStatus, IFormFile file, Context context, User createUser)
        {
            changePlaybookId(id);
            changePlayName(playName);
            changeContext(context);
            changInstollStatus(instollStatus);
            changePlayDesign(file);
            CreateUser = createUser;
            CreateDate = DateTime.Now;
            LastUpdateUser = createUser;
            LastUpdateDate = DateTime.Now;
        }

        public Playbook(PlaybookData data,IFormFile file,User createUser)
            :this(data.PlaybookId, new PlayName(data.PlayFullName, data.PlayShortName, data.PlayCallName),data.IntroduceStatus ,file, new Context(data.Context), createUser)
        {
        }

        public void changePlaybookId(int id)
        {
            PlaybookId = id;
        }

        public void changePlayName(PlayName playName)
        {
            PlayName = playName;
        }
        public void changInstollStatus(string instollStatus)
        {
            InstollStatus = instollStatus;
        }

        public void changePlayDesign(IFormFile file)
        {
            PlayDesign = new PlayDesign(file,PlayName);
        }

        public void changeContext(Context context)
        {
            Context = context;
        }

        public PlaybookData ToData()
        {
            PlaybookData data = new PlaybookData();

            data.PlaybookSystemId = PlaybookSystemId;
            data.PlaybookId = PlaybookId;
            //data.OffenseFormationId = OffenseFormation.PlayFormationId;
            //data.DefenceFormationId = DefenceFormation.PlayFormationId;
            data.PlayFullName = PlayName.FullName;
            data.PlayShortName = PlayName.ShortName;
            data.PlayCallName = PlayName.PlayCall;
            //data.Category = Cotegory;
            data.IntroduceStatus = InstollStatus;
            data.PlayDesignUrl = PlayDesign.Url;
            data.Context = Context.Text;
            data.CreateUserId = CreateUser.UserId;
            data.CreateDate = CreateDate;
            data.LastUpdateUserId = LastUpdateUser.UserId;
            data.LastUpdateDate = LastUpdateDate;

            return data;
        }
    }

    public class PlayDesign
    {
        public string Url { get; set; }
        public string FileName { get; set; }

        public IFormFile File { get; set; }

        public PlayDesign(IFormFile file, PlayName playName) 
        {
            if (file.Length == 0) return;

            var fileExtension = Path.GetExtension(file.FileName).ToLower();


            if (CanUseFileExtension(fileExtension)) 
            {
                File = null;
                return;
            }

            FileName = CreateFileName(playName,fileExtension);
            File = file;
        }
        /// <summary>
        /// プレイブックデザイン画像に使用可能なファイル拡張子か判別する。
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private bool CanUseFileExtension(string fileExtension)
        {
            var canUseFileExtension = new string[] { ".jpeg", "jpg", "png" };
            var result = false;

            foreach (var item in canUseFileExtension) 
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
        /// プレイブックデザイン画像の保存ファイル名を作成する。
        /// </summary>
        /// <param name="playName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        private string CreateFileName(PlayName playName,string fileExtension) 
        {
            var cantUseChars = Path.GetInvalidFileNameChars();
            Array.Resize(ref cantUseChars,cantUseChars.Length + 1);
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
    public class Context
    {
        public string Text { get; private set; }

        public Context(string text) 
        {
            Text = text;
        }
        public void changeText(string text) 
        {
            Text = text;
        }
    }

    public enum InstollStatus 
    {
        Instolled,
        Instolling ,
        Will_Instoll ,
        Want_Instoll 
    }
}
