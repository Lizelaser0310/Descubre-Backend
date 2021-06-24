using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Question
    {
        public Question()
        {
            Response = new HashSet<Response>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public string Question1 { get; set; }
        public string[] Alternatives { get; set; }
        public int? Weight { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Response> Response { get; set; }
    }
}
