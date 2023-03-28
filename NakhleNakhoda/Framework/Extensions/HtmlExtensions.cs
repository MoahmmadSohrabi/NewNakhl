using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace NakhleNakhoda.Web.Framework.Extensions
{
    /// <summary>
    /// HTML extensions
    /// </summary>
    public static class HtmlExtensions
    {
        #region Common extensions

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>Result</returns>
        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            var htmlOutput = writer.ToString();
            return htmlOutput;
        }

        #endregion
    }
}