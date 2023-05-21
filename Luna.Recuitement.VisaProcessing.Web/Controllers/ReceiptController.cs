using Luna.Recruitment.VisaProcessing.Data.DTO;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly lunaContext _context;
        private readonly IOptions<ERPSettings> SMSSettings;

        public ReceiptController(lunaContext context, IOptions<ERPSettings> eRPSettings)
        {
            _context = context;
            ERPSettings = eRPSettings;
        }

        private readonly IOptions<ERPSettings> ERPSettings;
        public async Task<IActionResult> ReceiptVoucher(string ReceiptAgent ="083827772654",string ReceiptFileAgent="")
        {
            if(ReceiptAgent!= "083827772654")
            {
                ViewBag.FileAgentId = ReceiptFileAgent;
                ViewBag.AgentId = ReceiptAgent;
            }
            else
            {
                ViewBag.FileAgentId = 0;
                ViewBag.AgentId = 0;
            }
            List<ReportDto> ENumberModel = new List<ReportDto>();
            ReceiptAgent = ReceiptAgent == null ? "" : ReceiptAgent;
            ReceiptAgent = ReceiptAgent == null ? "" : ReceiptAgent;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ReceiptProcedure";
            command.Parameters.Add(new SqlParameter("@AgentId", ReceiptAgent));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNo"] == DBNull.Value ? "" : Convert.ToString(row["FileNo"]);
                //eNumber.Price = row["Price"] == DBNull.Value ? "0" : Convert.ToString(row["Price"]);
                eNumber.Id = row["CandidateId"] == DBNull.Value ? "" : Convert.ToString(row["CandidateId"]);
                eNumber.visaProcessId = row["visaProcessId"] == DBNull.Value ? "" : Convert.ToString(row["visaProcessId"]);
                eNumber.Name = row["Name_Of_Person"] == DBNull.Value ? "" : Convert.ToString(row["Name_Of_Person"]);
                eNumber.Passport = row["PassportNo"] == DBNull.Value ? "" : Convert.ToString(row["PassportNo"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.CNIC = row["Cnic"] == DBNull.Value ? "" : Convert.ToString(row["Cnic"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.TotalReceived = row["TotalReceived"] == DBNull.Value ? 0 : Convert.ToInt64(row["TotalReceived"]);
                eNumber.TotalReceivable = row["TotalReceivable"] == DBNull.Value ? 0 : Convert.ToInt64(row["TotalReceivable"]);
                //eNumber.TotalExpenses = row["TotalExpenses"] == DBNull.Value ? 0 : Convert.ToInt64(row["TotalExpenses"]);
                eNumber.Balance = (eNumber.TotalReceivable + eNumber.TotalExpenses) - eNumber.TotalReceived;
                ENumberModel.Add(eNumber);
            }
            var agentGlCode = _context.Agent.Where(c => c.Id.ToString() == ReceiptAgent).FirstOrDefault();
            if (agentGlCode != null)
            {
                ViewBag.agentGLCode = agentGlCode.AgentGLCode;
            }
            else
            {
                ViewBag.agentGLCode = null;
            }
            //ViewBag.ReceiptFileNo = FileNumber;
            //ViewBag.ReceiptReference = Reference;
            ViewBag.ReceiptProcedureData = ENumberModel;
            ViewBag.Expences = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Code == "11").ToList(), "Id", "Name", 1);
            return View();


        }
        public IActionResult ReceiptVoucherJV(string FileNumber = "", string Reference = "")
        {
            ViewBag.Expences = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Code == "11").ToList(), "Id", "Name", 1);
            return View();
        }
        public async Task<JsonResult> EnterInJournel(JVRestClass restApiClass)
        {
            //for login
            var url = ERPSettings.Value.URL;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = ERPSettings.Value.usr,
                    pwd = ERPSettings.Value.pwd
                });
                streamWriter.Write(json);
            }
            var strSID = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
            for (int i = 0; i < pl_strResListA.Length; i++)
            {
                var pl_strValueListB = pl_strResListA[i].Split('=');

                if (pl_strValueListB[0].Equals("sid"))
                {
                    strSID = pl_strValueListB[1].ToString();
                    break;
                }
            }
            var client = new RestClient("http://saqib.erpnext.com/api/resource/Journal%20Entry?sid=" + strSID + "");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "text/plain");
            var body = @"{
" + "\n" +
            @"    ""Entry Type"":""Journal Entry"",
" + "\n" +
            @"    ""Series"" : ""ACC-JV-.YYYY.-"",
" + "\n" +
            @"  ""company"": ""LUNA CORPORATION"",
" + "\n" +
            @"  ""posting_date"":""02/02/2022"",
" + "\n" +
            @"  ""total_debit"":1.0,
" + "\n" +
            @"""total_credit"":1.0,
" + "\n" +
            @"""exchange_rate"":1.0,
" + "\n" +
            @"  ""accounts"": [{""account"":""1110 - Cash - LC"",""debit"":0.0,""credit"":1.0,
" + "\n" +
            @"  ""debit_in_account_currency"":0.0,
" + "\n" +
            @"  ""credit_in_account_currency"":1.0
" + "\n" +
            @"  },
" + "\n" +
            @"  {""account"":""1770 - Softwares - LC"",""debit"":1.0,""credit"":0.0,
" + "\n" +
            @"  ""debit_in_account_currency"":1.0,
" + "\n" +
            @"  ""credit_in_account_currency"":0.0}]
