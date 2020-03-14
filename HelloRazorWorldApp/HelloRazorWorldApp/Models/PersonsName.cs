using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloRazorWorldApp.Models
{
    public class PersonsName
    {
        public string FullName { get {return LastNameAlphabet + " " + FirstNameAlphabet; } }
        public string LastNameKanji { get; set; }
        public string FirstNameKanji { get; set; }
        public string LastNameAlphabet { get; set; }
        public string FirstNameAlphabet { get; set; }
    }
}
