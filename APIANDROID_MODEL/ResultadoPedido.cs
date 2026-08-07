using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_MODEL
{
    public class ResultadoPedido
    {
        public int id_pedido { get; set; }

        public string estado { get; set; } = string.Empty;

        public decimal total { get; set; }

        public string mensaje { get; set; } = string.Empty;

        public bool exito { get; set; }
    }
}
