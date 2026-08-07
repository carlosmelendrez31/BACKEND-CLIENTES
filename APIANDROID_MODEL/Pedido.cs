using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace APIANDROID_MODEL
{
    public class Pedido
    {
        public int id_pedido { get; set; }

        public int id_usuario { get; set; }

        public string correo { get; set; } = string.Empty;

        public string estado { get; set; } = string.Empty;

        public decimal total { get; set; }

        public DateTime fecha_pedido { get; set; }

        public DateTime? fecha_actualizacion { get; set; }

        public string productos_json { get; set; } = "[]";

        public List<DetallePedido> productos
        {
            get
            {
                try
                {
                    return JsonSerializer.Deserialize<List<DetallePedido>>(
                        productos_json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    ) ?? [];
                }
                catch
                {
                    return [];
                }
            }
        }
    }

   
}
