using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Common
{
    public class SessionService
    {
        public Object? Get(HttpContext context, string key)
        {
            Object obj;
            try
            {
                obj = ToObject(context.Session.Get(key));
            }
            catch (NullReferenceException)
            {
                obj = null;
            }
            return obj;
        }
        public void Set(HttpContext context, string key, Object value) 
        {
            context.Session.Set(key, ToBytes(value));
            
        }
        public Object ToObject(Byte[] bytes) 
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(bytes,0,bytes.Length);
            ms.Seek(0,SeekOrigin.Begin);
            return (Object)bf.Deserialize(ms);
        }

        public Byte[] ToBytes(Object obj) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms,obj);
            return ms.ToArray();
        }

        public User GetLoginUser(HttpContext context)
        {
            var data = (UserData)this.Get(context, "LoginUser");
            if (data == null) return null;
            return data.ToModel();
        }
    }
}
