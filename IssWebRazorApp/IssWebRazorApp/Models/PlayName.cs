using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class PlayName
    {
        public string FullName { get; private set; }
        public string ShortName { get; private set; }
        public string PlayCall { get; private set; }

        public PlayName(string FullName) 
        {
            ChangeFullName(FullName);
        }

        public PlayName(string fullName,string shortName) 
        {
            ChangeFullName(fullName);
            ChangeShortName(shortName);
        }
        public PlayName(string fullName, string shortName, string playCall)
        {
            ChangeFullName(fullName);
            ChangeShortName(shortName);
            ChangePlayCall(playCall);
        }

        public void ChangeFullName(string fullName)
        {
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }
            FullName = fullName;
        }

        public void ChangeShortName(string shortName)
        {
            ShortName = String.IsNullOrEmpty(shortName) ? FullName : shortName;
        }

        public void ChangePlayCall(string playCall)
        {
            PlayCall = String.IsNullOrEmpty(playCall) ? ShortName : playCall;
        }
    }
}
