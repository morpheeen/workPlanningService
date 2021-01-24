using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Services.Commands;
using WorkPlanning.Services.Conntracts;

namespace WorkPlanning.Api.Controllers.v1
{
    [ApiController]
    [Route("workers/v1")]
    public class WorkersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpGet]
        public async Task<ICollection<Worker>> GetWorkers()
        {
            var command = new GetWorkersCommand();
            var results = await _mediator.Send(command);

            return results;
        }

    }
}
