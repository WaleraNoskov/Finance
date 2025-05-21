using Finance.Application.Boards.Commands.CreateBoardCommand;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Finance.Web.Endpoints;

public class Boards : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(Create);
        // .MapGet(GetTodoItemsWithPagination)
        // .MapPost(CreateTodoItem)
        // .MapPut(UpdateTodoItem, "{id}")
        // .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
        // .MapDelete(DeleteTodoItem, "{id}");
    }

    public async Task<Created<int>> Create(ISender sender, CreateBoardCommand command)
    {
        var id = await sender.Send(command);
        
        return TypedResults.Created($"/{nameof(Boards)}/{id}", id);
    }
}
