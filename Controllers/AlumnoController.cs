using System;
using System.Collections.Generic;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.Controllers
{
    public class AlumnoController : Controller
    {
        //Se crea una propiedad privada que refleja el contexto del repositorio en el construcutor
        private EscuelaContext _context;

        [Route("Alumno")]
        [Route("Alumno/Index")]
        [Route("Alumno/Index/{alumnoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Index(string alumnoId)
        {
            if (!string.IsNullOrWhiteSpace(alumnoId))
            {
                var alumno = from alum in _context.Alumnos
                             where alum.Id == alumnoId
                             select alum;
                if(alumno.SingleOrDefault()!=null)             
                    return View(alumno.SingleOrDefault());

                return MultiAlumno();
            }
            else
            {
               return  MultiAlumno();
            }
        }

        public IActionResult MultiAlumnos()
        {
            return MultiAlumno();
        }

        private IActionResult MultiAlumno()
        {
            var listaAlumnos = _context.Alumnos;
            ViewBag.controller = "Alumno";
            ViewBag.actionEditar = "Editar";
            return View("MultiAlumno", listaAlumnos);
        }

        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;
            List<SelectListItem> listaCursos = SharedLoad.CargarSelectList(_context.Cursos.ToList());
            ViewData["listaCursos"] = listaCursos;

            return View();

        }

        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (alumno.Id == null)
                {
                    alumno.Id = Guid.NewGuid().ToString();
                }

                _context.Alumnos.Add(alumno);
                _context.SaveChanges();
                ViewBag.alert="alert-success";
                ViewBag.mensajeExtra = "Alumno creado exitosamente";
                return View("Index", alumno);
            }
            else
            {
                return View(alumno);
            }

        }

        [Route("Alumno/Editar/{alumnoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Editar(string alumnoId)
        {
            var alumno = from cur in _context.Alumnos
                         where cur.Id == alumnoId
                         select cur;
            ViewBag.Fecha = DateTime.Now;
            List<SelectListItem> listaCursos = SharedLoad.CargarSelectList(_context.Cursos.ToList());
            ViewData["listaCursos"] = listaCursos;
            if(alumno.SingleOrDefault()!=null)
                return View(alumno.FirstOrDefault());
            
            return RedirectToAction("Create");
        }

        [HttpPost]
        [Route("Alumno/Editar/{alumnoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Editar(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(alumno).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.alert="alert-success";
                ViewBag.mensajeExtra = "Alumno editado exitosamente";
                return View("Index", alumno);
            }
            else
            {
                return View(alumno);
            }




        }


        [Route("Alumno/Eliminar/{alumnoId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Eliminar(string alumnoId)
        {
            var alumno = _context.Alumnos.Where(d => d.Id == alumnoId).FirstOrDefault();
            if(alumno!=null)
            {ViewBag.Fecha = DateTime.Now;
            _context.Entry(alumno).State = EntityState.Deleted;
            _context.SaveChanges();
            ViewBag.alert="alert-danger";
            ViewBag.mensajeExtra = "Alumno eliminado exitosamente";

            return View("Index", alumno);
            }else
                return RedirectToAction("MultiAlumnos");
        }
        

        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }


    }
}