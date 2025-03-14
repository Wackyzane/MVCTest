using System;
using System.Data;
using System.Text.Json;
using Npgsql;
using Newtonsoft.Json.Serialization;
using TestProject.Controllers;
using TestProject.Services;
using TestProject.Repositories;

namespace TestProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(cors =>
            {
                cors.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllers();
            services.AddScoped<IDbConnection>(x => CreateDbConnection());
            services.AddTransient<PuppyService>();
            services.AddTransient<PuppyRepository>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private IDbConnection CreateDbConnection()
        {
            string connectionString = Configuration["db:ConnectionStrings:PgDbConnection"];
            return new NpgsqlConnection(connectionString);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}