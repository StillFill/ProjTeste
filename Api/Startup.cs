using AspnetCore.EFCore_Dapper.Data.Repositories.Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.Interfaces.Produtos;
using Domain.Models;
using Services;

namespace Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvcConfig => {
                mvcConfig.EnableEndpointRouting = false;
            });

            services.AddScoped<IParametrosRepository, ParamProdutoRepository>();
            services.AddScoped<IProdutosRepository, ProdutosRepository>();

            services.AddScoped<ITipoProdutoService, TipoProdutoService>();

            FluentMapper.Initialize(c =>
            {
                c.AddMap(new ParametrosMap());
                c.ForDommel();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(config => {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });

            app.UseMvc();
        }
    }
}
