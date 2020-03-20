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
        public Category Category { get; private set; }
        public string InstallStatus { get; private set; }
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
        public Playbook(int id, Category category, PlayName playName, string installStatus, IFormFile file, Context context, User createUser)
        {
            changePlaybookId(id);
            changeCategory(category);
            changePlayName(playName);
            changeContext(context);
            changInstallStatus(installStatus);
            changePlayDesign(file);
            CreateUser = createUser;
            CreateDate = DateTime.Now;
            LastUpdateUser = createUser;
            LastUpdateDate = DateTime.Now;
        }

        public Playbook(PlaybookData data, IFormFile file, User createUser)
            : this(data.PlaybookId, new Category(data.Category), new PlayName(data.PlayFullName, data.PlayShortName, data.PlayCallName), data.IntroduceStatus, file, new Context(data.Context), createUser)
        {
        }

        public void changePlaybookId(int id)
        {
            PlaybookId = id;
        }

        public void changeCategory(Category category) 
        {
            Category = category;
        }

        public void changePlayName(PlayName playName)
        {
            PlayName = playName;
        }
        public void changInstallStatus(string installStatus)
        {
            InstallStatus = installStatus;
        }
        public void changePlayDesign(IFormFile file)
        {
            PlayDesign = new PlayDesign(file, PlayName);
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
            data.Category = Category.Code;
            data.IntroduceStatus = InstallStatus;
            data.PlayDesignUrl = PlayDesign.Url;
            data.Context = Context.Text;
            data.CreateUserId = CreateUser.UserId;
            data.CreateDate = CreateDate;
            data.LastUpdateUserId = LastUpdateUser.UserId;
            data.LastUpdateDate = LastUpdateDate;

            return data;
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

    public enum InstallStatus
    {
        Instolled,
        Instolling,
        Will_Instoll,
        Want_Instoll
    }

    public static class InstallSatusService{
        public static string GetName(string status) {
            int index = int.Parse(status);
            string[] statusName = { "練習済み", "練習途中", "採用予定", "採用依頼" };
            return statusName[index];
        }
    }
}
