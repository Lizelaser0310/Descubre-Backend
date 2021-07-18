using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Result
    {
        public Result()
        {
            Recomendation = new HashSet<Recomendation>();
            TestResult = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Recomendation> Recomendation { get; set; }
        public virtual ICollection<TestResult> TestResult { get; set; }
    }
}
