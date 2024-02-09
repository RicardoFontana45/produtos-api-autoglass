using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProdutosApi.Application.Behaviors;
using ProdutosApi.Application.Queries;
using ProdutosApi.Infrastructure.Data;
using ProdutosApi.Infrastructure.Repositories;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace ProdutosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProdutosApi", Version = "v1" });
            });

            services.AddDbContext<ProdutosApiDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IProdutoQueries, ProdutoQueries>();
            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
            services.AddTransient<IFornecedorQueries, FornecedorQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProdutosApi v1"));
            }

            app.UseExceptionHandler(options => options.Run(async context =>
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var ex = context.Features.Get<IExceptionHandlerFeature>();

                if (ex == null) return;

                if (ex.Error is ValidationException validationException)
                {
                    var errors = validationException.Errors.Select(error => new { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage });
                    var json = JsonSerializer.Serialize(new { Message = ex.Error.Message, errors = errors });
                    await context.Response.WriteAsync(json);
                }
                else
                {
                    var errorMessage = new { Message = ex.Error.Message };
                    var json = JsonSerializer.Serialize(errorMessage);
                    await context.Response.WriteAsync(json);
                }
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
