using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DTO.Crearusuariodto
{
  
        public class ActualizarUsuarioDto
        {
            public string Correo { get; set; } = string.Empty;

            // Opcional: si viene null o vacío, el password actual se conserva
            public string? Contrasena { get; set; }

            public string Rol { get; set; } = string.Empty;
        }
   
}