" + "\n" +
            @"}";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecutePostAsync(request);
            Console.WriteLine(response.Content);
            return Json(new { response = response });


        }

        public async Task<JsonResult> EnterInPayment(PaymentRestClass restApiClass)
        {
            //for login
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://aliraza.erpnext.com/api/method/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = "engraliraza61@gmail.com",
                    pwd = "Aliraza@786"
                });
                streamWriter.Write(json);
            }
            var strSID = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
            for (int i = 0; i < pl_strResListA.Length; i++)
            {
                var pl_strValueListB = pl_strResListA[i].Split('=');

                if (pl_strValueListB[0].Equals("sid"))
                {
                    strSID = pl_strValueListB[1].ToString();
                    break;
                }
            }
            var client = new RestClient("https://aliraza.erpnext.com/api/resource/Payment%20Entry");
            //client.Timeout = -1;
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("sid", strSID);
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImFsaUBnbWFpbC5jb20iLCJQYXNzd29yZCI6IjEyMyIsImV4cCI6MTYyNTc1ODI5MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDozMDg4MS8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjMwODgxLyJ9.8vBt9rHZFcbsuAoCyp6PY9b1lB4CVTF3GTwWzLHFwMc");
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
            var body = @"{""doctype"":""Payment Entry"",""series"":""ACC-PAY-2021-"",""payment_type"":""Pay"",
" + "\n" +
            @"""Posting Date"":""2022-02-14"",
" + "\n" +
            @"""Company"": ""LUNA CORPORATION"",
" + "\n" +
            @"""paid_from"": ""1110 - Cash - LC"",
" + "\n" +
            @"""Account Currency"":""PKR"",
" + "\n" +
            @"""paid_to"":""2110 - Creditors - LC"",
" + "\n" +
            @"""paid_to_account_currency"":""PKR"",
" + "\n" +
            @"""Exchange Rate"":1.0,
" + "\n" +
            @"""paid_amount"" : 250.0,
" + "\n" +
            @"""received_amount"" : 1.0,
" + "\n" +
            @"""target_exchange_rate"":1.0,
" + "\n" +
            @"""party_type"":""Supplier"",
" + "\n" +
            @"""party"":""luna sup"",
