using IssWebRazorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    [Table("Playbooks")]
    public class PlaybookData
    {
        [Key]
        public int PlaybookSystemId { get; set; }
        [Display(Name = "PbId")]
        public int PlaybookId { get; set; }
        public int OffenseFormationId { get; set; }
        public int DefenceFormationId { get; set; }
        public string PlayFullName { get; set; }
        public string PlayShortName { get; set; }
        public string PlayCallName { get; set; }
        public string Category { get; set; }
        [ForeignKey("Category")]
        public CategoryData CategoryData { get; set; }
        [Display(Name ="Status")]
        public string IntroduceStatus { get; set; }
        public string PlayDesignUrl { get; set; }
        public string Context { get; set; }
        public int CreateUserId { get; set; }
        [ForeignKey("CreateUserId")]
        public UserData CreateUserData { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public int LastUpdateUserId { get; set; }
        [ForeignKey("LastUpdateUserId")]
        public UserData LastUpdateUserData { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastUpdateDate { get; set; }

        public Playbook ToModel() 
        {
            Playbook playbook = new Playbook(PlaybookSystemId);

            playbook.ChangePlaybookId(PlaybookId);
            playbook.ChangeCategory(CategoryData != null ? CategoryData.ToModel() : new Models.Category("0000"));
            playbook.ChangePlayName(new PlayName(PlayFullName,PlayShortName,PlayCallName));
            playbook.ChangeContext(new Context(Context));
            playbook.ChangeInstallStatus(IntroduceStatus);
            playbook.ChangePlayDesign(new PlayDesign(PlayDesignUrl));
            playbook.ChangeCreateUser(CreateUserData != null ? CreateUserData.ToModel() : new User(0,0), CreateDate);
            playbook.ChangeLastUpdateUser(LastUpdateUserData != null ? LastUpdateUserData.ToModel() : new User(0, 0), LastUpdateDate);

            return playbook;
        }
    }
}
