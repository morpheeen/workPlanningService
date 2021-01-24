using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Services.Commands;
using WorkPlanning.Services.Conntracts;

namespace WorkPlanning.Api.Controllers.v1
{
    [ApiController]
    [Route("shifts/v1")]
    public class ShiftsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("worker/{workerId:guid}")]
        [HttpGet]
        public async Task<ICollection<Shift>> GetWorkerShifts(Guid workerId)
        {
            var command = new GetWorkerShiftsCommand
            {
                WorkerId = workerId
            };
            var results = await _mediator.Send(command);

            return results;
        }
    }
}
