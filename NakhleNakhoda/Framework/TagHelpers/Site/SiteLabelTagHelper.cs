using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NakhleNakhoda.Web.Framework.TagHelpers.Site
{
    /// <summary>
    /// site-label tag helper
    /// </summary>
    [HtmlTargetElement("site-label", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class SiteLabelTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisplayHintAttributeName = "asp-display-hint";

        /// <summary>
        /// HtmlGenerator
        /// </summary>
        protected IHtmlGenerator Generator { get; set; }

        /// <summary>
        /// An expression to be evaluated against the current model
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression? For { get; set; }

        /// <summary>
        /// Indicates whether the hint should be displayed
        /// </summary>
        [HtmlAttributeName(DisplayHintAttributeName)]
        public bool DisplayHint { get; set; } = true;

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        /// <param name="workContext">Work context</param>
        /// <param name="localizationService">Localization service</param>
        public SiteLabelTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //generate label
            var tagBuilder = Generator.GenerateLabel(ViewContext, For!.ModelExplorer, For.Name, null, new { @class = "col-form-label" });
            if (tagBuilder != null)
            {
                //create a label wrapper
                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;
                //merge classes
                var classValue = output.Attributes.ContainsName("class") ? $"{output.Attributes["class"].Value}" : "col-md-3";
                output.Attributes.SetAttribute("class", classValue);

                var styleValue = output.Attributes.ContainsName("style") ? $"{output.Attributes["style"].Value}" : "text-align: right;font-size: 14px";
                output.Attributes.SetAttribute("style", styleValue);

                //add label
                output.Content.SetHtmlContent(tagBuilder);

                //add hint
                //if (For.Metadata.AdditionalValues.TryGetValue("NopResourceDisplayNameAttribute", out object value))
                //{
                //    var resourceDisplayName = value as NopResourceDisplayNameAttribute;
                //    if (resourceDisplayName != null && DisplayHint)
                //    {
                //        var langId = _workContext.WorkingLanguage.Id;
                //        var hintResource = _localizationService.GetResource(
                //            resourceDisplayName.ResourceKey + ".Hint", langId, returnEmptyIfNotFound: true,
                //            logIfNotFound: false);

                //        if (!string.IsNullOrEmpty(hintResource))
                //        {
                //            var hintContent = $"<div title='{WebUtility.HtmlEncode(hintResource)}' data-toggle='tooltip' class='ico-help'><i class='fa fa-question-circle'></i></div>";
                //            output.Content.AppendHtml(hintContent);
                //        }
                //    }
                //}
            }

            return base.ProcessAsync(context, output);

        }
    }
}