using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    public class CursoController : Controller
    {
        //Se crea una propiedad privada que refleja el contexto del repositorio en el construcutor
        private EscuelaContext _context;

        [Route("Curso")]
        [Route("Curso/Index")]
        [Route("Curso/Index/{cursoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Index(string cursoId)
        {
            if (!string.IsNullOrWhiteSpace(cursoId))
            {
                var curso = from cur in _context.Cursos
                            where cur.Id == cursoId
                            select cur;
                return View(curso.SingleOrDefault());
            }
            else
            {
                ViewBag.controller = "Curso";
                ViewBag.actionEditar = "Editar";
                return View("MultiCurso", _context.Cursos);
            }
        }

        public IActionResult MultiCurso()
        {
            var listaCursos = _context.Cursos;
            ViewBag.controller = "Curso";
            
            return View("MultiCurso", listaCursos);
        }



        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;

            return View();

        }

        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {   
                if(curso.Id ==null){
                    curso.Id = Guid.NewGuid().ToString();
                }
                var escuela = _context.Escuelas.FirstOrDefault();

                curso.EscuelaId = escuela.Id;

                _context.Cursos.Add(curso);
                _context.SaveChanges();
                ViewBag.alert="alert-success";
                ViewBag.mensajeExtra = "Curso creado exitosamente";
                return View("Index", curso);
            }
            else
            {
                return View(curso);
            }

        }

        [Route("Curso/Editar/{cursoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Editar(string cursoId)
        {
            var curso = from cur in _context.Cursos
                            where cur.Id == cursoId
                            select cur;
            ViewBag.Fecha = DateTime.Now;

            return View(curso.FirstOrDefault());

        }

        [HttpPost]
        [Route("Curso/Editar/{cursoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Editar(Curso curs)
        {
            if (ModelState.IsValid)
            {
               _context.Entry(curs).State =EntityState.Modified;
                _context.SaveChanges();
                ViewBag.alert="alert-success";
                ViewBag.mensajeExtra = "Curso editado exitosamente";
                return View("Index",curs);
            }
            else
            {
                return View(curs);
            }


            

        }

         [Route("Curso/Eliminar/{cursoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Eliminar(string cursoId)
        {
            var curso =  _context.Cursos.Where(d => d.Id==cursoId).FirstOrDefault();
            ViewBag.Fecha = DateTime.Now;
            _context.Entry(curso).State =EntityState.Deleted;
             _context.SaveChanges();
             ViewBag.alert="alert-danger";
             ViewBag.mensajeExtra = "Curso eliminado exitosamente";  

            return View("Index",curso);

        }




        public CursoController(EscuelaContext context)
        {
            _context = context;
        }


    }
}