using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DAL
{
    public class Equipos
    {


        public Equipos()
        {
            this.Alquilers = new HashSet<Alquiler>();
        }

        public int EquiposId { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serial { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Descripcion { get; set; }
        public Boolean Estado { get; set; }

        public virtual ICollection<Alquiler> Alquilers { get; set; }
    }
}
