using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Test
    {
        public Test()
        {
            Modality = new HashSet<Modality>();
            TestResult = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public string Denomination { get; set; }
        public int AverageTime { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Modality> Modality { get; set; }
        public virtual ICollection<TestResult> TestResult { get; set; }
    }
}
