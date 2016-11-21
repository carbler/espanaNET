using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapaPresentacion.Models
{
    public class EquiposBindingModels
    {
        public class CreateEquiposBindingModels
        {
            
          
            [Required]
            [Display(Name = "Tipo")]
            public string Tipo { get; set; }

            [Required]
            [Display(Name = "Modelo")]
            public string Modelo { get; set; }

            [Required]
            [Display(Name = "Serial")]
            public string Serial { get; set; }

            [Required]
            [Display(Name = "FechaCompra")]
            public DateTime FechaCompra { get; set; }

            [Required]
            [Display(Name = "Descripcion")]
            public string Descripcion { get; set; }

            [Required]
            [Display(Name = "Marca")]
            public string Marca { get; set; }


        }

    }
}