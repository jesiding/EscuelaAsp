namespace AspNetCore.Models
{
    public interface ILugar
    {
       string Dirección { get; set; }

       void LimpiarLugar();

    }
}