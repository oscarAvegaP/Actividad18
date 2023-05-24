using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? nombre { get; set; }
        public string? direccion { get; set; }
        public string? email { get; set; }
        public string? conducir { get; set; }

        public virtual ICollection<Alquiler>? Alquilers { get; set; }
    }
}
