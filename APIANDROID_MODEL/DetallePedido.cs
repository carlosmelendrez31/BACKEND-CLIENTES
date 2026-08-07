using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_MODEL
{
    public class DetallePedido
    {
        public int id_detalle { get; set; }

        public int id_producto { get; set; }

        public string nombre { get; set; } = string.Empty;

        public string? descripcion { get; set; }

        public string? categoria { get; set; }

        public string? imagen_url { get; set; }

        public int cantidad { get; set; }

        public decimal precio_unitario { get; set; }

        public decimal subtotal { get; set; }
    }
}
