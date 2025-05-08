namespace ProductosAPI.Models
{
    // Clase derivada (herencia)
    public class ProductoElectronico : Producto
    {
        public string Marca { get; set; }
        public int GarantiaMeses { get; set; }

        // Sobreescritura de método (polimorfismo)
        public override string ObtenerDescripcion()
        {
            return $"{Nombre} - {Marca} (Garantía: {GarantiaMeses} meses)";
        }
    }
}
