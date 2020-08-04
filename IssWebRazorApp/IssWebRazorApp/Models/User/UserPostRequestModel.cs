using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class UserPostRequestModel
    {
        public int UserId { get; set; } = 0;
        [Required]
        public string LoginId { get; set; } = "";
        [Required]
        public string DisplayName { get; set; } = "";
        [Required]
        public string FirstNameKanji { get; set; } = "";
        [Required]
        public string LastNameKanji { get; set; } = "";
        [Required]
        public string FirstNameRoman { get; set; } = "";
        [Required]
        public string LastNameRoman { get; set; } = "";
        [Required]
        [DataType("Password")]
        public string LoginPassword { get; set; } = "";
        public int UniformNumber { get; set; } = 0;
        public int PositionId { get; set; } = 0;
        public string UserType { get; set; } = "";
        public string SystemRole { get; set; } = "";
        public string Education { get; set; } = "";
        public double Height { get; set; } = 0.0;
        public double Weight { get; set; } = 0.0;
        [Required]
        [DataType("Password")]
        public string AuthenticationCode { get; set; } = "";
    }
}
