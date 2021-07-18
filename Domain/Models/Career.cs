using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Career
    {
        public Career()
        {
            CareerModality = new HashSet<CareerModality>();
            InstitutionCareer = new HashSet<InstitutionCareer>();
            Recomendation = new HashSet<Recomendation>();
        }

        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Information { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }
        public string Link { get; set; }
        public string Laborfield { get; set; }
        public string Salary { get; set; }
        public string Photo { get; set; }
        public string Categoriaid { get; set; }

        public virtual ICollection<CareerModality> CareerModality { get; set; }
        public virtual ICollection<InstitutionCareer> InstitutionCareer { get; set; }
        public virtual ICollection<Recomendation> Recomendation { get; set; }
    }
}
