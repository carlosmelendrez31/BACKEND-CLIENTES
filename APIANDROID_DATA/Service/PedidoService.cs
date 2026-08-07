using System.Text.Json;
using APIANDROID_DATA.Interface;
using APIANDROID_DTO.Pedidos;
using APIANDROID_MODEL;
using Dapper;

namespace APIANDROID_DATA.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly DbConnection _db;

        public PedidoService(DbConnection db)
        {
            _db = db;
        }

        public async Task<ResultadoPedido> CrearAsync(
            CrearPedidoDto dto
        )
        {
            using var con = _db.CreateConnection();

            var productosJson = JsonSerializer.Serialize(
                dto.Productos.Select(p => new
                {
                    id_producto = p.IdProducto,
                    cantidad = p.Cantidad
                })
            );

            const string sql = """
                SELECT
                    id_pedido,
                    estado,
                    total,
                    mensaje,
                    exito
                FROM public.crear_pedido
                (
                    @p_id_usuario,
                    CAST(@p_productos AS JSONB)
                );
                """;

            var resultado =
                await con.QueryFirstOrDefaultAsync<ResultadoPedido>(
                    sql,
                    new
                    {
                        p_id_usuario = dto.IdUsuario,
                        p_productos = productosJson
                    }
                );

            return resultado ?? new ResultadoPedido
            {
                id_pedido = 0,
                estado = string.Empty,
                total = 0,
                mensaje = "No se obtuvo respuesta al crear el pedido.",
                exito = false
            };
        }

        public async Task<IEnumerable<Pedido>> ObtenerPorUsuarioAsync(
            int idUsuario
        )
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT
                    id_pedido,
                    id_usuario,
                    correo,
                    estado,
                    total,
                    fecha_pedido,
                    fecha_actualizacion,
                    productos::TEXT AS productos_json
                FROM public.obtener_pedidos_usuario(
                    @p_id_usuario
                );
                """;

            return await con.QueryAsync<Pedido>(
                sql,
                new
                {
                    p_id_usuario = idUsuario
                }
            );
        }

        public async Task<IEnumerable<Pedido>>
            ObtenerActualesPorUsuarioAsync(int idUsuario)
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT
                    id_pedido,
                    id_usuario,
                    correo,
                    estado,
                    total,
                    fecha_pedido,
                    fecha_actualizacion,
                    productos::TEXT AS productos_json
                FROM public.obtener_pedidos_actuales_usuario(
                    @p_id_usuario
                );
                """;

            return await con.QueryAsync<Pedido>(
                sql,
                new
                {
                    p_id_usuario = idUsuario
                }
            );
        }

        public async Task<IEnumerable<Pedido>>
            ObtenerAnterioresPorUsuarioAsync(int idUsuario)
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT
                    id_pedido,
                    id_usuario,
                    correo,
                    estado,
                    total,
                    fecha_pedido,
                    fecha_actualizacion,
                    productos::TEXT AS productos_json
                FROM public.obtener_pedidos_anteriores_usuario(
                    @p_id_usuario
                );
                """;

            return await con.QueryAsync<Pedido>(
                sql,
                new
                {
                    p_id_usuario = idUsuario
                }
            );
        }

        public async Task<IEnumerable<Pedido>>
            ObtenerParaAdminAsync()
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT
                    id_pedido,
                    id_usuario,
                    correo,
                    estado,
                    total,
                    fecha_pedido,
                    NULL::TIMESTAMP AS fecha_actualizacion,
                    productos::TEXT AS productos_json
                FROM public.obtener_pedidos_admin();
                """;

            return await con.QueryAsync<Pedido>(sql);
        }

        public async Task<ResultadoEstadoPedido> CambiarEstadoAsync(
            int idPedido,
            CambiarEstadoPedidoDto dto
        )
        {
            using var con = _db.CreateConnection();

            const string sql = """
                SELECT
                    id_pedido,
                    estado,
                    mensaje,
                    exito
                FROM public.cambiar_estado_pedido
                (
                    @p_id_admin,
                    @p_id_pedido,
                    @p_nuevo_estado
                );
                """;

            var resultado =
                await con.QueryFirstOrDefaultAsync<
                    ResultadoEstadoPedido
                >(
                    sql,
                    new
                    {
                        p_id_admin = dto.IdAdmin,
                        p_id_pedido = idPedido,
                        p_nuevo_estado = dto.NuevoEstado
                    }
                );

            return resultado ?? new ResultadoEstadoPedido
            {
                id_pedido = idPedido,
                estado = string.Empty,
                mensaje =
                    "No se obtuvo respuesta al cambiar el estado.",
                exito = false
            };
        }
    }
}