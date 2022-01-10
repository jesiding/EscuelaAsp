using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Models
{
    /// <summary>
    /// En esta clase que hereda de DBContext la idea es referencias el modelo definido en Csharp con
    /// las tablas de la base de datos
    /// </summary>
    public class EscuelaContext : DbContext
    {
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }

        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        //se sobreescribe metodo de la clase dbcontext el cual tiene como funcionalidad
        //insertar datos a la base de datos definida
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //como primer paso se recomienda que se llame a el propio metodo de la clase padre y
            //Se le envia el parametro que se recibe
            base.OnModelCreating(modelBuilder);
            var escuela = new Escuela();
            escuela.AñoDeCreación = 2013;
            escuela.Nombre = "Sofias School";
            escuela.Id = Guid.NewGuid().ToString();
            escuela.TipoEscuela = TiposEscuela.Primaria;
            escuela.Pais = "Colombia";
            escuela.Ciudad = "Cartagena";
            escuela.Dirección = "Brisas de Galicia Apto 405 Bloque M";



            //Cargar Curos de la escuela
            var cursos = CargarCursos(escuela);

            //x cada curso cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);

            var alumnos=CargarAlumnos(cursos);

            modelBuilder.Entity<Escuela>().HasData(
                escuela
            );
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();
            Random rd = new Random();
            //x cada curso cargar alumnos
            foreach (var curso in cursos)
            {
                int cant = rd.Next(5, 20);
                var tmplist = GenerarAlumnosAlAzar(cant, curso);
                listaAlumnos.AddRange(tmplist);
            }
            var count =listaAlumnos.Count();
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {

                var tmplist = new List<Asignatura>{
                   new Asignatura
              {
                  Nombre = "Matemáticas",
                  CursoId = curso.Id
              },
                new Asignatura{ Nombre = "Educación Física",CursoId = curso.Id},
                new Asignatura{ Nombre = "Castellano",CursoId = curso.Id},
                new Asignatura{ Nombre = "Ciencias Naturales",CursoId = curso.Id},
                new Asignatura{ Nombre = "Programacion",CursoId = curso.Id}
                };
                listaCompleta.AddRange(tmplist);
            }
            return listaCompleta;

        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso(){
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId=escuela.Id,
                    Nombre = "101",
                    Jornada = TiposJornada.Mañana,
                    Dirección="Av 101"
                },
                new Curso(){Id = Guid.NewGuid().ToString(),EscuelaId=escuela.Id,Nombre = "201",Jornada = TiposJornada.Mañana,Dirección="Av 101"},
                new Curso(){Id = Guid.NewGuid().ToString(),EscuelaId=escuela.Id,Nombre = "301",Jornada = TiposJornada.Mañana,Dirección="Av 101"},
                new Curso(){Id = Guid.NewGuid().ToString(),EscuelaId=escuela.Id,Nombre = "401",Jornada = TiposJornada.Tarde,Dirección="Av 101"},
                new Curso(){Id = Guid.NewGuid().ToString(),EscuelaId=escuela.Id,Nombre = "501",Jornada = TiposJornada.Tarde,Dirección="Av 101"},
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad,Curso curso)
        {
            string[] nombre = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno {
                                    CursoId = curso.Id,
                                    Nombre = $"{n1} {n2} {a1}" };
            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }


    }
}