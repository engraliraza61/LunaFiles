#pragma checksum "D:\LUNA\LUNA PROD\Code\Luna.Recuitement.VisaProcessing.Web\Views\Shared\passportModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ab74283bacbdae7ce091d2806ec2f9e5f5c51041"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_passportModal), @"mvc.1.0.view", @"/Views/Shared/passportModal.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab74283bacbdae7ce091d2806ec2f9e5f5c51041", @"/Views/Shared/passportModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a398e6794262128d82e49352d048303a2258dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_passportModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
            WriteLiteral(@"
<div>
    <a href=""#!"" id=""passsportDetailDialog"" class=""btn btn-primary float-right"" data-toggle=""modal"" onclick=""showPassport('N', this);"">Add</a>
    <div class=""modal fade"" id=""passportModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""passportModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Passport Detail</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ab74283bacbdae7ce091d2806ec2f9e5f5c510414136", async() => {
                WriteLiteral(@"
                        <div class=""form-group col-md-6"" style=""display:none;"">
                            <label>Id</label>
                            <input type=""hidden"" id=""passportId"" class=""form-control"" />
                        </div>
                        <div style=""display:none;"" class=""form-row"">
                            <div class=""form-group col-md-6"">
                                <label>Given Name</label>
                                <input id=""passportGivenName"" type=""text"" max=""25"" required class=""form-control"">
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>Surname</label>
                                <input id=""passportSurname"" type=""text"" max=""25"" required class=""form-control"">
                            </div>
                        </div>
                        <div style=""display:none;"" class=""form-row"">
                            <div class=""form-group col-md-6");
                WriteLiteral(@""">
                                <label>Place Of Issue</label>
                                <select class=""form-control DDFilter"" id=""passportPlaceOfIssue"">
                                </select>
                            </div>
                        </div>
                        <div class=""form-row"">
                            <div class=""form-group col-md-6"">
                                <label>Passport No.</label>
                                <input type=""text"" id=""passportNumber"" required max=""10"" class=""form-control"">
                            </div>
                        </div>
                        <div class=""form-row"">
                            <div class=""form-group col-md-6"">
                                <label>Issue Date</label>
                                <input type=""date"" id=""passportIssueDate"" class=""form-control dtPassport"">
");
                WriteLiteral(@"                            </div>
                            <div class=""form-group col-md-6"">
                                <label>Expiry Date</label>
                                <input type=""date"" id=""passportExpiryDate"" class=""form-control dtPassport"">
                            </div>
                        </div>
                        <h5 style=""display:none;"">Place Of Birth</h5>
                        <div style=""display:none;"" class=""form-row"">
                            <div class=""form-group col-md-6"">
                                <label>Country</label>
                                <select class=""form-control DDFilter"" id=""passportBirthCountry"">
                                </select>
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>City</label>
                                <select class=""form-control DDFilter"" id=""passportBirthCity"">
                                </select");
                WriteLiteral(@">
                            </div>
                        </div>
                        <div class=""form-row"" style=""display:none;"">
                            <div class=""form-group col-md-6"">
                                <label>Previous Nationality</label>
                                <select class=""form-control DDFilter"" id=""passportPreviousNationality"">
                                </select>
                            </div>
                            <div class=""form-group col-md-6"">
                                <label>Previous Passport#</label>
                                <input type=""text"" id=""passportPreviousNumber"" class=""form-control"">
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
                    <button type=""button"" class=""btn btn-secondary"" id=""passportDialogClosBtn"">Close</button>
                    <button type=""button"" class=""btn btn-success"" id=""passsportSaveDialog"" onclick=""savePassport()"">Save</button>
                </div>
            </div>
        </div>
    </div>

</div>
<table class=""table"">
    <thead>
        <tr>
");
            WriteLiteral("            <th>Passport#</th>\r\n            <th>Issue Date</th>\r\n            <th>Expiry Date</th>\r\n");
            WriteLiteral(@"            <th></th>
        </tr>
    </thead>
    <tbody id=""passportDetail"">
    </tbody>
</table>

<script type=""text/javascript"">

        //function getDateFormat(inputDate) {
        //    console.log(inputDate);
        //    var day = inputDate.split('/')[0];
        //    var month = inputDate.split('/')[1];
        //    var year = inputDate.split('/')[2];
        //    var monthName = 'Not Working';
        //    console.log('Day =>' + day);
        //    console.log('Month =>' +month);
        //    console.log('Year =>' +year);
        //    switch (month)
        //    {
        //        case '01' || '1':
        //            monthName = 'Jan';
        //            break;
        //        case '02' || '2':
        //            monthName = 'Feb';
        //            break;
        //        case '03' || '3':
        //            monthName = 'Mar';
        //            break;
        //        case '04' || '4':
        //            monthName = 'Apr';
    ");
            WriteLiteral(@"    //            break;
        //        case '05' || '5':
        //            monthName = 'May';
        //            break;
        //        case '06' || '6':
        //            monthName = 'Jun';
        //            break;
        //        case '07' || '7':
        //            monthName = 'Jul';
        //            break;
        //        case '08' || '8':
        //            monthName = 'Aug';
        //            break;
        //        case '09' || '9':
        //            monthName = 'Sep';
        //            break;
        //        case '10':
        //            monthName = 'Oct';
        //            break;
        //        case '11':
        //            monthName = 'Nov';
        //            break;
        //        case '12':
        //            monthName = 'Dec';
        //            break;
        //    }
        //    console.log('Final Date : ' + day + '-' + monthName + '-' + year);
        //    return day + '-' + monthName + '-'");
            WriteLiteral(@" + year;
        //}
        //function showPassport(type, passportRowId) {
        //    alert('function call');
        //    if (type == ""U"") {
        //        //console.log(type);
        //        console.log(passportRowId);
        //        $(""#passportId"").val($(""#""+passportRowId).closest('tr').children('td:nth(0)').text());
        //        $(""#passportGivenName"").val($(""#"" + passportRowId).closest('tr').children('td:nth(1)').text());
        //        $(""#passportSurname"").val($(""#"" + passportRowId).closest('tr').children('td:nth(2)').text());
        //        $(""#passportPlaceOfIssue"").val($(""#"" + passportRowId).closest('tr').children('td:nth(3)').text());
        //        $(""#passportNumber"").val($(""#"" + passportRowId).closest('tr').children('td:nth(4)').text());
        //        //document.getElementById(""passportIssueDate"").valueAsDate = new Date($(""#"" + passportRowId).closest('tr').children('td:nth(5)').text());
        //        //document.getElementById(""passportExpiryDate""");
            WriteLiteral(@").valueAsDate = new Date($(""#"" + passportRowId).closest('tr').children('td:nth(6)').text());
        //        $(""#passportIssueDate"").val(getDateFormat($(""#"" + passportRowId).closest('tr').children('td:nth(5)').text()));
        //        $(""#passportExpiryDate"").val(getDateFormat($(""#"" + passportRowId).closest('tr').children('td:nth(6)').text()));
        //        //$(""#passportExpiryDate"").val($(""#"" + passportRowId).closest('tr').children('td:nth(6)').text());
        //        $(""#passportBirthCountry"").val($(""#"" + passportRowId).closest('tr').children('td:nth(7)').text());
        //        $(""#passportBirthCity"").val($(""#"" + passportRowId).closest('tr').children('td:nth(8)').text());
        //    }
        //    else {
        //        $(""#passportId"").val(0);
        //        $(""#passportGivenName"").val("""");
        //        $(""#passportSurname"").val("""");
        //        $(""#passportPlaceOfIssue"").val("""");
        //        $(""#passportNumber"").val("""");
        //        $(""#passpor");
            WriteLiteral(@"tIssueDate"").val("""");
        //        $(""#passportExpiryDate"").val("""");
        //        $(""#passportBirthCountry"").val(1);
        //        $(""#passportBirthCity"").val(1);
        //    }
        //    $('#passportModal').modal('show');
        //}
        //function savePassport() {
        //    debugger;
        //    var data = {
        //        ""id"": $(""#passportId"").val(),
        //        ""candidateProfileId"": $('.pkid').val(),
        //        ""firstName"": $(""#passportGivenName"").val(),
        //        ""lastName"": $(""#passportSurname"").val(),
        //        ""PlaceOfIssueCountryId"": $(""#passportPlaceOfIssue"").val(),
        //        ""passportNo"": $(""#passportNumber"").val(),
        //        ""issueDate"": $(""#passportIssueDate"").val(),
        //        ""expiryDate"": $(""#passportExpiryDate"").val(),
        //        ""placeOfBirthCountryId"": $(""#passportBirthCountry"").val(),
        //        ""placeOfBirthCityId"": $(""#passportBirthCity"").val()
        //    };
        ");
            WriteLiteral(@"//    console.log(data);
        //    //if (!$(""#passportGivenName"").val()) {
        //    //    alert(""Given name is mandatory."");
        //    //    return false;
        //    //}
        //    //else if (!$(""#passportSurname"").val()) {
        //    //    alert(""Surname is mandatory."");
        //    //    return false;
        //    //}
        //    if (!$(""#passportNumber"").val()) {
        //        alert(""Passport number is mandatory."");
        //        return false;
        //    }
        //    else if (!$(""#passportIssueDate"").val()) {
        //        alert(""Issue date is mandatory."");
        //        return false;
        //    }
        //    else if (!$(""#passportExpiryDate"").val()) {
        //        alert(""Expiry date is mandatory."");
        //        return false;
        //    }
        //    //if (!document.forms['frmpassport'].reportValidity()) {
        //    //    if ($(""#passportLastName"").validity.typeMismatch) {
        //    //        $(""#passportLa");
            WriteLiteral(@"stName"").setCustomValidity(""Last name should contian only characters!"");
        //    //    }
        //    //    return false;
        //    //}
        //    $.ajax({
        //        type: ""POST"",
        //        url: '/CandidateProfile/Updatepassport',
        //        data: data,
        //        dataType: ""json"",
        //        success: function (data) {
        //            toastr.success('passport added successfuly.')
        //            var html = '<tr>' +
        //                '<td style=""display:none;"">' + data.id + '</td>' +
        //                '<td style=""display:none;"">' + $('#passportGivenName').val() + '</td>' +
        //                '<td style=""display:none;"">' + $('#passportSurname').val() + '</td>' +
        //                '<td style=""display:none;"">' + $('#passportPlaceOfIssue').val() + '</td>' +
        //                '<td>' + $('#passportNumber').val() + '</td>' +
        //                '<td>' + $('#passportIssueDate').val() + '</td>' +");
            WriteLiteral(@"
        //                '<td>' + $('#passportExpiryDate').val() + '</td>' +
        //                '<td style=""display:none;"">' + $('#passportBirthCountry').val() + '</td>' +
        //                '<td style=""display:none;"">' + $('#passportBirthCity').val() + '</td>' +
        //                '<td><a href=""#"" onclick=""showPassport(""U"",this)""><i class=""fas fa-edit""></i></a><a href=""#"" onclick=""deletePassport('+data.id+')""><i class=""fas fa-trash""></i></a></td>' +
        //                '</tr>';
        //            if ($('#passportId').val() == ""0"") {
        //                $('#passportDetail').append(html);
        //            } else {
        //                //$(""#passportRow"" + $('#passportId').val()).children('td:nth(1)').text($('#passportGivenName').val());
        //                //$(""#passportRow"" + $('#passportId').val()).children('td:nth(2)').text($('#passportSurname').val());
        //                //$(""#passportRow"" + $('#passportId').val()).children('td:nth(3)'");
            WriteLiteral(@").text($('#passportPlaceOfIssue').val());
        //                $(""#passportRow"" + $('#passportId').val()).children('td:nth(4)').text($('#passportNumber').val());
        //                $(""#passportRow"" + $('#passportId').val()).children('td:nth(5)').text($('#passportIssueDate').val());
        //                $(""#passportRow"" + $('#passportId').val()).children('td:nth(6)').text($('#passportExpiryDate').val());
        //                //$(""#passportRow"" + $('#passportId').val()).children('td:nth(7)').text($('#passportBirthCountry').val());
        //                //$(""#passportRow"" + $('#passportId').val()).children('td:nth(8)').text($('#passportBirthCity').val());
        //            }
        //        },
        //        error: function () {
        //            toastr.error('while occured while saving passport.')
        //        }
        //    });
        //    return false;
        //}
        //function deletePassport(passportId) {
        //    var data = { ""Id"": pass");
            WriteLiteral(@"portId};
        //    if (confirm(""Are you sure to delete this passport info?"")) {
        //        if (!passportId || passportId==""0"") {
        //            //Remove line from table only.
        //            $(""#passportRow"" + passportId).remove();
        //        }
        //        else {
        //            $.ajax({
        //                type: ""POST"",
        //                url: '/CandidateProfile/DeletePassport',
        //                data: data,
        //                dataType: ""json"",
        //                success: function (data) {
        //                    toastr.success('passport deleted.')
        //                    $(""#passportRow"" + passportId).remove();
        //                },
        //                error: function () {
        //                    toastr.error('while occured while deleting passport.')
        //                }
        //            });
        //        }
        //    }

        //    return false;
        /");
            WriteLiteral("/}\r\n    </script>");
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
