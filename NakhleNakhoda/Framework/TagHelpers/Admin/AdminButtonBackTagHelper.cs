using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NakhleNakhoda.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// admin-button-back tag helper
    /// </summary>
    [HtmlTargetElement("admin-button-back")]
    public class AdminButtonBackTagHelper : AnchorTagHelper
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public AdminButtonBackTagHelper(IHtmlGenerator htmlGenerator) : base(htmlGenerator) { }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            // Set the HTML element
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            // Set HTML Class
            output.Attributes.SetAttribute("class", "btn btn-primary");
            output.Content.SetHtmlContent("<i class=\"far fa-arrow-alt-circle-right fa-fw fa-lg\" ></i> بازگشت به لیست ");

            return base.ProcessAsync(context, output);
        }
    }
}