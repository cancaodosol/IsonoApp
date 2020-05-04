using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Exceptions
{
    public class ISSModelException : Exception
    {
        public string ModelName { get; set; }
        public string? ModelFieldName { get; set; }
        public string ModelErrId { get; set; }
        public ISSModelException(string modelName ,string? modelFieldName,string modelErrId,string? message)
            :base(message)
        {
            ModelName = modelName;
            ModelFieldName = modelFieldName;
            ModelErrId = modelErrId;
        }
        public ISSModelException(string? message,Exception innerException)
            : base(message,innerException)
        {
        }
    }
}
