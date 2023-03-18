using AutoMapper;
using TaskManagerSystem.Application.Task.Command.UpdateTask;
using TaskManagerSystem.Application.Task.Query;
using TaskManagerSystem.Application.Task.Query.GetTask;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.AutoMapper
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<UpdateTaskCommand, SimpleTask>();
        }
    }
}
