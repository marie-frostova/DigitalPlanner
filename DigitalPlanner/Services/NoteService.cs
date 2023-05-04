using DigitalPlanner.Data;
using DigitalPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlanner.Services;

public class NoteService
{
    private DBContext db;
    

    public NoteService(DBContext db)
    {
        this.db = db;
    }

    public List<Note> GetNotesByDirectory(string directory, Guid userId)
    {
        return db.Notes.Where(n => n.Directory == directory && n.User == userId).ToList();
    }

    public static void SplitPath(string path, ref string directory, ref string name)
    {
        var i = path.Length - 1;
        while (i >= 0 && path[i] != '$')
            i--;
        directory = (i >= 0) ? path.Substring(0, i) : null;
        name = (i >= 0) ? path.Substring(i + 1) : path;
    }

    public async Task<Note?> GetNoteByPath(string path, Guid userId)
    {
        string name = null;
        string directory = null;
        SplitPath(path, ref directory, ref name);
        return await db.Notes.FirstOrDefaultAsync(n => n.Name == name && n.Directory == directory && n.User == userId);
    }

    public async Task<Note?> GetNoteByDate(string dateString, Guid userId)
    {
        return await GetNoteByPath("calendar$" + dateString, userId);
    }
    
    public async Task<Note?> GetNoteById(Guid id)
    {
        return await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
    }
    
    public IEnumerable<Note> GetNotesByUserId(Guid userId)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new ArgumentException($"There is no user with id {userId}");
        }
        return user.Notes;
    }

    public Note CreateNote(Note note, Guid userId)
    {
        var newNote = new Note()
        {
            Id = Guid.NewGuid(),
            User = userId,
            CreationDate = DateTime.Now,
            Directory = note.Directory,
            NoteDate = note.NoteDate,
        };
        return newNote;
    }
    
    public async Task<Note> UpdateOrCreateNote(Note note, Guid userId)
    {
        var now = DateTime.Now;
        var newNote = GetNoteById(note.Id).Result ?? CreateNote(note, userId);
        newNote.Content = note.Content;
        newNote.Tags = note.Tags;
        newNote.LastEdited = now;
        newNote.Name = note.Name;
        return await AddOrUpdateNoteInDatabase(newNote);
    }

    private async Task<Note> AddOrUpdateNoteInDatabase(Note note)
    {
        var n = await db.Notes.FirstOrDefaultAsync(n => n.Id == note.Id && n.User == note.User);
        if (n == null)
            return await AddNoteToDatabase(note);
        else
            return await UpdateNoteToDatabase(note);
    }

    private async Task<Note> AddNoteToDatabase(Note note)
    {
        db.Add(note);
        var user = db.Users.FirstOrDefaultAsync(u => u.Id == note.User).Result;
        user.Notes ??= new List<Note>();
        user.Notes.Add(note);
        db.Update(user);
        await db.SaveChangesAsync();
        return note;
    }
    
    private async Task<Note> UpdateNoteToDatabase(Note note)
    {
        db.Update(note);
        var user = db.Users.FirstOrDefaultAsync(u => u.Id == note.User).Result;
        db.Update(user);
        await db.SaveChangesAsync();
        return note;
    }

    public void DeleteNote(Note note)
    {
        var user = db.Users.FirstOrDefaultAsync(u => u.Id == note.User).Result;
        user.Notes?.Remove(note);
        db.Update(user);
        db.Notes.Remove(note);
        db.SaveChanges();
    }

    public Note GetViewModel(string path, Guid userId)
    {
        string name = null;
        string directory = null;

        SplitPath(path, ref directory, ref name);
        if (directory == "calendar")
        {
            var note = GetNoteByDate(name, userId).Result 
                ?? new Note() 
                {
                    Id = Guid.NewGuid(), 
                    Directory = directory,
                    Name = name,
                    CreationDate = DateTime.Now,
                    Content = "",
                    LastEdited = DateTime.Now,
                    NoteDate = DateTime.Now,
                    Tags = new List<Tag>(),
                    User = userId, 
                };
            note.NoteDate = DateTime.Parse(name);
            return note;
        }
        else
        {
            var note = GetNoteByPath(path, userId).Result 
                ?? new Note() { Directory = path,  };
            return note;
        }
    }
}