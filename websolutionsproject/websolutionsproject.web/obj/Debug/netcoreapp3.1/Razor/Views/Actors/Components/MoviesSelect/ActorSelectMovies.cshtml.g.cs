#pragma checksum "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Actors\Components\MoviesSelect\ActorSelectMovies.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "15273f5413efbda57a8f3dda9f2670a010bcf2ca"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Actors_Components_MoviesSelect_ActorSelectMovies), @"mvc.1.0.view", @"/Views/Actors/Components/MoviesSelect/ActorSelectMovies.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15273f5413efbda57a8f3dda9f2670a010bcf2ca", @"/Views/Actors/Components/MoviesSelect/ActorSelectMovies.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"543cf694f24b8bccaab3f9528606d18c3c6fd525", @"/Views/_ViewImports.cshtml")]
    public class Views_Actors_Components_MoviesSelect_ActorSelectMovies : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("multioption"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Actors\Components\MoviesSelect\ActorSelectMovies.cshtml"
  
    ViewData["Title"] = "ActorSelectMovies";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Actors\Components\MoviesSelect\ActorSelectMovies.cshtml"
 foreach(GetMovieModel movie in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "15273f5413efbda57a8f3dda9f2670a010bcf2ca7482", async() => {
#nullable restore
#line 8 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Actors\Components\MoviesSelect\ActorSelectMovies.cshtml"
                                             Write(movie.Name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 8 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Actors\Components\MoviesSelect\ActorSelectMovies.cshtml"
                           WriteLiteral(movie.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 9 "C:\Users\arthu\Documents\School\Odisee\2021S1\.Net Web Solutions\Labo\2021projectnetwebsolutions\websolutionsproject\websolutionsproject.web\Views\Actors\Components\MoviesSelect\ActorSelectMovies.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
