using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevIO.UI.Site.Extensions
{
    public class EmailTagHelper : TagHelper
    {
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + "gmail.com";
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);
        }
    }
}
