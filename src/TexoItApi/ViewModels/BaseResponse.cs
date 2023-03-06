using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexoIt.Api
{
    public class BaseResponse
    {
        public CustomHttpMessage Status { get; set; }

        public BaseResponse()
        {
            Status = new CustomHttpMessage()
            {
                Code = CustomHttpMessage.CODE_OK
            };
        }
    }
}
