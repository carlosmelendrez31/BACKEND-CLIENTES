using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DTO.CLIENTESDTO
{
    public class Actualizardto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public DateOnly FechaNacimiento { get; set; }
    }
}
