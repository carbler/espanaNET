using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class DocenteDTO
    {
        public int DocenteId { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public int HorasAsignadas { get; set; }

        public string Institucion { get; set; }
    }
}
