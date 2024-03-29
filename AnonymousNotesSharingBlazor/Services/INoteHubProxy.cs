namespace AnonymousNotesSharingBlazor.Services
{
    public interface INoteHubProxy
    {
        public Task InitializeHubAsync(Uri uri, Action OnReceiveStackChanged);
        public Task SendStackChangedAsync();
    }
}
