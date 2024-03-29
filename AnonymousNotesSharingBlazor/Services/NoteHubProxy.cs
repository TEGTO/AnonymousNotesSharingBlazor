
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace AnonymousNotesSharingBlazor.Services
{
    public class NoteHubProxy : INoteHubProxy, IAsyncDisposable
    {
        protected HubConnection? hubConnection;

        public Task InitializeHubAsync(Uri uri, Action OnReceiveStackChanged)
        {
            hubConnection = new HubConnectionBuilder()
                 .WithUrl(uri)
                 .Build();
            hubConnection.On("ReceiveStackChanged", () =>
            {
                OnReceiveStackChanged?.Invoke();
            });
            return hubConnection.StartAsync();
        }
        public async Task SendStackChangedAsync()
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("StackChanged");
        }
        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
                await hubConnection.DisposeAsync();
        }
    }
}
