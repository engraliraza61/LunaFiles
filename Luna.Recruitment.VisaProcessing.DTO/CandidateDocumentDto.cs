using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.DTO
{
    public class CandidateDocumentDto
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? FileTypeEntitySetupId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".pdf", ".png",".xls",".xlsx" })]
        public IFormFile File { get; set; }
      
    }
}
