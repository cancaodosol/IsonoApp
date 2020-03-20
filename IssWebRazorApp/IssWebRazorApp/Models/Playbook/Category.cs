using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class Category
    { 
        public string Code { get; set; }
        public string Session { get; set; }
        public string Name { get; set; }

        public Category(string code)
        {
            Code = code;
            Session = "";
            Name = "";
        }
        public Category(string code,string session,string name) 
        {
            Code = code;
            Session = session;
            Name = name;
        }
    }
}
