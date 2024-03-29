using AnonymousNotesSharingBlazor.Models;

namespace AnonymousNotesSharingBlazor.Services
{
    public interface INoteDataService
    {
        public int GetTotalObjectCount();
        public IEnumerable<NoteData> GetNotesOnPage(int page, int amountObjectsPerPage);
        public void SetNote(NoteData note);
    }
}
