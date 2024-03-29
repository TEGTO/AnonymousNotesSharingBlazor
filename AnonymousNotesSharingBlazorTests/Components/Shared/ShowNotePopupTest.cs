using Microsoft.Extensions.Options;

namespace AnonymousNotesSharingBlazorTests.Components.Shared
{
    public class ShowNotePopupTest : BunitTestContext
    {
        protected Mock<INoteDataService> mockNoteService;
        protected Mock<IIdGenerator> mockIdGenerator;
        protected Mock<ILogger> mockLogger;

        [SetUp]
        public virtual void SetUp()
        {
            mockNoteService = new Mock<INoteDataService>();
            mockIdGenerator = new Mock<IIdGenerator>();
            mockLogger = new Mock<ILogger>();
            var mockModalService = new Mock<IOptions<ModalService>>();
            var mockNoteHubProxy = new Mock<INoteHubProxy>();

            mockModalService.Setup(x => x.Value).Returns(new ModalService());
            mockIdGenerator.Setup(x => x.GetNextId()).Returns("1");

            Services.AddSingleton(mockNoteService.Object);
            Services.AddSingleton(mockIdGenerator.Object);
            Services.AddSingleton(mockLogger.Object);
            Services.AddSingleton(mockModalService.Object.Value);
            Services.AddSingleton(mockNoteHubProxy.Object);

            JSInterop.SetupVoid("window.blazorBootstrap.modal.initialize", _ => true);
            JSInterop.Mode = JSRuntimeMode.Loose;
        }
        [Test]
        public void ShowPopup_CorreclyShowsNotePopup()
        {
            //Arrange
            string expectedHtml;
            expectedHtml = "   <div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "<div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\"></div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>\n";
            //Act
            var cut = RenderComponent<ShowNotePopupAdapter>(
               parameters => parameters.Add(p => p.TriggerShowPopup, false));
            //Assert
            cut.MarkupMatches(expectedHtml);
            //Arrange
            expectedHtml = "  <div id=\"1\" class=\"modal fade\" tabindex=\"-1\" >\n";
            expectedHtml += "  <div class=\"modal-dialog     \">\n";
            expectedHtml += "    <div class=\"modal-content\">\n";
            expectedHtml += "      <div class=\"modal-header text-bg-light border-bottom  \">\n";
            expectedHtml += "        <h5 class=\"modal-title\">Popup Test</h5>\n";
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
            expectedHtml += "          <button type=\"submit\" class=\"btn-secondary note-button\" style=\"margin-top:10px; min-width: 60px; align-content:center;\">Popup test button text</button>\n";
            expectedHtml += "        </form>\n";
            expectedHtml += "      </div>\n";
            expectedHtml += "    </div>\n";
            expectedHtml += "  </div>\n";
            expectedHtml += "</div>System.Threading.Tasks.Task`1[System.Threading.Tasks.VoidTaskResult]\n";

            //Act
            cut.SetParametersAndRender(parameters => parameters.Add(p => p.TriggerShowPopup, true));
            //Assert
            cut.MarkupMatches(expectedHtml);
        }
        [Test]
        public void ShowPopup_NullReferenceExceptionCallsLogger()
        {
            //Arrange
            var cut = RenderComponent<ShowNotePopupAdapter>(
             parameters => parameters
             .Add(p => p.RenderModal, false)
             .Add(p => p.TriggerShowPopup, true));
            mockLogger.Verify(x => x.Error("Object reference not set to an instance of an object."), Times.Once);
        }
    }
}
