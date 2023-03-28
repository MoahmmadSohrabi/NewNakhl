﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NakhleNakhoda.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// admin-label tag helper
    /// </summary>
    [HtmlTargetElement("admin-button-save")]
    public class AdminButtonSaveTagHelper : TagHelper
    {
        public string Type { get; set; } = "Submit";
        public string Class { get; set; } = "btn btn-success";
        public string Name { get; set; } = "";
        public string Text { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("type", Type);
            output.Attributes.SetAttribute("Name", Name);
            output.Attributes.SetAttribute("class", $"btn btn-{Class}");
            output.Content.SetHtmlContent("<i class=\"far fa-save fa-fw fa-lg\" ></i>").AppendHtml($" {Text} ");
        }
    }
}