using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class InstitutionCareer
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int CareerId { get; set; }

        public virtual Career Career { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
