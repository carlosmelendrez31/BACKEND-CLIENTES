using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DTO.LoginDTO
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
