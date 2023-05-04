using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlanner.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }
        public Guid User { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEdited { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }

        [NotMapped]
        public DateTime NoteDate { get; set; }
    }
}
