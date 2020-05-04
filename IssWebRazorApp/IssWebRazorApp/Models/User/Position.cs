using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    [Serializable]
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionType { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public Position(int id,string type,string fullName,string shortName) 
        {
            PositionId = id;
            PositionType = type;
            FullName = fullName;
            ShortName = shortName;
        }
        public Position(PositionData data)
        {
            PositionId = data.PositionId;
            PositionType = data.PositionType;
            FullName = data.FullName;
            ShortName = data.ShortName;
        }

        public PositionData ToData()
        {
            var data = new PositionData();
            data.PositionId = PositionId;
            data.PositionType = PositionType;
            data.FullName = FullName;
            data.ShortName = ShortName;
            return data;
        }
    }
}
