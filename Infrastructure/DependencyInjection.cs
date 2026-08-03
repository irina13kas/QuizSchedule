using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfstructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IFineRepository, FineRepository>();
            services.AddScoped<IGameParticipantRepository, GameParticipantRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPointsRepository, PointsRepository>();
            services.AddScoped<IPushRepository, PushRepository>();
            services.AddScoped<IQuizmanRepository, QuizmanRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IReplacementRepository, ReplacementRepository>();
            services.AddScoped<ISmartRepository, SmartRepository>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            return services;
        }
    }
}
