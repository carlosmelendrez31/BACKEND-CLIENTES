using APIANDROID_DATA.Interface;
using APIANDROID_DTO.Productos;
using Microsoft.AspNetCore.Mvc;

namespace APIANDROID.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productosService;

        public ProductoController(IProductoService productosService)
        {
            _productosService = productosService;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var productos =
                    await _productosService.ObtenerTodosAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al obtener los productos.",
                    error = ex.Message
                });
            }
        }

        // GET: api/Producto/1
        [HttpGet("{idProducto:int}")]
        public async Task<IActionResult> ObtenerPorId(int idProducto)
        {
            if (idProducto <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "El ID debe ser mayor que cero."
                });
            }

            try
            {
                var producto =
                    await _productosService.ObtenerPorIdAsync(idProducto);

                if (producto is null)
                {
                    return NotFound(new
                    {
                        mensaje = "El producto no existe."
                    });
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al obtener el producto.",
                    error = ex.Message
                });
            }
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> Crear(
            [FromBody] CrearProductoDto dto
        )
        {
            try
            {
                var resultado =
                    await _productosService.CrearAsync(dto);

                if (!resultado.Exito)
                {
                    return BadRequest(resultado);
                }

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new
                    {
                        idProducto = resultado.id_producto
                    },
                    resultado
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al crear el producto.",
                    error = ex.Message
                });
            }
        }

        // PUT: api/Producto/1
        [HttpPut("{idProducto:int}")]
        public async Task<IActionResult> Actualizar(
            int idProducto,
            [FromBody] ActualizarProductoDto dto
        )
        {
            if (idProducto <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "El ID debe ser mayor que cero."
                });
            }

            try
            {
                var resultado =
                    await _productosService.ActualizarAsync(
                        idProducto,
                        dto
                    );

                if (!resultado.Exito)
                {
                    return NotFound(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al actualizar el producto.",
                    error = ex.Message
                });
            }
        }

        // DELETE: api/Producto/1
        [HttpDelete("{idProducto:int}")]
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            if (idProducto <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "El ID debe ser mayor que cero."
                });
            }

            try
            {
                var resultado =
                    await _productosService.EliminarAsync(idProducto);

                if (!resultado.Exito)
                {
                    return NotFound(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al eliminar el producto.",
                    error = ex.Message
                });
            }
        }
    }
}