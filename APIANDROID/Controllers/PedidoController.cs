using APIANDROID_DATA.Interface;
using APIANDROID_DTO.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace APIANDROID.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(
            IPedidoService pedidoService
        )
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
            [FromBody] CrearPedidoDto dto
        )
        {
            if (dto.IdUsuario <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "El ID del usuario no es válido."
                });
            }

            if (
                dto.Productos == null ||
                dto.Productos.Count == 0
            )
            {
                return BadRequest(new
                {
                    mensaje =
                        "Debes agregar al menos un producto."
                });
            }

            if (dto.Productos.Any(p =>
                    p.IdProducto <= 0 ||
                    p.Cantidad <= 0))
            {
                return BadRequest(new
                {
                    mensaje =
                        "Los productos y cantidades no son válidos."
                });
            }

            try
            {
                var resultado =
                    await _pedidoService.CrearAsync(dto);

                if (!resultado.exito)
                {
                    return BadRequest(resultado);
                }

                return StatusCode(
                    StatusCodes.Status201Created,
                    resultado
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Ocurrió un error al crear el pedido.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("usuario/{idUsuario:int}")]
        public async Task<IActionResult> ObtenerPorUsuario(
            int idUsuario
        )
        {
            if (idUsuario <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "ID de usuario no válido."
                });
            }

            try
            {
                var pedidos =
                    await _pedidoService
                        .ObtenerPorUsuarioAsync(idUsuario);

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Error al obtener los pedidos.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("usuario/{idUsuario:int}/actuales")]
        public async Task<IActionResult> ObtenerActuales(
            int idUsuario
        )
        {
            if (idUsuario <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "ID de usuario no válido."
                });
            }

            try
            {
                var pedidos =
                    await _pedidoService
                        .ObtenerActualesPorUsuarioAsync(
                            idUsuario
                        );

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Error al obtener los pedidos actuales.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("usuario/{idUsuario:int}/anteriores")]
        public async Task<IActionResult> ObtenerAnteriores(
            int idUsuario
        )
        {
            if (idUsuario <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "ID de usuario no válido."
                });
            }

            try
            {
                var pedidos =
                    await _pedidoService
                        .ObtenerAnterioresPorUsuarioAsync(
                            idUsuario
                        );

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Error al obtener pedidos anteriores.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("admin")]
        public async Task<IActionResult> ObtenerParaAdmin()
        {
            try
            {
                var pedidos =
                    await _pedidoService.ObtenerParaAdminAsync();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Error al obtener los pedidos del administrador.",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{idPedido:int}/estado")]
        public async Task<IActionResult> CambiarEstado(
            int idPedido,
            [FromBody] CambiarEstadoPedidoDto dto
        )
        {
            if (idPedido <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "ID de pedido no válido."
                });
            }

            if (dto.IdAdmin <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "ID de administrador no válido."
                });
            }

            var estadosPermitidos = new[]
            {
                "Aceptado",
                "En proceso",
                "Finalizado"
            };

            var estadoValido =
                estadosPermitidos.Any(e =>
                    e.Equals(
                        dto.NuevoEstado?.Trim(),
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (!estadoValido)
            {
                return BadRequest(new
                {
                    mensaje =
                        "El estado debe ser Aceptado, En proceso o Finalizado."
                });
            }

            try
            {
                var resultado =
                    await _pedidoService.CambiarEstadoAsync(
                        idPedido,
                        dto
                    );

                if (!resultado.exito)
                {
                    return BadRequest(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Error al cambiar el estado del pedido.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("wear/{idUsuario:int}")]
        public async Task<IActionResult> ObtenerPedidoWear(
    int idUsuario
)
        {
            if (idUsuario <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "ID de usuario no válido."
                });
            }

            try
            {
                var pedido =
                    await _pedidoService
                        .ObtenerPedidoWearAsync(idUsuario);

                if (pedido == null)
                {
                    return NotFound(new
                    {
                        mensaje =
                            "El usuario no tiene pedidos activos."
                    });
                }

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje =
                        "Error al consultar el pedido para Wear OS.",
                    error = ex.Message
                });
            }
        }
    }
}
