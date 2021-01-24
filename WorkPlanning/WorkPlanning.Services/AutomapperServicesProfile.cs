using AutoMapper;
using WorkPlanning.DA.Models;
using WorkPlanning.Services.Contracts;

namespace WorkPlanning.Services
{
    public class AutomapperServicesProfile : Profile
    {

        public AutomapperServicesProfile()
        {

            CreateMap<WorkerModel, Worker>()
                .ReverseMap();

            CreateMap<ShiftModel, Shift>()
                .ReverseMap();
        }
    }
}
