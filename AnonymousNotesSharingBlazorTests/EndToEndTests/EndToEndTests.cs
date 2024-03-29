using Microsoft.Playwright;
using TestContext = NUnit.Framework.TestContext;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class EndToEndTests : PageTest
{
    private readonly string TEST_URL = TestContext.Parameters["testUrl"];

    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync(TEST_URL);
    }
    [Test]
    public async Task HomePage_ShouldHasCorrectComponents()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "New note" })).ToBeVisibleAsync();
        await Expect(Page.GetByPlaceholder("Enter search term")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("totalPages")).ToBeVisibleAsync();
        await Expect(Page.GetByText("®Anonymous Notes Sharing")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("totalPages")).ToContainTextAsync("Total Notes:");
        await Expect(Page.GetByRole(AriaRole.List)).ToContainTextAsync("1");
        await Expect(Page.GetByLabel("page navigation")).ToBeVisibleAsync();
        await Expect(Page.GetByText("New note Total Notes:")).ToBeVisibleAsync();
    }
    [Test]
    public async Task CreateNotePopup_ShouldHasCorrectComponents()
    {
        await Page.GetByRole(AriaRole.Button, new() { Name = "New note" }).ClickAsync();
        await Expect(Page.Locator("div").Filter(new() { HasTextRegex = new Regex("^Create Note$") })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Dialog).Locator("div").Filter(new() { HasText = "Note Title Note Message Create" }).Nth(2)).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Create" })).ToBeVisibleAsync();
        await Expect(Page.GetByText("Note Title")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Note Message")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("noteTitle")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("noteMessage")).ToBeVisibleAsync();
        await Expect(Page.Locator("form")).ToContainTextAsync("Note Title");
        await Expect(Page.Locator("form")).ToContainTextAsync("Note Message");
        await Expect(Page.GetByLabel("noteTitle")).ToBeEmptyAsync();
        await Expect(Page.GetByLabel("noteMessage")).ToBeEmptyAsync();
    }
    [Test]
    public async Task CreateNotePopup_CreatesNewNote()
    {
        await CreateTestNote();

        await Expect(Page.GetByText("Test Created 0 days ago View note Edit note", new() { Exact = true }).First).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("Test");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("Created 0 days ago");
        await Expect(Page.Locator("div:nth-child(3) > div > .btn-secondary").First).ToBeVisibleAsync();
        await Expect(Page.Locator("div:nth-child(2) > .btn-secondary").First).ToBeVisibleAsync();
        await Page.Locator("div:nth-child(3) > div > .btn-secondary").First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("Test");
        await Expect(Page.GetByText("Test", new() { Exact = true }).Nth(1)).ToBeVisibleAsync();
    }
    [Test]
    public async Task EditNotePopup_EditesNewNote()
    {
        await CreateTestNote();

        await Page.Locator("div:nth-child(2) > .btn-secondary").First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "noteTitle" })).ToHaveValueAsync("Test");
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "noteMessage" })).ToHaveValueAsync("Test");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "noteTitle" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "noteTitle" }).FillAsync("NewTest");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "noteMessage" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "noteMessage" }).FillAsync("NewTest");
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "noteTitle" })).ToHaveValueAsync("NewTest");
        await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "noteMessage" })).ToHaveValueAsync("NewTest");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Edit", Exact = true }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("NewTest");
        await Page.Locator("div:nth-child(3) > div > .btn-secondary").First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("NewTest");
    }
    [Test]
    public async Task SearchBar_FindsCorrectNote()
    {
        await CreateTestNote("CorrectNote");
        await CreateTestNote();
        await CreateTestNote();
        await CreateTestNote();

        await Page.GetByPlaceholder("Enter search term").FillAsync("CorrectNote");
        await Page.GetByRole(AriaRole.Main).Locator("div").Filter(new() { HasText = "CorrectNote Created 0 days" }).Nth(2).ClickAsync();
        await Expect(Page.GetByText("CorrectNote Created 0 days")).ToBeVisibleAsync();
    }
    private async Task CreateTestNote(string text = "Test")
    {
        await Page.GetByRole(AriaRole.Button, new() { Name = "New note" }).ClickAsync();
        await Page.GetByLabel("noteTitle").ClickAsync();
        await Page.GetByLabel("noteTitle").FillAsync("Test");
        await Page.GetByLabel("noteMessage").ClickAsync();
        await Page.GetByLabel("noteMessage").FillAsync("Test");
        await Expect(Page.GetByLabel("noteTitle")).ToHaveValueAsync("Test");
        await Expect(Page.GetByLabel("noteMessage")).ToHaveValueAsync("Test");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
    }
}