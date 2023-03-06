using System.Collections.Generic;

namespace TexoIt.Api
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public class BadRequestResponse: BaseResponse
    {
        public IEnumerable<CustomHttpMessage> Erros { get; set; }

        public BadRequestResponse()
        {
            Status = new CustomHttpMessage()
            {
                Code = CustomHttpMessage.CODE_BAD_REQUEST,
                Message = "Baq Request"
            };
        }


        public BadRequestResponse(string mesage): this()
        {
            Status.Message = mesage;
        }

    }
}
