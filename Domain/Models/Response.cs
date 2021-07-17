using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Response
    {
        public int Id { get; set; }
        public int TestResultId { get; set; }
        public int AlternativeId { get; set; }
        public int Score { get; set; }

        public virtual Alternative Alternative { get; set; }
        public virtual TestResult TestResult { get; set; }
    }
}
