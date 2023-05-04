using System.ComponentModel.DataAnnotations;

namespace DigitalPlanner.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [Key]
        public Guid Id { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
