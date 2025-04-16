using Finance.Application.Boards.Commands.CloseAccesCommand;
using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.OpenAccessCommand;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Boards.Commands;

public class CloseAccessTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCloseAccess()
    {
        var userId = await Testing.RunAsUserAsync("User", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Name", userId));
        var userToAddId = await Testing.RunAsUserAsync("UserToAdd", "Password1!", []);
        await Testing.SendAsync(new OpenAccessCommand(boardId, userToAddId, userId));
        
        await Testing.SendAsync(new CloseAccessCommand(boardId, userToAddId, userId));
        
        var board = await Testing.FindAsync<Board>(boardId);
        board.Should().NotBeNull();
        board!.UserIds.Should().NotContain(userToAddId);
        board.AdminIds.Should().NotContain(userToAddId);
    }
}
