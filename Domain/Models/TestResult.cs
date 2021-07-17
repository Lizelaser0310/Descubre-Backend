using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class TestResult
    {
        public TestResult()
        {
            Response = new HashSet<Response>();
        }

        public int Id { get; set; }
        public int ResultId { get; set; }
        public int TestId { get; set; }
        public int ModalityId { get; set; }
        public int Total { get; set; }
        public int? AverageTime { get; set; }

        public virtual Modality Modality { get; set; }
        public virtual Result Result { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<Response> Response { get; set; }
    }
}
