using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DTO.Crearusuariodto
{
    public class ResultadoActualizarUsuario
    {
        public int id_usuario { get; set; }
        public string correo { get; set; } = string.Empty;
        public string rol { get; set; } = string.Empty;
        public string mensaje { get; set; } = string.Empty;
        public bool exito { get; set; }
    }

    // Result de eliminar — no necesita id/correo/rol de vuelta
    public class ResultadoEliminarUsuario
    {
        public string mensaje { get; set; } = string.Empty;
        public bool exito { get; set; }
    }

    // Fila cruda que regresa obtener_usuarios()
    public class UsuarioListadoRow
    {
        public int id_usuario { get; set; }
        public string correo { get; set; } = string.Empty;
        public string rol { get; set; } = string.Empty;
    }
}
