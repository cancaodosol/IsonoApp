using IssWebRazorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    [Serializable]
    [Table("Users")]
    public class UserData
    {
        [Key]
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string FirstNameKanji { get; set; }
        public string LastNameKanji { get; set; }
        public string FirstNameRoman { get; set; }
        public string LastNameRoman { get; set; }
        public string LoginPassword { get; set; }
        public int UniformNumber { get; set; }
        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public PositionData PositionData { get; set; }
        public string UserType { get; set; }
        public string SystemRole { get; set; }
        public string Education { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        
        public User ToModel() 
        {
            var name = new UserName(DisplayName,FirstNameKanji,LastNameKanji,FirstNameRoman,LastNameRoman);
            var potision = PositionData != null ? new Position(PositionData) : new Position(PositionId,"","","");
            return new User(UserId,LoginId,name,LoginPassword,UniformNumber,potision,UserType,SystemRole,Education,Height,Weight);
        }
    }
}
