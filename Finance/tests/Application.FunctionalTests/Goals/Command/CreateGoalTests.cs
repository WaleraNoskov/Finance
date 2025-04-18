using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.OpenAccessCommand;
using Finance.Application.Common.Exceptions;
using Finance.Application.Goals.Commands.CreateGoal;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Goals.Command;

public class CreateGoalTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateGoalCommand("", "", 0, DateOnly.MinValue, 0, null);

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireBoardExists()
    {
        var userId = await Testing.RunAsDefaultUserAsync();
        var command = new CreateGoalCommand(userId, "Test", 0, DateOnly.MinValue, 0, null);

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireValidCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board", userId));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);

        var command = new CreateGoalCommand(anotherUserId, "Test", 0, DateOnly.MinValue, boardId, null);

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
    

    [Test]
    public async Task ShouldRequireValidOwnerUser()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board", userId));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        
        var command = new CreateGoalCommand(userId, "Test", 0, DateOnly.MinValue, boardId, anotherUserId);

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldCreateGoal()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board", userId));
        await Testing.SendAsync(new OpenAccessCommand(boardId, anotherUserId, userId));

        var command = new CreateGoalCommand(userId, "Test", 52, new DateOnly(2022, 1, 1), boardId, anotherUserId);
        var goalId = await Testing.SendAsync(command);

        var goal = await Testing.FindAsync<Goal>(goalId);
        goal.Should().NotBeNull();
        goal!.Id.Should().Be(goalId);
        goal.BoardId.Should().Be(boardId);
        goal.Name.Should().Be("Test");
        goal.Amount.Should().Be(52);
        goal.DeadLineDate.Should().Be(new DateOnly(2022, 1, 1));
        goal.OwnerUserId.Should().Be(anotherUserId);
    }
}
