using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Luna.Recruitment.VisaProcessing.Data.DTO;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly lunaContext _context;
        public ReportsController(lunaContext context)
        {
            _context = context;
        }
        public IActionResult ENumber(string FileNumber = "", string ENumber = "")
        {
            List<ReportDto> ENumberModel = new List<ReportDto>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ENumber";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            
            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNumber"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Name = row["Name"] == DBNull.Value ? "" : Convert.ToString(row["Name"]);
                eNumber.FatherName = row["FatherName"] == DBNull.Value ? "" : Convert.ToString(row["FatherName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.ENumber = row["ENumber"] == DBNull.Value ? "" : Convert.ToString(row["ENumber"]);
                eNumber.ENumberDate = Convert.ToString(row["ENumberDate"]);
                eNumber.ProtectorDate = Convert.ToString(row["ProtectorDate"]);
                eNumber.BiometricSendingDate = Convert.ToString(row["BiometricSendingDate"]);
                eNumber.BiometricReceivingDate = Convert.ToString(row["BiometricReceivingDate"]);
                eNumber.MedicalOnlineSendingDate = Convert.ToString(row["MedicalOnlineSendingDate"]);
                eNumber.MedicalOnlineReceivingDate = Convert.ToString(row["MedicalOnlineReceivingDate"]);
                eNumber.PassportSubmissionDate = Convert.ToString(row["PassportSubmissionDate"]);
                eNumber.PassportReceivingDate = Convert.ToString(row["PassportReceivingDate"]);
                eNumber.DeploymentDate = Convert.ToString(row["DeploymentDate"]);
                eNumber.Company = Convert.ToString(row["Company"]);
                eNumber.Status = Convert.ToString(row["Status"]);
                eNumber.StatusRemarks = row["StatusRemarks"] == DBNull.Value ? "" : Convert.ToString(row["StatusRemarks"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.RefContact = row["RefContact"] == DBNull.Value ? "" : Convert.ToString(row["RefContact"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }


        public IActionResult ENumberReport(string FileNumber="0", string ENumberDateFrom="", string ENumberDateTo="")
        {
            var oepVisa = _context.OepvisaDemand.Where(e => e.Id.ToString() == FileNumber).FirstOrDefault();
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ENumberReport";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@ENumberDateFrom", ENumberDateFrom));
            command.Parameters.Add(new SqlParameter("@ENumberDateTo", ENumberDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.SelectionGroup = row["SelectionGroup"] == DBNull.Value ? "" : Convert.ToString(row["SelectionGroup"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.ENumber = row["E-Number"] == DBNull.Value ? "" : Convert.ToString(row["E-Number"]);
                eNumber.EDate = row["E-Date"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["E-Date"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }
        //[Authorize(Roles = "Administrator,HR")]
        public IActionResult Receivable(string FileNumber = "1", string ENumberDateFrom = null, string ENumberDateTo = null)
        {

            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ReceivableReport";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@ENumberDateFrom", ENumberDateFrom));
            command.Parameters.Add(new SqlParameter("@ENumberDateTo", ENumberDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.CandidateId = row["CandidateId"] == DBNull.Value ? "" : Convert.ToString(row["CandidateId"]);
                eNumber.VisaProcessId = row["VisaProcessId"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessId"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.selectiongroup = row["selectiongroup"] == DBNull.Value ? "" : Convert.ToString(row["selectiongroup"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.ENumber = row["E-Number"] == DBNull.Value ? "" : Convert.ToString(row["E-Number"]);
                eNumber.TotalReceivable = row["TotalReceivable"] == DBNull.Value ? "" : Convert.ToString(row["TotalReceivable"]);
                eNumber.EDate = row["E-Date"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["E-Date"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }


        public IActionResult SubmittionReport(string FileNumber="1",string SubmittionDateFrom="", string SubmittionDateTo="")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_SubmittionReport";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@SubmittionDateFrom", SubmittionDateFrom));
            command.Parameters.Add(new SqlParameter("@SubmittionDateTo", SubmittionDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.ENumber = row["E-Number"] == DBNull.Value ? "" : Convert.ToString(row["E-Number"]);
                eNumber.Conslate = row["Conslate"] == DBNull.Value ? "" : Convert.ToString(row["Conslate"]);
                eNumber.EDate = row["E-Date"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["E-Date"]);
                eNumber.SubmittionDate = row["SubmissionDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["SubmissionDate"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }

        public IActionResult AIO(string FileNumber = "1", string ENumber = "", string ENumberDateFrom="", string ENumberDateTo="")
        {
            List<ReportDto> ENumberModel = new List<ReportDto>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_AIO";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            command.Parameters.Add(new SqlParameter("@ENumberDateFrom", ENumberDateFrom));
            command.Parameters.Add(new SqlParameter("@ENumberDateTo", ENumberDateTo));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNumber"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Name = row["Name"] == DBNull.Value ? "" : Convert.ToString(row["Name"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.ENumber = row["ENumber"] == DBNull.Value ? "" : Convert.ToString(row["ENumber"]);
                eNumber.ENumberDate = Convert.ToString(row["ENumberDate"]);
                eNumber.BiometricSendingDate = Convert.ToString(row["BiometricSendingDate"]);
                eNumber.BiometricReceivingDate = Convert.ToString(row["BiometricReceivingDate"]);
                eNumber.MedicalOnlineSendingDate = Convert.ToString(row["MedicalOnlineSendingDate"]);
                eNumber.MedicalOnlineReceivingDate = Convert.ToString(row["MedicalOnlineReceivingDate"]);
                eNumber.PassportSubmissionDate = Convert.ToString(row["PassportSubmissionDate"]);
                eNumber.PassportReceivingDate = Convert.ToString(row["PassportReceivingDate"]);
                eNumber.Company = Convert.ToString(row["Company"]);
                eNumber.Status = Convert.ToString(row["Status"]);
                eNumber.StatusRemarks = row["StatusRemarks"] == DBNull.Value ? "" : Convert.ToString(row["StatusRemarks"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }
        //public IActionResult TabReport(string FileNumber = "", string ENumber = "", DateTime? ENumberDateFrom = null, DateTime? ENumberDateTo = null)
        //{
        //    TabReport report = new TabReport();
        //    report.Start = GetReportData("SP_Processing");
        //    report.Biometric = GetReportData("SP_Biometric");
        //    report.MedicalOnline = GetReportData("SP_Medical");
        //    report.Submission = GetReportData("SP_Submission");
        //    report.Endorsement = GetReportData("SP_Endorsement");
        //    report.Protector = GetProtectorForTab("SP_Protector");
        //    report.Deployment = GetReportData("SP_Deployment");
        //    return View(report);
        //}
        public IActionResult TabReport(string FileNumberVisaProcessing)
        {
            FileNumberVisaProcessing = FileNumberVisaProcessing == null ? "" : FileNumberVisaProcessing;
            TabReport report = new TabReport();
            //if (FileNumber == null || FileNumber==Convert.ToString(0))
            //{
            //    report.Start = GetReportData("SP_Processing", FileNumber);
            //    report.Biometric = GetReportData("SP_Biometric", FileNumber);
            //    report.MedicalOnline = GetReportData("SP_Medical", FileNumber);
            //    report.Submission = GetReportData("SP_Submission", FileNumber);
            //    return View(report);
            //}
            //else
            //{
                report.Start = GetReportData("SP_Start", FileNumberVisaProcessing);
                report.ENumber = GetReportData("SP_Processing", FileNumberVisaProcessing);
                report.Biometric = GetReportData("SP_Biometric", FileNumberVisaProcessing);
                report.MedicalOnline = GetReportData("SP_Medical", FileNumberVisaProcessing);
                report.Submission = GetReportData("SP_Submission", FileNumberVisaProcessing);
                //report.Endorsement = GetReportData("SP_Endorsement", oepVisa.Code);
                //report.Protector = GetProtectorForTab("SP_Protector", oepVisa.Code);
                //report.Deployment = GetReportData("SP_Deployment", oepVisa.Code);
                return View(report);
           // }
           
        }
        public IActionResult TabReportForReloading(string FileNumber)
        {
            var oepVisa = _context.OepvisaDemand.Where(e => e.Code == FileNumber).FirstOrDefault();
            return RedirectToAction("TabReport", new { FileNumber = oepVisa.Id });

        }
        public IActionResult Deployment(string FileNumber="")
        {
            TabReport report = new TabReport();
            FileNumber = FileNumber == null ? "" : FileNumber;
                //report.Start = GetReportData("SP_Processing", FileNumber);
                //report.Biometric = GetReportData("SP_Biometric", FileNumber);
                //report.MedicalOnline = GetReportData("SP_Medical", FileNumber);
                //report.Submission = GetReportData("SP_Submission", FileNumber);
                report.Endorsement = GetReportData("SP_Endorsement", FileNumber);
                report.Protector = GetProtectorForTab("SP_Protector", FileNumber);
                report.Deployment = GetReportData("SP_Deployment", FileNumber);
                report.TravelBooking = GetTravelData("SP_TravlBookings", FileNumber);
                report.CaseCloure = GetCloureData("SP_CaseCloure", FileNumber);
                return View(report);
           
        }
        public IActionResult DeploymentForReloading(string FileNumber)
        {
            var oepVisa = _context.OepvisaDemand.Where(e => e.Code == FileNumber).FirstOrDefault();
            return RedirectToAction("Deployment", new { FileNumber = oepVisa.Id });

        }
        public IActionResult SelectionReportForReloading(string FileNumber)
        {
            var oepVisa = _context.OepvisaDemand.Where(e => e.Id.ToString() == FileNumber).FirstOrDefault();
            return RedirectToAction("SelectionReport", new { FileNumber = oepVisa.Code });

        }
        public IActionResult FileProcessingDropdown(string FileNumber = "", string ProcessingDateFrom = "", string ProcessingDateTo = "")
        {
            var oepVisa = _context.OepvisaDemand.Where(e => e.Id.ToString() == FileNumber).FirstOrDefault();
            return RedirectToAction("FileProcessing", new { FileNumber = oepVisa.Code, ProcessingDateFrom= ProcessingDateFrom, ProcessingDateTo= ProcessingDateTo });

        }
        [HttpPost]
        public ActionResult TabReportForUpdate(ReportDto oep)
        {
            TabReport report = new TabReport();
            report.Start = GetReportData("SP_Processing", oep.FileNumber);
            report.Biometric = GetReportData("SP_Biometric", oep.FileNumber);
            report.MedicalOnline = GetReportData("SP_Medical", oep.FileNumber);
            report.Submission = GetReportData("SP_Submission", oep.FileNumber);
            report.Endorsement = GetReportData("SP_Endorsement", oep.FileNumber);
            report.Protector = GetProtectorForTab("SP_Protector", oep.FileNumber);
            report.Deployment = GetReportData("SP_Deployment", oep.FileNumber);
            return Json(report);
        }


        List<ReportDto> GetReportData(string reportSP,string FileNumber)
        {
            List<ReportDto> reportDataModel = new List<ReportDto>();
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = reportSP;
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNumber"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Id = row["Id"] == DBNull.Value ? "" : Convert.ToString(row["Id"]);
                eNumber.visaProcessId = row["visaProcessId"] == DBNull.Value ? "" : Convert.ToString(row["visaProcessId"]);
                eNumber.Name = row["Name"] == DBNull.Value ? "" : Convert.ToString(row["Name"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.ENumber = row["ENumber"] == DBNull.Value ? "" : Convert.ToString(row["ENumber"]);
                eNumber.ENumberDate = Convert.ToString(row["ENumberDate"]);
                eNumber.BiometricSendingDate = Convert.ToString(row["BiometricSendingDate"]);
                eNumber.BiometricReceivingDate = Convert.ToString(row["BiometricReceivingDate"]);
                eNumber.MedicalOnlineSendingDate = Convert.ToString(row["MedicalOnlineSendingDate"]);
                if (reportSP == "SP_Processing")
                {
                    eNumber.TradeId =Convert.ToInt32(row["TradeId"]);
                }
                if (reportSP == "SP_Start")
                {
                    eNumber.ProcessingStartDate = Convert.ToString(row["ProcessingStartDate"]);
                }
                eNumber.MedicalOnlineReceivingDate = Convert.ToString(row["MedicalOnlineReceivingDate"]);
                eNumber.PassportSubmissionDate = Convert.ToString(row["PassportSubmissionDate"]);
                eNumber.PassportReceivingDate = Convert.ToString(row["PassportReceivingDate"]);
                eNumber.Company = Convert.ToString(row["Company"]);
                eNumber.Status = Convert.ToString(row["Status"]);
                eNumber.StatusRemarks = row["StatusRemarks"] == DBNull.Value ? "" : Convert.ToString(row["StatusRemarks"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                reportDataModel.Add(eNumber);
            }

            return reportDataModel;
        }

        List<ReportDto> GetTravelData(string reportSP, string FileNumber)
        {
            List<ReportDto> reportDataModel = new List<ReportDto>();
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = reportSP;
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNumber"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Id = row["Id"] == DBNull.Value ? "" : Convert.ToString(row["Id"]);
                eNumber.visaProcessId = row["visaProcessId"] == DBNull.Value ? "" : Convert.ToString(row["visaProcessId"]);
                eNumber.Name = row["Name"] == DBNull.Value ? "" : Convert.ToString(row["Name"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.FlightNumber = Convert.ToString(row["FlightNumber"]);
                eNumber.TicketNumber = Convert.ToString(row["TicketNumber"]);
                eNumber.SectorFrom = Convert.ToString(row["SectorFrom"]);
                eNumber.SectorTO = Convert.ToString(row["SectorTo"]);
                eNumber.ArivalDate = Convert.ToString(row["ArrivalDate"]);
                eNumber.DepartureDate = Convert.ToString(row["DepartureDate"]);
                reportDataModel.Add(eNumber);
            }

            return reportDataModel;
        }

        List<ReportDto> GetCloureData(string reportSP, string FileNumber)
        {
            List<ReportDto> reportDataModel = new List<ReportDto>();
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = reportSP;
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNumber"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Id = row["Id"] == DBNull.Value ? "" : Convert.ToString(row["Id"]);
                eNumber.visaProcessId = row["visaProcessId"] == DBNull.Value ? "" : Convert.ToString(row["visaProcessId"]);
                eNumber.Name = row["Name"] == DBNull.Value ? "" : Convert.ToString(row["Name"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.DocumentHandOverDate = Convert.ToString(row["DocumentHandoverDate"]);
                eNumber.DateOfCloure = Convert.ToString(row["DateOfCloure"]);
                reportDataModel.Add(eNumber);
            }

            return reportDataModel;
        }

        List<ReportDto> GetProtectorForTab(string reportSP,string FileNumber)
        {
            List<ReportDto> reportDataModel = new List<ReportDto>();
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = reportSP;
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            
            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.visaProcessId = row["visaProcessId"] == DBNull.Value ? "" : Convert.ToString(row["visaProcessId"]);
                eNumber.FileNumber = row["FileNumber"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Name = row["Name"] == DBNull.Value ? "" : Convert.ToString(row["Name"]);
                eNumber.RegistrationDate = row["RegistrationDate"] == DBNull.Value ? "" : Convert.ToString(row["RegistrationDate"]);
                eNumber.RegistrationNo = row["RegistrationNumber"] == DBNull.Value ? "" : Convert.ToString(row["RegistrationNumber"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.ENumber = row["ENumber"] == DBNull.Value ? "" : Convert.ToString(row["ENumber"]);
                eNumber.ENumberDate = Convert.ToString(row["ENumberDate"]);
                eNumber.BiometricSendingDate = Convert.ToString(row["BiometricSendingDate"]);
                eNumber.BiometricReceivingDate = Convert.ToString(row["BiometricReceivingDate"]);
                eNumber.MedicalOnlineSendingDate = Convert.ToString(row["MedicalOnlineSendingDate"]);
                eNumber.MedicalOnlineReceivingDate = Convert.ToString(row["MedicalOnlineReceivingDate"]);
                eNumber.PassportSubmissionDate = Convert.ToString(row["PassportSubmissionDate"]);
                eNumber.PassportReceivingDate = Convert.ToString(row["PassportReceivingDate"]);
                eNumber.Company = Convert.ToString(row["Company"]);
                eNumber.Status = Convert.ToString(row["Status"]);
                eNumber.StatusRemarks = row["StatusRemarks"] == DBNull.Value ? "" : Convert.ToString(row["StatusRemarks"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                reportDataModel.Add(eNumber);
            }

            return reportDataModel;
        }
        public IActionResult FileProcessing(string FileNumber = "", string ProcessingDateFrom = "01/01/2022", string ProcessingDateTo = "01/01/2022")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_Final_Processing_For_Approval";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            command.Parameters.Add(new SqlParameter("@ProcessingDateFrom", ProcessingDateFrom));
            command.Parameters.Add(new SqlParameter("@ProcessingDateTo", ProcessingDateTo));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ?  0: Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.StartingDate = row["StartingDate"] == DBNull.Value? Convert.ToDateTime(null): Convert.ToDateTime(row["StartingDate"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }
        public IActionResult BankRefund(string FileNumber =null, string CompanyName="0", string BankRefundDateFrom = "01/01/2022", string BankRefundDateTo = "01/01/2022")  
        {
            if (CompanyName is null)
            {
                CompanyName = "0";
            }
            //BankRefundDateFrom = BankRefundDateFrom == new DateTime() ? DateTime.Now : BankRefundDateFrom;
            //BankRefundDateTo = BankRefundDateTo == new DateTime() ? DateTime.Now : BankRefundDateTo;
            //var BankRefundDateFromA = BankRefundDateFrom.ToString("G");
            //var BankRefundDateToA = BankRefundDateTo.ToString("G");
            //List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //DateTime DateFrom = Convert.ToDateTime((BankRefundDateFromA));
            //DateTime DateTo = Convert.ToDateTime(BankRefundDateToA);
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_Bank_Certificate_Refund_Report";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@CompanyName", CompanyName));
            command.Parameters.Add(new SqlParameter("@BankRefundDateFrom", BankRefundDateFrom));
            command.Parameters.Add(new SqlParameter("@BankRefundDateTo", BankRefundDateTo));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.PermissionNumebr = row["PermissionNumber"] == DBNull.Value ? 0 : Convert.ToInt32(row["PermissionNumber"]);
                eNumber.PermissionNumebr = row["PermissionNumber"] == DBNull.Value ? 0 : Convert.ToInt32(row["PermissionNumber"]);
                eNumber.PermissionDate = row["PermissionDate"] == DBNull.Value ? "" : Convert.ToString(row["PermissionDate"]);
                eNumber.RegistrationNumber = row["RegistrationNumber"] == DBNull.Value ? "" : Convert.ToString(row["RegistrationNumber"]);
                eNumber.PermissionDate = row["PermissionDate"] == DBNull.Value ? "" : Convert.ToString(row["PermissionDate"]);
                eNumber.NameOfPerson = row["Name_Of_Person"] == DBNull.Value ? "" : Convert.ToString(row["Name_Of_Person"]);
                eNumber.PassportNo = row["PassportNo"] == DBNull.Value ? "" : Convert.ToString(row["PassportNo"]);
                eNumber.Registrationdates = row["RegistrationDate"] == DBNull.Value ? "" : Convert.ToString(row["RegistrationDate"]);
                eNumber.DepartureDate = row["DepartureDate1"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["DepartureDate1"]);
                eNumber.Sector = row["SECTOR"] == DBNull.Value ? "" : Convert.ToString(row["SECTOR"]);
                eNumber.FlightNumber = row["FlightNumber1"] == DBNull.Value ? "" : Convert.ToString(row["FlightNumber1"]);
                eNumber.TicketNumber = row["TicketNumber1"] == DBNull.Value ? "" : Convert.ToString(row["TicketNumber1"]);
                eNumber.FileNo = row["FileNo"] == DBNull.Value ? "" : Convert.ToString(row["FileNo"]);
               
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }

        public IActionResult VarificationClosing(string FileNumber="", string ProcessingDateFrom="", string ProcessingDateTo="")
        {
                List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ACCOUNT_VERIFICATION_CLOSING_REPORT";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@VarificationDateFrom", ProcessingDateFrom));
            command.Parameters.Add(new SqlParameter("@VarificationDateTo", ProcessingDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
    
            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.Consulate = row["counslate"] == DBNull.Value ? "" : Convert.ToString(row["counslate"]);
                eNumber.OEP = row["OEP"] == DBNull.Value ? "" : Convert.ToString(row["OEP"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.Sector = row["Sector"] == DBNull.Value ? "" : Convert.ToString(row["Sector"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.Registration = row["Registration"] == DBNull.Value ? "" : Convert.ToString(row["Registration"]);
                eNumber.RegistrationDate = row["RegistrationDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["RegistrationDate"]);
                eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);
                eNumber.StatusRemarks = row["StatusRemarks"] == DBNull.Value ? "" : Convert.ToString(row["StatusRemarks"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }

        public IActionResult PassportProtector(string FileNumber="1", string PassportDateFrom="", string PassportDateTo="")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ProtectorReport";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@ProtectorDateFrom", PassportDateFrom));
            command.Parameters.Add(new SqlParameter("@ProtectorDateTo", PassportDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.Consulate = row["counslate"] == DBNull.Value ? "" : Convert.ToString(row["counslate"]);
                eNumber.OEP = row["OEP"] == DBNull.Value ? "" : Convert.ToString(row["OEP"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.Registration = row["Registration"] == DBNull.Value ? "" : Convert.ToString(row["Registration"]);
                eNumber.RegistrationDate = row["RegistrationDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["RegistrationDate"]);
                eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);
                eNumber.CnicNo = row["CNIC"] == DBNull.Value ? "" : Convert.ToString(row["CNIC"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }
        public IActionResult PassportFromCunslate(string FileNumber="1", string ProcessingDateFrom="", string ProcessingDateTo="")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_PassportRecievedFromConsulate";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@PassportReceivedDateFrom", ProcessingDateFrom));
            command.Parameters.Add(new SqlParameter("@PassportReceivedDateTo", ProcessingDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.Consulate = row["counslate"] == DBNull.Value ? "" : Convert.ToString(row["counslate"]);
                eNumber.OEP = row["OEP"] == DBNull.Value ? "" : Convert.ToString(row["OEP"]);
                eNumber.NameOfCandidate = row["NameOfCandidate"] == DBNull.Value ? "" : Convert.ToString(row["NameOfCandidate"]);
                eNumber.FatherName = row["FathersName"] == DBNull.Value ? "" : Convert.ToString(row["FathersName"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.Profession = row["Profession"] == DBNull.Value ? "" : Convert.ToString(row["Profession"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.ENumber = row["E-Number"] == DBNull.Value ? "" : Convert.ToString(row["E-Number"]);
                eNumber.EDate = row["E-Date"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["E-Date"]);
                eNumber.SubmittionDate = row["SubmissionDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["SubmissionDate"]);
                eNumber.ReceivingDate = row["ReceivingDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["ReceivingDate"]);
                eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }

        public IActionResult SelectionReport(string FileNumber = "1")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == "0" ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Selection_Report";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            //command.Parameters.Add(new SqlParameter("@PassportReceivedDateFrom", ProcessingDateFrom));
            //command.Parameters.Add(new SqlParrameter("@PassportReceivedDateTo", ProcessingDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileNo"] == DBNull.Value ? "" : Convert.ToString(row["FileNo"]);
                eNumber.NameOfCandidate = row["Name_Of_Person"] == DBNull.Value ? "" : Convert.ToString(row["Name_Of_Person"]);
                eNumber.FatherName = row["FatherName"] == DBNull.Value ? "" : Convert.ToString(row["FatherName"]);
                eNumber.Passport = row["Passport#"] == DBNull.Value ? "" : Convert.ToString(row["Passport#"]);
                eNumber.SelectionTrade = row["SelectionTrade"] == DBNull.Value ? "" : Convert.ToString(row["SelectionTrade"]);
                eNumber.Salary = row["Salary"] == DBNull.Value ? "" : Convert.ToString(row["Salary"]);
                eNumber.Trade = row["VisaTrade"] == DBNull.Value ? "" : Convert.ToString(row["VisaTrade"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.EDate = row["E-Date"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["E-Date"]);
                eNumber.Contact = row["Contact"] == DBNull.Value ? "" : Convert.ToString(row["Contact"]);
                eNumber.FoodDetail = row["FoodDetail"] == DBNull.Value ? "" : Convert.ToString(row["FoodDetail"]);
                eNumber.StatusRemarks = row["CurrentStatus"] == DBNull.Value ? "" : Convert.ToString(row["CurrentStatus"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.SelectionGroup = row["selectiongroup"] == DBNull.Value ? "" : Convert.ToString(row["selectiongroup"]);
                ENumberModel.Add(eNumber);
            }
            return View(ENumberModel);
        }
        public IActionResult ReservationReport(string FileNumber="1", string ProcessingDateFrom="", string ProcessingDateTo="")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_ReservationReport";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@ReservationDateFrom", ProcessingDateFrom));
            command.Parameters.Add(new SqlParameter("@ReservationDateTo", ProcessingDateTo));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Sr = row["Sr"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sr"]);
                eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                eNumber.Consulate = row["counslate"] == DBNull.Value ? "" : Convert.ToString(row["counslate"]);
                eNumber.OEP = row["OEP"] == DBNull.Value ? "" : Convert.ToString(row["OEP"]);
                eNumber.GivenName = row["GivenName"] == DBNull.Value ? "" : Convert.ToString(row["GivenName"]);
                eNumber.SurName = row["Surname"] == DBNull.Value ? "" : Convert.ToString(row["Surname"]);
                eNumber.FatherName = row["FatherName"] == DBNull.Value ? "" : Convert.ToString(row["FatherName"]);
                eNumber.DateOfBirth = row["DateOfBirth"] == DBNull.Value ? "" : Convert.ToString(row["DateOfBirth"]);
                eNumber.CnicNo = row["CnicNO"] == DBNull.Value ? "" : Convert.ToString(row["CnicNO"]);
                eNumber.Passport = row["Passport"] == DBNull.Value ? "" : Convert.ToString(row["Passport"]);
                eNumber.DateOfIsse = row["DateOfIssue"] == DBNull.Value ? "" : Convert.ToString(row["DateOfIssue"]);
                eNumber.DateOfExpiry = row["DateOfExpiry"] == DBNull.Value ? "" : Convert.ToString(row["DateOfExpiry"]);
                eNumber.Sector = row["SECTOR"] == DBNull.Value ? "" : Convert.ToString(row["SECTOR"]);
                eNumber.Company = row["Company"] == DBNull.Value ? "" : Convert.ToString(row["Company"]);
                eNumber.SelectionGroup = row["SelectionGroup"] == DBNull.Value ? "" : Convert.ToString(row["SelectionGroup"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);

                eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);
                eNumber.StatusRemarks = row["StatusRemarks"] == DBNull.Value ? "" : Convert.ToString(row["StatusRemarks"]);
                eNumber.VisaStampingDate = row["VisaStampingDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["VisaStampingDate"]);
                eNumber.VisaExpiryDate = row["VisaExpiryDate"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["VisaExpiryDate"]);
                //eNumber.VaccineDose = row["VaccineDose"] == DBNull.Value ? "" : Convert.ToString(row["VaccineDose"]);
                //eNumber.VaccineDate = row["VaccineDate"] == DBNull.Value ? "" : Convert.ToString(row["VaccineDate"]);
                //eNumber.VaccineName = row["VaccineName"] == DBNull.Value ? "" : Convert.ToString(row["VaccineName"]);
                ENumberModel.Add(eNumber);
            }   
            return View(ENumberModel);
        }

        public IActionResult FileHistoryReport(string FileNumber="1")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "File_History_Report";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.FileNo = row["FileNo"] == DBNull.Value ? "" : Convert.ToString(row["FileNo"]);
                eNumber.VisaQuantity = row["VisaQty"] == DBNull.Value ? "" : Convert.ToString(row["VisaQty"]);
                eNumber.NameOfPerson = row["Name_Of_Person"] == DBNull.Value ? "" : Convert.ToString(row["Name_Of_Person"]);
                eNumber.PassportNo = row["PassportNo"] == DBNull.Value ? "" : Convert.ToString(row["PassportNo"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);
               
                ENumberModel.Add(eNumber);
            }
            DataTable dtA = new DataTable();
            SqlCommand commandA = new SqlCommand();
            commandA.CommandType = CommandType.StoredProcedure;
            commandA.CommandText = "SP_FileHisyoryReportSummery";
            commandA.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            commandA.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter daA = new SqlDataAdapter(commandA);
            daA.Fill(dtA);
            FileHistorySummary fileHistory = new FileHistorySummary();
            List<FileHistorySummary> fileHistorylist = new List<FileHistorySummary>();
            foreach (DataRow row in dtA.Rows)
            {
                
                fileHistory.Trade = row["Trade"] == DBNull.Value ? "0" : Convert.ToString(row["Trade"]);
                fileHistory.Total = row["Total"] == DBNull.Value ? "0" : Convert.ToString(row["Total"]);
                 fileHistory.UnderProcessing = row["UnderProcessing"] == DBNull.Value ? "0" : Convert.ToString(row["UnderProcessing"]);
                fileHistorylist.Add(fileHistory);
                
            }
            if (dtA.Rows.Count == 0)
            {
                fileHistory.Trade = "0";
                fileHistory.Total = "0";
                fileHistory.UnderProcessing = "0";
                fileHistorylist.Add(fileHistory);
            }
            //if (fileHistory.Total == null && fileHistory.Trade == null && fileHistory.UnderProcessing == null)
            //{
            //    fileHistory.Total = "null";
            //    fileHistory.Trade = "null";
            //    fileHistory.UnderProcessing = "null";
            //}
            //else if (fileHistory.Trade == null&& fileHistory.UnderProcessing == null)
            //{
            //    fileHistory.Trade = "null";
            //    fileHistory.UnderProcessing = "null";
            //}
            //else if(fileHistory.Trade==null&& fileHistory.Total==null)
            //{
            //    fileHistory.Trade = "null";
            //    fileHistory.Total = "null";
            //}
            //else if (fileHistory.Total == null &&fileHistory.UnderProcessing == null)
            //{
            //    fileHistory.Total = "null";
            //    fileHistory.UnderProcessing = "null";
            //}
            //else if (fileHistory.UnderProcessing == null)
            //{
            //    fileHistory.UnderProcessing="null";
            //}
            //else if (fileHistory.Total == null)
            //{
            //    fileHistory.Total = "null";
            //}
            //else if (fileHistory.Trade == null)
            //{
            //    fileHistory.Trade = "null";
            //}
            //List <FileHistorySummary> fileHistorySummaries = fileHistorylist;
            //ViewBag.files= Json(fileHistorylist); 
            return View(ENumberModel);
        }
        public JsonResult GetInformation(string visaNumber)
        {
            try
            {
                List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetInformation";
                command.Parameters.Add(new SqlParameter("@Code", visaNumber));
                //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
                _context.Database.SetCommandTimeout(0);
                command.Connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    var eNumber = new FinalProcessing();

                    eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                    eNumber.ArabicName = row["ArabicName"] == DBNull.Value ? "" : Convert.ToString(row["ArabicName"]);
                    eNumber.VisaQuantity = row["Quantity"] == DBNull.Value ? "" : Convert.ToString(row["Quantity"]);
                    ENumberModel.Add(eNumber);
                }
                return Json(ENumberModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult FileHistorySummary(string FileNumber = "1")
        {
            List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_FileHisyoryReportSummery";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new FinalProcessing();
                eNumber.Total = row["Total"] == DBNull.Value ? "" : Convert.ToString(row["Total"]);
                eNumber.UnderProcessing = row["UnderProcessing"] == DBNull.Value ? "" : Convert.ToString(row["UnderProcessing"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);

                ENumberModel.Add(eNumber);
            }
            return Json(ENumberModel);
        }

        public IActionResult BulkSelectionInsert(string SponsorIdA, string AddToCandidateBulkSelection,string SelectionGroupA)
        {
            CandidateSelection candidate = _context.CandidateSelection.Where(c => c.SponserId == Convert.ToInt64(SponsorIdA) && c.Id== Convert.ToInt64(SelectionGroupA)).FirstOrDefault();
            if (Convert.ToInt64(SponsorIdA) == 0)
            {
                return RedirectToAction("SelectionCandidate", "CandidateSelection",new {SponsorId= SponsorIdA });
            }
            List<CandidateBulkSelection> ENumberModel = new List<CandidateBulkSelection>();
            //FileNumber = FileNumber == null ? "" : FileNumber;
            //VisaNumber = VisaNumber == null ? "" : VisaNumber;
            //ENumber = ENumber == null ? "" : ENumber;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BulkSelectionInsert";
            command.Parameters.Add(new SqlParameter("@SponserId", candidate.Id));
            command.Parameters.Add(new SqlParameter("@BatchNumber", AddToCandidateBulkSelection));
            //command.Parameters.Add(new SqlParameter("@BatchNumber", AddToCandidateBulkSelection));
            //command.Parameters.Add(new SqlParameter("@FatherName", candidateBulk.FatherName));
            //command.Parameters.Add(new SqlParameter("@MaritalStatus", candidateBulk.MaritalStatus));
            //command.Parameters.Add(new SqlParameter("@Gender", candidateBulk.Gender));
            //command.Parameters.Add(new SqlParameter("@CNICNumber", candidateBulk.CNICNumber));
            //command.Parameters.Add(new SqlParameter("@CNCIIssueDate", candidateBulk.CNCIIssueDate));
            //command.Parameters.Add(new SqlParameter("@CNICExpiryDate", candidateBulk.CNICExpiryDate));
            //command.Parameters.Add(new SqlParameter("@DateofBirth", candidateBulk.DateOfBirth));
            //command.Parameters.Add(new SqlParameter("@BirthCountry", candidateBulk.BirthCountry));
            //command.Parameters.Add(new SqlParameter("@PermanentAddress", candidateBulk.PermanentAddress));
            //command.Parameters.Add(new SqlParameter("@CurrentAddress", candidateBulk.CurrentAddress));
            //command.Parameters.Add(new SqlParameter("@Religion", candidateBulk.Religion));
            //command.Parameters.Add(new SqlParameter("@Cell", candidateBulk.Cell));
            //command.Parameters.Add(new SqlParameter("@PassportNo", candidateBulk.PassportNo));
            //command.Parameters.Add(new SqlParameter("@PassportIssueDate", candidateBulk.PassportIssueDate));
            //command.Parameters.Add(new SqlParameter("@SelectionTrade", candidateBulk.SelectionTrade));
            //command.Parameters.Add(new SqlParameter("@Salary", candidateBulk.Salary));
            //command.Parameters.Add(new SqlParameter("@ContrctDurationType", candidateBulk.ContrctDurationType));
            //command.Parameters.Add(new SqlParameter("@ContrctDuration", candidateBulk.ContrctDuration));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new CandidateBulkSelection();
                //eNumber.FileNo = row["FileNo"] == DBNull.Value ? "" : Convert.ToString(row["FileNo"]);
                //eNumber.VisaQuantity = row["VisaQty"] == DBNull.Value ? "" : Convert.ToString(row["VisaQty"]);
                //eNumber.NameOfPerson = row["Name_Of_Person"] == DBNull.Value ? "" : Convert.ToString(row["Name_Of_Person"]);
                //eNumber.PassportNo = row["PassportNo"] == DBNull.Value ? "" : Convert.ToString(row["PassportNo"]);
                //eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                //eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);

                ENumberModel.Add(eNumber);
            }
            return RedirectToAction("SelectionCandidate","CandidateSelection");
        }

        public IActionResult ReceiptFiles(string FileNumber = "",string Reference= "")
        {
            List<Receipt> ENumberModel = new List<Receipt>();
            FileNumber = FileNumber == null ? "" : FileNumber;
            Reference = Reference == null ? "" : Reference;
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ReceiptProcedure";
            command.Parameters.Add(new SqlParameter("@FileNumber", FileNumber));
            command.Parameters.Add(new SqlParameter("@Reference", Reference));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var eNumber = new ReportDto();
                eNumber.FileNumber = row["FileNo"] == DBNull.Value ? "" : Convert.ToString(row["FileNumber"]);
                eNumber.Id = row["CandidateId"] == DBNull.Value ? "" : Convert.ToString(row["CandidateId"]);
                eNumber.Name = row["Name_Of_Person"] == DBNull.Value ? "" : Convert.ToString(row["Name_Of_Person"]);
                eNumber.Passport = row["PassportNo"] == DBNull.Value ? "" : Convert.ToString(row["PassportNo"]);
                eNumber.Reference = row["Reference"] == DBNull.Value ? "" : Convert.ToString(row["Reference"]);
                eNumber.CNIC = row["Cnic"] == DBNull.Value ? "" : Convert.ToString(row["Cnic"]);
                eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                
               
            }
            ViewBag.ReceiptProcedureData = ENumberModel;
            return View("ReceiptVoucher", "Receipt");
        }



        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateStartTab(UpdateStartTabReportDTO detail)
        {
            var Massage = "SUCCESS";
            try
            {
                VisaProcess newList = _context.VisaProcess.Find(detail.Id); 
                var candidateSelectionDetailId = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == detail.CandidateId).FirstOrDefault();
                if (newList == null)
                {
                    VisaProcess visaProcess = new VisaProcess();
                    visaProcess.CandidateSelectionDetailId = candidateSelectionDetailId.Id;
                    visaProcess.VisaTradeEntitySetupId = detail.TradeId;
                }
                newList.Enumber = detail.ENumber;
                newList.EnumberDate = detail.ENumberDate;
                newList.StatusRemarks = detail.eNumberRemarks;
                _context.VisaProcess.Update(newList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(Massage);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateProcessingTab(UpdateProcessingTabReportDTO detail)
        {
            var Massage = "SUCCESS";
            try
            {
                VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                var candidateSelectionDetailId = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == detail.CandidateId).FirstOrDefault();
                if (newList == null)
                {
                    VisaProcess visaProcess = new VisaProcess();
                    visaProcess.CandidateSelectionDetailId = candidateSelectionDetailId.Id;
                    visaProcess.VisaTradeEntitySetupId = detail.TradeId;
                }
                newList.ProcessingStartDate =Convert.ToDateTime(detail.ProcessingStartDate);
                _context.VisaProcess.Update(newList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(Massage);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateBiometricTab(UpdateBiometricTabReportDTO detail)
        {
            var Massage="SUCCESS";
            try
            {
                VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                newList.BiometricReceivingDate = detail.BiometricReceivingDate;
                newList.BiometricSendingDate = detail.BiometricSendingDate;
                newList.StatusRemarks = detail.biometricRemarks;


                _context.VisaProcess.Update(newList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(Massage);
        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateMedicalTab(UpdateMedicalTabReportDTO detail)
        {
            var Massage="ERROR";
            if (detail != null)
            {
                try
                {
                    VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                    newList.MedicalOnlineReceivingDate = detail.MedicalOnlineReceivingDate;
                    newList.MedicalOnlineSendingDate = detail.MedicalOnlineSendingDate;
                    newList.StatusRemarks = detail.medicalRemarks;
                    _context.VisaProcess.Update(newList);
                    _context.SaveChanges();
                    Massage = "SUCCESSS";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
            return Json(Massage);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateTravelTab(UpdateTravelTabReportDTO detail)
        {
            var Massage="ERROR";
            if (detail != null)
            {
                try
                {
                    VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                    newList.ArrivalDate1 = detail.ArrivalDate1;
                    newList.DepartureDate1 = detail.DepartureDate1;
                    newList.FlightNumber1 = detail.FlightNumber1;
                    newList.TicketNumber1 = detail.TicketNumber1;
                    newList.Sector1From = detail.Sector1From;
                    newList.Sector1To = detail.Sector1To;
                    _context.VisaProcess.Update(newList);
                    _context.SaveChanges();
                    Massage = "SUCCESSS";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
            return Json(Massage);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateCaseCloureTab(UpdateCaseCloureTabReportDTO detail)
        {
            var Massage="ERROR";
            if (detail != null)
            {
                try
                {
                    VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                    newList.DocumentHandoverDate = detail.DocumentHandoverDate;
                    newList.DateOfCloure = detail.DateOfCloure;
                    _context.VisaProcess.Update(newList);
                    _context.SaveChanges();
                    Massage = "SUCCESSS";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
            return Json(Massage);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateEnorsementTab(UpdateEnorsementTabReport detail)
        {
            var Massage = "Success";
            try
            {
                VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                newList.PassportReceivingDate = detail.PassportReceivingDate;


                _context.VisaProcess.Update(newList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(Massage);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateProtectorTab(UpdateProtectorTabReportDTO detail)
        {
            var Massage = "SUCCESS";
            if (detail is null)
            {
                throw new ArgumentNullException(nameof(detail));
            }

            try
            {
                VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                newList.RegistrationDate = detail.RegistrationDate;
                newList.RegistrationNumber = detail.RegistrationNumber;
                _context.VisaProcess.Update(newList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(Massage);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateSubmissionTab(UpdateSubmissionTabReportDTO detail)
        {
            var Massage = "SUCCESS";
            try
            {
                VisaProcess newList = _context.VisaProcess.Find(detail.Id);
                newList.PassportSubmissionDate = detail.PassportSubmissionDate;
                newList.StatusRemarks = detail.submiisonRemarks;
                _context.VisaProcess.Update(newList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Json(Massage);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetSector()
        {
            var SectorInfo = _context.EntitySetup.Where(t => t.EntityTypeId == 7).ToList();
            return Json(SectorInfo);
        }

    }
}