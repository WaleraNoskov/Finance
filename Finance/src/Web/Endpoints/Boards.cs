using Finance.Application.Boards.Commands.CreateBoardCommand;
using Finance.Application.Boards.Commands.DeleteBoardCommand;
using Finance.Application.Boards.Commands.EditCommand;
using Finance.Application.Boards.Queries.GetBoards;
using Finance.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Finance.Web.Endpoints;

public class Boards : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(Create, "Create")
            .MapGet(GetAll, "GetAll")
            .MapPut(Update, "Update")
            .MapDelete(Delete, "Delete");
    }

    public async Task<Created<int>> Create(ISender sender, CreateBoardCommand command)
    {
        var id = await sender.Send(command);
        
        return TypedResults.Created($"/{nameof(Boards)}/{id}", id);
    }
    
    public async Task<Ok<PaginatedList<BoardBriefDto>>> GetAll(ISender sender,
        [AsParameters] GetBoardsWithPagination query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
    
    public async Task<Results<NoContent, BadRequest>> Update(ISender sender,
        UpdateBoardCommand command)
    {
        await sender.Send(command);

        return TypedResults.NoContent();
    }
    
    public async Task<NoContent> Delete(ISender sender, int id)
    {
        await sender.Send(new DeleteBoardCommand(id));

        return TypedResults.NoContent();
    }
}
