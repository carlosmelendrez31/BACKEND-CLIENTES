// Controllers/ClienteController.cs
using APIANDROID_DATA.Services;
using APIANDROID_DTO.CLIENTESDTO;
using APIANDROID_DTO.LoginDTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] CrearClienteDto dto)
    {
        var creado = await _service.CrearClienteAsync(dto);

        if (!creado)
            return Conflict(new { mensaje = "El correo ya está registrado." });

        return Ok(new { mensaje = "Cliente creado correctamente." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Logindto dto)
    {
        var resultado = await _service.LoginAsync(dto);

        if (resultado == null)
            return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });

        return Ok(resultado);
    }
    [HttpGet("clientes")]
    public async Task<IActionResult> ObtenerClientes()
    {
        var clientes = await _service.ObtenerClientesAsync();
        return Ok(clientes);
    }

}