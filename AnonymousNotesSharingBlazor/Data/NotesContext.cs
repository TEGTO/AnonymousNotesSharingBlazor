using AnonymousNotesSharingBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace AnonymousNotesSharingBlazor.Data
{
    public class NotesContext : DbContext
    {
        public virtual DbSet<NoteData> Notes { get; set; } = null!;
        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {
        }
    }
}
