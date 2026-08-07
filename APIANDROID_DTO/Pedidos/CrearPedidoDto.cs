using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DTO.Pedidos
{
    public class CrearPedidoDto
    {
        public int IdUsuario { get; set; }

        public List<ProductoPedidoDto> Productos { get; set; } = [];
    }
}
