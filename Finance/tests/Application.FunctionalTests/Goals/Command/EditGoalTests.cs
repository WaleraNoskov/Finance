using Finance.Application.Common.Exceptions;
using Finance.Application.Goals.Commands.EditGoalCommand;

namespace Finance.Application.FunctionalTests.Goals.Command;

public class EditGoalTests
{
    // [Test]
    // public async Task ShouldRequireMinimumFields()
    // {
    //     var command = new EditGoalCommand(0, "", "", 0, 0);
    //
    //     await FluentActions
    //         .Invoking(() => Testing.SendAsync(command))
    //         .Should().ThrowAsync<ValidationException>();
    // }
    //
    // [Test]
    // public async Task ShouldRequireBoardExists()
    // {
    //     var userId = await Testing.RunAsDefaultUserAsync();
    //     var command = new EditGoalCommand(userId, "Test", 0, DateOnly.MinValue, 0, null);
    //
    //     await FluentActions
    //         .Invoking(() => Testing.SendAsync(command))
    //         .Should().ThrowAsync<NotFoundException>();
    // }
    //
    // [Test]
    // public async Task ShouldRequireValidCurrentUser()
    // {
    //     var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
    //     var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board", userId));
    //     var anotherUserId = await Testing.RunAsUserAsync("User2", "Password2!", []);
    //     
    //     var command = new CreateGoalCommand(anotherUserId, "Test", 0, DateOnly.MinValue, boardId, null);
    //
    //     await FluentActions
    //         .Invoking(() => Testing.SendAsync(command))
    //         .Should().ThrowAsync<UnauthorizedAccessException>();
    // }
    //
    // [Test]
    // public async Task ShouldRequireValidOwnerUser()
    // {
    //     var userId = await Testing.RunAsUserAsync("User1", "Password1!", []);
    //     var boardId = await Testing.SendAsync(new CreateBoardCommand("Test Board", userId));
    //     
    //     var command = new CreateGoalCommand(userId, "Test", 0, DateOnly.MinValue, boardId, "");
    //     
    //     await FluentActions
    //         .Invoking(() => Testing.SendAsync(command))
    //         .Should().ThrowAsync<NotFoundException>();
    // }
}
