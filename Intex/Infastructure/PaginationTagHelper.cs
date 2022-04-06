using System;
using Intex.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Intex.Infastructure
{
    [HtmlTargetElement("div", Attributes = "page-tag")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;

        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]

        public ViewContext vc { get; set; }

        public PageInfo PageTag { get; set; }
        public string PageAction { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");
        

        //for (int i = 1; i < PageTag.TotalPages; i++)
            //{
                //TagBuilder tb = new TagBuilder("a");

                //tb.Attributes["href"] = uh.Action(PageAction, new {pageNum = i});
                //tb.InnerHtml.AppendHtml(i.ToString());

                //final.InnerHtml.AppendHtml(tb);

            //}
         foreach (var x in PageTag.Pages)
            { 
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, x);
                tb.InnerHtml.AppendHtml(x.ToString());
                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
