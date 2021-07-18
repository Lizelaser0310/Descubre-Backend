using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Institution
    {
        public Institution()
        {
            InstitutionCareer = new HashSet<InstitutionCareer>();
        }

        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Information { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }
        public string Departament { get; set; }
        public string Province { get; set; }
        public string Creationdate { get; set; }
        public string Licensing { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<InstitutionCareer> InstitutionCareer { get; set; }
    }
}
