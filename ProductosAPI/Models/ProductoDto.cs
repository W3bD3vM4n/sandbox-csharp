namespace ProductosAPI.Models
{
    // DTO para transferencia de datos
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // electronico o alimenticio
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Marca { get; set; }
        public int GarantiaMeses { get; set; }
        public DateTime? FechaCaducidad { get; set; }
    }
}
