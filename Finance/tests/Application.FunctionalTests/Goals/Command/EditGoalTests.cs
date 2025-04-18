using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.EditCommand;
using Finance.Application.Common.Exceptions;
using Finance.Application.Goals.Commands.CreateGoal;
using Finance.Application.Goals.Commands.EditGoalCommand;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Goals.Command;

public class EditGoalTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new EditGoalCommand(0, "", 0, null);
    
        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }
    
    [Test]
    public async Task ShouldRequireGoalExists()
    {
        var userId = await Testing.RunAsDefaultUserAsync();
        
        var command = new EditGoalCommand(0, "Test", 0, null);
        
        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldRequireValidCurrentUser()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board"));
        var goalId = await Testing.SendAsync(new CreateGoalCommand( "Test Goal", 0, DateOnly.MinValue, boardId, null));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        
        var command = new EditGoalCommand(goalId, "Test Goal 2", 0, anotherUserId);
    
        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
    
    [Test]
    public async Task ShouldRequireOwnerUserExists()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board"));
        
        var command = new CreateGoalCommand("Test", 0, DateOnly.MinValue, boardId, "123, not mutters");
        
        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldEditGoal()
    {
        var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board"));
        var goalId = await Testing.SendAsync(new CreateGoalCommand("Test Goal", 0, DateOnly.MinValue, boardId, null));
        var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
        userId = await Testing.RunAsUserAsync("User1", "Password1!", []);

        await Testing.SendAsync(new EditGoalCommand(goalId, "Test Goal 2", 22, anotherUserId));

        var goal = await Testing.FindAsync<Goal>(goalId);
        goal.Should().NotBeNull();
        goal!.Id.Should().Be(goalId);
        goal.OwnerUserId.Should().Be(anotherUserId);
        goal.Name.Should().Be("Test Goal 2");
        goal.Amount.Should().Be(22);
        goal.BoardId.Should().Be(boardId);
        goal.LastModifiedBy.Should().NotBeNull();
        goal.LastModifiedBy.Should().Be(userId);
        goal.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
