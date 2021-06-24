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

        public virtual ICollection<InstitutionCareer> InstitutionCareer { get; set; }
    }
}
