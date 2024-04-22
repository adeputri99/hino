using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Application.Interfaces.Repositories.Configuration.Dapper;
using SkeletonApi.Application.Interfaces.Repositories.Filtering;
using SkeletonApi.Persistence.Contexts;
using SkeletonApi.Persistence.Repositories;
using SkeletonApi.Persistence.Repositories.Dapper;
using SkeletonApi.Persistence.Repositories.Filtering;

namespace SkeletonApi.Persistence.IServiceCollectionExtensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(IGenRepository<>), typeof(GenRepository<>))
                .AddTransient(typeof(IDataRepository<>), typeof(DataRepository<>))
                .AddScoped<DapperUnitOfWorkContext>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IAuthenticationUserRepository, AuthenticationRepository>()
                .AddTransient<IAccountRepository, AccountRepository>()
                .AddTransient<INotificationRepository, NotificationRepository>()
                .AddScoped<IDiviceDateRepository, DeviceDataRepository>()
                .AddScoped<ITypeRepository, TypeRepository>()
                .AddScoped<IOperatorRepository, OperatorRepository>()
                .AddScoped<ITaskRepository, TaskRepository>()
                .AddScoped<IBreakeRepository, BreakeRepository>()
                .AddScoped<IRepairRepository, RepairRepository>()
                .AddScoped<IDayRepository, DayRepository>()
                .AddScoped<IDefaultRepository, DefaultRepository>()
                .AddScoped<IMonthRepository, MonthRepository>()
                .AddScoped<IWeekRepository, WeekRepository>()
                .AddScoped<IYearRepository, YearRepository>()
                .AddTransient<IUserRepository, UserRepository>();
        }
    }
}