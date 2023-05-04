using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlanner.Models
{
    public class FolderModel
    {
        [Key]
        public Guid FolderId { get; set; }

        public string Name { get; set; }

        public string Directory { get; set; }

        [NotMapped]
        public string ParentDirectory { get; set; }

        [NotMapped]
        public List<Note> Notes { get; set; }

        [NotMapped]
        public List<FolderModel> Folders { get; set; }

        public Guid User { get; set; }
    }
}
