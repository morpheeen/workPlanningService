using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanning.DA.Models;
using WorkPlanning.DA.Repository;

namespace WorkPlanning.Services.Commands
{
    public class DeleteShiftCommand : IRequest<bool>
    {
        public Guid ShiftId { get; set; }
    }

    internal class DeleteShiftCommandValidator : AbstractValidator<DeleteShiftCommand>
    {
        public DeleteShiftCommandValidator()
        {
            RuleFor(_ => _.ShiftId)
               .NotEqual(Guid.Empty)
               .WithErrorCode(Errors.InvalidId);
        }
    }

    internal class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand, bool>
    {
        private readonly IRepository _repository;

        public DeleteShiftCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteShiftCommand command, CancellationToken cancellationToken)
        {
            var shift = await _repository.Get<ShiftModel>()
                .FirstOrDefaultAsync(s => s.Guid == command.ShiftId, cancellationToken);

            if (shift == null)
                return false;

            _repository.Delete(shift);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
