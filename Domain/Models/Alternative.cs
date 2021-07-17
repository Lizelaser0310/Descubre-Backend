using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Alternative
    {
        public Alternative()
        {
            Response = new HashSet<Response>();
        }

        public int Id { get; set; }
        public int ModalityId { get; set; }
        public string Denomination { get; set; }
        public int? Weight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }

        public virtual Modality Modality { get; set; }
        public virtual ICollection<Response> Response { get; set; }
    }
}
