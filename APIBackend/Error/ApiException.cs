using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackend.Error
{
    public class ApiException : APIResponce
    {
        public string Details { get; }
        public ApiException(int statusCode, string message = null,string details=null) : 
        base(statusCode, message)
        {
            Details = details;
        }

        
    }
}