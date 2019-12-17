using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17
{
    public static class Registrations
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<BanksManager, BanksManager>();
            services.AddTransient<ConversionsManager, ConversionsManager>();
            services.AddTransient<CryptoManager, CryptoManager>();
            services.AddTransient<CurrenciesManager, CurrenciesManager>();
            services.AddTransient<FeesManager, FeesManager>();
            services.AddTransient<LoginManager, LoginManager>();
            services.AddTransient<RegisterUserManager, RegisterUserManager>();
        }
    }
}
