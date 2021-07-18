using Domain.Models;

namespace CareerGuidance.Models
{
    public class ModalityDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Denomination { get; set; }
        
        public static ModalityDTO Create(Modality m)
        {
            return new() {
                Id = m.Id,
                TestId = m.TestId,
                Denomination = m.Denomination,
            };
        }
    }
}