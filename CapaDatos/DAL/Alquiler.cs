using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DAL
{
   public class Alquiler
    {

        public Alquiler()
        {
            this.Equipos = new HashSet<Equipos>();
        }
        public int AlquilerId { get; set; }
        public string nombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
     

        public virtual ICollection<Equipos> Equipos { get; set; }
    }
}