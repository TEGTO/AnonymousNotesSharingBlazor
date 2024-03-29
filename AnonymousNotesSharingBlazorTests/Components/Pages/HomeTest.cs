using AnonymousNotesSharingBlazor.Components.Pages;

namespace AnonymousNotesSharingBlazorTests.Components.Pages
{
    public class HomeTest : CommunicationBaseTest
    {
        public class HomeHelper : Home
        {
            public IEnumerable<NoteData> Notes { get => notes; }
            public string SearchTerm { get => searchTerm; set => searchTerm = value; }

            public void DefineNotesPublic() => DefineNotes();
        }

        [Test]
        public void HomeComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "  <div class=\"main-color side-row top-row\" >\n";
            expectedHtml += "  <div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "    <div class=\"modal-dialog     \">\n";
            expectedHtml += "      <div class=\"modal-content\"></div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div class=\"d-flex p-2 top-menu\" >\n";
            expectedHtml += "    <div class=\"button-area\" >\n";
            expectedHtml += "      <button type=\"button\"  class=\"btn-secondary note-button\" >New note</button>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "    <div class=\"search-note\" >\n";
            expectedHtml += "      <div class=\"input-group mb-3\" >\n";
            expectedHtml += "        <input type=\"text\"  class=\"form-control\" placeholder=\"Enter search term\" aria-describedby=\"basic-addon2\" >\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "    <div aria-label=\"totalPages\" class=\"note-label note-count\" >\n";
            expectedHtml += "      Total Notes: 0</div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div class=\"d-flex flex-column content main-layout\" >\n";
            expectedHtml += "  <div class=\"d-flex flex-column p-3 notes-area\" ></div>\n";
            expectedHtml += "  <nav aria-label=\"page navigation\">\n";
            expectedHtml += "    <ul class=\"pagination justify-content-center\"></ul>\n";
            expectedHtml += "  </nav>\n";
            expectedHtml += "</div>\n";
            //Act
            var cut = RenderComponent<HomeHelper>();
            //Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void GetDataFromService_ReceivesDataFromService()
        {
            NoteData noteData1 = new NoteData()
            { Id = 1, CreatedDate = DateTime.MaxValue, NoteMessage = "TestMessage1", Title = "TestTitle1" };
            NoteData noteData2 = new NoteData()
            { Id = 2, CreatedDate = DateTime.MinValue, NoteMessage = "TestMessage2", Title = "TestTitle2" };
            mockNoteService.Setup(x => x.GetNotesOnPage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<NoteData>() { noteData1, noteData2 });
            //Act
            var cut = RenderComponent<HomeHelper>();
            //Assert
            mockNoteService.Verify(x => x.GetNotesOnPage(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockNoteService.Verify(x => x.GetTotalNotesCount(), Times.Once);
            Assert.That(noteData1, Is.EqualTo(cut.Instance.Notes.First()));
            Assert.That(noteData2, Is.EqualTo(cut.Instance.Notes.Last()));
            //Act
            cut.Instance.DefineNotesPublic();
            //Assert
            mockNoteService.Verify(x => x.GetNotesOnPage(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            mockNoteService.Verify(x => x.GetTotalNotesCount(), Times.Exactly(2));
            Assert.That(cut.Instance.Notes.Count(), Is.EqualTo(2));
        }
        [Test]
        public void GetNotesThatContainTerm_ReturnsExpectedNotes()
        {
            // Arrange
            var testData = new List<NoteData> {
            new NoteData { Title = "A", NoteMessage = "A" },
            new NoteData { Title = "B", NoteMessage = "A" },
            new NoteData { Title = "C", NoteMessage = "C" }
            };
            mockNoteService.Setup(x => x.GetNotesWithTextOnPage("A", It.IsAny<int>(), It.IsAny<int>())).Returns(testData.Take(2));
            mockNoteService.Setup(x => x.GetTotalNotesCountWithText("A")).Returns(2);
            var cut = RenderComponent<HomeHelper>();
            // Act
            cut.Instance.SearchTerm = "A";
            cut.Instance.DefineNotesPublic();
            mockNoteService.Verify(x => x.GetNotesWithTextOnPage(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockNoteService.Verify(x => x.GetTotalNotesCountWithText(It.IsAny<string>()), Times.Once);
            Assert.That(cut.Instance.Notes.Count(), Is.EqualTo(2));
            Assert.IsTrue(cut.Instance.Notes.Any(n => n.Title.Contains("A") || n.NoteMessage.Contains("A")));
        }
    }
}
