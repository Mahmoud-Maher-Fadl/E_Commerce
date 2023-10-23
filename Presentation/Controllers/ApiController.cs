using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public abstract class ApiController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected ApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
}