#pragma checksum "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Shared\medicalModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03e650e4aca61cf861b4591dae22559d0a0bb621"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_medicalModal), @"mvc.1.0.view", @"/Views/Shared/medicalModal.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03e650e4aca61cf861b4591dae22559d0a0bb621", @"/Views/Shared/medicalModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a398e6794262128d82e49352d048303a2258dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_medicalModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "3", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "4", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "5", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<div>
    <a href=""#!"" class=""btn btn-primary float-right"" data-toggle=""modal"" onclick=""showMedical('N',this)"" ;>Add</a>
    <div class=""modal fade"" id=""medicalModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""medicalModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Medical Detail</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03e650e4aca61cf861b4591dae22559d0a0bb6215669", async() => {
                WriteLiteral(@"
                        <div class=""form-row"">
                            <div class=""form-group col-md-6"" style=""display:none;"">
                                <label>Id</label>
                                <input type=""hidden"" id=""medicalId"" class=""form-control"" />
                            </div>
                            <div class=""form-group col-md-6"" style=""display:none;"">
                                <label>Sponser</label>
                                <select id=""sponserId"" class=""form-control DDFilter"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03e650e4aca61cf861b4591dae22559d0a0bb6216523", async() => {
                    WriteLiteral("Sponser A");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03e650e4aca61cf861b4591dae22559d0a0bb6218108", async() => {
                    WriteLiteral("Sponser B");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03e650e4aca61cf861b4591dae22559d0a0bb6219370", async() => {
                    WriteLiteral("Sponser C");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03e650e4aca61cf861b4591dae22559d0a0bb62110632", async() => {
                    WriteLiteral("Sponser D");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03e650e4aca61cf861b4591dae22559d0a0bb62111895", async() => {
                    WriteLiteral("Sponser E");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                </select>
                            </div>
                            <div class=""form-group col-md-12"">
                                <label>Medical Center</label>
                                <select id=""medicalCenterName"" class=""form-control DDFilter""></select>
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>GHC Code</label>
                                <input type=""text"" id=""ghccodeNo"" class=""form-control"">
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>GCC Slip No</label>
                                <input type=""number"" id=""gccslipNo"" max=""20"" class=""form-control"">
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>Date Examined</label>
                                <input type=""da");
                WriteLiteral(@"te"" id=""dateExamined"" required class=""form-control"" />
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>Report ExpiryDate</label>
                                <input type=""date"" id=""reportExpiryDate"" required class=""form-control"">
                            </div>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" id=""closeMedicalModal"" class=""btn btn-secondary"">Close</button>
                    <button type=""button"" class=""btn btn-success"" id=""SaveMedicalDialogBtn"" ");
            WriteLiteral(@">Save</button>
                </div>
                <div id=""show_stored_data"">

                </div>

            </div>
        </div>
    </div>

</div>
<table class=""table"">
    <thead>
        <tr>
            <th style=""display:none;"">Record Id (PK)</th>
            <th>Medical Center</th>
            <th>GCC Slip#</th>
            <th>GHC Code</th>
            <th>Date Examined</th>
            <th>Expiry Date</th>
            <th></th>
        </tr>
    </thead>
    <tbody id=""medicalDetail"">
    </tbody>
</table>

<script type=""text/javascript"">
    //function showMedical(type,button) {
    //    if (type == ""U"") {
    //        $(""#medicalId"").val($(button).closest('tr').children('td:nth(0)').text());
    //        //$(""#sponserId"").val($(button).closest('tr').children('td:nth(1)').text());
    //        $(""#medicalCenterName"").val($(button).closest('tr').children('td:nth(1)').text());
    //        $(""#ghccodeNo"").val($(button).closest('tr').children('td:nth(2)'");
            WriteLiteral(@").text()); //change label to slip
    //        $(""#gccslipNo"").val($(button).closest('tr').children('td:nth(3)').text());//hide
    //        document.getElementById(""dateExamined"").valueAsDate = new Date($(button).closest('tr').children('td:nth(4)').text());
    //        document.getElementById(""reportExpiryDate"").valueAsDate = new Date($(button).closest('tr').children('td:nth(5)').text());
    //    }
    //    else //(type == ""N"") {
    //    {
    //        debugger;
    //        $(""#medicalId"").val(0);
    //        $(""#medicalCenterName"").val("""")
    //        $(""#ghccodeNo"").val("""");
    //        $(""#gccslipNo"").val("""");
    //        $(""#dateExamined"").val("""");
    //        $(""#reportExpiryDate"").val("""")
    //    }
    //    $('#medicalModal').modal('show');
    //}
    //function saveMedical() {
    //    var data = {
    //        ""id"": $(""#medicalId"").val(),
    //        ""candidateProfileId"": $("".pkid"").val(),
    //        ""sponserId"": $(""#sponserId"").val(),
    //    ");
            WriteLiteral(@"    ""medicalCenterName"": $(""#medicalCenterName"").val(),
    //        ""ghccodeNo"": $(""#ghccodeNo"").val(),
    //        ""gccslipNo"": $(""#gccslipNo"").val(),
    //        ""dateExamined"": $(""#dateExamined"").val(),
    //        ""reportExpiryDate"": $(""#reportExpiryDate"").val()
    //    };
    //    if (!$(""#dateExamined"").val()) {
    //        alert(""Date Examined is mandatory."");
    //        return false;
    //    }
    //    if (!$(""#reportExpiryDate"").val()) {
    //        alert(""Expiry Date is mandatory."");
    //        return false;
    //    }
    //    $.ajax({
    //        type: ""POST"",
    //        url: '/CandidateProfile/UpdateMedical',
    //        data: data,
    //        dataType: ""json"",

    //        success: function (data) {
    //            toastr.success('Medical record saved successfully.', ""Alert"")
    //            var html = '<tr>' +
    //                '<td style=""display:none;"">' + data.id + '</td>' +
    //                //'<td style=""display:none");
            WriteLiteral(@";"">' + $('#sponserId').val() + '</td>' +
    //                //'<td>' + $('#sponserId option:selected').text() + '</td>' +
    //                '<td>' + $('#medicalCenterName').val() + '</td>' +
    //                '<td>' + $('#ghccodeNo').val() + '</td>' +
    //                '<td>' + $('#gccslipNo').val() + '</td>' +
    //                '<td>' + $('#dateExamined').val() + '</td>' +
    //                '<td>' + $('#reportExpiryDate').val() + '</td>' +
    //                '<td><a href=""#!""  onclick=""ShowDialog(\'U\',this);""><i class=""fas fa-edit""></i></a><a href=""#"" onclick=""deleteMedical(' + data.id +')""><i class=""fas fa-trash""></i></a></td>' +
    //                '</tr>';
    //            if ($('#medicalId').val() == ""0"") {
    //                $('#medicalDetail').append(html);
    //            } else {
    //                //$(""#medicalRow"" + $('#medicalId').val()).children('td:nth(2)').text($('#sponserId option:selected').text());
    //                $(""#medicalRow"" + $('");
            WriteLiteral(@"#medicalId').val()).children('td:nth(1)').text($('#medicalCenterName').val());
    //                $(""#medicalRow"" + $('#medicalId').val()).children('td:nth(2)').text($('#ghccodeNo').val());
    //                $(""#medicalRow"" + $('#medicalId').val()).children('td:nth(3)').text($('#gccslipNo').val());
    //                $(""#medicalRow"" + $('#medicalId').val()).children('td:nth(4)').text($('#dateExamined').val());
    //                $(""#medicalRow"" + $('#medicalId').val()).children('td:nth(5)').text($('#reportExpiryDate').val());
    //            }
    //        },
    //        error: function () {
    //            toastr.error('Error occured while saving medical record.', ""Error"")
    //        }
    //    });

    //    return false;
    //}
    //function deleteMedical(medicalId) {
    //    var data = { ""Id"": medicalId };
    //    if (confirm(""Are you sure to delete this medical info?"")) {
    //        if (!medicalId || medicalId == ""0"") {
    //            //Remove line fr");
            WriteLiteral(@"om table only.
    //            $(""#medicalRow"" + medicalId).remove();
    //        }
    //        else {
    //            $.ajax({
    //                type: ""POST"",
    //                url: '/CandidateProfile/DeleteMedical',
    //                data: data,
    //                dataType: ""json"",
    //                success: function (data) {
    //                    toastr.success('Medical info deleted.')
    //                    $(""#medicalRow"" + medicalId).remove();
    //                },
    //                error: function () {
    //                    toastr.error('while occured while deleting medical info.')
    //                }
    //            });
    //        }
    //    }
    //    return false;
    //}
</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
