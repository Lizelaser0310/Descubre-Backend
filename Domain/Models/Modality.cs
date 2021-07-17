using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Modality
    {
        public Modality()
        {
            Alternative = new HashSet<Alternative>();
            CareerModality = new HashSet<CareerModality>();
            TestResult = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public string Denomination { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Alternative> Alternative { get; set; }
        public virtual ICollection<CareerModality> CareerModality { get; set; }
        public virtual ICollection<TestResult> TestResult { get; set; }
    }
}
