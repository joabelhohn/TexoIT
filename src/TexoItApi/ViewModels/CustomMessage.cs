using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexoIt.Api
{
    public class CustomHttpMessage
    {
        public const string CODE_OK = "OK";
        public const string CODE_BAD_REQUEST = "BAD_REQUEST";
        public const string CODE_INTERNAL_ERROR = "INTERNAL_ERROR";

        public string Code { get; set; }
        public string Message { get; set; }
    }
}
