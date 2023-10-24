using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Application.Messages
{
    public class ValidationResponse : BaseResponse
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationResponse(IDictionary<string, string[]> errors, string message) : base(message,false)
        {
            Errors = errors;
            Message = message;
        }
    }
}