" + "\n" +
            @"""mode_of_payment"":""Cash""
" + "\n" +
            @"}";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecutePostAsync(request);
            Console.WriteLine(response.Content);
            return Json(new { response = response });


        }
        public async Task<JsonResult> GetChartOfAccount()
        {
            //for login
            var url = ERPSettings.Value.URL;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = ERPSettings.Value.usr,
                    pwd = ERPSettings.Value.pwd
                });
                streamWriter.Write(json);
            }
            var strSID = "";
            dynamic jsonA;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            try
            {
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                    for (int j = 0; j < pl_strResListA.Length; j++)
                    {
                        var pl_strValueListB = pl_strResListA[j].Split('=');

                        if (pl_strValueListB[0].Equals("sid"))
                        {
                            strSID = pl_strValueListB[1].ToString();
                            break;
                        }
                    }
                    var client = new RestClient(url + "/api/resource/Account");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Get;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "");
                    request.AddParameter("text/plain", ParameterType.RequestBody);
                    var response = await client.ExecuteGetAsync(request);
                    jsonA = JsonConvert.DeserializeObject(response.Content);
                    Console.WriteLine(response.Content);
                    return Json(new { response = jsonA });
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = ex.Message });
            }
            return Json(httpResponse.StatusCode);

        }
        public async Task<JsonResult> GetChartOfAccountReceipt()
        {
            //for login
            var url = ERPSettings.Value.URL;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = ERPSettings.Value.usr,
                    pwd = ERPSettings.Value.pwd
                });
                streamWriter.Write(json);
            }
            var strSID = "";
            dynamic jsonA;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            try
            {
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                    for (int j = 0; j < pl_strResListA.Length; j++)
                    {
                        var pl_strValueListB = pl_strResListA[j].Split('=');

                        if (pl_strValueListB[0].Equals("sid"))
                        {
                            strSID = pl_strValueListB[1].ToString();
                            break;
                        }
                    }
                    var client = new RestClient(url + "/api/resource/Account?limit_start=1&debug=True&limit=1000&filters=[[%22is_group%22,%22=%22,%22No%22]]");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Get;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "");
                    request.AddParameter("text/plain", ParameterType.RequestBody);
                    var response = await client.ExecuteGetAsync(request);
                    jsonA = JsonConvert.DeserializeObject(response.Content);
                    Console.WriteLine(response.Content);
                    return Json(new { response = jsonA });
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = ex.Message });
            }
            return Json(httpResponse.StatusCode);

        }
        public HttpResponseMessage Get(string url, WebHeaderCollection Header)
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("session-id", "12345");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            //.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }
        public ActionResult GetLogOut(string usr = "", string pwd = "")
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://saqib.erpnext.com/api/method/logout");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = usr,
                    pwd = pwd
                });

                streamWriter.Write(json);
            }
            var result = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
            return Json(new { Message = result });
        }
        public async Task<JsonResult> SavePayments(List<long> lstReceipt, List<long> lstReceiptAmount, List<long> lstVisaProcessId, Receipts receipt, PaymentApiClass apiClass, List<string> File, List<string> Passport, List<long> Agent, List<string> cheqNo, List<string> discription, List<string> account)
        {
            var state = "";
            try
            {
                var GlStatus = "";
                List<string> VoucherNo = new List<string>();
                Agent agent = new Agent();
                state = "a";
                for (int i = 0; i < account.Count; i++)
                {
                    agent = _context.Agent.Where(c => c.Name == account[i]).FirstOrDefault();
                    if (agent != null)
                    {
                        state = "b";
                        if (agent.AgentGLCode == null)
                        {
                            state = "c";
                            return Json(new { response = "Agent GLCode is not defined 'Administration->Agent'", state = state });
                        }
                    }

                    List<Receipt> reportsForAdd = new List<Receipt>();
                    List<Receipt> reportsForUpdate = new List<Receipt>();
                    //for login
                    var url = ERPSettings.Value.URL;
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        state = "d";
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            usr = ERPSettings.Value.usr,
                            pwd = ERPSettings.Value.pwd
                        });
                        streamWriter.Write(json);
                    }
                    var strSID = "";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                    state = "d";
                    for (int j = 0; j < pl_strResListA.Length; j++)
                    {
                        var pl_strValueListB = pl_strResListA[j].Split('=');

                        if (pl_strValueListB[0].Equals("sid"))
                        {
                            strSID = pl_strValueListB[1].ToString();
                            break;
                        }
                    }
                    state = "e";
                    var client = new RestClient(url + "/api/resource/Payment%20Entry");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                    state = "f";
                    apiClass.paid_amount = lstReceiptAmount[i];
                    apiClass.received_amount = lstReceiptAmount[i];
                    apiClass.reference_no = cheqNo[i];
                    apiClass.reference_date = apiClass.Posting_Date;
                    apiClass.Posting_Date = apiClass.Posting_Date;
                    state = "g";
                    apiClass.remarks = discription[i];
                    JsonResult json1 = await GetChartOfAccounts();
                    var jsonObjectA = JsonConvert.SerializeObject(json1);
                    bool isPresent = jsonObjectA.Contains(account[i]);
                    state = "h";
                    if (isPresent)
                    {
                        state = "i";
                        apiClass.paid_to = account[i];
                    }
                    else
                    {
                        state = "j";
                        apiClass.paid_to = agent.AgentGLCode;
                    }
                    state = "k";
                    var body = JsonConvert.SerializeObject(apiClass);
                    state = body;
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    var response = await client.ExecutePostAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                        var VoucherNoA = pl_strResListX[2].ToString();
                        var VoucherNoB = VoucherNoA.Replace(',', ';').Split(';');
                        var VoucherNoC = VoucherNoB[0];
                        var VoucherNoD = RemoveSpecialCharacters(VoucherNoC);
                        VoucherNo.Add(VoucherNoD);
                        GlStatus = "Posted";
                    }
                    else
                    {
                        GlStatus = "Error";
                        return Json(new { response = "Error While Saving Receipt", state = state });
                    }

                }
                var recepts = new Receipts();
                recepts.Date = receipt.Date;
                recepts.Account = apiClass.paid_from;
                state = "n";
                recepts.ReceiptType = receipt.ReceiptType;
                recepts.ReceiptNo = Guid.NewGuid().ToString();
                _context.Receipts.Add(recepts);
                _context.SaveChanges();
                var ReceiptId = _context.Receipts.ToList().LastOrDefault();
                state = "o";
                for (int i = 0; i < VoucherNo.Count; i++)
                {
                    if (lstReceiptAmount[i] > 0)
                    {
                        var oepvisa = _context.OepvisaDemand.Where(c => c.Code == File[i]).FirstOrDefault();
                        var candidate = _context.Passport.Where(c => c.PassportNo == Passport[i]).FirstOrDefault();

                        var receptsDeatil = new ReceiptsDetail();
                        if (agent != null)
                        {
                            receptsDeatil.AgentId = agent.Id;
                        }
                        receptsDeatil.Amount = lstReceiptAmount[i].ToString();
                        receptsDeatil.CandidateProfileId = candidate.CandidateProfileId;
                        receptsDeatil.CheckNo = cheqNo[i];
                        if (oepvisa != null)
                        {
                            receptsDeatil.OepVisaDemandId = oepvisa.Id;
                        }
                        receptsDeatil.VoucherNo = VoucherNo[i];
                        receptsDeatil.GlStatus = GlStatus;
                        receptsDeatil.Description = discription[i];
                        receptsDeatil.ReceiptsId = Convert.ToInt64(ReceiptId.Id);
                        _context.ReceiptsDetail.Add(receptsDeatil);
                        _context.SaveChanges();
                        state = "p";
                    }
                }
                ViewBag.VoucherNo = VoucherNo[0];
                state = VoucherNo[0];
                return Json(new { response = $"Receipt Added successfully-{VoucherNo[0]}-", voucherNo = VoucherNo[0], state = state });
            }
            catch (Exception ex)
            {
                return Json(new { success = "Error While Saving Receipt" });
            }
        }
        public async Task<JsonResult> SavePaymetCombineded(List<long> lstReceipt, List<long> lstReceiptAmount, List<long> lstVisaProcessId, Receipts receipt, PaymentApiClass apiClass, List<string> File, List<string> Passport, List<long> Agent, List<string> cheqNo, List<string> discription, List<string> account)
        {
            bool status = false;
            try
            {
                var VoucherNo = "";
                var GlStatus = "";
                Agent agent = new Agent();
                if (account.Count > 0)
                {
                    agent = _context.Agent.Where(c => c.Name == account[0].ToString()).FirstOrDefault();
                    if (agent != null)
                    {
                        if (agent.AgentGLCode == null)
                        {
                            return Json(new { response = "Agent GLCode is not defined 'Administration->Agent'" });
                        }
                    }
                }
                long sum = lstReceiptAmount.Sum();

                List<Receipt> reportsForAdd = new List<Receipt>();
                List<Receipt> reportsForUpdate = new List<Receipt>();
                //for login
                var url = ERPSettings.Value.URL;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        usr = ERPSettings.Value.usr,
                        pwd = ERPSettings.Value.pwd
                    });
                    streamWriter.Write(json);
                }
                var strSID = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                for (int j = 0; j < pl_strResListA.Length; j++)
                {
                    var pl_strValueListB = pl_strResListA[j].Split('=');

                    if (pl_strValueListB[0].Equals("sid"))
                    {
                        strSID = pl_strValueListB[1].ToString();
                        break;
                    }
                }
                var client = new RestClient(url + "/api/resource/Payment%20Entry");
                //client.Timeout = -1;
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("sid", strSID);
                request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                apiClass.paid_amount = sum;
                apiClass.received_amount = sum;
                apiClass.reference_no = cheqNo[0];
                apiClass.reference_date = apiClass.Posting_Date;
                apiClass.Posting_Date = apiClass.Posting_Date;
                apiClass.user_remark = discription[0];
                JsonResult json1 = await GetChartOfAccounts();
                var jsonObjectA = JsonConvert.SerializeObject(json1);
                bool isPresent = jsonObjectA.Contains(account[0]);
                if (isPresent)
                {
                    apiClass.paid_to = account[0];
                }
                else
                {
                    apiClass.paid_to = agent.AgentGLCode;
                }

                var body = JsonConvert.SerializeObject(apiClass);
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                var response = await client.ExecutePostAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                    VoucherNo = pl_strResListX[2].ToString();
                    var VoucherNoA = VoucherNo.Replace(',', ';').Split(';');
                    VoucherNo = VoucherNoA[0];
                    VoucherNo = RemoveSpecialCharacters(VoucherNo);
                    GlStatus = "Posted";

                }
                else
                {
                    return Json(new { response = "Erro while saving Receipt" });
                    GlStatus = "Error";
                }
                Guid guid = new Guid();
                var recepts = new Receipts();
                recepts.Date = receipt.Date;
                recepts.Account = account[0];
                recepts.ReceiptType = receipt.ReceiptType;
                recepts.ReceiptNo = Guid.NewGuid().ToString();
                _context.Receipts.Add(recepts);
                _context.SaveChanges();
                var ReceiptId = _context.Receipts.ToList().LastOrDefault();
                for (int i = 0; i < lstReceiptAmount.Count; i++)
                {
                    if (lstReceiptAmount[i] > 0)
                    {
                        var oepvisa = _context.OepvisaDemand.Where(c => c.Code == File[i]).FirstOrDefault();
                        var candidate = _context.Passport.Where(c => c.PassportNo == Passport[i]).FirstOrDefault();

                        var receptsDeatil = new ReceiptsDetail();
                        if (agent != null)
                        {
                            receptsDeatil.AgentId = agent.Id;
                        }
                        receptsDeatil.Amount = lstReceiptAmount[i].ToString();
                        receptsDeatil.CandidateProfileId = candidate.CandidateProfileId;
                        receptsDeatil.CheckNo = cheqNo[i];
                        if (oepvisa != null)
                        {
                            receptsDeatil.OepVisaDemandId = oepvisa.Id;
                        }
                        receptsDeatil.VoucherNo = VoucherNo;
                        receptsDeatil.GlStatus = GlStatus;
                        receptsDeatil.Description = discription[i];
                        receptsDeatil.ReceiptsId = Convert.ToInt64(ReceiptId.Id);
                        _context.ReceiptsDetail.Add(receptsDeatil);
                        _context.SaveChanges();
                    }
                }

                return Json(new { response = $"Receipt Added successfully-{VoucherNo}-", voucherNo = VoucherNo });


            }
            catch (Exception ex)
            {
                return Json(new { response = "Erro while saving Receipt" });
            }
        }


        public async Task<JsonResult> SaveReceipt(List<long> lstReceipt, List<long> lstReceiptAmount, List<long> lstVisaProcessId, Receipts receipt, ReceiptApiClass apiClass,List<string>File, List<string> Passport, List<long> Agent,List<string> cheqNo,List<string>discription,List<string> account)
        {
            var state = "";
            try
            {
                var GlStatus = "";
                List<string> VoucherNo = new List<string>();
                Agent agent=new Agent();
                state = "a";
                for (int i = 0; i < account.Count; i++)
                {
                    agent = _context.Agent.Where(c => c.Name == account[i]).FirstOrDefault();
                    if (agent != null)
                    {
                        state = "b";
                        if (agent.AgentGLCode == null)
                        {
                            state = "c";
                            return Json(new { response = "Agent GLCode is not defined 'Administration->Agent'", state = state });
                        }
                    }
                    
                    List<Receipt> reportsForAdd = new List<Receipt>();
                    List<Receipt> reportsForUpdate = new List<Receipt>();
                    //for login
                    var url = ERPSettings.Value.URL;
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        state = "d";
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            usr = ERPSettings.Value.usr,
                            pwd = ERPSettings.Value.pwd
                        });
                        streamWriter.Write(json);
                    }
                    var strSID = "";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                    state = "d";
                    for (int j = 0; j < pl_strResListA.Length; j++)
                    {
                        var pl_strValueListB = pl_strResListA[j].Split('=');

                        if (pl_strValueListB[0].Equals("sid"))
                        {
                            strSID = pl_strValueListB[1].ToString();
                            break;
                        }
                    }
                    state = "e";
                    var client = new RestClient(url + "/api/resource/Payment%20Entry");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                    state = "f";
                    apiClass.paid_amount =lstReceiptAmount[i];
                    apiClass.received_amount = lstReceiptAmount[i];
                    apiClass.reference_no = cheqNo[i];
                    apiClass.reference_date = apiClass.posting_date;
                    state = "g";
                    apiClass.remarks = discription[i];
                    JsonResult json1 =await GetChartOfAccounts();
                    var jsonObjectA = JsonConvert.SerializeObject(json1);
                    bool isPresent=jsonObjectA.Contains(account[i]);
                    state = "h";
                    if (isPresent)
                    {
                        state = "i";
                        apiClass.paid_to = account[i];
                    }
                    else
                    {
                        state = "j";
                        apiClass.paid_to = agent.AgentGLCode;
                    }
                    state = "k";
                    var body = JsonConvert.SerializeObject(apiClass);
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                        var response = await client.ExecutePostAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        state = "l";
                        var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                        var VoucherNoA=pl_strResListX[2].ToString();
                        var VoucherNoB = VoucherNoA.Replace(',', ';').Split(';');
                        var VoucherNoC = VoucherNoB[0];
                        var VoucherNoD=RemoveSpecialCharacters(VoucherNoC);
                        VoucherNo.Add(VoucherNoD);  
                        GlStatus = "Posted";
                    }
                    else
                    {
                        state = "m";
                        GlStatus = "Error";
                        return Json(new { response = "Error While Saving Receipt", state = state });
                    }

                }
                var recepts = new Receipts();
                recepts.Date = receipt.Date;
                recepts.Account = apiClass.paid_from;
                state = "n";
                recepts.ReceiptType = receipt.ReceiptType;
                recepts.ReceiptNo = Guid.NewGuid().ToString();
                _context.Receipts.Add(recepts);
                _context.SaveChanges();
                var ReceiptId = _context.Receipts.ToList().LastOrDefault();
                state = "o";
                for (int i = 0; i < VoucherNo.Count; i++)
                {
                    if (lstReceiptAmount[i] > 0)
                    {
                        var oepvisa = _context.OepvisaDemand.Where(c => c.Code == File[i]).FirstOrDefault();
                        var candidate = _context.Passport.Where(c => c.PassportNo == Passport[i]).FirstOrDefault();

                        var receptsDeatil = new ReceiptsDetail();
                        if (agent != null)
                        {
                            receptsDeatil.AgentId = agent.Id;
                        }
                        receptsDeatil.Amount = lstReceiptAmount[i].ToString();
                        receptsDeatil.CandidateProfileId = candidate.CandidateProfileId;
                        receptsDeatil.CheckNo = cheqNo[i];
                        if (oepvisa != null)
                        {
                            receptsDeatil.OepVisaDemandId = oepvisa.Id;
                        }
                        receptsDeatil.VoucherNo = VoucherNo[i];
                        receptsDeatil.GlStatus = GlStatus;
                        receptsDeatil.Description = discription[i];
                        receptsDeatil.ReceiptsId =Convert.ToInt64(ReceiptId.Id);
                        _context.ReceiptsDetail.Add(receptsDeatil);
                        _context.SaveChanges();
                        state = "p";
                    }
                }
                ViewBag.VoucherNo = VoucherNo[0];
                state = VoucherNo[0];
                return Json(new { response = $"Receipt Added successfully-{VoucherNo[0]}-", voucherNo = VoucherNo[0],state=state });
            }
            catch (Exception ex)
            {
                return Json(new { success = "Error While Saving Receipt" });
            }
        }
        //RomoveSpecialCharacter
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9')
                    || (str[i] >= 'A' && str[i] <= 'z')
                        || (str[i] == '.' || str[i] == '_'
                        ||(str[i]=='-')))
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
        public async Task<JsonResult> GetChartOfAccounts()
        {
            //for login
            var url = ERPSettings.Value.URL;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = ERPSettings.Value.usr,
                    pwd = ERPSettings.Value.pwd
                });
                streamWriter.Write(json);
            }
            var strSID = "";
            dynamic jsonA;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            try
            {
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                    for (int j = 0; j < pl_strResListA.Length; j++)
                    {
                        var pl_strValueListB = pl_strResListA[j].Split('=');

                        if (pl_strValueListB[0].Equals("sid"))
                        {
                            strSID = pl_strValueListB[1].ToString();
                            break;
                        }
                    }
                    var client = new RestClient(url + "/api/resource/Account?limit_start=1&debug=True&limit=1000&filters=[[%22is_group%22,%22=%22,%22No%22]]");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Get;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "");
                    request.AddParameter("text/plain", ParameterType.RequestBody);
                    var response = await client.ExecuteGetAsync(request);
                    jsonA = JsonConvert.DeserializeObject(response.Content);
                    Console.WriteLine(response.Content);
                    return Json(new { response = jsonA });
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = ex.Message });
            }
            return Json(httpResponse.StatusCode);

        }
        public async Task<JsonResult> SaveReceiptCombined(List<long> lstReceipt, List<long> lstReceiptAmount, List<long> lstVisaProcessId, Receipts receipt, ReceiptApiClass apiClass, List<string> File, List<string> Passport, List<long> Agent, List<string> cheqNo, List<string> discription, List<string> account)
        {
            bool status = false;
            try
            {
                var VoucherNo = "";
                var GlStatus = "";
                Agent agent = new Agent();
                if (account.Count > 0)
                {
                    agent = _context.Agent.Where(c => c.Name == account[0].ToString()).FirstOrDefault();
                    if (agent != null)
                    {
                        if (agent.AgentGLCode == null)
                        {
                            return Json(new { response = "Agent GLCode is not defined 'Administration->Agent'" });
                        }
                    }
                }
                    long sum = lstReceiptAmount.Sum();

                List<Receipt> reportsForAdd = new List<Receipt>();
                    List<Receipt> reportsForUpdate = new List<Receipt>();
                    //for login
                    var url = ERPSettings.Value.URL;
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            usr = ERPSettings.Value.usr,
                            pwd = ERPSettings.Value.pwd
                        });
                        streamWriter.Write(json);
                    }
                    var strSID = "";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                    for (int j = 0; j < pl_strResListA.Length; j++)
                    {
                        var pl_strValueListB = pl_strResListA[j].Split('=');

                        if (pl_strValueListB[0].Equals("sid"))
                        {
                            strSID = pl_strValueListB[1].ToString();
                            break;
                        }
                    }
                    var client = new RestClient(url + "/api/resource/Payment%20Entry");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                    apiClass.paid_amount = sum;
                    apiClass.received_amount = sum;
                    apiClass.reference_no = cheqNo[0];
                    apiClass.reference_date = apiClass.posting_date;
                    apiClass.user_remark = discription[0];
                    JsonResult json1 = await GetChartOfAccounts();
                    var jsonObjectA = JsonConvert.SerializeObject(json1);
                    bool isPresent = jsonObjectA.Contains(account[0]);
                    if (isPresent)
                    {
                        apiClass.paid_to = account[0];
                    }
                    else
                    {
                        apiClass.paid_to = agent.AgentGLCode;
                    }

                    var body = JsonConvert.SerializeObject(apiClass);
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    var response = await client.ExecutePostAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                        VoucherNo = pl_strResListX[2].ToString();
                        var VoucherNoA = VoucherNo.Replace(',', ';').Split(';');
                        VoucherNo = VoucherNoA[0];
                        VoucherNo = RemoveSpecialCharacters(VoucherNo);
                        GlStatus = "Posted";

                    }
                    else
                    {
                        return Json(new { response = "Erro while saving Receipt" });
                         GlStatus = "Error";
                    }
                Guid guid = new Guid();
                var recepts = new Receipts();
                recepts.Date = receipt.Date;
                recepts.Account = account[0];
                recepts.ReceiptType = receipt.ReceiptType;
                recepts.ReceiptNo = Guid.NewGuid().ToString();
                _context.Receipts.Add(recepts);
                _context.SaveChanges();
                var ReceiptId = _context.Receipts.ToList().LastOrDefault();
                for (int i = 0; i < lstReceiptAmount.Count; i++)
                {
                    if (lstReceiptAmount[i] > 0)
                    {
                        var oepvisa = _context.OepvisaDemand.Where(c => c.Code == File[i]).FirstOrDefault();
                        var candidate = _context.Passport.Where(c => c.PassportNo == Passport[i]).FirstOrDefault();

                        var receptsDeatil = new ReceiptsDetail();
                        if (agent != null)
                        {
                            receptsDeatil.AgentId = agent.Id;
                        }
                        receptsDeatil.Amount = lstReceiptAmount[i].ToString();
                        receptsDeatil.CandidateProfileId = candidate.CandidateProfileId;
                        receptsDeatil.CheckNo = cheqNo[i];
                        if (oepvisa != null)
                        {
                            receptsDeatil.OepVisaDemandId = oepvisa.Id;
                        }
                        receptsDeatil.VoucherNo = VoucherNo;
                        receptsDeatil.GlStatus = GlStatus;
                        receptsDeatil.Description = discription[i];
                        receptsDeatil.ReceiptsId = Convert.ToInt64(ReceiptId.Id);
                        _context.ReceiptsDetail.Add(receptsDeatil);
                        _context.SaveChanges();
                    }
                }

                return Json(new { response = $"Receipt Added successfully-{VoucherNo}-", voucherNo = VoucherNo });


            }
            catch (Exception ex)
            {
                return Json(new { response = "Erro while saving Receipt" });
            }
        }

        public async Task<bool> SaveReceiptA(ReceiptApiClass apiClass, List<long> lstReceiptAmount, List<DateTime> DateA, List<string> receiptType, List<string> Account_Paid_From, List<long> CheckNo,List<DateTime> CheckNoDate)
        {
            bool status = false;
            try
            {
                //for login
                var url = ERPSettings.Value.URL;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        usr = ERPSettings.Value.usr,
                        pwd = ERPSettings.Value.pwd
                    });
                    streamWriter.Write(json);
                }
                var strSID = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                for (int j = 0; j < pl_strResListA.Length; j++)
                {
                    var pl_strValueListB = pl_strResListA[j].Split('=');

                    if (pl_strValueListB[0].Equals("sid"))
                    {
                        strSID = pl_strValueListB[1].ToString();
                        break;
                    }
                }
                for (int i = 0; i < lstReceiptAmount.Count; i++)
                {
                    var client = new RestClient(url + "/api/resource/Payment%20Entry");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                    apiClass.paid_amount = lstReceiptAmount[i];
                    var postingDate = DateA[i].Year + "-" + DateA[i].Month + "-" + DateA[i].Day;
                    apiClass.posting_date = postingDate;
                    apiClass.mode_of_payment = receiptType[i];
                    apiClass.paid_from = Account_Paid_From[i];
                    if (receiptType[i] == "Bank")
                    {
                        apiClass.reference_no = CheckNo[i].ToString();
                        apiClass.reference_date = CheckNoDate[i].ToString();
                        apiClass.mode_of_payment = "Bank";
                    }
                    var body = JsonConvert.SerializeObject(apiClass);
                    var VoucherNo = "";
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    var response = await client.ExecutePostAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                        VoucherNo = pl_strResListX[2].ToString();
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        //{"reference_date":"2022-09-20T00:00:00","mode_of_payment":"Bank","doctype":"Payment Entry","series":"ACC-PAY-.YYYY.-","Payment_Type":"Receive","posting_date":"2022-9-30","Company":"LUNA CORPORATION","Account_Paid_From":"ABL0010001760700048 SHAHID LATIF - LC","Account_Currency":"PKR","paid_to":"1110 - Cash - LC","paid_to_account_currency":"PKR","Exchange_Rate":1.0,"paid_amount":10.0,"received_amount":10.0,"target_exchange_rate":1.0,"party_type":"Customer","party":"Luan Cust","Mode_of_Payment":null,"reference_no":"987987"}

    public async Task<JsonResult> SavePayment(List<long> lstReceipt, List<long> lstReceiptAmount, List<long> lstVisaProcessId, List<long> EntitySetupId, Expenses receipt, PaymentRestClass restClass)
        {
            bool status = false;
            try
            {
                List<Expenses> reportsForAdd = new List<Expenses>();
                for (int i = 0; i < lstReceipt.Count; i++)
                {
                    var body = JsonConvert.SerializeObject(restClass);
                    var recept = new Expenses();
                    recept.CandidateProfileId = lstReceipt[i];
                    recept.VisaProcessId = lstVisaProcessId[i];
                    recept.EntitySetupId = EntitySetupId[i];
                    recept.Amount = lstReceiptAmount[i];
                    recept.Date = receipt.Date;
                    recept.ReceiptType = receipt.ReceiptType;
                    recept.BankAccoutNo = receipt.BankAccoutNo;
                    recept.CheqNo = receipt.CheqNo;
                    if (lstReceiptAmount[i] > 0)
                    {
                        //for login
                        var url = ERPSettings.Value.URL;
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                usr = ERPSettings.Value.usr,
                                pwd = ERPSettings.Value.pwd
                            });
                            streamWriter.Write(json);
                        }
                        var strSID = "";
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                        for (int j = 0; j < pl_strResListA.Length; j++)
                        {
                            var pl_strValueListB = pl_strResListA[j].Split('=');

                            if (pl_strValueListB[0].Equals("sid"))
                            {
                                strSID = pl_strValueListB[1].ToString();
                                break;
                            }
                        }
                        var client = new RestClient(url + "/api/resource/Payment%20Entry");
                        //client.Timeout = -1;
                        var request = new RestRequest();
                        request.Method = Method.Post;
                        request.AddHeader("sid", strSID);
                        request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                        request.AddHeader("Content-Type", "text/plain");
                        request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                        restClass.paid_amount = Convert.ToInt64(recept.Amount);
                        body = JsonConvert.SerializeObject(restClass);
                        var VoucherNo = "";
                        var GlStatus = "";
                        request.AddParameter("text/plain", body, ParameterType.RequestBody);
                        var response = await client.ExecutePostAsync(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                            VoucherNo = pl_strResListX[2].ToString();
                            GlStatus = "Posted";
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                        Console.WriteLine(response.Content);
                        recept.VoucherNo = VoucherNo;
                        recept.GlStatus = GlStatus;
                        reportsForAdd.Add(recept);
                    }
                    //return Json(new { response = response });

                }
                //if (reportsForUpdate.C
                //{
                //    List<Payment> receiptsA = reportsForUpdate.Where(c => c.Amount != 0).ToList();
                //    _context.Payment.UpdateRange(receiptsA);
                //    _context.SaveChanges();
                //}
                //else if (reportsForAdd.Count > 0)
                //{
                List<Expenses> receiptsA = reportsForAdd.Where(c => c.Amount != 0).ToList();
                _context.Expenses.AddRange(receiptsA);
                _context.SaveChanges();
                //

                
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status });
        }
        public async Task<JsonResult> SavePaymentA(PaymentRestClass restClass,List<long> lstReceiptAmount, List<DateTime> DateA, List<string> receiptType, List<string> paid_from, List<long> CheckNo, List<DateTime> CheckNoDate)
        {
            bool status = false;
            try
            {
                //for login
                var url = ERPSettings.Value.URL;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        usr = ERPSettings.Value.usr,
                        pwd = ERPSettings.Value.pwd
                    });
                    streamWriter.Write(json);
                }
                var strSID = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                for (int j = 0; j < pl_strResListA.Length; j++)
                {
                    var pl_strValueListB = pl_strResListA[j].Split('=');

                    if (pl_strValueListB[0].Equals("sid"))
                    {
                        strSID = pl_strValueListB[1].ToString();
                        break;
                    }
                }
                for (int i = 0; i < lstReceiptAmount.Count; i++)
                {
                    var client = new RestClient(url + "/api/resource/Payment%20Entry");
                    //client.Timeout = -1;
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddHeader("sid", strSID);
                    request.AddHeader("Authorization", "Bearer 9ad3a8ef8f7e0c4:be36d9710e473d3");
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                    restClass.paid_amount = lstReceiptAmount[i];
                    var postingDate = DateA[i].Year + "-" + DateA[i].Month + "-" + DateA[i].Day;
                    restClass.posting_date = postingDate;
                    restClass.mode_of_payment = receiptType[i];
                    restClass.paid_from = paid_from[i];
                    if (receiptType[i] == "Bank")
                    {
                        restClass.reference_no = CheckNo[i];
                        restClass.reference_date = CheckNoDate[i];
                        restClass.mode_of_payment = "Bank";
                    }
                    var body = JsonConvert.SerializeObject(restClass);
                    var VoucherNo = "";
                    var GlStatus = "";
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    var response = await client.ExecutePostAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var pl_strResListX = response.Content.Replace(':', ';').Split(';');
                        VoucherNo = pl_strResListX[2].ToString();
                        GlStatus = "Posted";
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                
                //


            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status });
        }

        public async Task<JsonResult> SaveInvoice(List<long> lstReceipt, List<long> lstReceiptAmount, List<long> lstVisaProcessId)
        {
            bool status = false;
            try
            {
                List<Invoice> reportsForAdd = new List<Invoice>();
                for (int i = 0; i < lstReceipt.Count; i++)
                {
                    var recept = new Invoice();
                    recept.CandidateProfileId = lstReceipt[i];
                    recept.VisaProcessId = lstVisaProcessId[i];
                    recept.Amount = lstReceiptAmount[i];
                    recept.InvoiceDate = DateTime.Now;
                    recept.Status = 1;
                    reportsForAdd.Add(recept);
                }
                List<Invoice> receiptsA = reportsForAdd.Where(c => c.Amount != 0).ToList();
                _context.Invoice.AddRange(receiptsA);
                _context.SaveChanges();

                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status });
        }
        public async Task<bool> SaveJV(JVRestClass apiClass)
        {
            bool status = false;
            try
            {
                //for login
                var url = ERPSettings.Value.URL;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        usr = ERPSettings.Value.usr,
                        pwd = ERPSettings.Value.pwd
                    });
                    streamWriter.Write(json);
                }
                var strSID = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                for (int j = 0; j < pl_strResListA.Length; j++)
                {
                    var pl_strValueListB = pl_strResListA[j].Split('=');

                    if (pl_strValueListB[0].Equals("sid"))
                    {
                        strSID = pl_strValueListB[1].ToString();
                        break;
                    }
                }
                var client = new RestClient(url + "/api/resource/Journal%20Entry");
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                var body = JsonConvert.SerializeObject(apiClass);
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                var response = await client.ExecutePostAsync(request);
                Console.WriteLine(response.Content);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }


        public async Task<bool> SaveJVA(JVRestClass apiClass)
        {
            bool status = false;
            try
            {
                //for login
                var url = ERPSettings.Value.URL;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        usr = ERPSettings.Value.usr,
                        pwd = ERPSettings.Value.pwd
                    });
                    streamWriter.Write(json);
                }
                var strSID = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
                for (int j = 0; j < pl_strResListA.Length; j++)
                {
                    var pl_strValueListB = pl_strResListA[j].Split('=');

                    if (pl_strValueListB[0].Equals("sid"))
                    {
                        strSID = pl_strValueListB[1].ToString();
                        break;
                    }
                }
                    var client = new RestClient(url + "/api/resource/Journal%20Entry");
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "full_name=" + ERPSettings.Value.usr + "; sid=" + strSID + "; system_user=yes; user_id=" + ERPSettings.Value.usr + "; user_image=");
                    var body = JsonConvert.SerializeObject(apiClass);
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    var response = await client.ExecutePostAsync(request);
                    Console.WriteLine(response.Content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        public async Task<JsonResult> toNextTab()
        {
            //for login
            var url = ERPSettings.Value.URL;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/method/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    usr = ERPSettings.Value.usr,
                    pwd = ERPSettings.Value.pwd
                });
                streamWriter.Write(json);
            }
            var strSID = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var pl_strResListA = httpResponse.Headers["Set-Cookie"].Replace(',', ';').Split(';');
            for (int j = 0; j < pl_strResListA.Length; j++)
            {
                var pl_strValueListB = pl_strResListA[j].Split('=');

                if (pl_strValueListB[0].Equals("sid"))
                {
                    strSID = pl_strValueListB[1].ToString();
                    break;
                }
            }


            var client = new RestClient("http://aliraza.erpnext.com/app/home");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", "token bdf6f5deb246b7e:13841c9d1884c39");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("sid", strSID);
            var response = await client.ExecutePostAsync(request);
            Console.WriteLine(response.Content);
            return Json(new { ali = "ali" });
        }
        public JsonResult GetFileByAgentId(int agentId)
        {
            var sql = $@"Select DISTINCT ovd.Code
                From CandidateProfile as cp
                left join CandidateSelectionDetail as csd on csd.CandidateProfileId=cp.Id
                left join VisaProcess as vp on vp.CandidateSelectionDetailId=csd.Id
                left join OEPVisaDemand as ovd on ovd.Id=vp.OEPVisaDemandId
                left join Agent as a on a.Id=cp.AgentId
                where a.Id={agentId}";
            DataTable dt = new DataTable();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                if (sql.StartsWith("SP_"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    command.CommandType = CommandType.Text;
                }
                command.CommandText = sql;
               // command.Parameters.Add(new SqlParameter("candidateProfileId", OEPVisaDemandId));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            List<DataRow> list = dt.AsEnumerable().ToList();
            var listA=list.Select(c => c.ItemArray).ToList();
            return Json(listA);

        }

        public JsonResult GetPassportByFileCode(int fileCode)
        {
            var sql = $@"Select DISTINCT p.PassportNo
                From CandidateProfile as cp
                left join CandidateSelectionDetail as csd on csd.CandidateProfileId=cp.Id
                left join VisaProcess as vp on vp.CandidateSelectionDetailId=csd.Id
                left join OEPVisaDemand as ovd on ovd.Id=vp.OEPVisaDemandId
                left join Agent as a on a.Id=cp.AgentId
				left join Passport as p on p.CandidateProfileId=cp.Id
                where ovd.Code={fileCode}";
            DataTable dt = new DataTable();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                if (sql.StartsWith("SP_"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    command.CommandType = CommandType.Text;
                }
                command.CommandText = sql;
                // command.Parameters.Add(new SqlParameter("candidateProfileId", OEPVisaDemandId));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            List<DataRow> list = dt.AsEnumerable().ToList();
            var listA = list.Select(c => c.ItemArray).ToList();
            return Json(listA);

        }
        public JsonResult GetCandidateNameByPassportId(string passport)
        {
            var sql = $@"Select DISTINCT cp.FirstName+' '+cp.LastName as fullName,r.Amount
                From CandidateProfile as cp
                left join CandidateSelectionDetail as csd on csd.CandidateProfileId=cp.Id
                left join VisaProcess as vp on vp.CandidateSelectionDetailId=csd.Id
                left join OEPVisaDemand as ovd on ovd.Id=vp.OEPVisaDemandId
                left join Agent as a on a.Id=cp.AgentId
				left join Passport as p on p.CandidateProfileId=cp.Id
				left join Receipt as r on r.CandidateProfileId=cp.Id
                where p.PassportNo='{passport}'";
            DataTable dt = new DataTable();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                if (sql.StartsWith("SP_"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    command.CommandType = CommandType.Text;
                }
                command.CommandText = sql;
                // command.Parameters.Add(new SqlParameter("candidateProfileId", OEPVisaDemandId));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            List<DataRow> list = dt.AsEnumerable().ToList();
            var listA = list.Select(c => c.ItemArray).ToList();
            return Json(listA);

        }

    }
}
