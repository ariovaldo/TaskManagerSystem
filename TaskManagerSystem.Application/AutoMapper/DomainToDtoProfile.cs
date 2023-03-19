using AutoMapper;
using TaskManagerSystem.Application.Task.Query.Response;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.AutoMapper
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<SimpleTask, TaskResponse>();
        }
    }
}
