using CapaDatos.Infraestructura;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DAL
{
    public class Docente
    {

        public Docente()
        {
            Alquileres = new List<AlquilerInstitucion>();

        }
        public int DocenteId { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public int HorasAsignadas { get; set; }
        public string Institucion { get; set; }

        public virtual ICollection<AlquilerInstitucion> Alquileres { get; set; }

    }
}
