using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Application.Services.Tasks;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Interfaces.Services;
using TaskManagerSystem.Infra.Data.Context;
using TaskManagerSystem.Infra.Data.Repository;
using TaskManagerSystem.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) =>
    {
        services.AddDbContext<TaskManagerSystemContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ITaskService, TaskService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
