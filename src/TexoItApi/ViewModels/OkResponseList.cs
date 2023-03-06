using System.Collections.Generic;
using System.Linq;

namespace TexoIt.Api
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public class OkResponseList<T>: BaseResponse
    {
        public IEnumerable<T> Results { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }

        public OkResponseList()
        {
            Status = new CustomHttpMessage()
            {
                Code = CustomHttpMessage.CODE_OK
            };
        }

        public OkResponseList(IReadOnlyList<T> @results) : this()
        {
            Total = @results.Count();
            Results = @results;
        }

        public OkResponseList(IReadOnlyList<T> @results, int limit, int offset) : this()
        {
            Total = @results.Count();
            Limit = limit;
            Offset = (Total >= offset) ? offset : 0;
            Results = @results.Skip(Offset).Take(Limit);
        }
    }
}
