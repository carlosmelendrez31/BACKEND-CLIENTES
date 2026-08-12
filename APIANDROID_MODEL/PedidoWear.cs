using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_MODEL
{
    using System.Text.Json;

    namespace APIANDROID_MODEL
    {
        public class PedidoWear
        {
            public int id_pedido { get; set; }

            public string estado { get; set; } = string.Empty;

            public decimal total { get; set; }

            public DateTime fecha_pedido { get; set; }

            public string productos_json { get; set; } = "[]";

            public List<ProductoPedidoWear> productos
            {
                get
                {
                    try
                    {
                        return JsonSerializer.Deserialize<
                            List<ProductoPedidoWear>
                        >(
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

        public class ProductoPedidoWear
        {
            public int id_producto { get; set; }

            public string nombre { get; set; } =
                string.Empty;

            public int cantidad { get; set; }

            public decimal subtotal { get; set; }
        }
    }
}
