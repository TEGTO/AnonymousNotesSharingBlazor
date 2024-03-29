using AnonymousNotesSharingBlazor.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AnonymousNotesSharingBlazorTests.Hubs
{
    public class NoteHubTest : ShowNotePopupTest
    {
        private Mock<IHubCallerClients> mockClients;
        private Mock<IClientProxy> mockClientProxy;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.Others).Returns(mockClientProxy.Object);
        }
        [Test]
        public async Task HubSendStackChangedTest_SendsSuccessfully()
        {
            //Arrange
            NoteHub simpleHub = new NoteHub()
            {
                Clients = mockClients.Object
            };
            //Act
            await simpleHub.StackChanged();
            //Assert
            mockClients.Verify(clients => clients.Others, Times.Once);
            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "ReceiveStackChanged",
                    It.IsAny<object[]>(),
                    default(CancellationToken)),
                Times.Once);
        }
        [Test]
        public async Task HubSendStackChangedTest_NeverReceives()
        {
            //Arrange
            NoteHub simpleHub = new NoteHub()
            {
                Clients = mockClients.Object
            };
            //Act
            await simpleHub.StackChanged();
            //Assert
            mockClients.Verify(clients => clients.Others, Times.Once);
            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "WrongMethodName",
                    It.IsAny<object[]>(),
                    default(CancellationToken)),
                Times.Never);
        }
    }
}
