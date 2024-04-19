using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackend.Error
{
    public class APIValidationErrorResponse : APIResponce
    {
        public APIValidationErrorResponse() : 
        base(400)
        {
        }
        public IEnumerable<string> Error { get; set; }
    }
}