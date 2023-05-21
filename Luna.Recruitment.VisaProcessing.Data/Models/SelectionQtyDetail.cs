using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public class SelectionQtyDetail
    {
        public long Id { get; set; }
        public long CandidateSelectionId { get; set; }
        public long EntitySetupId { get; set; }
        public long SelectionQty { get; set; }
        public virtual EntitySetup EntitySetup { get; set; }
    }
}
