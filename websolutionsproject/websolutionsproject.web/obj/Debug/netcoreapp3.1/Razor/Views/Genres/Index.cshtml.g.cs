#pragma checksum "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0027916d43ada4d0a59d504a39945598d8d15aa5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Genres_Index), @"mvc.1.0.view", @"/Views/Genres/Index.cshtml")]
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
#nullable restore
#line 2 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
using websolutionsproject.models.Genres;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0027916d43ada4d0a59d504a39945598d8d15aa5", @"/Views/Genres/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"543cf694f24b8bccaab3f9528606d18c3c6fd525", @"/Views/_ViewImports.cshtml")]
    public class Views_Genres_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<websolutionsproject.models.Genres.GetGenreModel>>
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
#line 4 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
  
    ViewData["Title"] = Localizer["All genres"];
    ClaimsPrincipal user = HttpContextAccessor.HttpContext.User;


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 10 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
Write(Localizer["All genres"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n");
#nullable restore
#line 12 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
 if (user.IsInRole("Editor") || user.IsInRole("Administrator"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0027916d43ada4d0a59d504a39945598d8d15aa58030", async() => {
#nullable restore
#line 15 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
                                                  Write(Localizer["Create new genre"]);

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
#line 17 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row row-flex\">\r\n");
#nullable restore
#line 20 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
     foreach (GetGenreModel getGenreModel in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"d-inline-block col-md-4 col-sm-6 col-xs-12 p-3 my-2 mx-2 border rounded border-success\">\r\n            <h3><a");
            BeginWriteAttribute("href", " href=\"", 676, "\"", 746, 1);
#nullable restore
#line 23 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
WriteAttributeValue("", 683, Url.Action("Details", "Genres", new { id = getGenreModel.Id }), 683, 63, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 23 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
                                                                                     Write(getGenreModel.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n        </div>\r\n");
#nullable restore
#line 25 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Genres\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<websolutionsproject.models.Genres.GetGenreModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591