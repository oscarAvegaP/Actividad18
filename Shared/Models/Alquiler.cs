using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Alquiler
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public double precio { get; set; }
        public int IdCoche { get; set; }
        public int? ClientesId { get; set; } 
        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<Coches>? Coches { get; set; }
    }
}
