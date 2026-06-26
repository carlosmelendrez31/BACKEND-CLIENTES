using APIANDROID_DATA.Interface;
using APIANDROID_DTO.CLIENTESDTO;
using APIANDROID_DTO.Crearusuariodto;
using APIANDROID_DTO.LoginDTO;
using Microsoft.AspNetCore.Mvc;

namespace APIANDROID.Controllers
{
    // Controllers/UsuarioController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] CrearUsuarioDto dto)
        {
            var creado = await _service.CrearUsuarioAsync(dto);
            if (!creado)
                return Conflict(new { mensaje = "El correo ya está registrado." });

            return Ok(new { mensaje = "Usuario creado correctamente." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Logindto dto)
        {
            var resultado = await _service.LoginAsync(dto);
            if (resultado == null)
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });

            return Ok(resultado);
        }
    }
}
