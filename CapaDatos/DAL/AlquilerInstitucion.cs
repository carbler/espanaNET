using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DAL
{
    public class AlquilerInstitucion
    {

        public AlquilerInstitucion()
        {
            this.Equipos = new HashSet<Equipos>();
        }

        public int AlquilerInstitucionId { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }

        public string Salon { get; set; }
        public string  Descripcion { get; set; }
        public virtual Docente Docente { get; set; }
        public string Institucion { get; set; }

        public virtual ICollection<Equipos> Equipos { get; set; }
    }
}