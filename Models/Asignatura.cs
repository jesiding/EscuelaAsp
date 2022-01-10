using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Models
{
    public class Asignatura:ObjetoEscuelaBase
    {
        
        [Required(ErrorMessage ="El nombre de la asignatura es requerido")]
        public override string Nombre { get; set; }   

        [Required(ErrorMessage ="Please select student course")]
        public string CursoId { get; set; }

        public Curso Curso { get; set; }

        public List<Evaluación> Evaluaciones { get; set; } //= new List<Evaluación>();
        
        
        
        
    }
}