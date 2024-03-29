using AnonymousNotesSharingBlazor.Data;
using AnonymousNotesSharingBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace AnonymousNotesSharingBlazor.Services
{
    public class NotesDataService : INoteDataService
    {
        private readonly IDbContextFactory<NotesContext> dbContextFactory;

        public NotesDataService(IDbContextFactory<NotesContext> contextFactory)
        {
            this.dbContextFactory = contextFactory;
        }
        public int GetTotalObjectCount()
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                return notesContext.Notes.Count();
            }
        }
        public IEnumerable<NoteData> GetNotesOnPage(int page, int amountObjectsPerPage)
        {
            using (var notesContext = dbContextFactory.CreateDbContext())
            {
                IEnumerable<NoteData> notesOnPage = new List<NoteData>();
                if (GetTotalObjectCount() > 0)
                {
                    int offset = (page - 1) * amountObjectsPerPage;
                    var maxId = notesContext.Notes.Max(note => note.Id);
                    var startId = maxId - offset;
                    notesOnPage = notesContext.Notes
                    .Where(note => note.Id <= startId && note.Id > startId - page * amountObjectsPerPage)
                    .OrderByDescending(note => note.Id)
                    .AsNoTracking()
                    .ToList();
                }
                return notesOnPage;
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
