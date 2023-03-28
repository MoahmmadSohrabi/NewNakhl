using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NakhleNakhoda.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public abstract partial class BaseAdminController : Controller
    {
        public BaseAdminController()
        {
        }


        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult"/> object that serializes the specified object to JavaScript Object Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// 
        /// <returns>
        /// The result object that serializes the specified object to JSON format.
        /// </returns>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The JSON request behavior</param>
        public override JsonResult Json(object? data)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            return base.Json(data, serializerSettings);
        }
    }
}