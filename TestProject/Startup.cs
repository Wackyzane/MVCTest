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

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                // To handle reference loops, use `JsonSerializerOptions`
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

                // Customize the property naming policy, for example, use camel case
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

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

            //using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            //{
            //    conn.Open();
            //    Console.WriteLine("Connected to PostgreSQL database!");

            //    // Example query execution
            //    using (var cmd = new NpgsqlCommand("SELECT version();", conn))
            //    {
            //        string version = (string)cmd.ExecuteScalar();
            //        Console.WriteLine($"PostgreSQL Version: {version}");
            //    }
            //    return conn;
            //}
            return new NpgsqlConnection(connectionString);
            //return new MSqlConnection(connectionString);
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
