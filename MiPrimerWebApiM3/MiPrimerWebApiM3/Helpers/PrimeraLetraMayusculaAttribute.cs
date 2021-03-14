using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Helpers
{
    //VALIDACION POR ATRIBUTO
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute //heredamos de una clase ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) //Verificamos que el valor no sea nulo o vacio
            {
                return ValidationResult.Success;
            }
            var firstLetter = value.ToString()[0].ToString(); //Buscamos la primera letra

            if (firstLetter != firstLetter.ToUpper()) //Verificamos que sea mayuscula
            {
                return new ValidationResult("La Primera Letra debe ser Mayuscula METODO POR ATRIBUTO"); //Mostramos un error si no es mayuscula
            }
            return base.IsValid(value, validationContext);
        }
    }
}
