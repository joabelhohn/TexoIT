using System.Collections.Generic;

namespace TexoIt.Api
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public class OkResponse<T>: BaseResponse
    {
        public T Result { get; set; }

        public OkResponse(T @result): base()
        {
            Result = @result;
        }
    }
}
