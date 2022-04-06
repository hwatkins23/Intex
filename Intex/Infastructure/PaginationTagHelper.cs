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

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]

        public ViewContext vc { get; set; }

        public PageInfo PageTag { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }

        public bool PageClassesEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");

            for (int i = PageTag.StartPage; i <= PageTag.EndPage && i < PageTag.TotalPages; i++)
            //for (int i = 1; i < PageTag.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");

                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageTag.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                tb.AddCssClass(PageClass);
                tb.InnerHtml.AppendHtml(i.ToString());

                final.InnerHtml.AppendHtml(tb);

            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}

