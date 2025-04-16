using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Common.Exceptions;
using Finance.Domain.Entities;

namespace Finance.Application.FunctionalTests.Boards.Commands;

public class CreateBoardTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBoardCommand("", "");

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireCurrentUser()
    {
        var command = new CreateBoardCommand("Test", "");

        await FluentActions
            .Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateBoard()
    {
        var userId = await Testing.RunAsDefaultUserAsync();

        var command = new CreateBoardCommand("Test Board", userId);
        var id = await Testing.SendAsync(command);
        var item = await Testing.FindAsync<Board>(id);

        item.Should().NotBeNull();
        item!.CreatedBy.Should().Be(userId);
        item.Name.Should().Be(command.Name);
        item.UserIds.Should().Contain(userId);
        item.AdminIds.Should().Contain(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
