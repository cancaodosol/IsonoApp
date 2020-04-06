using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Schedule
{
    public class Place
    {
        public int PlaceId { get; private set; }
        public string Name { get; private set; }

        public Place(string name) 
        {
            Name = name;
        }
    }
}
