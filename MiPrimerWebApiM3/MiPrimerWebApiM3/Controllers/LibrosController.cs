using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Context;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(x => x.Autor).ToList(); //traerenmos el libro con su autor 
        }

        [HttpGet("{id}", Name = "ObtenerLibro")] //Retorna un recurso en especifico y a ese numero lo llamaremos ID --- LE PONDREMOS UN NOMBRE A ESA RUTA
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id); //buscamos el autor MUESTRAME EL libro CUYO ID SEA IGUAL AL QUE TE MANDO

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        //insert
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)   //Cuando se manda por un post ira en el cuerpo de la peticion
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);  //Pasamos RUTA DEL RECURSO, parametros del GET , pasamos autor
        }

        //update
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro) //parametros ID que viene de la URL , Autor que viene del cuerpo
        {
            if (id != libro.Id) //validamos el Id del libro que queremos actualizar
            {
                return BadRequest();
            }

            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.Id == id); //validacion buscar libro en la base de datos
            if (libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}
