using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Test
    {
        public Test()
        {
            Question = new HashSet<Question>();
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public string Denomination { get; set; }
        public int AverageTime { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Question> Question { get; set; }
        public virtual ICollection<Result> Result { get; set; }
    }
}
