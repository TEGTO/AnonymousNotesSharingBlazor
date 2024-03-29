using AnonymousNotesSharingBlazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace AnonymousNotesSharingBlazor.Hubs
{
    public class NoteHub : Hub
    {
        public async Task StackChanged()
        {
            await Clients.All.SendAsync("ReceiveStackChanged");
        }
    }
}
