using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Application.Wrappers
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public ServiceResponse(T data, string message, bool success) : base(message, success)
        {
            Data = data;
            Message = message;
        }
    }
}
