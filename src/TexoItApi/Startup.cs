using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TexoIt.Core.Interfaces;
using TexoIt.Infra.EntityFramework;

namespace TexoIt.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<MovieContext>(c => c.UseSqlite("DataSource=file::memory:?cache=shared"));
            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(
                    new BadRequestResponse() { Erros = context.ModelState.ToHttpMessage() });
                })
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffK";
                });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
