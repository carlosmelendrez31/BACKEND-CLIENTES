using APIANDROID_DTO.Productos;
using APIANDROID_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DATA.Interface
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();

        Task<Producto?> ObtenerPorIdAsync(int idProducto);

        Task<ResultadoOperacion> CrearAsync(CrearProductoDto dto);

        Task<ResultadoOperacion> ActualizarAsync(
            int idProducto,
            ActualizarProductoDto dto
        );

        Task<ResultadoOperacion> EliminarAsync(int idProducto);
    }
}
