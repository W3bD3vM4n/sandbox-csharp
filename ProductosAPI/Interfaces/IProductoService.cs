using ProductosAPI.Models;

namespace ProductosAPI.Interfaces
{
    public interface IProductoService
    {
        IEnumerable<Producto> ObtenerTodosLosProductos();
        Producto ObtenerProductoPorId(int id);
        void CrearProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void EliminarProducto(int id);
        IEnumerable<Producto> BuscarProductos(string criterio);
    }
}
