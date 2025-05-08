using ProductosAPI.Models;
using System.Collections.Concurrent;

namespace ProductosAPI.Repositories
{
    // Implementación alternativa que usa ConcurrentDictionary (para escenarios concurrentes)
    public class ProductoRepositoryConcurrente
    {
        private readonly ConcurrentDictionary<int, Producto> _productos;

        public ProductoRepositoryConcurrente()
        {
            _productos = new ConcurrentDictionary<int, Producto>();

            // Datos iniciales
            var electronico1 = new ProductoElectronico
            {
                Id = 1,
                Nombre = "Laptop",
                Precio = 1200.00m,
                Marca = "Dell",
                GarantiaMeses = 24
            };

            _productos.TryAdd(electronico1.Id, electronico1);
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _productos.Values;
        }

        public Producto ObtenerPorId(int id)
        {
            _productos.TryGetValue(id, out var producto);
            return producto;
        }

        public void Agregar(Producto producto)
        {
            _productos.TryAdd(producto.Id, producto);
        }

        public void Actualizar(Producto producto)
        {
            _productos.AddOrUpdate(producto.Id, producto, (key, oldValue) => producto);
        }

        public void Eliminar(int id)
        {
            _productos.TryRemove(id, out _);
        }
    }
}
