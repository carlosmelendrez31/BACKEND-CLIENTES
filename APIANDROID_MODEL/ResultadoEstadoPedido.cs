using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_MODEL
{
    public class ResultadoEstadoPedido
    {
        public int id_pedido { get; set; }

        public string estado { get; set; } = string.Empty;

        public string mensaje { get; set; } = string.Empty;

        public bool exito { get; set; }
    }
}