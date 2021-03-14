    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiPrimerWebApiM3.Context;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Controllers
{
    //COn el controlador interactuamos con la tabla
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase //tenemos que darle la propiedad de controllerbase
    {
        private readonly ApplicationDbContext context;
        private readonly IClaseB claseB;
        private readonly ILogger<AutoresController> logger;

        //Inyextamos una instacion de ApplicationDbcontext en nuestro controlador
        public AutoresController(ApplicationDbContext context, ClaseB claseB,
            ILogger<AutoresController> logger) 
        {
            this.context = context;
            this.claseB = claseB;
            this.logger = logger;
        }

        //caba vez que quiera agregar una funcion similar tengo que crear una url diferente ya que me va agenerar conflicto en las funciones
        [HttpGet("Primer")]  //tenemos que definir una url diferente
        public ActionResult<Autor> GetPrimerAutor()  //me devolvera el primer autor de mi lista
        {
            return context.Autores.FirstOrDefault();
        }


        //Tengo 2 URL diferentes a los cuales me podra responder 
        //https://localhost:44394/api/autores/listado
        [HttpGet("Listado")] //esta es la funcion que ejecutaremos cuando se haga un httpget sobre nuestro url api/autores ESTA ES UNA PLANTILLA DE ROUTEO 
        //https://localhost:44394/listado
        [HttpGet("/Listado")] //si coloco un "/" voy a ignorar la ruta anterior https://localhost:44394/api/autores/listado = https://localhost:44394/listado
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get() //esta funcion se ejecutara cuando hagamos un get   ActionResult son los codigos de respuesta  IEnumerable es la entidad que nos retorna
        {
            logger.LogInformation("Obteniendo los autores"); //esto sale en consola o base de datos
            claseB.HacerAlgo();
            return context.Autores.Include(x => x.Libros).ToList(); //Data relacionada incluiremos los libros en la tabla autores
        }

        //select  api/autores/id/param2
        //[HttpGet("{id}/{param2?}", Name = "ObtenerAutor")] //Retorna un recurso en especifico y a ese numero lo llamaremos ID --- LE PONDREMOS UN NOMBRE A ESA RUTA---si agregamos un signo de interrogacion ese parametro es opcional
        [HttpGet("{id}/{param2=Gavilan}", Name = "ObtenerAutor")] // si yo quiero que tenga un valor por defecto le pondre un igual

        //Programacion Asincrona        
        //public async Task<ActionResult<Autor>> Get(int id, [BindRequired]string param2) //le estamos pasando 2 parametros a la URL --- bindrequired nos pide el valor requerido param2
        //con el actionresult<> retornamos varios datos como un 404 o un autor
        //usamos FirstOrDefaultAsync para usar promgramacion asincrona y marcamos la funcion como async y retornaremos un Tas<ActionResult<>>
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            logger.LogDebug("Buscando Autor de id " + id.ToString());
            //usamos el await para indicar que esperamos el resultado de esta operacion
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id); //buscamos el autor MUESTRAME EL AUTR CUYO ID SEA IGUAL AL QUE TE MANDO
            
            if (autor == null)
            {
                logger.LogWarning($"El autor de ID {id} no ha sido encontrado"); 
                return NotFound();
            }

            return autor;
        }

        //Programacion sincrona
        /*
        public ActionResult<Autor> Get(int id, string param2) 

        {          
            var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id); 
            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }
        */

        //IActionResult
        /*
        [HttpGet("{id}/{param2=Gavilan}", Name = "ObtenerAutor")]  Por buena practica debemos usar un ActionResult<>
          public IActionResult Get(int id, string param2)
        {
            var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id); 
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor); usamos el Ok para poder retornar un 200 Ok y en el cuerpo pondremos el autor
        }

        */


        //insert
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)   //Cuando se manda por un post ira en el cuerpo de la peticion
        {
            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);  //Pasamos RUTA DEL RECURSO, parametros del GET , pasamos autor
        }

        //update
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor value) //parametros ID que viene de la URL , Autor que viene del cuerpo
        {
            if (id != value.Id) //validamos el Id del autor que queremos actualizar
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id); //validacion buscar autor en la base de datos
            if (autor == null)
            {
                return NotFound();
            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}


/*
Fuentes de Datos

FromHeader : la fuente sera la cabecera de la peticion http
FromQuery : la fuente sera un query string
FromRoute: la fuente sera de los valores de ruta
FromForm : la fuente sera de algun tipo de contenido
FromBody : la fuente sera del cuerpo de la peticion 
FromServices: la fuente seran servicion de la aplicacion




Validaciones

Required : campo requerido 
StringLength : longitud de caracteres
Range : rango de un valor
CreditCard : tarjeta de credito
Compare : comparar 2 campos
Phone : formato de telefono
RegularExpression : valida que la expresion coincida con una expresion regular
Url: valida un url
BindRequired: requiere que se le haga bind a un propiedad

*/