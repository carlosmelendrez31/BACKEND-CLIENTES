using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DTO.Productos
{
    public class CrearProductoDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        [Range(0, 999999.99, ErrorMessage = "El precio no puede ser negativo.")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Url(ErrorMessage = "La URL de la imagen no es válida.")]
        public string? ImagenUrl { get; set; }
    }
}
