using System.Security.Authentication;
using DigitalPlanner.Data;
using DigitalPlanner.Models;
using DigitalPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlanner.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly DBContext db;
        private readonly NoteService noteServices;
        private readonly UserService userService;

        public NoteController(DBContext db)
        {
            this.db = db;
            noteServices = new NoteService(db);
            userService = new UserService(db);
        }

        public IActionResult ShowNote(Note note)
        {
            return View("ShowNote", note);
        }

        public IActionResult Note([FromQuery] string path)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            var note = noteServices.GetNoteByPath(path, userId).Result;
            if (note == null)
            {
                return Redirect($"/Note/EditNote?path={path}");
            }
            return ShowNote(note);
        }

        public IActionResult EditNote([FromQuery] string path)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            var view = noteServices.GetViewModel(path, userId);
            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrCreate(Note note)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            await noteServices.UpdateOrCreateNote(note, userId);
            var path = note.Directory + '$' + note.Name;
            return Redirect($"/Note/Note?path={path}");
        }

        public IActionResult DeleteNote([FromQuery] string path)
        {
            var user = User;
            var userId = userService.GetCurrentUserId(user).Result;
            var note = noteServices.GetNoteByPath(path, userId).Result;
            noteServices.DeleteNote(note);
            return Redirect($"/Workspace/Folder?directory={note.Directory}");
        }
    }
}