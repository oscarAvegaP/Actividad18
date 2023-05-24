using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Coches
    {
            public int Id { get; set; }
            public string Modelo { get; set; }
            public string Marca { get; set; }
            public string año { get; set; }
            public int kilometraje { get; set; }
            public int estado { get; set; }

            public int ?AlquilerId { get; set; }
            public virtual Alquiler? Alquiler { get; set; }
    }
}
