#pragma checksum "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a705a7f47b50a3d5033b22c4c7b6e5e81729da72"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reviews_Index), @"mvc.1.0.view", @"/Views/Reviews/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Actors;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Genres;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Directors;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Reviews;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Movies;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a705a7f47b50a3d5033b22c4c7b6e5e81729da72", @"/Views/Reviews/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"543cf694f24b8bccaab3f9528606d18c3c6fd525", @"/Views/_ViewImports.cshtml")]
    public class Views_Reviews_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<websolutionsproject.models.Reviews.GetReviewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
  
    ViewData["Title"] = Localizer["All reviews"];
    ClaimsPrincipal user = HttpContextAccessor.HttpContext.User;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 8 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
Write(Localizer["All reviews"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n");
#nullable restore
#line 10 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
 if (user.IsInRole("Guest"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a705a7f47b50a3d5033b22c4c7b6e5e81729da727236", async() => {
#nullable restore
#line 13 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                  Write(Localizer["Create review"]);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </p>\r\n");
#nullable restore
#line 15 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<ul class=\"thumbnails\">\r\n");
#nullable restore
#line 18 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
     foreach (GetReviewModel review in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"span4\">\r\n            <div class=\"thumbnail\">\r\n                <h3><a");
            BeginWriteAttribute("href", " href=\"", 548, "\"", 616, 1);
#nullable restore
#line 22 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 555, Url.Action("Details", "Movies", new { id = review.MovieId }), 555, 61, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 22 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                                                       Write(review.Movie.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3> \r\n                <h4>");
#nullable restore
#line 23 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
               Write(Localizer["By"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <a");
            BeginWriteAttribute("href", " href=\"", 687, "\"", 753, 1);
#nullable restore
#line 23 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 694, Url.Action("Details", "Users", new { id = review.UserId }), 694, 59, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 23 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                                                                      Write(Html.DisplayFor(modelItem => review.User.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h4>\r\n                <div class=\"rating\">\r\n");
#nullable restore
#line 25 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                     for (int i = 0; i < review.Rating; i++)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span style=\"color: orange;\" class=\"fa fa-star checked\"></span>\r\n");
#nullable restore
#line 28 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                     for (int i = 0; i < 10 - review.Rating; i++)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span class=\"fa fa-star\"></span>\r\n");
#nullable restore
#line 32 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <p class=\"thumbnailDescription\">");
#nullable restore
#line 34 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                           Write(Html.DisplayFor(modelItem => review.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p class=\"text-sm-left\">");
#nullable restore
#line 35 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                   Write(Localizer["Reviewed on"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 35 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                             Write(review.Date.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n");
#nullable restore
#line 37 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                 if (user.IsInRole("Administrator"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a class=\"btn btn-danger\"");
            BeginWriteAttribute("href", " href=\"", 1577, "\"", 1640, 1);
#nullable restore
#line 39 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 1584, Url.Action("Delete", "Reviews", new { id = review.Id }), 1584, 56, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 39 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                                                                         Write(Localizer["Delete"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                    <a class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 1711, "\"", 1772, 1);
#nullable restore
#line 40 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 1718, Url.Action("Edit", "Reviews", new { id = review.Id }), 1718, 54, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 40 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                                                                     Write(Localizer["Edit"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                    <a class=\"btn btn-success\"");
            BeginWriteAttribute("href", " href=\"", 1844, "\"", 1908, 1);
#nullable restore
#line 41 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 1851, Url.Action("Details", "Reviews", new { id = review.Id }), 1851, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Details</a>\r\n");
#nullable restore
#line 42 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                }
                else if (user.IsInRole("Editor"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 2055, "\"", 2116, 1);
#nullable restore
#line 45 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 2062, Url.Action("Edit", "Reviews", new { id = review.Id }), 2062, 54, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 45 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                                                                                                     Write(Localizer["Edit"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                    <a class=\"btn btn-success\"");
            BeginWriteAttribute("href", " href=\"", 2188, "\"", 2252, 1);
#nullable restore
#line 46 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 2195, Url.Action("Details", "Reviews", new { id = review.Id }), 2195, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Details</a>\r\n");
#nullable restore
#line 47 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                }
                else if (user.IsInRole("Guest"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a class=\"btn btn-success\"");
            BeginWriteAttribute("href", " href=\"", 2401, "\"", 2465, 1);
#nullable restore
#line 50 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
WriteAttributeValue("", 2408, Url.Action("Details", "Reviews", new { id = review.Id }), 2408, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Details</a>\r\n");
#nullable restore
#line 51 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </li>\r\n");
#nullable restore
#line 54 "C:\Users\arthu\Documents\GitRepo\AspNetMoviemind\websolutionsproject\websolutionsproject.web\Views\Reviews\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<websolutionsproject.models.Reviews.GetReviewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
