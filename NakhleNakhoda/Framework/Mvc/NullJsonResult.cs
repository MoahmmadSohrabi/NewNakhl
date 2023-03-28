using Microsoft.AspNetCore.Mvc;

namespace NakhleNakhoda.Web.Framework.Mvc
{
    public class NullJsonResult : JsonResult
    {
        public NullJsonResult() : base(null)
        {
        }
    }
}
