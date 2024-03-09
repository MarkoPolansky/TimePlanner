using TimePlanner.App.Options;
using TimePlanner.DAL.Factories;
using TimePlanner.DAL;
using TimePlanner.DAL.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimePlanner.BL.Mappers;
using TimePlanner.Common.Tests.Factories;

namespace TimePlanner.App;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        DALOptions dalOptions = new();
        configuration.GetSection("TimePlanner:DAL").Bind(dalOptions);

        services.AddSingleton<DALOptions>(dalOptions);

        if (dalOptions.LocalDb is null && dalOptions.Sqlite is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (dalOptions.LocalDb?.Enabled == false && dalOptions.Sqlite?.Enabled == false)
        {
            throw new InvalidOperationException("No persistence provider enabled");
        }

        if ((dalOptions.LocalDb?.Enabled == true) && (dalOptions.Sqlite?.Enabled == true))
        {
            throw new InvalidOperationException("Both persistence providers enabled");
        }

        if (dalOptions.LocalDb?.Enabled == true)
        {
            services.AddSingleton<IDbContextFactory<TimePlannerDbContext>>(provider => new DbContextSqLiteTestingFactory(dalOptions.LocalDb.ConnectionString ));
            services.AddSingleton<IDbMigrator, NoneDbMigrator>();
        }

        if (dalOptions.Sqlite?.Enabled == true)
        {
            if (dalOptions.Sqlite.DatabaseName is null)
            {
                throw new InvalidOperationException($"{nameof(dalOptions.Sqlite.DatabaseName)} is not set");

            }
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName!);
            services.AddSingleton<IDbContextFactory<TimePlannerDbContext>>(provider => new DbContextSqLiteTestingFactory(databaseFilePath, dalOptions?.Sqlite?.SeedDemoData ?? false));
            services.AddSingleton<IDbMigrator, SqliteDbMigrator>();
        }

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ActivityTypeEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<UserEntityMapper>();
        services.AddSingleton<ProjectUserRelationEntityMapper>();

        return services;
    }
}
