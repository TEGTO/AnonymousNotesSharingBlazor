using AnonymousNotesSharingBlazor.Components.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AnonymousNotesSharingBlazorTests.Components.Layout
{
    public class MainLayoutTest : BunitTestContext
    {
        [Test]
        public void MainLayoutComponentTest_RendersCorrectly()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "<div class=\"page\" >\n";
            expectedHtml += "  <main >\n";
            expectedHtml += "    <div class=\"main-color\" ></div>\n";
            expectedHtml += "    <div class=\"main-color side-row bottom-row\" >\n";
            expectedHtml += "      <div class=\"note-label\" >\n";
            expectedHtml += "        ®Anonymous Notes Sharing Blazor\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </main>\n";
            expectedHtml += "</div>\n";
            expectedHtml += "<div id=\"blazor-error-ui\" >\n";
            expectedHtml += "  An unhandled error has occurred.\n";
            expectedHtml += "  <a href=\"\" class=\"reload\" >Reload</a>\n";
            expectedHtml += "  <a class=\"dismiss\" >&#128473;</a>\n";
            expectedHtml += "</div>\n";
            //Act
            var cut = RenderComponent<MainLayout>();
            //Assert
            cut.MarkupMatches(expectedHtml);
        }
    }
}
