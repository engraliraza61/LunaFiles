using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public class AssignTestCenterToPermission
    {
        public long Id { get; set; }
        public long PermissionId { get; set; }
        public long TestCenterId { get; set; }
        public DateTime AssignDate { get; set; }
    }
}
