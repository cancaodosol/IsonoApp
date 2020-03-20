using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    [Table("Categories")]
    public class CategoryData
    {
        [Key]
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Session { get; set; }
        public string Name { get; set; }
    }
}
