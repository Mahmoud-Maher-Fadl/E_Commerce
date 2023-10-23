using Application.Category.Commands;
using Application.Category.Queries;
using Domain.common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ApiController
{
    [HttpPost]
    public async Task<Result> Create(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut]
    public async Task<Result> Update(UpdateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<Result> Delete(string id)
    {
        return await Mediator.Send(new DeleteCategoryCommand { Id = id });
    }

    [HttpGet]
    public async Task<Result> GetAll()
    {
        
        return await Mediator.Send(new GetCategoriesQuery());
    }

    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }
}