using Microsoft.Extensions.DependencyInjection;
using MiniBook.Data.Context;
using MiniBook.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBook.Data
{
    public static class ServiceRegistration
    {
        public static void AddResourceData( this IServiceCollection services, string connectString, string dbName)
        {
            services.AddSingleton(s => new ResourceDbContext(connectString, dbName));
            services.AddScoped<UserRepository>();
            services.AddScoped<PostRepository>();
            services.AddScoped<FeedRepository>();
        }
    }
}
