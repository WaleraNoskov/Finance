using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.OpenAccessCommand;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Boards.Commands;

public class OpenAccessTests : BaseTestFixture
{
    [Test]
    public async Task ShouldOpenAccess()
    {
        var userId = await Testing.RunAsUserAsync("User", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Name", userId));
        var userToAddId = await Testing.RunAsUserAsync("UserToAdd", "Password1!", []);

        await Testing.SendAsync(new OpenAccessCommand(boardId, userToAddId, userId));
        
        var board = await Testing.FindAsync<Board>(boardId);
        board.Should().NotBeNull();
        board!.UserIds.Should().Contain(userToAddId);
        board.AdminIds.Should().NotContain(userToAddId);
    }
}
