namespace AnonymousNotesSharingBlazorTests.Components.Shared
{
    public class NoteTest : ShowNotePopupTest
    {
        [Test]
        public void NoteComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "   <div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "  <div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\"></div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div class=\"d-flex flex-column secondary-color note\" >\n";
            expectedHtml += "  <div class=\"d-flex note-panel\" >\n";
            expectedHtml += "    <div class=\"note-label note-title\" ></div>\n";
            expectedHtml += "    <div class=\"note-label note-date\" >\n";
            expectedHtml += "      Created 738973 days ago\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "    <div class=\"d-flex note-buttons\" >\n";
            expectedHtml += "      <div >\n";
            expectedHtml += "        <button aria-label=\"toggleDescription\" type=\"button\"  class=\"btn-secondary note-button\" >View note</button>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "      <div >\n";
            expectedHtml += "        <button aria-label=\"editNote\" type=\"button\"  class=\"btn-secondary note-button\" >Edit note</button>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            //Act
            var cut = RenderComponent<Note>();
            //Assert
            cut.MarkupMatches(expectedHtml);
            //Act
            cut.Find("[aria-label=toggleDescription]").Click();
            //Assert
            cut.Find(".note-description").MarkupMatches("<div class=\"note-description\" >\r\n  <div class=\"main-color note-label text\" ></div>\r\n</div>");
        }
        [Test]
        public void NoteWithParamsComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "<div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "  <div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\"></div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div class=\"d-flex flex-column secondary-color note\" >\n";
            expectedHtml += "  <div class=\"d-flex note-panel\" >\n";
            expectedHtml += "    <div class=\"note-label note-title\" >TestTitle</div>\n";
            expectedHtml += "    <div class=\"note-label note-date\" >\n";
            expectedHtml += "      Created -2913085 days ago\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "    <div class=\"d-flex note-buttons\" >\n";
            expectedHtml += "      <div >\n";
            expectedHtml += "        <button aria-label=\"toggleDescription\" type=\"button\"  class=\"btn-secondary note-button\" >View note</button>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "      <div >\n";
            expectedHtml += "        <button aria-label=\"editNote\" type=\"button\"  class=\"btn-secondary note-button\" >Edit note</button>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            //Act
            var cut = RenderComponent<Note>(
                parameters => parameters.Add(p => p.NoteData, new NoteData()
                { Id = 10, CreatedDate = DateTime.MaxValue, NoteMessage = "TestMessage", Title = "TestTitle" }));
            //Assert
            cut.MarkupMatches(expectedHtml);
            //Act
            cut.Find("[aria-label=toggleDescription]").Click();
            //Assert
            cut.Find(".note-description").MarkupMatches("<div class=\"note-description\" >\r\n  <div class=\"main-color note-label text\" >TestMessage</div>\r\n</div>");
        }
        [Test]
        public void ShowNoteEditPopup_ShowsCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "<div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "  <div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\">\n";
            expectedHtml += "      <div class=\"modal-header text-bg-light border-bottom  \">\n";
            expectedHtml += "        <h5 class=\"modal-title\">Edit Note</h5>\n";
            expectedHtml += "        <i class=\"bi bi-x-lg\" role=\"button\" ></i>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "      <div class=\"modal-body \">\n";
            expectedHtml += "        <form >\n";
            expectedHtml += "          <div class=\"form-group\">\n";
            expectedHtml += "            <label for=\"noteTitle\" class=\"note-label\">Note Title</label>\n";
            expectedHtml += "            <input type=\"text\" class=\"form-control\" id=\"noteTitle\" value=\"TestTitle\" >\n";
            expectedHtml += "          </div>\n";
            expectedHtml += "          <div class=\"form-group\">\n";
            expectedHtml += "            <label for=\"noteMessage\" class=\"note-label\">Note Message</label>\n";
            expectedHtml += "            <input type=\"text\" class=\"form-control\" id=\"noteMessage\" value=\"TestMessage\" >\n";
            expectedHtml += "          </div>\n";
            expectedHtml += "          <button type=\"submit\" class=\"btn-secondary note-button\" style=\"margin-top:10px; min-width: 60px; align-content:center;\">Edit</button>\n";
            expectedHtml += "        </form>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div class=\"d-flex flex-column secondary-color note\" >\n";
            expectedHtml += "  <div class=\"d-flex note-panel\" >\n";
            expectedHtml += "    <div class=\"note-label note-title\" >TestTitle</div>\n";
            expectedHtml += "    <div class=\"note-label note-date\" >\n";
            expectedHtml += "      Created -2913085 days ago\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "    <div class=\"d-flex note-buttons\" >\n";
            expectedHtml += "      <div >\n";
            expectedHtml += "        <button aria-label=\"toggleDescription\" type=\"button\"  class=\"btn-secondary note-button\" >View note</button>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "      <div >\n";
            expectedHtml += "        <button aria-label=\"editNote\" type=\"button\"  class=\"btn-secondary note-button\" >Edit note</button>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";

            var cut = RenderComponent<Note>(
               parameters => parameters.Add(p => p.NoteData, new NoteData()
               { Id = 10, CreatedDate = DateTime.MaxValue, NoteMessage = "TestMessage", Title = "TestTitle" }));
            //Act
            cut.Find("[aria-label=editNote]").Click();
            //Assert
            cut.MarkupMatches(expectedHtml);
        }
    }
}
