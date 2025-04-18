using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.OpenAccessCommand;
using Finance.Application.Common.Exceptions;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Boards.Commands;

public class OpenAccessTests : BaseTestFixture
{
    [Test]
    public async Task ShouldOpenAccess()
    {
        var userId = await Testing.RunAsUserAsync("User", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Name"));
        var userToAddId = await Testing.RunAsUserAsync("UserToAdd", "Password1!", []);
        await Testing.RunAsUserAsync("User", "Password1!", []);

        await Testing.SendAsync(new OpenAccessCommand(boardId, userToAddId));
        
        var board = await Testing.FindAsync<Board>(boardId);
        board.Should().NotBeNull();
        board!.UserIds.Should().Contain(userToAddId);
        board.AdminIds.Should().NotContain(userToAddId);
    }
    
    [Test]
    public async Task ShouldRequireValidCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test"));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        

        await FluentActions.Invoking(() => Testing.SendAsync(new OpenAccessCommand(boardId, anotherUserId)))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
    
    [Test]
    public async Task ShouldRequireCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("user1", "User1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test board"));
        var anotherUserId = await Testing.RunAsUserAsync("user2", "User2!", []);
        
        await FluentActions.Invoking(() => Testing.SendAsync(new OpenAccessCommand(boardId, anotherUserId)))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
