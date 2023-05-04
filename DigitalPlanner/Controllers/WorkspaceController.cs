using DigitalPlanner.Data;
using DigitalPlanner.Models;
using DigitalPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPlanner.Controllers
{
    [Authorize]
    public class WorkspaceController : Controller
    {
        private readonly DBContext db;
        private readonly UserService userService;
        private readonly NoteService noteService;

        public WorkspaceController(DBContext db)
        {
            this.db = db;
            noteService = new NoteService(db);
            userService = new UserService(db);
        }

        public List<FolderModel> GetFoldersByDirectory(string directory, Guid userId)
        {
            return db.Folders.Where(f => f.Directory == directory && f.User == userId).ToList();
        }

        private string GetParent(string path)
        {
            var i = path.Length - 1;
            while (i >= 0 && path[i] != '$')
                i--;
            return i >= 0 ? path.Substring(0, i) : null;
        }

        public IActionResult Folder([FromQuery] string directory)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            FolderModel folder = new FolderModel()
            {
                Notes = noteService.GetNotesByDirectory(directory, userId),
                Folders = GetFoldersByDirectory(directory, userId),
                Directory = directory,
                ParentDirectory = GetParent(directory)
            };
            return View("Folder", folder);
        }

        public IActionResult AddFolder([FromQuery] string directory, [FromQuery] string name)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            var f = new FolderModel()
            {
                Name = name,
                FolderId = Guid.NewGuid(),
                Directory = directory,
                User = userId,
            };
            db.Folders.Add(f);
            db.SaveChanges();
            return Folder(directory);
        }

        public IActionResult Delete([FromQuery] string path)
        {
            string name = null;
            string directory = null;
            NoteService.SplitPath(path, ref directory, ref name);

            var folder = db.Folders.FirstOrDefault(f => f.Directory == directory && f.Name == name);
            DeleteFolder(folder);
            return Redirect($"/Workspace/Folder?directory={directory}");
        }

        public void DeleteFolder(FolderModel folder)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            var directory = folder.Directory + '$' + folder.Name;
            var Notes = noteService.GetNotesByDirectory(directory, userId);
            var Folders = GetFoldersByDirectory(directory, userId);
            foreach (var n in Notes)
            {
                noteService.DeleteNote(n);
            }
            foreach (var f in Folders)
            {
                DeleteFolder(f);
            }
            db.Folders.Remove(folder);
            db.SaveChanges();
        }
    }
}
