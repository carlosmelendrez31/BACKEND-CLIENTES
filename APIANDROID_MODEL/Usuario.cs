using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_MODEL
{
    // Models/Usuario.cs
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Rol { get; set; } = "";
    }
}

