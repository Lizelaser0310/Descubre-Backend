using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Result
    {
        public Result()
        {
            Recomendation = new HashSet<Recomendation>();
            Response = new HashSet<Response>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int Score { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Recomendation> Recomendation { get; set; }
        public virtual ICollection<Response> Response { get; set; }
    }
}
