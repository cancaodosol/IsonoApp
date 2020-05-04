using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using IssWebRazorApp.Models;

namespace IssWebRazorApp.Data
{
    [Serializable]
    [Table("Positions")]
    public class PositionData
    {
        [Key]
        public int PositionId { get; set; }
        public string PositionType { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public Position ToModel() 
        {
            return new Position(this);
        }
    }
}
