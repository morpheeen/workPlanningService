using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Services.Commands;
using WorkPlanning.Services.Contracts;

namespace WorkPlanning.Api.Controllers.v1
{
    /// <summary>
    /// Manages Workers
    /// </summary>
    [ApiController]
    [Route("workers/v1")]
    public class WorkersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves workers list
        /// </summary>
        /// <returns></returns>
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
