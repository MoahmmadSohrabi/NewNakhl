using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NakhleNakhoda.Web.Framework.TagHelpers.Site
{
    /// <summary>
    /// site-textarea tag helper
    /// </summary>
    [HtmlTargetElement("site-textarea", Attributes = ForAttributeName)]
    public class SiteTextAreaTagHelper : TextAreaTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string RequiredAttributeName = "asp-required";
        private const string DisabledAttributeName = "asp-disabled";

        /// <summary>
        /// Indicates whether the input is disabled
        /// </summary>
        [HtmlAttributeName(DisabledAttributeName)]
        public string IsDisabled { set; get; } = "false";

        /// <summary>
        /// Indicates whether the field is required
        /// </summary>
        [HtmlAttributeName(RequiredAttributeName)]
        public string IsRequired { set; get; } = "false";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public SiteTextAreaTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //tag details
            output.TagName = "textarea";
            output.TagMode = TagMode.StartTagAndEndTag;

            //merge classes
            var classValue = output.Attributes.ContainsName("class")
                ? $"{output.Attributes["class"].Value} form-control"
                : "form-control";
            output.Attributes.SetAttribute("class", classValue);

            //add disabled attribute
            bool.TryParse(IsDisabled, out bool disabled);
            if (disabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }

            //additional parameters
            var rowsNumber = output.Attributes.ContainsName("rows") ? output.Attributes["rows"].Value : 4;
            output.Attributes.SetAttribute("rows", rowsNumber);
            var colsNumber = output.Attributes.ContainsName("cols") ? output.Attributes["cols"].Value : 20;
            output.Attributes.SetAttribute("cols", colsNumber);

            //required asterisk
            bool.TryParse(IsRequired, out bool required);
            if (required)
            {
                output.PreElement.SetHtmlContent("<div class='input-group input-group-required'>");
                output.PostElement.SetHtmlContent("<div class=\"input-group-btn\"><span class=\"required\">*</span></div></div>");
            }

            base.Process(context, output);
        }
    }
}