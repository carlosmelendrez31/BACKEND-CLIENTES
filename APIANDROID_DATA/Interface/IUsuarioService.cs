using APIANDROID_DTO.CLIENTESDTO;
using APIANDROID_DTO.Crearusuariodto;
using APIANDROID_DTO.LoginDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DATA.Interface
{
    // Interfaces/IUsuarioService.cs
    public interface IUsuarioService
    {
        Task<AuthResponseDto?> LoginAsync(Logindto dto);
        Task<bool> CrearUsuarioAsync(CrearUsuarioDto dto);
    }
}
