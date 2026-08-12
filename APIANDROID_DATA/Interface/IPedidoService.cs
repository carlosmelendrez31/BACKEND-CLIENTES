using APIANDROID_DTO.Pedidos;
using APIANDROID_MODEL;
using APIANDROID_MODEL.APIANDROID_MODEL;

namespace APIANDROID_DATA.Interface
{
    public interface IPedidoService
    {
        Task<ResultadoPedido> CrearAsync(
            CrearPedidoDto dto
        );

        Task<IEnumerable<Pedido>> ObtenerPorUsuarioAsync(
            int idUsuario
        );

        Task<IEnumerable<Pedido>> ObtenerActualesPorUsuarioAsync(
            int idUsuario
        );

        Task<IEnumerable<Pedido>> ObtenerAnterioresPorUsuarioAsync(
            int idUsuario
        );

        Task<IEnumerable<Pedido>> ObtenerParaAdminAsync();

        Task<ResultadoEstadoPedido> CambiarEstadoAsync(
            int idPedido,
            CambiarEstadoPedidoDto dto
        );

        Task<PedidoWear?> ObtenerPedidoWearAsync(
    int idUsuario
);
    }
}
