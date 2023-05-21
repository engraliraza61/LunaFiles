#pragma checksum "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a65659fc0cd945ac7c760e7430a1b991b99ea52f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_Receivable), @"mvc.1.0.view", @"/Views/Reports/Receivable.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a65659fc0cd945ac7c760e7430a1b991b99ea52f", @"/Views/Reports/Receivable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a398e6794262128d82e49352d048303a2258dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Reports_Receivable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Luna.Recruitment.VisaProcessing.Data.DTO.FinalProcessing>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Receivable", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Reports", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmReceivable"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
  
    ViewData["Title"] = "Receivable Reports";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>Receivable Report</h1>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a65659fc0cd945ac7c760e7430a1b991b99ea52f4587", async() => {
                WriteLiteral(@"
    <div class=""row"">
        <div class=""col-md-6"">
            <div class=""form-group"">
                <label class=""control-label"">File Number</label>
                <select id=""ReceivableFileNo"" name=""FileNumber"" class=""form-control DDFilter""></select>
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
                WriteLiteral("        <label class=\"control-label\"></label>\r\n                <input type=\"submit\" value=\"Filter\" class=\"btn btn-primary form-control\" />\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
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
<table class=""table table-responsive-md applyDataTableWithExport"" id=""ReceivableTable"">
    <thead>
        <tr>
            <th>Sr</th>
            <th>File ID</th>
            <th>Candidate Name</th>
            <th>Father Name</th>
            <th>Passport #</th>
            <th>Profession</th>
            <th>Company</th>
            <th>SelectionGroup</th>
            <th>Reference</th>
            <th>E-Number</th>
            <th>E-Date</th>
            <th>Total Receivable</th>
            <th>Amount</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 56 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 59 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.Sr));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 60 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.FileID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 61 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.NameOfCandidate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 62 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.FatherName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 63 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.Passport));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 64 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.Profession));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 65 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.Company));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 66 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.selectiongroup));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 67 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.Reference));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 68 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(Html.DisplayFor(modelItem => item.ENumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 69 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(item.EDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 70 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
               Write(item.TotalReceivable);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    <input type=\"number\" id=\"InvoiceAmountId\" value=\"0\" />\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 75 "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Reports\Receivable.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            WriteLiteral(@"<div class=""col-md-6"">
    <div class=""form-group col-md-12 float-center"">
        <button class=""btn btn-primary float-right"" id=""btnSaveReceivable"" value=""Save"">Save</button>
    </div>
</div>

<script>
    $(document).ready(function () {
        $.getJSON('/CandidateProfile/GetFileNo/', function (data) {
            $(""#ReceivableFileNo option"").remove();
            $(""#ReceivableFileNo"").append('<option value=0>--select File No--</option>');
            $.each(data, function () {
                $(""#ReceivableFileNo"").append('<option value=' + this.code + '>' + this.code + '</option>');
            });
            $(""#ReceivableFileNo"").val();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert('Error getting FileNo!');
        });
        $(""#frmReceivable"").submit(function () {
            if (!$('#ReceivableFileNo').val()) {
                alert('please select file no');
                return false;
            }
        });
        $('#btnSaveReceiv");
            WriteLiteral(@"able').click(function () {
            var lstCandidateId = [];
            var lstVisaProcessId = [];
            var lstReceiptAmount = [];
            var EntitySetupId = [];
            var table = $('#ReceivableTable').DataTable();
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                console.log(this.data());
                lstCandidateId.push(this.data()[0]);
                console.group(""SELECTED Receipt"");
                console.log(lstCandidateId);
                console.groupEnd();
            });
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                console.log(this.data());
                lstVisaProcessId.push(this.data()[1]);
                console.group(""SELECTED Receipt"");
                console.log(lstVisaProcessId);
                console.groupEnd();
            });
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var d = this.data();
                console.l");
            WriteLiteral(@"og(d);
                var cell = table.cell({ row: rowIdx, column: 13 }).node();
                lstReceiptAmount.push($('input', cell).val());
                console.group(""SELECTED Receipt"");
                console.groupEnd();
            });
            var ReceiptFileNo = """";
            var ReceiptReference = """";
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var d = this.data();
                console.log(d);
                ReceiptFileNo = this.data()[3];
                console.group(""SELECTED Receipt"");
                console.groupEnd();
            });
            var data = {
                ""lstReceipt"": lstCandidateId,
                ""lstReceiptAmount"": lstReceiptAmount,
                ""lstVisaProcessId"": lstVisaProcessId,
            };
            $.ajax({
                type: ""POST"",
                url: '/Receipt/SaveInvoice',
                data: data,
                dataType: ""json"",
                success: function");
            WriteLiteral(@" (data) {
                    if (data.status == false) {
                        alert(""error"");
                    }
                    if (data.status == true) {
                        toastr.success('Receivable added successfuly.');
                        setTimeout(() => { console.log(""redirecting!""); }, 5000);
                        window.location.href = ""/Reports/Receivable?FileNumber="" + ReceiptFileNo;
                    }
                    //alert('success');
                    //console.log(data);
                    //toastr.success('payment added successfuly.');
                    //setTimeout(() => { console.log(""redirecting!""); }, 3000);
                    //window.location.href = ""/Receipt/ReceiptVoucher?FileNumber="" + ReceiptFileNo + ""&Reference="" + ReceiptReference;
                },
                error: function () {
                    alert('error');
                    toastr.error('while occured while saving passport.')
                }
            });");
            WriteLiteral("\n        });\r\n    });\r\n</script>\r\n\r\n\r\n");
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
