using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Response
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int QuestionId { get; set; }
        public int AlternativeIdx { get; set; }

        public virtual Question Question { get; set; }
        public virtual Result Result { get; set; }
    }
}
