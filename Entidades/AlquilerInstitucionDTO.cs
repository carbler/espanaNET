using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class AlquilerInstitucionDTO
    {
        public int AlquilerInstitucionId { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }

        public string Salon { get; set; }
        public string Descripcion { get; set; }
        public int  Docente { get; set; }
        public string Institucion { get; set; }

        public string equipos { get; set; }

        public String NombreDocente { get; set; }

    }
}
