namespace AnonymousNotesSharingBlazorTests.Components.Shared
{
    public class CommunicationBaseTest : ShowNotePopupTest
    {
        public class CommunicationBaseHelper : CommunicationBaseComponent
        {
            public Task SendStackChangedPublic() => SendStackChanged();
        }

        protected Mock<INoteHubProxy> mockNoteHubProxy;

        public override void SetUp()
        {
            base.SetUp();
            mockNoteHubProxy = new Mock<INoteHubProxy>();
            Services.AddSingleton(mockNoteHubProxy.Object);
        }

        [Test]
        public void OnInitializedAsync_InitializesHubConnection()
        {
            //Act
            var cut = RenderComponent<CommunicationBaseHelper>();
            //Assert
            mockNoteHubProxy.Verify(x => x.InitializeHubAsync(It.IsAny<Uri>(), It.IsAny<Action>()), Times.Once);
        }
        [Test]
        public async Task SendStackChanged_SendsAndReceivesCorrectly()
        {
            //Arrange
            int receiveAmount = 0;
            mockNoteHubProxy.Setup(x => x.SendStackChangedAsync()).Callback(() => receiveAmount++);
            var cut = RenderComponent<CommunicationBaseHelper>();
            //Act
            await cut.Instance.SendStackChangedPublic();
            //Assert
            mockNoteHubProxy.Verify(x => x.SendStackChangedAsync(), Times.Once);
            Assert.That(receiveAmount, Is.EqualTo(1));
        }
    }
}
