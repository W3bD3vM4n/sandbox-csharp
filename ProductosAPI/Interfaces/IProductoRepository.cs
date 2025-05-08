using ProductosAPI.Models;

namespace ProductosAPI.Interfaces
{
    // Interfaces (aplicando Principio de Segregación de Interfaces - ISP)
    public interface IProductoRepository
    {
        IEnumerable<Producto> ObtenerTodos();
        Producto ObtenerPorId(int id);
        void Agregar(Producto producto);
        void Actualizar(Producto producto);
        void Eliminar(int id);
    }
}
