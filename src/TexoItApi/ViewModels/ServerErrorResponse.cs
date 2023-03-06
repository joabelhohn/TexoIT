using System.Collections.Generic;

namespace TexoIt.Api
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public class ServerErrorResponse : BaseResponse
    {
        public IEnumerable<CustomHttpMessage> Erros { get; set; }

        public ServerErrorResponse(string statusCode, string statusMessage)
        {
            Status = new CustomHttpMessage()
            {
                Code = statusCode,
                Message = statusMessage
            };
        }

    }
}
