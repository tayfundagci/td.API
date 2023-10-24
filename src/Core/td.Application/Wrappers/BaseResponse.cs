using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Application.Wrappers
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public BaseResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public BaseResponse()
        {
            
        }
    }
}
