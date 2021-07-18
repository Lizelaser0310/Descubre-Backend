using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Status
    {
        public Status()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public string Denomination { get; set; }

        public virtual ICollection<Result> Result { get; set; }
    }
}
