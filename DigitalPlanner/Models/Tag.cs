using System.ComponentModel.DataAnnotations;

namespace DigitalPlanner.Models
{
    public class Tag
    {
        public string Name { get; set; }

        [Key]
        public Guid Id { get; set; }
    }
}
