#pragma checksum "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0d91403e28a55898cddf3abd6304e79343d99f5c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AppUser_Details), @"mvc.1.0.view", @"/Views/AppUser/Details.cshtml")]
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
#line 1 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\_ViewImports.cshtml"
using Luna.Recruitment.VisaProcessing.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\_ViewImports.cshtml"
using Luna.Recruitment.VisaProcessing.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d91403e28a55898cddf3abd6304e79343d99f5c", @"/Views/AppUser/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a398e6794262128d82e49352d048303a2258dff", @"/Views/_ViewImports.cshtml")]
    public class Views_AppUser_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Luna.Recruitment.VisaProcessing.Data.Models.AspNetUsers>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
  
    ViewData["Title"] = "Details User";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>AspNetUsers</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 14 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 17 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 20 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.NormalizedUserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 23 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.NormalizedUserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 26 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 29 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 32 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.NormalizedEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 35 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.NormalizedEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 38 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.EmailConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 41 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.EmailConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 44 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.PasswordHash));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 47 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.PasswordHash));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 50 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.SecurityStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 53 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.SecurityStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 56 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ConcurrencyStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 59 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.ConcurrencyStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 62 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 65 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 68 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.PhoneNumberConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 71 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.PhoneNumberConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 74 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.TwoFactorEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 77 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.TwoFactorEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 80 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.LockoutEnd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 83 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.LockoutEnd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 86 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.LockoutEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 89 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.LockoutEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 92 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.AccessFailedCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 95 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.AccessFailedCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0d91403e28a55898cddf3abd6304e79343d99f5c13563", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 100 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\AppUser\Details.cshtml"
                           WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0d91403e28a55898cddf3abd6304e79343d99f5c15715", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Luna.Recruitment.VisaProcessing.Data.Models.AspNetUsers> Html { get; private set; }
    }
}
#pragma warning restore 1591