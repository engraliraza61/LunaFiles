using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Web.Extensions
{
    public class EmailClass
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "sender@gmail.com"; //Sender Email Address  
        static string password = "Abc@123$%^"; //Sender Password  
        static string emailToAddress = "receiver@gmail.com"; //Receiver Email Address  
        static string subject = "Hello";    
        static string body = "Hello, This is Email sending test using gmail.";
    }
}
