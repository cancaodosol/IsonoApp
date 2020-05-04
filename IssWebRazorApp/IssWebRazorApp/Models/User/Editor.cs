using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class Editor
    {
        public User CreateUser;
        public DateTime CreateDate;
        public User LastUpdateUser;
        public DateTime LastUpdateDate;
        public string Display{ get { return LastUpdateUser.UserName.NameRoman +" : "+LastUpdateDate.ToString("yyyy-MM-dd HH:mm") ; } }

        public Editor(User createUser,DateTime createDate) 
        {
            CreateUser = createUser;
            CreateDate = createDate;
        }
        public Editor(User createUser, DateTime createDate, User updateUser, DateTime updateDate)
        {
            CreateUser = createUser;
            CreateDate = createDate;
            LastUpdateUser = updateUser;
            LastUpdateDate = updateDate;
        }
    }
}
