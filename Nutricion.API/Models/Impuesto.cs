using System.ComponentModel.DataAnnotations;

namespace Nutricion.API.Models
{
    public class Impuesto
    {
        public int Id { get; set; }
        private float Cantidad;
        private float Procentage;
        private float Total;
    }
}
