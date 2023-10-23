using Application.Item.Commands;
using Application.Item.Queries;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[ApiController]
[Route("api/controller")]
public class ItemsController : ApiController
{
    [HttpPost]
    public async Task<Result> Add(CreateItemCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut]
    public async Task<Result> Update(UpdateItemCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete(string id)
    {
        return await Mediator.Send(new DeleteItemCommand() { Id = id });
    }
    
    [HttpGet]
    public async Task<Result> GetAll()
    {
        return await Mediator.Send(new GetItemsQuery());
    }
    
    public ItemsController(IMediator mediator) : base(mediator)
    {
    }
}