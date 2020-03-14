using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelloRazorWorldApp.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        [Display(Name = "名前")]
        [Required]
        public string Name { get ; set; }
        [Display(Name = "メールアドレス")]
        [EmailAddress]
        public string Mail { get; set; }
        [Display(Name = "年齢")]
        [Range(0,200)]
        public int Age { get; set; }


        [Display(Name = "投稿")]
        public ICollection<Message> Messages { get; set; }

    }
}
