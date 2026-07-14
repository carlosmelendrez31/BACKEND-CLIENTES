using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_MODEL
{
    public class Producto
    {
        public int id_producto { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string? imagen_url { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }

    public class ResultadoOperacion
    {
        public int id_producto { get; set; }

        public string Mensaje { get; set; } = string.Empty;

        public bool Exito { get; set; }
    }
}

