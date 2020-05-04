using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Exceptions
{
    public class ISSServiceException : Exception
    {
        public ISSServiceException(string? message)
            :base(message)
        {
        }
        
        public ISSServiceException(string? message,Exception innerException)
            :base(message,innerException)
        {
        }

    }
}
