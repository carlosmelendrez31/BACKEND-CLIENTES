using APIANDROID_DATA.Interface;
using APIANDROID_DTO.Crearusuariodto;
using APIANDROID_DTO.LoginDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIANDROID.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(
            IUsuarioService usuarioService
        )
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registro/cliente")]
        public async Task<IActionResult> RegistrarCliente(
            [FromBody] CrearUsuarioDto dto
        )
        {
            if (string.IsNullOrWhiteSpace(dto.Correo))
            {
                return BadRequest(new
                {
                    mensaje = "El correo es obligatorio."
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                return BadRequest(new
                {
                    mensaje = "La contraseña es obligatoria."
                });
            }

            try
            {
                var resultado =
                    await _usuarioService.CrearClienteAsync(dto);

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
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al registrar el cliente.",
                        error = ex.Message
                    }
                );
            }
        }

        [HttpPost("registro/admin")]
        public async Task<IActionResult> RegistrarAdmin(
            [FromBody] CrearUsuarioDto dto
        )
        {
            if (string.IsNullOrWhiteSpace(dto.Correo))
            {
                return BadRequest(new
                {
                    mensaje = "El correo es obligatorio."
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                return BadRequest(new
                {
                    mensaje = "La contraseña es obligatoria."
                });
            }

            try
            {
                var resultado =
                    await _usuarioService.CrearAdminAsync(dto);

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
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al registrar el administrador.",
                        error = ex.Message
                    }
                );
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] Logindto dto
        )
        {
            try
            {
                var resultado =
                    await _usuarioService.LoginAsync(dto);

                if (resultado == null)
                {
                    return Unauthorized(new
                    {
                        mensaje =
                            "Correo o contraseña incorrectos."
                    });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al iniciar sesión.",
                        error = ex.Message
                    }
                );
            }
        }
        // GET api/Usuario
        [HttpGet]
       
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }

        // PUT api/Usuario/5
        [HttpPut("{id}")]
       
        public async Task<IActionResult> ActualizarUsuario(
            int id,
            [FromBody] ActualizarUsuarioDto dto
        )
        {
            var resultado = await _usuarioService.ActualizarUsuarioAsync(id, dto);

            if (!resultado.exito)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // DELETE api/Usuario/5
        [HttpDelete("{id}")]
      
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var resultado = await _usuarioService.EliminarUsuarioAsync(id);

            if (!resultado.exito)
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}