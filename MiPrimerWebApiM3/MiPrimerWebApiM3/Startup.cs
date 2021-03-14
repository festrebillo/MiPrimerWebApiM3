    using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiPrimerWebApiM3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3
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
            //Centralizamos la Inyeccion de dependencias en un solo lugar
            services.AddTransient<ClaseB>(); //INYECCION DE DEPENDENCIAS Cuando tengamos una dependecia de IclaseB en algun lugar de nuestra applicacion esta sera satisfecha con ClaseB
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); //configuramos la conexion a la BD
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); //solucion a error de referencia ciclica

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

    /*
     Tiempo de Vida: Los servicion pueden tener diferentes tiempos de vida, tenemos 3 tipos:

    Trasient: Cada vez que un servicio sea solicitado se va a servir una nueva instancia de la clase 
    EJ:
    services.AddTransient<IClaseB, ClaseB>();

    Scoped: Son creados uno por cada peticion HTTP, es decir si distintas clases piden el mismo servicio durante una peticion http se les estregara la misma instancia 
    EJ:
    services.AddScoped<IClaseB, ClaseB>();

    Singleton: Siempre se nos dara la misma instancia del servicio
    EJ:
    services.AddSingleton<IClaseB, ClaseB>();



    
     */
}
