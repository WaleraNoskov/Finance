namespace Finance.Application.Goals.Commands.EditGoalCommand;

public record EditGoalCommand(int id, string CurrentUserId, string Name, decimal? Amount) : IRequest<int>;

