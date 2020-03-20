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
        public CategoryData CategoryData{ get; set; }
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
    }
}
