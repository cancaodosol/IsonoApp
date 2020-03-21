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
        [Display(Name ="Status")]
        public string IntroduceStatus { get; set; }
        public string PlayDesignUrl { get; set; }
        public string Context { get; set; }
        public int CreateUserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public int LastUpdateUserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastUpdateDate { get; set; }

        public Playbook ToModel(IList<Category> categories) 
        {
            Playbook playbook = new Playbook(PlaybookSystemId);

            playbook.ChangePlaybookId(PlaybookId);
            var category = categories.FirstOrDefault(m => m.Code.Equals(Category));
            playbook.ChangeCategory(category != null ? category : new Models.Category("0000"));
            playbook.ChangePlayName(new PlayName(PlayFullName,PlayShortName,PlayCallName));
            playbook.ChangeContext(new Context(Context));
            playbook.ChangeInstallStatus(IntroduceStatus);
            playbook.ChangePlayDesign(new PlayDesign(PlayDesignUrl));
            playbook.ChangeCreateUser(new User(CreateUserId,""), CreateDate);
            playbook.ChangeLastUpdateUser(new User(CreateUserId,""),LastUpdateDate);

            return playbook;
        }
    }
}
