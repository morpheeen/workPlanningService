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
    public class AddWorkerShiftCommand : IRequest<Shift>
    {
        public Guid WorkerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    internal class AddWorkerShiftCommandValidator : AbstractValidator<AddWorkerShiftCommand>
    {
        public AddWorkerShiftCommandValidator()
        {
            RuleFor(_ => _.WorkerId)
               .NotEqual(Guid.Empty)
               .WithErrorCode(Errors.InvalidWorkerId);

            RuleFor(_ => _.Start)
              .LessThan(_ => _.End)
              .WithErrorCode(Errors.StartShouldBeLowerThanEnd);
        }
    }

    internal class AddWorkerShiftCommandHandler : IRequestHandler<AddWorkerShiftCommand, Shift>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AddWorkerShiftCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Shift> Handle(AddWorkerShiftCommand command, CancellationToken cancellationToken)
        {
            var shiftLength = (command.End - command.Start).TotalHours;
            if (shiftLength > 24)
                throw new Exception(Errors.ShiftSpansOver24Hours);

            var worker = await _repository.Get<WorkerModel>()
                .AnyAsync(s => s.Guid == command.WorkerId, cancellationToken);
            if (!worker)
                throw new Exception(Errors.WorkerNotFound);

            var existingShifts = await _repository.Get<ShiftModel>().Where(s => s.WorkerId == command.WorkerId).AsNoTracking().ToListAsync(cancellationToken);

            ValidationHelper.ValidateShift(command.Start, command.End, existingShifts);

            var newShift = new ShiftModel
            {
                Guid = Guid.NewGuid(),
                WorkerId = command.WorkerId,
                Start = command.Start,
                End = command.End
            };

            _repository.Add(newShift);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ShiftModel, Shift>(newShift);
        }
    }
}
