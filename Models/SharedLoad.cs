using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
namespace AspNetCore.Models
{
    public static class SharedLoad
    {

        /// <summary>
        /// Metodo estatico que retorna un select list con los cursos, por medio de este metodo alimentamos los select en las paginas html
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static List<SelectListItem> CargarSelectList(IEnumerable<ObjetoEscuelaBase> lista)
        {
          
            return lista.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id,
                    Selected = false
                };
            });
        }

        
     
    }
}