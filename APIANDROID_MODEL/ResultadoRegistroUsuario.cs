using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   namespace APIANDROID_MODEL
    {
        public class ResultadoRegistroUsuario
        {
            public int id_usuario { get; set; }

            public string correo { get; set; } = string.Empty;

            public string rol { get; set; } = string.Empty;

            public string mensaje { get; set; } = string.Empty;

            public bool exito { get; set; }
        }
    }

