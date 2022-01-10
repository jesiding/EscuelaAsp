using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required(ErrorMessage ="El nombre del curso es requerido")]
        [StringLength(5)]// tamaño maximo
        public override string Nombre { get; set; }

        [Required(ErrorMessage ="Please select a working day")]
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }

        [Display(Prompt ="Direccion Correspondencia",Name ="Address")]
        [Required(ErrorMessage ="Se requiere incluir una direccion")]
        [MinLength(10,ErrorMessage ="Longitud minima es de 10 caracteres")]
        public string Dirección { get; set; }

        //por convencion se coloca la {Entidad}Id para referenciar la clase padre llave foranea
        public string EscuelaId { get; set; }

        //Se crea la propiedad que tiene como tipo de dato el objeto completo de la entidad relacionada
        public Escuela Escuela { get; set; }

        // public override string ToString()
        // {
        //     return  $"{Nombre}";
        // }
        
        
        
        


      
    }
}