using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Recomendation
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int CareerId { get; set; }
        public string Comments { get; set; }

        public virtual Career Career { get; set; }
        public virtual Result Result { get; set; }
    }
}
