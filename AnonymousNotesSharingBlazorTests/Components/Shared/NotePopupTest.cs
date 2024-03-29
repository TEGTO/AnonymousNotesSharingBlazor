namespace AnonymousNotesSharingBlazorTests.Components.Shared
{
    public class NotePopupTest : CommunicationBaseTest
    {
        [Test]
        public void NotePopupComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = " <form >\n";
            expectedHtml += "  <div class=\"form-group\">\n";
            expectedHtml += "    <label for=\"noteTitle\" class=\"note-label\">Note Title</label>\n";
            expectedHtml += "    <input aria-label=\"noteTitle\" type=\"text\" class=\"form-control\" id=\"noteTitle\" value=\"\" >\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div class=\"form-group\">\n";
            expectedHtml += "    <label for=\"noteMessage\" class=\"note-label\">Note Message</label>\n";
            expectedHtml += "    <input aria-label=\"noteMessage\" type=\"text\" class=\"form-control\" id=\"noteMessage\" value=\"\" >\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <button type=\"submit\" class=\"btn-secondary note-button\" style=\"margin-top:10px; min-width: 60px; align-content:center;\"></button>\n";
            expectedHtml += "</form>\n";
            //Act
            var cut = RenderComponent<NotePopup>();
            //Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void NotePopupWithParamsComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = " <form >\n";
            expectedHtml += "  <div class=\"form-group\">\n";
            expectedHtml += "    <label for=\"noteTitle\" class=\"note-label\">Note Title</label>\n";
            expectedHtml += "    <input aria-label=\"noteTitle\" type=\"text\" class=\"form-control\" id=\"noteTitle\" value=\"TestTitle\" >\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <div class=\"form-group\">\n";
            expectedHtml += "    <label for=\"noteMessage\" class=\"note-label\">Note Message</label>\n";
            expectedHtml += "    <input aria-label=\"noteMessage\" type=\"text\" class=\"form-control\" id=\"noteMessage\" value=\"TestMessage\" >\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "  <button type=\"submit\" class=\"btn-secondary note-button\" style=\"margin-top:10px; min-width: 60px; align-content:center;\">Test</button>\n";
            expectedHtml += "</form>\n";
            //Act
            var cut = RenderComponent<NotePopup>(
                parameters => parameters.Add(p => p.NoteData, new NoteData()
                { Id = 10, CreatedDate = DateTime.MaxValue, NoteMessage = "TestMessage", Title = "TestTitle" })
                .Add(p => p.SubmitButtonText, "Test"));
            //Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void SubmitFormTest_SendsCorrectly()
        {
            //Arrange
            NoteData noteData = new NoteData()
            { Id = 10, CreatedDate = DateTime.MaxValue, NoteMessage = "TestMessage", Title = "TestTitle" };
            var cut = RenderComponent<NotePopup>(
                parameters => parameters.Add(p => p.NoteData, noteData));
            //Act
            cut.Find("form").Submit();
            //Assert
            mockNoteService.Verify(x => x.SetNote(noteData), Times.Once);
            mockNoteHubProxy.Verify(x => x.SendStackChangedAsync(), Times.Never);
            //Arrange
            cut.SetParametersAndRender(
                parameters => parameters.Add(p => p.NoteData, null));
            //Act
            cut.Find("form").Submit();
            //Assert
            mockNoteHubProxy.Verify(x => x.SendStackChangedAsync(), Times.Once);
        }
    }
}
