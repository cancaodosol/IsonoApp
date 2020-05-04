using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public enum UserType
    {
        Player,
        Staff
    }

    public static class UserTypeService 
    {
        private readonly static string[] TypeName = { "選手", "スタッフ" };
        public static string DisplayName(this UserType type) 
        {
            return TypeName[(int)type];
        }
        public static string GetName(string type) 
        {
            string name;
            try
            {
                name = TypeName[int.Parse(type)];
            }
            catch (Exception) 
            {
                name = "";
            }
            return name;
        }
        public static SelectList GetSelectList()
        {
            var types = GetHushtable();
            return new SelectList(types, "Key", "Value");
        }
        public static Hashtable GetHushtable()
        {
            var types = new Hashtable();
            foreach (UserType type in Enum.GetValues(typeof(UserType)))
            {
                string text = type.DisplayName();
                string value = ((int)type).ToString();
                types.Add(value, text);
            }
            return types;
        }
    }
}
