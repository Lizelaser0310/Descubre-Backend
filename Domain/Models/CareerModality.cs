using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CareerModality
    {
        public int Id { get; set; }
        public int CareerId { get; set; }
        public int ModalityId { get; set; }
        public int Weight { get; set; }

        public virtual Career Career { get; set; }
        public virtual Modality Modality { get; set; }
    }
}
