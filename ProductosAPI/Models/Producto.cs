namespace ProductosAPI.Models
{
    // Clase base abstracta (aplicando OOP: herencia y abstracción)
    public abstract class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public abstract string ObtenerDescripcion();
    }
}
