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
    [HttpGet("clientes")]
    public async Task<IActionResult> ObtenerClientes()
    {
        var clientes = await _service.ObtenerClientesAsync();
        return Ok(clientes);
    }

    [HttpGet("clientes/{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var cliente = await _service.ObtenerClientePorIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] CrearClienteDto dto)
    {
        var creado = await _service.CrearClienteAsync(dto);
        if (!creado) return Conflict(new { mensaje = "El correo ya está registrado." });
        return Ok(new { mensaje = "Cliente creado correctamente." });
    }

    [HttpPut("clientes/{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Actualizardto dto)
    {
        var actualizado = await _service.ActualizarClienteAsync(id, dto);
        if (!actualizado) return NotFound();
        return Ok(new { mensaje = "Cliente actualizado." });
    }

    [HttpDelete("clientes/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _service.EliminarClienteAsync(id);
        if (!eliminado) return NotFound();
        return Ok(new { mensaje = "Cliente eliminado." });
    }

}