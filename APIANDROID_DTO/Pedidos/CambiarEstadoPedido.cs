using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace APIANDROID_DTO.Pedidos
    {
        public class CambiarEstadoPedidoDto
        {
            public int IdAdmin { get; set; }

            public string NuevoEstado { get; set; } = string.Empty;
        }
    }

