using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
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
            ChangePlayDesign(new PlayDesign(file,PlayName));
            CreateUser = createUser;
            CreateDate = DateTime.Now;
            LastUpdateUser = createUser;
            LastUpdateDate = DateTime.Now;
        }

        public Playbook(PlaybookData data, IFormFile file, User createUser)
            : this(data.PlaybookId, new Category(data.Category), new PlayName(data.PlayFullName, data.PlayShortName, data.PlayCallName), data.IntroduceStatus, file, new Context(data.Context), createUser)
        {
        }

        public void ChangePlaybook(PlaybookData data, IFormFile file, User updateUser)
        {
            ChangeInstallStatus(data.IntroduceStatus);
            ChangeCategory(new Category(data.Category));
            ChangePlayName(new PlayName(data.PlayFullName, data.PlayShortName, data.PlayCallName));
            ChangePlayDesign(file);
            ChangeContext(new Context(data.Context));
            ChangeLastUpdateUser(updateUser, DateTime.Now);
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
            PlayDesign.ChangeFile(file, PlayName);
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
        Want_Instoll,
        Not_Adopted
    }

    public static class InstallSatusService{
        private readonly static string[] StatusName = { "練習済み", "練習途中", "採用予定", "採用依頼", " 不採用 " };
        public static string DisplayName(this InstallStatus status)
        {            
            return StatusName[(int)status];
        }

        public static string GetName(string status) 
        {
            string name;
            try
            {
                name = StatusName[int.Parse(status)];
            }
            catch (Exception ex) 
            {
                name = "";
            }
            return name;
        }

        public class EnumerateInstallStatuses
        {
            public IEnumerator<InstallStatus> GetEnumerator() 
            {
                foreach (var status in Enum.GetValues(typeof(InstallStatus))) 
                    yield return (InstallStatus)status;                
            }
        }

        public static EnumerateInstallStatuses Enumerate() 
        {
            return new EnumerateInstallStatuses();
        }

        //TODO 並び順がKEY順にならないので対応が必要。
        public static SelectList GetSelectList() 
        {
            var statuses = new Hashtable();
            foreach (var status in Enumerate()) 
            {
                string text = status.DisplayName();
                string value = ((int)status).ToString();
                statuses.Add(value,text);
            }

            return new SelectList(statuses,"Key","Value");
        }
    }
}
