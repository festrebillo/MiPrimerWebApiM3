using MiPrimerWebApiM3.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Entities
{
    //IValidatableObject es VALIDACION POR MODELO
    public class Autor //: IValidatableObject  //entidades de nuestra base de datos
    {
        public int Id { get; set; }

        [Required]
        //[PrimeraLetraMayuscula]
       // [StringLength(10, ErrorMessage = "El campo debe tener {1} caracteres o menos")]
        public string Nombre { get; set; }
        /* [Range(18, 120)] // Validacion de edad debe ser entre 18 y 120
         public int Edad { get; set; }
         //[CreditCard]
         public string CreditCard { get; set; }
         [Url]
         public string Url { get; set; }
        */
        public List<Libro> Libros { get; set; }
       
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primerLetra = Nombre[0].ToString();
                if (primerLetra != primerLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula VALIDACION POR MODELO", new string[] { nameof(Nombre)}); //arreglo donde le paso el nombre de la propiedad
                }

            }
        }
    }
}
