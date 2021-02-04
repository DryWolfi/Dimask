using BLL.Interfaces;
using BLL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Infrastructure
{
    public static class BllServices
    {
        public static void AddBLL(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddDbContext<AppDBContext>(optins => optins.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAssignmentService, AssignmentService>();
            services.AddTransient<IUserAssignmentService, UserAssignmentService>();
        }
    }
}
