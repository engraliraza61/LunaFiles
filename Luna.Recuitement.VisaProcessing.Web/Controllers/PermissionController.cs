using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using System.IO;
using System.Net.Http;
using System.Net;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using iTextSharp.text;
using System.Net.Http.Headers;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class PermissionController : Controller
    {
        private readonly lunaContext _context;
        private IWebHostEnvironment environment;
        private readonly string documentRoot = "";
        private readonly string unicodeFontPath = "";//@"C:\fonts\ArialUnicodeMS.ttf";
        iTextSharp.text.Font pdfCellFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        public PermissionController(lunaContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            environment = _environment;
            unicodeFontPath = Path.Combine(Directory.GetCurrentDirectory(), this.environment.WebRootPath, "documents", "ArialUnicodeMS.ttf");
            documentRoot = Path.Combine(Directory.GetCurrentDirectory(), this.environment.WebRootPath, "documents");
        }




        // GET: Permission
        //  [Authorize(Roles = "Administrator,HR")]
        public async Task<IActionResult> Index(long OepvisaDemandId = 0)
        {
            //var lunaContext = _context.OepvisaDemand
            //   .Include(o => o.Counslate)
            //   .Include(o => o.OepvisaDemandStatusEntitySetup)
            //   .Include(o => o.Sponser)
            //   .Include(o => o.OepvisaDemandDetail)
            //   .Include(o => o.PermissionRequest).Where(o => o.Id == OepvisaDemandId);
            //var lunaContext = _context.PermissionRequest.Where(t => t.OepvisaDemandId == OepvisaDemandId)
            //    .Include(p => p.OepvisaDemand);
            var lunaContext = await _context.PermissionRequest
                .Include(t => t.OepvisaDemand)
                .ThenInclude(p => p.Counslate)
                .Include(p => p.OepvisaDemand.OepvisaDemandStatusEntitySetup)
                .Include(p => p.OepvisaDemand.Sponser)
                .Include(p => p.OepvisaDemand.OepvisaDemandDetail)
                .Where(t => ((OepvisaDemandId == 0)|| (t.OepvisaDemandId == OepvisaDemandId))).ToListAsync();
            ViewData["OepvisaDemandId"] = OepvisaDemandId;
            return View(lunaContext);
        }

        // GET: Permission/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionRequest = await _context.PermissionRequest
                .Include(p => p.OepvisaDemand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissionRequest == null)
            {
                return NotFound();
            }

            return View(permissionRequest);
        }

        // GET: Permission/Create
        public IActionResult Create(long OepvisaDemandId)
        {
            var model = new PermissionRequest();
            model.OepvisaDemandId = OepvisaDemandId;
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", OepvisaDemandId);
            ViewData["PermissionTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityTypeId == 2), "Id", "Name");
            return View(model);
        }

        // POST: Permission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OepvisaDemandId,PermissionNumber,ApplyDate,ReceivingDate,ValidityDate,PermissionTypeEntitySetupId")] PermissionRequest permissionRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissionRequest);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Permission", new { OepvisaDemandId = permissionRequest.OepvisaDemandId });
            }
            ViewData["PermissionTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityTypeId == 2), "Id", "Name", permissionRequest.OepvisaDemandId);
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", permissionRequest.OepvisaDemandId);
            return View(permissionRequest);
        }

        // GET: Permission/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionRequest = await _context.PermissionRequest.FindAsync(id);
            if (permissionRequest == null)
            {
                return NotFound();
            }
            ViewData["PermissionTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityTypeId == 2), "Id", "Name", permissionRequest.OepvisaDemandId);
            ViewData["PermsiionTestCenterId"] = id;
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", permissionRequest.OepvisaDemandId);
            return View(permissionRequest);
        }

        // POST: Permission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,OepvisaDemandId,PermissionNumber,ApplyDate,ReceivingDate,ValidityDate,PermissionTypeEntitySetupId")] PermissionRequest permissionRequest)
        {
            if (id != permissionRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissionRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionRequestExists(permissionRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Permission", new { OepvisaDemandId = permissionRequest.OepvisaDemandId });

            }

            ViewData["PermissionTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityTypeId == 2), "Id", "Name", permissionRequest.OepvisaDemandId);
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", permissionRequest.OepvisaDemandId);
            return View(permissionRequest);
        }

        // GET: Permission/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionRequest = await _context.PermissionRequest
                .Include(p => p.OepvisaDemand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissionRequest == null)
            {
                return NotFound();
            }

            return View(permissionRequest);
        }

        // POST: Permission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var permissionRequest = await _context.PermissionRequest.FindAsync(id);
            _context.PermissionRequest.Remove(permissionRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

         public ActionResult Report(long OEPVisaDemandId, string report = "Undertaking1")
         {
            var newFile = Path.Combine(Directory.GetCurrentDirectory(), this.environment.WebRootPath, "documents", "ut.pdf");
            var pdfCellForTotal = new PdfPCell(new Paragraph("Total", pdfCellFont));
            pdfCellForTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            string reportName = "";
            PdfReader reader = null;
            PdfStamper stamper = null;
            try
            {
                var dt = GetData(OEPVisaDemandId, report);
                string LI = "";
                if (dt.Rows[0]["txtOEPCode"].ToString() == "01")
                    LI = "";
                else if (dt.Rows[0]["txtOEPCode"].ToString() == "02")
                    LI = "LI";
                if (report.Equals("UndertakingForTest"))
                {
                    reportName = $"UndertakingForTest{LI}.pdf";
                }
                if (report.Equals("AgencyUndertaking"))
                {
                    reportName = $"AgencyUndertaking{LI}.pdf";
                }
                else if (report.Equals("Undertaking1"))
                {
                    reportName = $"Undertaking1{LI}.pdf";
                }
                else if (report.Equals("InterviewTestCenter"))
                {
                    reportName = $"InterviewTestCenter{LI}.pdf";
                }
                else if (report.Equals("NewGovernmentOfPakistan"))
                {
                    reportName = $"NewGovernmentOfPakistan{LI}.pdf";
                }
                else if (report.Equals("Undertaking2"))
                {
                    reportName = $"Undertaking2{LI}.pdf";
                }
                else if (report.Equals("DemandLetter"))
                {
                    reportName = $"DemandLetter{LI}.pdf";
                }
                else if (report.Equals("FinalPermission"))
                {
                    reportName = $"FinalPermission{LI}.pdf";
                }
                else if (report.Equals("ChallanForm"))
                {
                    reportName = $"ChallanForm{LI}.pdf";
                }
                else if (report.Equals("Form7"))
                {
                    reportName = $"Form7{LI}.pdf";
                }
                else if (report.Equals("VisaFormKhi"))
                {
                    reportName = $"VisaFormKhi{LI}.pdf";
                }
                else if (report.Equals("VisaFormIsb"))
                {
                    reportName = $"VisaFormIsb{LI}.pdf";
                }
                else if (report.Equals("NBPSlip"))
                {
                    reportName = $"NBPSlip{LI}.pdf";
                }
                else if (report.Equals("Registration"))
                {
                    reportName = $"Registration{LI}.pdf";
                }
                else if (report.Equals("Undertaking3"))
                {
                    reportName = $"Undertaking3{LI}.pdf";
                }
                else if (report.Equals("Contract"))
                {
                    reportName = $"Contract{LI}.pdf";
                }
                else if (report.Equals("Agreement"))
                {
                    reportName = $"Agreement{LI}.pdf";
                }
                var path = Path.Combine(documentRoot, reportName);
                System.IO.File.Copy(Path.Combine(documentRoot, reportName), Path.Combine(documentRoot, "ut.pdf"), true);

                reader = new PdfReader(path);            

                Rectangle pagesize = reader.GetPageSize(1);
                //doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

                stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Open));
                stamper.FormFlattening = true;
              
                AcroFields form = stamper.AcroFields;

                BaseFont unicode = BaseFont.CreateFont(unicodeFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                form.AddSubstitutionFont(unicode);
                var dtJobDetail = new DataTable();
                if (reportName == $"InterviewTestCenter{LI}.pdf"||reportName== $"NewGovernmentOfPakistan{LI}.pdf")
                {
                    dtJobDetail = GetJobDetails(OEPVisaDemandId,report);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        form.SetField("systemDate", DateTime.Now.ToString("MMMM dd, yyyy"));
                        //form.GenerateAppearances=false;                    
                        foreach (DataColumn dataColumn in dt.Columns)
                        {
                            if (dataColumn.ColumnName == "txtDate")
                            {
                                var txtDate = DateTime.Parse(dt.Rows[0][dataColumn.ColumnName].ToString());
                                form.SetField(dataColumn.ColumnName, txtDate.ToString("MMMM,dd,yyyy"));
                            }
                            form.SetField(dataColumn.ColumnName, Convert.ToString(dt.Rows[0][dataColumn.ColumnName]));
                        }
                    }
                }
                else
                {
                    dtJobDetail = GetJobDetails(OEPVisaDemandId,reportName);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        
                            form.SetField("txtDate", DateTime.Now.ToString("MMMM,dd,yyyy"));
                        //form.GenerateAppearances=false;                    
                        foreach (DataColumn dataColumn in dt.Columns)
                        {
                            if (dataColumn.ColumnName == "txtDate")
                            {
                                 var dataIn=dt.Rows[0][dataColumn.ColumnName];
                                if (dataIn == "")
                                {
                                    form.SetField(dataColumn.ColumnName, Convert.ToString(dt.Rows[0][dataColumn.ColumnName]));
                                }
                                else
                                {
                                    var txtDate = DateTime.Now.ToString("MMMM,dd,yyyy");
                                    form.SetField(dataColumn.ColumnName,txtDate.ToString());
                                }
                                
                            }
                            else
                            {
                                form.SetField(dataColumn.ColumnName, Convert.ToString(dt.Rows[0][dataColumn.ColumnName]));
                            }
                        }
                    }
                }
                if ((dtJobDetail != null && dtJobDetail.Rows.Count > 0) && (report.Equals("DemandLetter") || report.Equals("FinalPermission")||report.Equals("InterviewTestCenter")|| report.Equals("NewGovernmentOfPakistan")))
                {
                    object sumObject;
                    if(report== "InterviewTestCenter")
                    {
                        sumObject = 0;
                    }
                    else if (report == "NewGovernmentOfPakistan")
                    {
                        sumObject = 0;
                    }
                    else
                    {
                        
                        sumObject = dtJobDetail.Compute("Sum(txtQuantity)", string.Empty);
                    }
                    PdfPTable table = null;
                    if (report.Equals("DemandLetter"))
                    {
                        table = new PdfPTable(4);
                        table.SetWidths(new int[] { 1, 6, 2, 3 });
                        table.AddCell(SetPDFCell("Sr#", PdfPCell.ALIGN_CENTER));
                        table.AddCell("Category of Workers");
                        table.AddCell("No. Of Required");
                        table.AddCell("Monthly Salary");
                        table.HeaderRows = 1;
                        
                        for (int i = 0; i < dtJobDetail.Rows.Count; i++)
                        {
                            table.AddCell(SetPDFCell((i + 1).ToString() + ".", PdfPCell.ALIGN_CENTER));
                            table.AddCell(Convert.ToString(dtJobDetail.Rows[i]["txtJobTitle"]));
                            table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["txtQuantity"]), PdfPCell.ALIGN_CENTER));
                            table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["txtSalary"]), PdfPCell.ALIGN_CENTER));
                        }
                        table.AddCell("");
                        table.AddCell(SetPDFCell("Total", PdfPCell.ALIGN_CENTER));
                        table.AddCell(SetPDFCell(sumObject.ToString(), PdfPCell.ALIGN_CENTER));
                        table.AddCell("");
                        ColumnText column = new ColumnText(stamper.GetOverContent(1));
                        Rectangle rectPage1 = new Rectangle(10, 10, 600, 550);
                        column.SetSimpleColumn(rectPage1);
                        column.AddElement(table);
                        int pagecount = 1;
                        Rectangle rectPage2 = new Rectangle(36, 36, 559, 806);
                        int status = column.Go();
                        while (ColumnText.HasMoreText(status))
                        {
                            status = triggerNewPage(stamper, pagesize, column, rectPage2, ++pagecount);
                        }
                    }
                    else if (report.Equals("FinalPermission"))
                    {
                        table = new PdfPTable(6);
                        table.SetWidths(new int[] { 1, 4, 3, 2, 2, 2 });
                        table.AddCell(SetPDFCell("Sr#", PdfPCell.ALIGN_CENTER));
                        table.AddCell("Category of Workers");
                        table.AddCell("No. Of Required");
                        table.AddCell("Monthly Salary");
                        table.AddCell("Contract Period");
                        table.AddCell("Remarks");
                        table.HeaderRows = 1;
                        for (int i = 0; i < dtJobDetail.Rows.Count; i++)
                        {
                            table.AddCell(SetPDFCell((i + 1).ToString() + ".", PdfPCell.ALIGN_CENTER));
                            table.AddCell(Convert.ToString(dtJobDetail.Rows[i]["txtJobTitle"]));
                            table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["txtQuantity"]), PdfPCell.ALIGN_CENTER));
                            table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["txtSalary"]), PdfPCell.ALIGN_CENTER));
                            table.AddCell(Convert.ToString(dtJobDetail.Rows[i]["txtContractDuration"]) + " Years");
                            table.AddCell(Convert.ToString(dtJobDetail.Rows[i]["txtRemarks"]));
                        }
                        table.AddCell("");
                        table.AddCell(SetPDFCell("Total", PdfPCell.ALIGN_CENTER));
                        table.AddCell(SetPDFCell(sumObject.ToString(), PdfPCell.ALIGN_CENTER));
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        ColumnText column = new ColumnText(stamper.GetOverContent(1));
                        Rectangle rectPage1 = new Rectangle(10, 10, 600, 750);
                        column.SetSimpleColumn(rectPage1);
                        column.AddElement(table);
                        int pagecount = 1;
                        Rectangle rectPage2 = new Rectangle(36, 36, 559, 806);
                        int status = column.Go();
                        while (ColumnText.HasMoreText(status))
                        {
                            status = triggerNewPage(stamper, pagesize, column, rectPage2, ++pagecount);
                        }
                    }
                    else if (report.Equals("InterviewTestCenter"))
                    {
                        table = new PdfPTable(3);
                        table.SetWidths(new int[] { 4, 5, 3 });
                        table.AddCell(SetPDFCell("Permission NO. & Date", PdfPCell.ALIGN_CENTER));
                        table.AddCell("Address");
                        table.AddCell("DATE");
                        table.HeaderRows = 1;
                        
                        for (int i = 0; i < dtJobDetail.Rows.Count; i++)
                        {
                            table.AddCell(Convert.ToString(dtJobDetail.Rows[i]["sponsorName"]) +",Permission No-"+ Convert.ToString(dtJobDetail.Rows[i]["permissionNo"]));
                            table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["centerName"])+",Address-"+ Convert.ToString(dtJobDetail.Rows[i]["centerAddress"]), PdfPCell.ALIGN_CENTER));
                            var txtDate = DateTime.Parse(dtJobDetail.Rows[i]["AssignDate"].ToString());
                            table.AddCell(SetDefaultPDFCell(txtDate.ToString("MMMM,dd,yyyy"), PdfPCell.ALIGN_CENTER));
                        }
                        
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        ColumnText column = new ColumnText(stamper.GetOverContent(1));
                        Rectangle rectPage1 = new Rectangle(10, 10, 600, 460);
                        column.SetSimpleColumn(rectPage1);
                        column.AddElement(table);
                        int pagecount = 1;
                        Rectangle rectPage2 = new Rectangle(36, 36, 559, 806);
                        int status = column.Go();
                        while (ColumnText.HasMoreText(status))
                        {
                            status = triggerNewPage(stamper, pagesize, column, rectPage2, ++pagecount);
                        }
                    }
                    else if (report.Equals("NewGovernmentOfPakistan"))
                    {
                        table = new PdfPTable(3);
                        table.SetWidths(new int[] { 4, 2, 2 });
                        table.AddCell(SetPDFCell("Permission NO. & Date", PdfPCell.ALIGN_CENTER));
                        table.AddCell("Address");
                        table.AddCell("DATE");
                        table.HeaderRows = 1;

                        for (int i = 0; i < dtJobDetail.Rows.Count; i++)
                        {
                            BaseFont bf = BaseFont.CreateFont(
                        BaseFont.TIMES_ROMAN,
                        BaseFont.CP1252,
                        BaseFont.EMBEDDED);
                            Font fontH1 = new Font(bf, 16, Font.NORMAL);
                            //table.AddCell(new PdfPCell(new Phrase(Convert.ToString(dtJobDetail.Rows[i]["sponsorName"]) + ",Permission No-" + Convert.ToString(dtJobDetail.Rows[i]["permissionNo"]))),fontH1,SelectList,);
                            table.AddCell(Convert.ToString(dtJobDetail.Rows[i]["sponsorName"]) + ",Permission No-" + Convert.ToString(dtJobDetail.Rows[i]["permissionNo"]));
                            table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["centerName"]) + ",Address-" + Convert.ToString(dtJobDetail.Rows[i]["centerAddress"]), PdfPCell.ALIGN_CENTER));
                            var Assign = Convert.ToString(dtJobDetail.Rows[i]["AssignDate"]);
                            if (Convert.ToString(dtJobDetail.Rows[i]["AssignDate"])!="")
                            {
                                var txtDateA = DateTime.Parse(dtJobDetail.Rows[i]["AssignDate"].ToString());
                                table.AddCell(SetDefaultPDFCell(txtDateA.ToString(
                               "MMMM,dd,yyyy"), PdfPCell.ALIGN_CENTER));
                            }
                            else
                            {
                                table.AddCell(SetDefaultPDFCell(Convert.ToString(dtJobDetail.Rows[i]["AssignDate"]), PdfPCell.ALIGN_CENTER));
                            }
                        }

                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        ColumnText column = new ColumnText(stamper.GetOverContent(1));
                        Rectangle rectPage1 = new Rectangle(10, 10, 600, 700);
                        column.SetSimpleColumn(rectPage1);
                        column.AddElement(table);
                        int pagecount = 1;
                        Rectangle rectPage2 = new Rectangle(36, 36, 559, 806);
                        int status = column.Go();
                        while (ColumnText.HasMoreText(status))
                        {
                            status = triggerNewPage(stamper, pagesize, column, rectPage2, ++pagecount);
                        }
                    }
                }
                else if (report.Equals("VisaFormIsb"))
                {
                    if (!String.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[0]["VisaNumber"]).Trim()))
                    {
                        GenerateBarCode(stamper, Convert.ToString(dt.Rows[0]["VisaNumber"]), 350f, 800f);   
                    }
                    if (!String.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[0]["ENumber"]).Trim()))
                    {
                        GenerateBarCode(stamper, Convert.ToString(dt.Rows[0]["ENumber"]), 150f, 800f);
                    }
                }
                //else if (report.Equals("VisaFormKhi"))
                //{
                //    GenerateBarCode(stamper, Convert.ToString(dt.Rows[0]["VisaNumber"]), 75f, 625f);
                //    GenerateBarCode(stamper, Convert.ToString(dt.Rows[0]["ENumber"]), 440f, 625f);
                //}
                stamper.Close();
                reader.Close();
                byte[] bytes = System.IO.File.ReadAllBytes(newFile);
                //Send the File to Download.
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(bytes);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline"); //opens in tab, use 'attachment' to download instead
                response.Content.Headers.ContentDisposition.FileName = $"{report}_{DateTime.Now.ToString("MMddyyyyHHmmss")}.pdf";
                return File(bytes, "application/pdf");
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                stamper.Close();
                reader.Close();
            }
        }


        private static void GenerateBarCode(PdfStamper stamper,string barcodeText,float posX,float posY)
        {
            PdfContentByte cb = stamper.GetUnderContent(1);// i being the pdf page number.
            Barcode128 barcode = new Barcode128();
            barcode.CodeType = Barcode.CODE128;
            barcode.ChecksumText = true;
            barcode.GenerateChecksum = true;
            barcode.StartStopText = true;
            barcode.Code = barcodeText;
            Rectangle rect = barcode.BarcodeSize;
            PdfTemplate template = cb.CreateTemplate(400f, 300f);

            barcode.PlaceBarcode(template, BaseColor.BLACK, BaseColor.BLACK);
            Image image =  Image.GetInstance(template);
            image.SetAbsolutePosition(posX, posY);
            cb.AddImage(image);

        }

        private static void GenerateBarCode2(PdfStamper stamper, string barcodeText, float posX, float posY)
        {
            PdfContentByte cb = stamper.GetUnderContent(1);// i being the pdf page number.
            BarcodePDF417 barcode = new BarcodePDF417();
            barcode.SetText(barcodeText);
            Rectangle rect = barcode.GetBarcodeSize();
            PdfTemplate template = cb.CreateTemplate(150f, 75f);

            barcode.PlaceBarcode(template, BaseColor.BLACK, 1, 1);

            Image image = Image.GetInstance(template);
            //image.ScaleAbsolute(535, 135);
            image.SetAbsolutePosition(posX, posY);
            cb.AddImage(image);
        }
        public int triggerNewPage(PdfStamper stamper, Rectangle pagesize, ColumnText column, Rectangle rect, int pagecount)
        {
            stamper.InsertPage(pagecount, pagesize);
            PdfContentByte canvas = stamper.GetOverContent(pagecount);
            column.Canvas = canvas;
            column.SetSimpleColumn(rect);
            return column.Go();
        }
        public async Task<IActionResult> Undertaking2(string filename = "undertaking.pdf")
        {
            if (filename == null)
                return Content("filename does not exist");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", filename);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private bool PermissionRequestExists(long id)
        {
            return _context.PermissionRequest.Any(e => e.Id == id);
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public DataTable GetData(long OEPVisaDemandId, string report = "undertaking")
        {
            var hijri = new HijriCalendar();
            string sql = "";
            if (report.Equals("Undertaking1"))
            {
                sql = $@"select o.Code txtOEPCode, ol.ProprieterName txtProprieter,o.Name txtOEP, ol.LicenseNumber txtOEPLicenseNumber,VisaNumberDate txtDate,' Two Years ' txtContractDuration, ovd.BatchNumber txtVisaNumber, (select Sum(Quantity) from OEPVisaDemandDetail od where  od.OEPVisaDemandId=ovd.Id) txtQuantity,
                                            s.Name +','+c2.Name+' ,'+c.Name  txtSponser,s.Phone txtPhone,'' txtFax, s.Email txtEmail,s.Address txtAddress,pr.PermissionNumber txtPermissionNumber  From PermissionRequest pr
                                            inner join OEPVisaDemand ovd on pr.OEPVisaDemandId=ovd.Id
                                            inner join OEP o on ovd.OEPId=o.Id
                                            inner join OEPLicense ol on ol.OEPId=o.Id
                                            left join Sponser s on s.Id=ovd.SponserId
                                            left join country c on s.countryId=c.Id
											left join city c2 on s.cityId=c2.Id
                                            WHERE OEPVisaDemandId={OEPVisaDemandId}";
            }if (report.Equals("AgencyUndertaking"))
            {
                sql = $@"select TOP(1) s.Id as SponsorId,(
                               SELECT TOP 1        PassportNo
                               FROM   Passport AS p
                              WHERE  p.CandidateProfileId = cp.Id
							  order by p.ExpiryDate desc
                           ) PassportNo ,cp.FirstName+' '+cp.LastName as Name, o.Code txtOEPCode, ol.ProprieterName txtProprieter,o.Name txtOEP, ol.LicenseNumber txtOEPLicenseNumber,VisaNumberDate txtDate,' Two Years ' txtContractDuration, ovd.BatchNumber txtVisaNumber, (select Sum(Quantity) from OEPVisaDemandDetail od where  od.OEPVisaDemandId=ovd.Id) txtQuantity,
                                            s.Name +','+c2.Name+' ,'+c.Name  txtSponser,s.Phone txtPhone,'' txtFax, s.Email txtEmail,s.Address txtAddress,pr.PermissionNumber txtPermissionNumber  From PermissionRequest pr
                                            inner join OEPVisaDemand ovd on pr.OEPVisaDemandId=ovd.Id
                                            inner join OEP o on ovd.OEPId=o.Id
                                            inner join OEPLicense ol on ol.OEPId=o.Id
                                            left join Sponser s on s.Id=ovd.SponserId
                                            left join country c on s.countryId=c.Id
											left join city c2 on s.cityId=c2.Id
											left join VisaProcess vp on vp.OEPVisaDemandId=ovd.Id
											left join CandidateSelectionDetail csd on csd.Id=vp.CandidateSelectionDetailId
											left join CandidateProfile cp on cp.Id=csd.CandidateProfileId
											left join Passport p on p.CandidateProfileId=cp.Id
                                            WHERE ovd.Id={OEPVisaDemandId}";
            }
            else if (report.Equals("UndertakingForTest"))
            {
                sql = $@"select o.Code txtOEPCode, ol.ProprieterName txtProprieter,o.Name txtOEP, ol.LicenseNumber txtOEPLicenseNumber,VisaNumberDate txtDate,' Two Years ' txtContractDuration, ovd.BatchNumber txtVisaNumber, (select Sum(Quantity) from OEPVisaDemandDetail od where  od.OEPVisaDemandId=ovd.Id) txtQuantity,
                                            s.Name +','+c2.Name+' ,'+c.Name  txtSponser,s.Phone txtPhone,'' txtFax, s.Email txtEmail,s.Address txtAddress,pr.PermissionNumber txtPermissionNumber  From PermissionRequest pr
                                            inner join OEPVisaDemand ovd on pr.OEPVisaDemandId=ovd.Id
                                            inner join OEP o on ovd.OEPId=o.Id
                                            inner join OEPLicense ol on ol.OEPId=o.Id
                                            left join Sponser s on s.Id=ovd.SponserId
                                            left join country c on s.countryId=c.Id
											left join city c2 on s.cityId=c2.Id
                                            WHERE OEPVisaDemandId={OEPVisaDemandId}";
            }
            else if (report.Equals("Undertaking2"))
            {
                sql = $@"select o.Code txtOEPCode,ol.ProprieterName txtProprieter,o.Name txtOEP,ol.LicenseNumber txtOEPLicenseNumber,VisaNumberDate txtDate,' Two Years ' txtContractDuration,
                                            ovd.BatchNumber txtVisaNumber, (select Sum(Quantity) from OEPVisaDemandDetail od where  od.OEPVisaDemandId=ovd.Id) txtQuantity,
                                            s.Name txtSponser,o.Phone txtPhone,'' txtFax, o.Email txtEmail,s.Name txtAddress,pr.PermissionNumber txtPermissionNumber  From PermissionRequest pr
                                            inner join OEPVisaDemand ovd on pr.OEPVisaDemandId=ovd.Id
                                            inner join OEP o on ovd.OEPId=o.Id
                                            inner join OEPLicense ol on ol.OEPId=o.Id
                                            left join Sponser s on s.Id=ovd.SponserId 
                                            WHERE OEPVisaDemandId={OEPVisaDemandId}";
            }
            else if (report.Equals("DemandLetter"))
            {
                sql = $@"select o.Code txtOEPCode,ol.ProprieterName txtProprieter,s.Name txtOEP,ovd.BatchNumber txtOEPLicenseNumber,VisaNumberDate txtDate,' Two Years ' txtContractDuration, ovd.BatchNumber txtVisaNumber, (select Sum(Quantity) from OEPVisaDemandDetail od where  od.OEPVisaDemandId=ovd.Id) txtQuantity,
                                            s.Name txtSponser,o.Phone txtPhone,'' txtFax, o.Email txtEmail,o.Address txtAddress,pr.PermissionNumber txtPermissionNumber  From PermissionRequest pr
                                            inner join OEPVisaDemand ovd on pr.OEPVisaDemandId=ovd.Id
                                            inner join OEP o on ovd.OEPId=o.Id
                                            inner join OEPLicense ol on ol.OEPId=o.Id
                                            left join Sponser s on s.Id=ovd.SponserId 
                                            WHERE OEPVisaDemandId={OEPVisaDemandId}";
            }
            else if (report.Equals("FinalPermission"))
            {
                sql = $@"select o.Code txtOEPCode,s.Name txtProprieter,o.Name txtOEP,ovd.Code txtFileNumber,ol.LicenseNumber txtOEPLicenseNumber,VisaNumberDate txtDate,' Two Years ' txtContractDuration, 
                                            ovd.BatchNumber txtVisaNumber, (select Sum(Quantity) from OEPVisaDemandDetail od where  od.OEPVisaDemandId=ovd.Id) txtQuantity,
                                            s.Name txtSponser,o.Phone txtPhone,'' txtFax, o.Email txtEmail,o.Address txtAddress,pr.PermissionNumber txtPermissionNumber  From PermissionRequest pr
                                            inner join OEPVisaDemand ovd on pr.OEPVisaDemandId=ovd.Id
                                            inner join OEP o on ovd.OEPId=o.Id
                                            inner join OEPLicense ol on ol.OEPId=o.Id
                                            left join Sponser s on s.Id=ovd.SponserId 
                                            WHERE OEPVisaDemandId={OEPVisaDemandId}";
            }
            else if (report.Equals("InterviewTestCenter")||report.Equals("NewGovernmentOfPakistan"))
            {
                sql = $@"Select 
                     ol.ProprieterName as txtProprieter,o.Code as txtOEPCode,
                    sr.Name as sponsorName,ol.LicenseNumber as licenceNo
                      From 
                       OEPVisaDemand ovd
                      inner join Sponser sr on sr.Id=ovd.SponserId
                      inner join OEP o on ovd.OEPId=o.Id
                      inner join OEPLicense ol on ol.OEPId=o.Id
                      where ovd.Id={OEPVisaDemandId}";
            }
            else if (report.Equals("ChallanForm"))
            {
                sql = "SP_ChallanForm";
            }
            else if (report.Equals("Form7"))
            {
                sql = "SP_Form7";
            }
            else if (report.Equals("VisaFormKhi"))
            {
                sql = "SP_VisaFormKHI";
            }
            else if (report.Equals("VisaFormIsb"))
            {
                sql = "SP_VisaFormISB";
            }
            else if (report.Equals("NBPSlip"))
            {
                sql = "SP_NBPSlip";
            }
            else if (report.Equals("Registration"))
            {
                sql = "SP_Registration";
            }
            else if (report.Equals("Contract"))
            {
                sql = "SP_Contract";
            }
            else if (report.Equals("Undertaking3"))
            {
                sql = "SP_Undertaking3";
            }
            else if (report.Equals("Agreement"))
            {
                sql = "SP_Agreement";
            }
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
                command.Parameters.Add(new SqlParameter("candidateProfileId", OEPVisaDemandId));
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        public DataTable GetJobDetails(long OEPVisaDemandId,string reportName)
        {
            string sql = "";
            if (reportName == "InterviewTestCenter"||reportName== "NewGovernmentOfPakistan")
            {
                sql = $@"Select TC.Name as centerName,TC.Address as centerAddress,
                     ol.ProprieterName as txtProprieter,o.Code as txtOEPCode,
                    sr.Name as sponsorName,pr.PermissionNumber as permissionNo,ATCP.AssignDate as AssignDate
                      From AssignTestCenterToPermission ATCP
                      inner join PermissionRequest pr on pr.Id = ATCP.PermissionId
                      inner join TestCenter TC on TC.Id = ATCP.TestCenterId
                      inner join OEPVisaDemand ovd on ovd.Id = pr.OEPVisaDemandId
                      inner join Sponser sr on sr.Id = ovd.SponserId
                      inner join OEP o on ovd.OEPId = o.Id
                      inner join OEPLicense ol on ol.OEPId = o.Id
                      where pr.OEPVisaDemandId ={ OEPVisaDemandId}";
            }
            else
            {
                sql = $@"select es.Name txtJobTitle,ovdd.Quantity txtQuantity,CONVERT(NVARCHAR,ISNULL(ovdd.MinSalary,''))+'-'+CONVERT(NVARCHAR,ISNULL(ovdd.MaxSalary,'')) txtSalary,CAST(isnull(ovdd.DurationYears,'0') as NVARCHAR(100))+' Years, '+CAST(isnull(ovdd.DurationMonths,'0') AS NVARCHAR(100))+' Months' txtContractDuration, '-' txtRemarks From OEPVisaDemandDetail ovdd inner join EntitySetup es on ovdd.JobType_EntitySetupId=es.Id 
                     where OEPVisaDemandId={OEPVisaDemandId} and es.EntityTypeId=5";
            }
           
            DataTable dtJobDetails = new DataTable();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    dtJobDetails.Load(reader);
                }
            }
            return dtJobDetails;
        }

        private PdfPCell SetPDFCell(string Title, int alignment = PdfPCell.ALIGN_CENTER)
        {
            var pdfCell = new PdfPCell(new Paragraph(Title, pdfCellFont));
            pdfCell.HorizontalAlignment = alignment;
            return pdfCell;
        }
        private PdfPCell SetDefaultPDFCell(string Title, int alignment = PdfPCell.ALIGN_CENTER)
        {
            var pdfCell = new PdfPCell(new Paragraph(Title));
            pdfCell.HorizontalAlignment = alignment;
            return pdfCell;
        }
    }
}
