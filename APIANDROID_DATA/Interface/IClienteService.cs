using APIANDROID_DTO.CLIENTESDTO;
using APIANDROID_DTO.LoginDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DATA.Services
{
    public interface IClienteService
    {
        Task<AuthResponseDto?> LoginAsync(Logindto dto);
        Task<bool> CrearClienteAsync(CrearClienteDto dto);
        Task<IEnumerable<ClienteDto>> ObtenerClientesAsync();
    }
}
