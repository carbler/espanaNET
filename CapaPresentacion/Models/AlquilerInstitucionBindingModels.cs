using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapaPresentacion.Models
{
    public class AlquilerInstitucionBindingModels
    {
        public class CreateAlquilerInstitucionBindingModel
        {


            public string Salon { get; set; }
            public string Descripcion { get; set; }
            public int  Docente { get; set; }
            public DateTime fechaInicial { get; set; }
            public DateTime fechaFinal { get; set; }

            public String equipos;
        }
    }
}