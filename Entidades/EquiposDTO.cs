using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public  class EquiposDTO
    {
        public int EquiposId { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }   
        public string Estado { get; set; }
        public string Modelo { get; set; }
        public string Serial { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Descripcion { get; set; }

    }
}
