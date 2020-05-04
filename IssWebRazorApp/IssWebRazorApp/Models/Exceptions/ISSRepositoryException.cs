using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Exceptions
{
    public class ISSRepositoryException : Exception
    {
        public ISSRepositoryException(string? message) 
            :base(message)
        {
        }
        public ISSRepositoryException(string? message,Exception innerException) 
            :base(message,innerException)
        {
        }

    }
}
