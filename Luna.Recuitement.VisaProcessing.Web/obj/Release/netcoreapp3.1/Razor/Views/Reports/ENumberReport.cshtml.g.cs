#pragma checksum "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00cbfcc68d49bed107a5b25de49a1bf85634f0e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_ENumberReport), @"mvc.1.0.view", @"/Views/Reports/ENumberReport.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00cbfcc68d49bed107a5b25de49a1bf85634f0e9", @"/Views/Reports/ENumberReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a398e6794262128d82e49352d048303a2258dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Reports_ENumberReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Luna.Recruitment.VisaProcessing.Data.DTO.FinalProcessing>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ENumberReport", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Reports", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmENumber"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
  
    ViewData["Title"] = "ENumberReport";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>E-Number Report</h1>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "00cbfcc68d49bed107a5b25de49a1bf85634f0e94602", async() => {
                WriteLiteral(@"
    <div class=""row"">
        <div class=""col-md-6"">
            <div class=""form-group"">
                <label class=""control-label"">File Number</label>
                <select id=""FileNumber"" name=""FileNumber"" class=""form-control DDFilter""></select>
            </div>
        </div>
        <div class=""col-md-6"">
            <div class=""form-group"">
                <label class=""control-label"">ENumber Date From</label>
                <input type=""date"" id=""ENumberDateFrom"" name=""EnumberDateFrom"" max=""25"" class=""form-control"" />
            </div>
        </div>
        <div class=""col-md-6"">
            <div class=""form-group"">
                <label class=""control-label"">ENumber Date To</label>
                <input type=""date"" id=""ENumberDateTo"" name=""ENumberDateTo"" max=""25"" class=""form-control"" />
            </div>
        </div>
        <div class=""col-md-6"">
            &nbsp;
        </div>
        <div class=""col-md-6"">
            <div class=""form-group"">
              ");
                WriteLiteral("  <label class=\"control-label\"></label>\r\n                <input type=\"submit\" value=\"Filter\" class=\"btn btn-primary form-control\" />\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<table class=""table table-responsive-md applyDataTableWithExport"">
    <thead>
        <tr>
            <th>Sr</th>
            <th>File ID</th>
            <th>Candidate Name</th>
            <th>Father Name</th>
            <th>Passport #</th>
            <th>Profession</th>
            <th>Company</th>
            <th>Selection Group</th>
            <th>Reference</th>
            <th>E-Number</th>
            <th>E-Date</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 55 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 58 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.Sr));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 59 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.FileID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 60 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.NameOfCandidate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 61 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.FatherName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 62 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.Passport));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 63 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.Profession));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 64 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.Company));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 65 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.SelectionGroup));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 66 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.Reference));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 67 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(Html.DisplayFor(modelItem => item.ENumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 68 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
               Write(item.EDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 70 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\ENumberReport.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>


<script>
    $(document).ready(function () {
        $.getJSON('/CandidateProfile/GetFileNo/', function (data) {
            $(""#FileNumber option"").remove();
            $(""#FileNumber"").append('<option value=0>--select File No--</option>');
            $.each(data, function () {
                $(""#FileNumber"").append('<option value=' + this.code + '>' + this.code + '</option>');
            });
            $(""#FileNumber"").val();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert('Error getting FileNo!');
        });
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Luna.Recruitment.VisaProcessing.Data.DTO.FinalProcessing>> Html { get; private set; }
    }
}
#pragma warning restore 1591
