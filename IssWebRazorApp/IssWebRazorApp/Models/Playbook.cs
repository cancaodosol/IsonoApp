using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class Playbook
    {
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

        public Playbook(){ }

        //TODO : Updateメソッドをどこにセットしようか悩み中。最終更新日・ユーザーを取得したいがchangeメソッドの中に毎回書くのは嫌だから。
        public Playbook(int id, PlayName playName, string instollStatus, PlayDesign playDesign, Context context, User createUser)
        {
            changePlaybookId(id);
            changePlayName(playName);
            changeContext(context);
            changInstollStatus(instollStatus);
            changePlayDesign(playDesign);
            CreateUser = createUser;
            CreateDate = DateTime.Now;
            LastUpdateUser = createUser;
            LastUpdateDate = DateTime.Now;
        }

        public Playbook(PlaybookData data,IFormFile file,User createUser)
            :this(data.PlaybookId, new PlayName(data.PlayFullName, data.PlayShortName, data.PlayCallName),data.IntroduceStatus ,new PlayDesign(file), new Context(data.Context), createUser)
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

        public void changePlayDesign(PlayDesign playDesign)
        {
            PlayDesign = playDesign;
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
        public IFormFile File { get; set; }

        public PlayDesign(IFormFile file) 
        {
            File = file;
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
