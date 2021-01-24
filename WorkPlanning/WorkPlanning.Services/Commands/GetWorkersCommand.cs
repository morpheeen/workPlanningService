using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanning.DA.Models;
using WorkPlanning.DA.Repository;
using WorkPlanning.Services.Conntracts;

namespace WorkPlanning.Services.Commands
{
    public class GetWorkersCommand : IRequest<ICollection<Worker>>
    {
    }

    internal class GetWorkersCommandHandler : IRequestHandler<GetWorkersCommand, ICollection<Worker>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkersCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<Worker>> Handle(GetWorkersCommand command, CancellationToken cancellationToken)
        {
            var results = await _repository.Get<WorkerModel>()
                .AsNoTracking()
                .OrderBy(w => w.Name)
                .ToListAsync(cancellationToken);

            return _mapper.Map<Worker[]>(results);
        }
    }
}
