using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Data_Layer.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using iRepository;
using BusinessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApplication17.Data;
using NLog;
using System.IO;
using LoggerService;
using WebApplication17.Extensions;
using BusinessLayer.Framework;

namespace DataLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            //services.AddDbContext<TodoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var connection = @"Server=(localdb)\mssqllocaldb;Database=CryptoExchange;Trusted_Connection=True;ConnectRetryCount=0";
            // services.AddDbContext<Contexts>
            // (options => options.UseInMemoryDatabase("Crypto"));
            /*for create a db: Add - Migration si numele  */
            services.AddDbContext<Contexts>
             (options => options.UseSqlServer(connection));
            services.AddCors().AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddXmlDataContractSerializerFormatters();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<LoginRepository, LoginManager>();


            var config = new AutoMapper.MapperConfiguration(c =>
                {
                    c.AddProfile(new ApplicationProfile());
                });

            var mapper = config.CreateMapper();
            services.RegisterServices(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton(mapper);

            services.AddCors(options =>
            {
                options.AddPolicy("foo",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });


            services.AddControllers();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureCustomExceptionMiddleware();
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("foo");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
