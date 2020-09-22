using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using University.Application.Commands.CreateStudent;
using University.Application.Interfaces.Repositories;
using University.Infrastructure.Data.Repositories;

namespace University.API.Code.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateStudentCommand).GetTypeInfo().Assembly);

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();

            return services;
        }
    }
}
