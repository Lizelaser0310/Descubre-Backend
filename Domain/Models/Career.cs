using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Career
    {
        public Career()
        {
            InstitutionCareer = new HashSet<InstitutionCareer>();
            Recomendation = new HashSet<Recomendation>();
        }

        public int Id { get; set; }
        public string Denomination { get; set; }
        public short Information { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<InstitutionCareer> InstitutionCareer { get; set; }
        public virtual ICollection<Recomendation> Recomendation { get; set; }
    }
}
