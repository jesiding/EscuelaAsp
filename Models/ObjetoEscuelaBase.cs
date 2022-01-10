using System;

namespace AspNetCore.Models
{
    public abstract class ObjetoEscuelaBase
    {
        public string Id { get;  set; }

        //palabra reservada 'virtual' determina que las clases hijas puedan hacer uso peronalizada de la porpiedad osea un override
        public virtual string Nombre { get; set; }

        public ObjetoEscuelaBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public  override string ToString()
        {
            return $"{Nombre},{Id}";
        }
    }
}