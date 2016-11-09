using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DAL
{
   public class Alquiler
    {
        public int AlquilerId { get; set; }

        public string nombreCliente { get; set; }

        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public string Servicios { get; set; }

    }
}
