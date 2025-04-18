using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Goals.Commands.CreateGoal;
using Finance.Application.Goals.Commands.DeleteGoalCommand;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Goals.Command;

public class DeleteGoalTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board"));
        var goalId = await Testing.SendAsync(new CreateGoalCommand("Test Goal", 0, DateOnly.MinValue, boardId, null));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        
        await FluentActions
            .Invoking(() => Testing.SendAsync(new DeleteGoalCommand(goalId)))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldRequireGoalExists()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board"));
        
        await FluentActions
            .Invoking(() => Testing.SendAsync(new DeleteGoalCommand(0)))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteGoal()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board"));
        var goalId = await Testing.SendAsync(new CreateGoalCommand("Test Goal", 0, DateOnly.MinValue, boardId, null));
        
        await Testing.SendAsync(new DeleteGoalCommand(goalId));

        var goal = await Testing.FindAsync<Goal>(goalId);
        goal.Should().BeNull();
    }
}
