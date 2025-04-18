using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.DeleteBoardCommand;
using Finance.Application.Common.Exceptions;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Boards.Commands;

public class DeleteBoardTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBoardId()
    {
        var userId = await Testing.RunAsDefaultUserAsync();
        
        var command = new DeleteBoardCommand(99);

        await FluentActions.Invoking(() =>
            Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var userId = await Testing.RunAsDefaultUserAsync();
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Name"));

        await Testing.SendAsync(new DeleteBoardCommand(boardId));

        var board = await Testing.FindAsync<Board>(boardId);
        board.Should().BeNull();
    }

    [Test]
    public async Task ShouldRequireValidCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("user1", "User1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test board"));
        var anotherUserId = await Testing.RunAsUserAsync("user2", "User2!", []);
        
        await FluentActions.Invoking(() => Testing.SendAsync(new DeleteBoardCommand(boardId))).Should().ThrowAsync<UnauthorizedAccessException>();
    }
    
    [Test]
    public async Task ShouldRequireCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("user1", "User1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test board"));
        var anotherUserId = await Testing.RunAsUserAsync("user2", "User2!", []);
        
        await FluentActions.Invoking(() => Testing.SendAsync(new DeleteBoardCommand(boardId)))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
