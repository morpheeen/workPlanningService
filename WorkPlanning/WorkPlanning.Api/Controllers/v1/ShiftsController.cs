using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Services.Commands;
using WorkPlanning.Services.Contracts;

namespace WorkPlanning.Api.Controllers.v1
{
    /// <summary>
    /// Manages Shifts
    /// </summary>
    [ApiController]
    [Route("shifts/v1")]
    public class ShiftsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves shifts list for a worker
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes a shift
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        [Route("{shiftId:guid}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteShift(Guid shiftId)
        {
            var command = new DeleteShiftCommand
            {
                ShiftId = shiftId
            };
            var result = await _mediator.Send(command);
            if (result)
                return Ok();
            return BadRequest("Shift not found");
        }

        /// <summary>
        /// Adds a new shift for a worker
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        [Route("worker/{workerId:guid}")]
        [HttpPost]
        public async Task<ActionResult> AddWorkerShift(Guid workerId, [FromBody] Shift shift)
        {
            if (shift == null)
                return BadRequest("Invalid request body");
            var command = new AddWorkerShiftCommand
            {
                WorkerId = workerId,
                Start = shift.Start,
                End = shift.End
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Updates a shift
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        [Route("{shiftId:guid}")]
        [HttpPut]
        public async Task<ActionResult> UpdateShift(Guid shiftId, [FromBody] Shift shift)
        {
            if (shift == null)
                return BadRequest("Invalid request body");
            var command = new UpdateWorkerShiftCommand
            {
                Guid = shiftId,
                Start = shift.Start,
                End = shift.End
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
