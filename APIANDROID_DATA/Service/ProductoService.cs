using APIANDROID_DATA.Interface;
using APIANDROID_DTO.Productos;
using APIANDROID_MODEL;
using Dapper;

namespace APIANDROID_DATA.Service
{
    public class ProductoService : IProductoService
    {
        private readonly DbConnection _db;

        public ProductoService(DbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT *
                FROM public.obtener_productos();
                """;

            var productos = await con.QueryAsync<Producto>(sql);

            return productos;
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id_producto)
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT *
                FROM public.obtener_producto_por_id(@p_id_producto);
                """;

            var producto = await con.QueryFirstOrDefaultAsync<Producto>(
                sql,
                new
                {
                    p_id_producto = id_producto
                }
            );

            return producto;
        }

        public async Task<ResultadoOperacion> CrearAsync(
            CrearProductoDto dto
        )
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT *
                FROM public.crear_producto
                (
                    @p_nombre,
                    @p_descripcion,
                    @p_categoria,
                    @p_precio,
                    @p_stock,
                    @p_imagen_url
                );
                """;

            var resultado =
                await con.QueryFirstOrDefaultAsync<ResultadoOperacion>(
                    sql,
                    new
                    {
                        p_nombre = dto.Nombre,
                        p_descripcion = dto.Descripcion,
                        p_categoria = dto.Categoria,
                        p_precio = dto.Precio,
                        p_stock = dto.Stock,
                        p_imagen_url = dto.ImagenUrl
                    }
                );

            return resultado ?? new ResultadoOperacion
            {
                id_producto = 0,
                Mensaje = "No se obtuvo respuesta de la base de datos.",
                Exito = false
            };
        }

        public async Task<ResultadoOperacion> ActualizarAsync(
            int id_producto,
            ActualizarProductoDto dto
        )
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT *
                FROM public.actualizar_producto
                (
                    @p_id_producto,
                    @p_nombre,
                    @p_descripcion,
                    @p_categoria,
                    @p_precio,
                    @p_stock,
                    @p_imagen_url,
                    @p_activo
                );
                """;

            var resultado =
                await con.QueryFirstOrDefaultAsync<ResultadoOperacion>(
                    sql,
                    new
                    {
                        p_id_producto = id_producto,
                        p_nombre = dto.Nombre,
                        p_descripcion = dto.Descripcion,
                        p_categoria = dto.Categoria,
                        p_precio = dto.Precio,
                        p_stock = dto.Stock,
                        p_imagen_url = dto.ImagenUrl,
                        p_activo = dto.Activo
                    }
                );

            return resultado ?? new ResultadoOperacion
            {
                id_producto = id_producto,
                Mensaje = "No se obtuvo respuesta de la base de datos.",
                Exito = false
            };
        }

        public async Task<ResultadoOperacion> EliminarAsync(int id_producto)
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT *
                FROM public.eliminar_producto(@p_id_producto);
                """;

            var resultado =
                await con.QueryFirstOrDefaultAsync<ResultadoOperacion>(
                    sql,
                    new
                    {
                        p_id_producto = id_producto
                    }
                );

            return resultado ?? new ResultadoOperacion
            {
                id_producto = id_producto,
                Mensaje = "No se obtuvo respuesta de la base de datos.",
                Exito = false
            };
        }
    }
}