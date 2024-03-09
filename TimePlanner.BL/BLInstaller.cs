using TimePlanner.BL.Mappers;
using TimePlanner.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using TimePlanner.BL.Facades.Interfaces;
using TimePlanner.BL.Facades;
using TimePlanner.BL.Mappers.Interfaces;
using TimePlanner.DAL.Entities;
using TimePlanner.BL.Models;

namespace TimePlanner.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
            .AsMatchingInterface()
            .WithSingletonLifetime());

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(ModelMapperBase<,,>)))
            .AsMatchingInterface()
            .WithSingletonLifetime());

        return services;
    }
}
