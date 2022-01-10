using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AspNetCore.Models;

namespace AspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            //Se trata de que el webservice no inice hasta que se  cree la conexion a la base de datos
            var host = CreateHostBuilder(args).Build();

            //Se crea un espacio donde se lista los servicios configurados en el staturp.cs
            //para preguntar exactamente por el servicio del context (Este es el famoso Inyeccion de Dependecia)
            //Using se recomienda para hacer un proceso y muera cuando termine, evita que quede en memoria
            using (var scope = host.Services.CreateScope()) 
            {
                //almancena la lista de servicios
                var services = scope.ServiceProvider;
                try
                {
                    //crea el contexto solicitado
                    var context = services.GetRequiredService<EscuelaContext>();
                    //Asegurar que ya esta la base de datos creada
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocurrio un error");
                }
            }



            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
