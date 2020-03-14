using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloRazorWorldApp
{
    public class OtherModel : PageModel
    {
        public string Message { get; set; } = "sample message";
        public void OnPost()
        {
            Message = "You typed : " + Request.Form["msg"];
        }
    }
}