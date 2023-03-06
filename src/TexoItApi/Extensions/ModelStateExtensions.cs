using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TexoIt.Api
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<CustomHttpMessage> ToHttpMessage(this ModelStateDictionary ModelState) =>
            from vl in ModelState
            from er in vl.Value.Errors
            select new CustomHttpMessage()
            { 
                Code = vl.Key,
                Message = er.ErrorMessage
            };
    }
}
