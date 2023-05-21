using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Web
{
    public class PDFUtility
    {
        public ICollection GetFormFields(Stream pdfStream)
        {
            PdfReader reader = null;
            try
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                AcroFields acroFields = pdfReader.AcroFields;
                return (ICollection)acroFields.Fields.Keys;
            }
            finally
            {
                reader?.Close();
            }
        }

        public string FillForm(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", fileName = "undertaking.pdf");
            using (FileStream outFile = new FileStream("result.pdf", FileMode.Create))
            {
                PdfReader pdfReader1 = new PdfReader(path);
                PdfStamper pdfStamper1 = new PdfStamper(pdfReader1, outFile);
                AcroFields form = pdfStamper1.AcroFields;
                form.SetField("txtProprieter", "Proprieter");
                form.SetField("txtOEP", "OEP NO.");
                form.SetField("txtOEPLicenseNumber", "License Number");
                form.SetField("txtVisaNumber", "Visa Number");
                form.SetField("txtDate", "05 Feb, 2021");
                form.SetField("txtQuantity", "100");
                form.SetField("txtSponser", "Sponser Name");
                form.SetField("txtPhone", "0900");
                form.SetField("txtFax", "0500");
                form.SetField("txtEmail", "email@domain.com");
                form.SetField("txtAddress", "ADDRESS HERE");
                //rest of the code here
                //fields.SetField("n°1", "value");
                //...
                pdfStamper1.Close();
                pdfReader1.Close();
            }
            return path;
            //System.IO.FileStream inputStream = new FileStream(path, FileMode.Open);            
            //Stream outStream = new MemoryStream();
            //PdfReader pdfReader = null;
            //PdfStamper pdfStamper = null;
            //Stream inStream = null;
            //try
            //{
            //    pdfReader = new PdfReader(inputStream);
            //    pdfStamper = new PdfStamper(pdfReader, outStream);
            //    AcroFields form = pdfStamper.AcroFields;
            //    form.SetField("txtProprieter", "Proprieter");
            //    form.SetField("txtOEP", "OEP NO.");
            //    form.SetField("txtOEPLicenseNumber", "License Number");
            //    form.SetField("txtVisaNumber", "Visa Number");
            //    form.SetField("txtDate", "05 Feb, 2021");
            //    form.SetField("txtQuantity", "100"); 
            //    form.SetField("txtSponser", "Sponser Name");
            //    form.SetField("txtPhone", "0900");
            //    form.SetField("txtFax", "0500");
            //    form.SetField("txtEmail", "email@domain.com");
            //    form.SetField("txtAddress", "ADDRESS HERE");
            //    // set this if you want the result PDF to not be editable. 
            //    pdfStamper.FormFlattening = true;
            //    return outStream;
            //}
            //finally
            //{
            //    pdfStamper?.Close();
            //    pdfReader?.Close();
            //    inStream?.Close();
            //}
        }
    }
}
