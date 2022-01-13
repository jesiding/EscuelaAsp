using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace AspNetCore
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
            services.AddControllersWithViews();


            // services.AddRazorPages().AddViewOptions(option => 
            // {
            //     option.HtmlHelperOptions.ClientValidationEnabled=true;
            // });
            /*Configuracion de servicio que determina el contexto de la base de datos donde se asigna el tipo
                de base de datos y el nombre de la base de datos
            */
            // services.AddDbContext<EscuelaContext>(
            //     options => options.UseInMemoryDatabase(databaseName:"testDB")
            // );
            string connString = ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnectionString");
            services.AddDbContext<EscuelaContext>(
                options => options.UseSqlServer(connString)
            );
            services.Configure<RequestLocalizationOptions>(option =>
            {
                var lenguajesSoportados = new List<CultureInfo>
                {
                    new CultureInfo("es"),
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                    new CultureInfo("pt"),
                };
                option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es");
                option.SupportedCultures = lenguajesSoportados;
                option.SupportedUICultures = lenguajesSoportados;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Escuela}/{action=Index}/{id?}");
            });
        }
    }
}
