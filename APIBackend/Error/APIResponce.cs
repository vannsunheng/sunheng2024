using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLitePCL;

namespace APIBackend.Error
{
    public class APIResponce
    {
        public APIResponce(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatus(statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatus(int statusCode)
        {
            return statusCode switch
            {
                400=> "A Bad request, you have made",
                401=> "Autherized , you are not",
                404=> "Resource found, it was not",
                500=> "Errors are the path to the dard side, Errors lead to anger. Anger leads to hate. Hate leads to career change.",
                _=>null
            };
        }
    }
}