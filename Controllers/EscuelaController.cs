using System;
using System.Linq;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class EscuelaController : Controller
    {
        //Se crea una propiedad privada que refleja el contexto del repositorio en el construcutor
        private EscuelaContext _context;
        public IActionResult Index()
        {
            // se utiliza terminos de linq para alimentar a los modelos
            //FirstOrDefault se trae el primer registro de la tabla o modelo relacionado
            var escuela = _context.Escuelas.FirstOrDefault();

            return View(escuela);
        }

        public EscuelaController(EscuelaContext context)
        {
            _context=context;
        }
    }

}