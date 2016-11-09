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
            [Display(Name = "nombreEquipo")]
            public string nombreEquipo { get; set; }


            [Required]
            [Display(Name = "Marca")]
            public string Marca { get; set; }


            [Required]
            [Display(Name = "Estado")]
            public string Estado { get; set; }


        }

    }
}