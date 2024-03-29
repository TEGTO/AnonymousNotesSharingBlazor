using AnonymousNotesSharingBlazor.Data;
using Microsoft.EntityFrameworkCore;

namespace AnonymousNotesSharingBlazorTests.Services
{
    public class NotesServiceTest : BunitTestContext
    {
        private NotesDataService notesService;
        private Mock<NotesContext> mockDbContext;

        [SetUp]
        public void SetUp()
        {
            var mockDbContextFactory = new Mock<IDbContextFactory<NotesContext>>();
            mockDbContext = CreateMockDbContext();
            mockDbContext.Setup(m => m.Notes).Returns(GetTestNotesDbSet());
            mockDbContextFactory.Setup(m => m.CreateDbContext()).Returns(mockDbContext.Object);
            notesService = new NotesDataService(mockDbContextFactory.Object);
        }
        [Test]
        public void GetTotalObjectCount_ReturnsCorrectCount()
        {
            // Act
            var count = notesService.GetTotalObjectCount();
            // Assert
            Assert.That(count, Is.EqualTo(GetTestNotes().Count));
            Assert.That(GetTestNotes().Count, Is.EqualTo(3));
        }
        [Test]
        public void GetNotesOnPage_ReturnsCorrectNotes()
        {
            // Arrange
            int page = 1;
            int amountObjectsPerPage = 2;
            // Act
            var notes = notesService.GetNotesOnPage(page, amountObjectsPerPage);
            // Assert
            Assert.That(notes.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SetNote_AddsNewNote()
        {
            // Arrange
            var note = new NoteData { Id = 0, Title = "Test Title", NoteMessage = "Test Message", CreatedDate = DateTime.Now };
            // Act
            notesService.SetNote(note);
            // Assert
            mockDbContext.Verify(m => m.Add(It.IsAny<NoteData>()), Times.Once);
            mockDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
        [Test]
        public void SetNote_UpdatesExistedNote()
        {
            // Arrange
            var note = notesService.GetNotesOnPage(1, 3).Last();
            note.Title = "New Title";
            note.NoteMessage = "New Message";
            // Act
            notesService.SetNote(note);
            // Assert
            mockDbContext.Verify(m => m.Update(It.IsAny<NoteData>()), Times.Once);
            mockDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);

            Assert.That(note.Id, Is.EqualTo(1));
            var updatedNote = notesService.GetNotesOnPage(1, 3).Last();
            Assert.That(note, Is.EqualTo(updatedNote));
            Assert.That(updatedNote.Id, Is.EqualTo(1));
            Assert.That(updatedNote.Title, Is.EqualTo("New Title"));
            Assert.That(updatedNote.NoteMessage, Is.EqualTo("New Message"));
        }
        private DbSet<NoteData> GetTestNotesDbSet()
        {
            var testNotes = GetTestNotes();
            var mockNotes = new Mock<DbSet<NoteData>>();
            mockNotes.As<IQueryable<NoteData>>().Setup(m => m.Provider).Returns(testNotes.AsQueryable().Provider);
            mockNotes.As<IQueryable<NoteData>>().Setup(m => m.Expression).Returns(testNotes.AsQueryable().Expression);
            mockNotes.As<IQueryable<NoteData>>().Setup(m => m.ElementType).Returns(testNotes.AsQueryable().ElementType);
            mockNotes.As<IQueryable<NoteData>>().Setup(m => m.GetEnumerator()).Returns(() => testNotes.GetEnumerator());
            return mockNotes.Object;
        }
        private Mock<NotesContext> CreateMockDbContext()
        {
            var options = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var mockDbContext = new Mock<NotesContext>(options);
            return mockDbContext;
        }
        private List<NoteData> GetTestNotes()
        {
            return new List<NoteData>
            {
                new NoteData { Id = 1, Title = "Test Note 1", NoteMessage = "Message 1", CreatedDate = DateTime.Now },
                new NoteData { Id = 2, Title = "Test Note 2", NoteMessage = "Message 2", CreatedDate = DateTime.Now },
                new NoteData { Id = 3, Title = "Test Note 3", NoteMessage = "Message 3", CreatedDate = DateTime.Now }
            };
        }
    }
}
