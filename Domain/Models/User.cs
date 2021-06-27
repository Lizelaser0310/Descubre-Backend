using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public int RolId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Dni { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string Foto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Role Rol { get; set; }
        public virtual ICollection<Result> Result { get; set; }
    }
}
