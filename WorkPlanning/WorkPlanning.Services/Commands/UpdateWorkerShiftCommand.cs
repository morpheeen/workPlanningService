using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanning.DA.Models;
using WorkPlanning.DA.Repository;
using WorkPlanning.Services.Contracts;
using WorkPlanning.Services.Helpers;

namespace WorkPlanning.Services.Commands
{
    public class UpdateWorkerShiftCommand : IRequest<Shift>
    {
        public Guid Guid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    internal class UpdateWorkerShiftCommandValidator : AbstractValidator<UpdateWorkerShiftCommand>
    {
        public UpdateWorkerShiftCommandValidator()
        {
            RuleFor(_ => _.Guid)
              .NotEqual(Guid.Empty)
              .WithErrorCode(Errors.InvalidId);

            RuleFor(_ => _.Start)
              .LessThan(_ => _.End)
              .WithErrorCode(Errors.StartShouldBeLowerThanEnd);
        }
    }

    internal class UpdateWorkerShiftCommandHandler : IRequestHandler<UpdateWorkerShiftCommand, Shift>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UpdateWorkerShiftCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Shift> Handle(UpdateWorkerShiftCommand command, CancellationToken cancellationToken)
        {
            var shiftLength = (command.End - command.Start).TotalHours;
            if (shiftLength > 24)
                throw new Exception(Errors.ShiftSpansOver24Hours);

            var existingShift = await _repository.Get<ShiftModel>()
                .FirstOrDefaultAsync(s => s.Guid == command.Guid, cancellationToken);
            if (existingShift == null)
            {
                throw new Exception(Errors.ShiftNotFound);
            }

            var workerShifts = await _repository.Get<ShiftModel>()
                .Where(s => s.WorkerId == existingShift.WorkerId && s.Guid != existingShift.Guid)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            ValidationHelper.ValidateShift(command.Start, command.End, workerShifts);
            

            existingShift.Start = command.Start;
            existingShift.End = command.End;

            await _repository.SaveChangesAsync();

            return _mapper.Map<ShiftModel, Shift>(existingShift);
        }

    }
}
