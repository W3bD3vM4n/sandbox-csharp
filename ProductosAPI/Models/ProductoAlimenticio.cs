namespace ProductosAPI.Models
{
    // Otra clase derivada (herencia)
    public class ProductoAlimenticio : Producto
    {
        public DateTime FechaCaducidad { get; set; }

        // Sobreescritura de método (polimorfismo)
        public override string ObtenerDescripcion()
        {
            return $"{Nombre} (Caduca: {FechaCaducidad.ToShortDateString()})";
        }
    }
}
