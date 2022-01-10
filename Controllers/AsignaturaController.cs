using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    public class AsignaturaController : Controller
    {
        private EscuelaContext _context;

        [Route("Asignatura")]
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignaturaId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Index(string asignaturaId)
        {
            if (!string.IsNullOrWhiteSpace(asignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                 where asig.Id == asignaturaId
                                 select asig;
                return View(asignatura.SingleOrDefault());
            }
            else
            {
                ViewBag.controller = "Asignatura";
                ViewBag.actionEditar = "Editar";
                return View("MultiAsignatura", _context.Asignaturas);
            }
        }
        public IActionResult MultiAsignatura()
        {
            ViewBag.Fecha = DateTime.Now;
            ViewBag.controller = "Asignatura";
            ViewBag.actionEditar = "Editar";
            return View("MultiAsignatura", _context.Asignaturas);
        }

        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;
            List<SelectListItem> listaCursos = SharedLoad.CargarSelectListCursos(_context);
            ViewData["listaCursos"] = listaCursos;

            return View();

        }

        [HttpPost]
        public IActionResult Create(Asignatura asignatura)
        {
            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (asignatura.Id == null)
                {
                    asignatura.Id = Guid.NewGuid().ToString();
                }

                _context.Asignaturas.Add(asignatura);
                _context.SaveChanges();
                ViewBag.alert="alert-success";
                ViewBag.mensajeExtra = "Asignatura creado exitosamente";
                return View("Index", asignatura);
            }
            else
            {
                return View(asignatura);
            }

        }

        [Route("Asignatura/Editar/{asignaturaId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Editar(string asignaturaId)
        {
            var asignatura = from cur in _context.Asignaturas
                             where cur.Id == asignaturaId
                             select cur;
            ViewBag.Fecha = DateTime.Now;
            List<SelectListItem> listaCursos = SharedLoad.CargarSelectListCursos(_context);
            ViewData["listaCursos"] = listaCursos;
            return View(asignatura.FirstOrDefault());

        }

        [HttpPost]
        [Route("Asignatura/Editar/{asignaturaId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Editar(Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(asignatura).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.alert="alert-success";
                ViewBag.mensajeExtra = "Asignatura editado exitosamente";
                return View("Index", asignatura);
            }
            else
            {
                return View(asignatura);
            }




        }

        [Route("Asignatura/Eliminar/{asignaturaId}")] //con este enrutamiento determino los parametros que necesita para funcionar este metodo
        public IActionResult Eliminar(string asignaturaId)
        {
            var asignatura = _context.Asignaturas.Where(d => d.Id == asignaturaId).FirstOrDefault();
            ViewBag.Fecha = DateTime.Now;
            _context.Entry(asignatura).State = EntityState.Deleted;
            _context.SaveChanges();
            ViewBag.alert="alert-danger";
            ViewBag.mensajeExtra = "Asignatura eliminado exitosamente";

            return View("Index", asignatura);

        }



        //Es necesario crear un constructor que reciba un Dbcontext ya que esto es una caracteristica 
        //de la inyeccion de dependencia el cual fue configurado en la clase startup.cs
        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }
    }

}