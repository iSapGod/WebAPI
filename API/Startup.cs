using AutoMapper;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApiTestTask.Profiles;
using Microsoft.EntityFrameworkCore;
using WebApiTestTask.Services;

namespace WebApiTestTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<EFContext>(
                    options =>
                    options.UseSqlServer(Configuration.
                                         GetConnectionString("HostConnection"))
                );
            }
            else
            {
                services.AddDbContext<EFContext>(
                    options =>
                    options.UseSqlServer(Configuration.
                                         GetConnectionString("HostConnectionAzure"))
                );
            }
            services.AddCors(op => op.AddPolicy("Cors", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddControllers();
            services.AddAutoMapper(typeof(PhoneProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "CRUD demonstration", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("Cors");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "WebAPI API v1");
            });

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
