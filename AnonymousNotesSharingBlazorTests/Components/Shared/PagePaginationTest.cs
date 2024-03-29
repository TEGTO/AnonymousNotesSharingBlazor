namespace AnonymousNotesSharingBlazorTests.Components.Shared
{
    public class PagePaginationTest : BunitTestContext
    {
        [Test]
        public void PagePaginationComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "<nav aria-label=\"page navigation\">\n";
            expectedHtml += "  <ul class=\"pagination justify-content-center\"></ul>\n";
            expectedHtml += "</nav>\n";
            // Act
            var cut = RenderComponent<PagePagination>();
            // Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void PagePaginationWithParamsComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = " <nav aria-label=\"page navigation\">\n";
            expectedHtml += "  <ul class=\"pagination justify-content-center\">\n";
            expectedHtml += "    <li class=\"page-item active\">\n";
            expectedHtml += "      <button class=\"page-link\" >1</button>\n";
            expectedHtml += "    </li>\n";
            expectedHtml += "    <li class=\"page-item \">\n";
            expectedHtml += "      <button class=\"page-link\" >2</button>\n";
            expectedHtml += "    </li>\n";
            expectedHtml += "    <li class=\"page-item \">\n";
            expectedHtml += "      <button class=\"page-link\" >3</button>\n";
            expectedHtml += "    </li>\n";
            expectedHtml += "  </ul>\n";
            expectedHtml += "</nav>\n";
            expectedHtml += "\n";
            // Act
            var cut = RenderComponent<PagePagination>(parameters => parameters
            .Add(p => p.TotalPages, 3)
            .Add(p => p.CurrentPageNumber, 1)
            );
            // Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void ChangePageTest_InvokesCorrectAction()
        {
            //Arrange
            int i = 0;
            Action<int> onChangePage = (pageNumber) => i = pageNumber;
            var cut = RenderComponent<PagePagination>(parameters => parameters
              .Add(p => p.OnChangePage, onChangePage)
              .Add(p => p.TotalPages, 3)
              .Add(p => p.CurrentPageNumber, 1)
            );
            // Act
            cut.Find("li:last-child").FirstElementChild!.Click();
            //Assert
            Assert.That(i, Is.EqualTo(3));
        }
    }
}
