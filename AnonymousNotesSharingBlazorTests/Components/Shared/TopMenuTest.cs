namespace AnonymousNotesSharingBlazorTests.Components.Shared
{
    public class TopMenuTest : ShowNotePopupTest
    {
        [Test]
        public void TopMenuComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "<div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "  <div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\"></div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div class=\"d-flex p-2 top-menu\" >\n";
            expectedHtml += "  <div class=\"button-area\" >\n";
            expectedHtml += "    <button type=\"button\"  class=\"btn-secondary note-button\" >New note</button>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div class=\"search-note\" >\n";
            expectedHtml += "    <div class=\"input-group mb-3\" >\n";
            expectedHtml += "      <input type=\"text\"  class=\"form-control\" placeholder=\"Enter search term\" aria-describedby=\"basic-addon2\" >\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div aria-label=\"totalPages\" class=\"note-label note-count\" >\n";
            expectedHtml += "    Total Notes: 0</div>\n";
            expectedHtml += "</div>\n";
            // Act
            var cut = RenderComponent<TopMenu>();
            // Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void NewNoteButtonTest_ShowsNotePopup()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "  <div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "  <div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\">\n";
            expectedHtml += "      <div class=\"modal-header text-bg-light border-bottom  \">\n";
            expectedHtml += "        <h5 class=\"modal-title\">Create Note</h5>\n";
            expectedHtml += "        <i class=\"bi bi-x-lg\" role=\"button\" ></i>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "      <div class=\"modal-body \">\n";
            expectedHtml += "        <form >\n";
            expectedHtml += "          <div class=\"form-group\">\n";
            expectedHtml += "            <label for=\"noteTitle\" class=\"note-label\">Note Title</label>\n";
            expectedHtml += "            <input aria-label=\"noteTitle\" type=\"text\" class=\"form-control\" id=\"noteTitle\" value=\"\" >\n";
            expectedHtml += "          </div>\n";
            expectedHtml += "          <div class=\"form-group\">\n";
            expectedHtml += "            <label for=\"noteMessage\" class=\"note-label\">Note Message</label>\n";
            expectedHtml += "            <input aria-label=\"noteMessage\" type=\"text\" class=\"form-control\" id=\"noteMessage\" value=\"\" >\n";
            expectedHtml += "          </div>\n";
            expectedHtml += "          <button type=\"submit\" class=\"btn-secondary note-button\" style=\"margin-top:10px; min-width: 60px; align-content:center;\">Create</button>\n";
            expectedHtml += "        </form>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div class=\"d-flex p-2 top-menu\" >\n";
            expectedHtml += "  <div class=\"button-area\" >\n";
            expectedHtml += "    <button type=\"button\"  class=\"btn-secondary note-button\" >New note</button>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div class=\"search-note\" >\n";
            expectedHtml += "    <div class=\"input-group mb-3\" >\n";
            expectedHtml += "      <input type=\"text\"  class=\"form-control\" placeholder=\"Enter search term\" aria-describedby=\"basic-addon2\" >\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div aria-label=\"totalPages\" class=\"note-label note-count\" >\n";
            expectedHtml += "    Total Notes: 0</div>\n";
            expectedHtml += "</div>\n";

            var cut = RenderComponent<TopMenu>();
            // Act
            cut.Find("button").Click();
            // Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void SearchInputTest_InvokesCorrectAction()
        {
            //Arrange
            string i = "";
            Action<string> onSearch = (s) => i = s;
            var cut = RenderComponent<TopMenu>(parameters => parameters
              .Add(p => p.OnSearch, onSearch)
            );
            // Act
            cut.Find("input").Input("test");
            //Assert
            Assert.That(i, Is.SameAs("test"));
        }
        [Test]
        public void TotalPagesTest_CorrectTotalPagesNumber()
        {
            //Arrange
            var cut = RenderComponent<TopMenu>(parameters => parameters
              .Add(p => p.TotalNotes, 10));
            //Assert
            cut.Find("[aria-label=totalPages]").MarkupMatches("<div aria-label=\"totalPages\" class=\"note-label note-count\" >\r\n  Total Notes: 10</div>");
        }
    }
}
