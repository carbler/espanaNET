using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class AlquilerDTO
    {
        public int AlquilerId { get; set; }

        public string nombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public string equipos { get; set; }
       }
}
