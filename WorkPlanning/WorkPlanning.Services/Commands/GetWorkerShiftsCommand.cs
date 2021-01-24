using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanning.DA.Models;
using WorkPlanning.DA.Repository;
using WorkPlanning.Services.Conntracts;

namespace WorkPlanning.Services.Commands
{
    public class GetWorkerShiftsCommand : IRequest<ICollection<Shift>>
    {
        public Guid WorkerId { get; set; }
    }

    internal class GetWorkerShiftsCommandValidator : AbstractValidator<GetWorkerShiftsCommand>
    {
        public GetWorkerShiftsCommandValidator()
        {
            RuleFor(_ => _.WorkerId)
               .NotEqual(Guid.Empty)
               .WithErrorCode("Worker id is invalid");
        }
    }

    internal class GetWorkerShiftsCommandHandler : IRequestHandler<GetWorkerShiftsCommand, ICollection<Shift>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkerShiftsCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<Shift>> Handle(GetWorkerShiftsCommand command, CancellationToken cancellationToken)
        {
            var results = await _repository.Get<ShiftModel>()
                .Where(s=>s.WorkerId==command.WorkerId)
                .AsNoTracking()
                .OrderBy(w => w.Name)
                .ToListAsync(cancellationToken);

            return _mapper.Map<Shift[]>(results);
        }
    }
}
