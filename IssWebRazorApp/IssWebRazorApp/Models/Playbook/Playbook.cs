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
    public class Playbook
    {
        public int PlaybookSystemId { get; private set; }
        public int PlaybookId { get; private set; }
        public PlayName PlayName { get; private set; }
        public Category Category { get; private set; }

        [Display(Name = "Status")]
        public string InstallStatus { get; private set; }
        public PlayFormation OffenseFormation { get; private set; }
        public PlayFormation DefenceFormation { get; private set; }
        public PlayDesign PlayDesign { get; private set; }
        public Context Context { get; private set; }
        public User CreateUser { get; private set; }
        public DateTime CreateDate { get; private set; }
        public User LastUpdateUser { get; private set; }
        public DateTime LastUpdateDate { get; private set; }

        public Playbook(int systemId) 
        {
            PlaybookSystemId = systemId;
        }
        //TODO : Updateメソッドをどこにセットしようか悩み中。最終更新日・ユーザーを取得したいがchangeメソッドの中に毎回書くのは嫌だから。
        public Playbook(int id, Category category, PlayName playName, string installStatus, IFormFile file, Context context, User createUser)
        {
            ChangePlaybookId(id);
            ChangeCategory(category);
            ChangePlayName(playName);
            ChangeContext(context);
            ChangeInstallStatus(installStatus);
            ChangePlayDesign(file);
            CreateUser = createUser;
            CreateDate = DateTime.Now;
            LastUpdateUser = createUser;
            LastUpdateDate = DateTime.Now;
        }

        public Playbook(PlaybookData data, IFormFile file, User createUser)
            : this(data.PlaybookId, new Category(data.Category), new PlayName(data.PlayFullName, data.PlayShortName, data.PlayCallName), data.IntroduceStatus, file, new Context(data.Context), createUser)
        {
        }

        public void ChangePlaybookId(int id)
        {
            PlaybookId = id;
        }

        public void ChangeCategory(Category category) 
        {
            Category = category;
        }

        public void ChangePlayName(PlayName playName)
        {
            PlayName = playName;
        }
        public void ChangeInstallStatus(string installStatus)
        {
            InstallStatus = installStatus;
        }
        public void ChangePlayDesign(IFormFile file)
        {
            ChangePlayDesign(new PlayDesign(file, PlayName));
        }
        public void ChangePlayDesign(PlayDesign playDesign)
        {
            PlayDesign = playDesign;
        }

        public void ChangeContext(Context context)
        {
            Context = context;
        }

        public void ChangeCreateUser(User createUser , DateTime createDate) 
        {
            CreateUser = createUser;
            CreateDate = createDate;
        }

        public void ChangeLastUpdateUser(User lastUpdateUser, DateTime lastUpdateDate) 
        {
            LastUpdateUser = lastUpdateUser;
            LastUpdateDate = lastUpdateDate;
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
