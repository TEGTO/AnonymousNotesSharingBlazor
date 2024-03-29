using AnonymousNotesSharingBlazor.Components.Shared;
using AnonymousNotesSharingBlazor.Data;
using AnonymousNotesSharingBlazor.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace AnonymousNotesSharingBlazor.Services
{
    public class NotesDataService : INoteDataService
    {
        private readonly IDbContextFactory<NotesContext> dbContextFactory;

        public NotesDataService(IDbContextFactory<NotesContext> contextFactory)
        {
            this.dbContextFactory = contextFactory;
        }
        public int GetTotalNotesCount()
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                return notesContext.Notes.Count();
            }
        }
        public int GetTotalNotesCountWithText(string text)
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                return notesContext.Notes
                 .Where(note => (note.Title != null && note.Title.Contains(text))
                 || (note.NoteMessage != null && note.NoteMessage.Contains(text)))
                 .Count();
            }
        }
        public IEnumerable<NoteData> GetNotesOnPage(int page, int amountObjectsPerPage)
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                IEnumerable<NoteData> notesOnPage = new List<NoteData>();
                if (GetTotalNotesCount() > 0)
                {
                    notesOnPage = notesContext.Notes
                    .OrderByDescending(note => note.Id)
                    .Skip((page - 1) * amountObjectsPerPage)
                    .Take(amountObjectsPerPage)
                    .AsNoTracking()
                    .ToList();
                }
                return notesOnPage;
            }
        }
        public IEnumerable<NoteData> GetNotesWithTextOnPage(string text, int page, int amountObjectsPerPage)
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                IEnumerable<NoteData> notesThatContainText = new List<NoteData>();
                if (GetTotalNotesCount() > 0)
                {
                    notesThatContainText = notesContext.Notes
                       .Where(note => (note.Title != null && note.Title.Contains(text))
                       || (note.NoteMessage != null && note.NoteMessage.Contains(text)))
                       .OrderByDescending(note => note.Id)
                       .Skip((page - 1) * amountObjectsPerPage)
                       .Take(amountObjectsPerPage)
                       .AsNoTracking()
                       .ToList();
                }
                return notesThatContainText;
            }
        }
        public async void SetNote(NoteData note)
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                if (note.Id == 0)
                    notesContext.Add(note);
                else
                    notesContext.Update(note);
                await notesContext.SaveChangesAsync();
            }
        }
    }
}
