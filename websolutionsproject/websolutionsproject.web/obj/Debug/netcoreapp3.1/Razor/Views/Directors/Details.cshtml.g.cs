#pragma checksum "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "90fe5e0fa4c90649eb6e04ed7b0f957688df90cb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Directors_Details), @"mvc.1.0.view", @"/Views/Directors/Details.cshtml")]
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
#line 1 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Actors;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Genres;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Directors;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Reviews;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using websolutionsproject.models.Movies;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"90fe5e0fa4c90649eb6e04ed7b0f957688df90cb", @"/Views/Directors/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"543cf694f24b8bccaab3f9528606d18c3c6fd525", @"/Views/_ViewImports.cshtml")]
    public class Views_Directors_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<websolutionsproject.models.Directors.GetDirectorModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
  
    ViewData["Title"] = Localizer["Details director"];
    ClaimsPrincipal user = HttpContextAccessor.HttpContext.User;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"intro\">\r\n    <h1>");
#nullable restore
#line 9 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
   Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 9 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                    Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <p class=\"text-md-left\">");
#nullable restore
#line 10 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                       Write(Localizer["Nationality"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 10 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                                  Write(Model.Nationality);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    <p class=\"text-sm-left\">");
#nullable restore
#line 11 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                       Write(Localizer["Born on"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 11 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                             Write(Model.Birth.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    <p class=\"profiledescription\">");
#nullable restore
#line 12 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                             Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n</div>\r\n\r\n<h2>Movies</h2>\r\n<hr />\r\n");
#nullable restore
#line 17 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
 if (Enumerable.Any(Model.Movies))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ul class=\"thumbnails\">\r\n");
#nullable restore
#line 20 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
         foreach (GetMovieModel movie in Model.Movies)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"span4\">\r\n                <div class=\"thumbnail\">\r\n                    <h3><a");
            BeginWriteAttribute("href", " href=\"", 760, "\"", 822, 1);
#nullable restore
#line 24 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
WriteAttributeValue("", 767, Url.Action("Details", "Movies", new { id = movie.Id }), 767, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 24 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                                                                     Write(Html.DisplayFor(modelItem => movie.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3> <h4>");
#nullable restore
#line 24 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                                                                                                                            Write(Html.DisplayFor(modelItem => movie.Year));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    <div class=\"rating\">\r\n                        ");
#nullable restore
#line 26 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                   Write(Localizer["Average rating"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <div class=\"stars\">\r\n");
#nullable restore
#line 28 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                             for (int i = 0; i < movie.OverallRating; i++)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <span style=\"color: orange;\" class=\"fa fa-star checked\"></span>\r\n");
#nullable restore
#line 31 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                             for (int i = 0; i < 10 - movie.OverallRating; i++)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <span class=\"fa fa-star\"></span>\r\n");
#nullable restore
#line 35 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                    </div>\r\n                    <p class=\"thumbnailDescription\">");
#nullable restore
#line 38 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                               Write(Html.DisplayFor(modelItem => movie.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n            </li>\r\n");
#nullable restore
#line 41 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n");
#nullable restore
#line 43 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h3>");
#nullable restore
#line 46 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
   Write(Localizer["This director hasn't directed any movies (yet)!"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n");
#nullable restore
#line 47 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"crudLinks\">\r\n");
#nullable restore
#line 50 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
     if (user.IsInRole("Administrator"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <a class=\"btn btn-danger\"");
            BeginWriteAttribute("href", " href=\"", 1943, "\"", 2007, 1);
#nullable restore
#line 52 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
WriteAttributeValue("", 1950, Url.Action("Delete", "Directors", new { id = Model.Id }), 1950, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 52 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                                                                              Write(Localizer["Delete"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n        <a class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 2066, "\"", 2128, 1);
#nullable restore
#line 53 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
WriteAttributeValue("", 2073, Url.Action("Edit", "Directors", new { id = Model.Id }), 2073, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 53 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                                                                          Write(Localizer["Edit"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 54 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
    }
    else if (user.IsInRole("Editor"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<a class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 2230, "\"", 2292, 1);
#nullable restore
#line 57 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
WriteAttributeValue("", 2237, Url.Action("Edit", "Directors", new { id = Model.Id }), 2237, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 57 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                                                                  Write(Localizer["Edit"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 58 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90fe5e0fa4c90649eb6e04ed7b0f957688df90cb19868", async() => {
#nullable restore
#line 62 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Directors\Details.cshtml"
                                          Write(Localizer["Back to directors"]);

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
            WriteLiteral("\r\n</div> \r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<websolutionsproject.models.Directors.GetDirectorModel> Html { get; private set; }
    }
}
#pragma warning restore 1591