using AnonymousNotesSharingBlazor.Models;

namespace AnonymousNotesSharingBlazor.Services
{
    public interface INoteDataService
    {
        public int GetTotalNotesCount();
        public int GetTotalNotesCountWithText(string text);
        public IEnumerable<NoteData> GetNotesOnPage(int page, int amountObjectsPerPage);
        public IEnumerable<NoteData> GetNotesWithTextOnPage(string text, int page, int amountObjectsPerPage);
        public void SetNote(NoteData note);
    }
}
