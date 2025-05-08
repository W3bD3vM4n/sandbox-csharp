using ProductosAPI.Interfaces;
using ProductosAPI.Models;

namespace ProductosAPI.Repositories
{
    // Implementación del repositorio (Principio de Responsabilidad Única - SRP)
    public class ProductoRepository : IProductoRepository
    {
        // Utilizando Dictionary como colección para almacenamiento por clave-valor
        private readonly Dictionary<int, Producto> _productos;

        public ProductoRepository()
        {
            _productos = new Dictionary<int, Producto>();

            // Datos iniciales
            var electronico1 = new ProductoElectronico
            {
                Id = 1,
                Nombre = "Laptop",
                Precio = 1200.00m,
                Marca = "Dell",
                GarantiaMeses = 24
            };

            var electronico2 = new ProductoElectronico
            {
                Id = 2,
                Nombre = "Smartphone",
                Precio = 800.00m,
                Marca = "Samsung",
                GarantiaMeses = 12
            };

            var alimento1 = new ProductoAlimenticio
            {
                Id = 3,
                Nombre = "Leche",
                Precio = 1200.00m,
                FechaCaducidad = DateTime.Now.AddDays(10)
            };

            _productos.Add(electronico1.Id, electronico1 );
            _productos.Add(electronico2.Id, electronico2 );
            _productos.Add(alimento1.Id, alimento1 );
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            // Usamos Values para devolver la colección de productos
            return _productos.Values;
        }

        public Producto ObtenerPorId(int id)
        {
            if (_productos.TryGetValue(id, out var producto))
            {
                return producto;
            }
            return null;
        }

        public void Agregar(Producto producto)
        {
            if (!_productos.ContainsKey(producto.Id))
            {
                _productos.Add(producto.Id, producto);
            }
        }

        public void Actualizar(Producto producto)
        {
            if (_productos.ContainsKey(producto.Id))
            {
                _productos[producto.Id] = producto;
            }
        }

        public void Eliminar(int id)
        {
            if (_productos.ContainsKey(id))
            {
                _productos.Remove(id);
            }
        }
    }
}
