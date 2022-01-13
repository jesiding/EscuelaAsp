using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Models
{
    public class Evaluación : ObjetoEscuelaBase
    {
        public string CursoId { get; set; }
        public Curso Curso { get; set; }
        
        
        [Required(ErrorMessage ="El nombre de la Evaluacion es requerido")]
        public string AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        public string AsignaturaId { get; set; }
        public Asignatura Asignatura { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 5.00,ErrorMessage ="Nota no puede ser mayor de 5.00")]
        public decimal Nota { get; set; }

        public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}