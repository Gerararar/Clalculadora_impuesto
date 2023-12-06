using System.ComponentModel.DataAnnotations;

namespace Nutricion.Models
{
    public class Impuesto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La {0} es requerida")]
        public float Cantidad;
        public float Procentage;
        public float Total;
        public DateTime Fecha { get; set; }
    }
}
