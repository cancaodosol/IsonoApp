using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class PlayFormation
    {
        public int PlayFormationId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
    }
}
