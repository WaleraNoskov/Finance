using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.EditCommand;
using Finance.Application.Common.Exceptions;

namespace Finance.Application.FunctionalTests.Boards.Commands;

public class EditBoardTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new EditBoardCommand(0, "");

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }
    
    [Test]
    public async Task ShouldRequireValidCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test"));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        

        await FluentActions.Invoking(() => Testing.SendAsync(new EditBoardCommand(boardId, anotherUserId)))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
    
    [Test]
    public async Task ShouldRequireCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("user1", "User1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test board"));
        
        await FluentActions.Invoking(() => Testing.SendAsync(new EditBoardCommand(boardId, "")))
            .Should().ThrowAsync<ValidationException>();
    }
}
