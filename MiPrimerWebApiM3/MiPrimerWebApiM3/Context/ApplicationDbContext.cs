using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Context
{
    public class ApplicationDbContext: DbContext  //aqui configuramos nuestras tablas de BD
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  //creamos un constructor, le pasaremos una instancia del dbcontext
            :base(options)  //a la clase base le pasamos estas opciones
        {

        }
        //creamos la relaciones con nuestra base de datos
        public DbSet<Autor> Autores { get; set; } //aqui estamos diciendo que en nuestra base de datos habra una tabla autores con el esquema de la calse autor
        public DbSet<Libro> Libros { get; set; }
    }
}
