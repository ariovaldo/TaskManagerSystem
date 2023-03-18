using AutoMapper;
using TaskManagerSystem.Application.Task.Query;
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
