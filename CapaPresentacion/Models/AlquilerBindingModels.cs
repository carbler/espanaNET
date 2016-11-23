using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapaPresentacion.Models
{
    public class AlquilerBindingModels
    {
        public class CreateAlquilerBindingModel
        {

            [Required]
            [Display(Name = "nombreCliente")]
            public string nombreCliente { get; set; }


            [Required]
            [Display(Name = "Telefono")]
            public string Telefono { get; set; }


            [Required]
            [Display(Name = "Direccion")]
            public string Direccion { get; set; }

            [Required]
            [Display(Name = "fechaInicial")]
            public DateTime fechaInicial { get; set; }

            [Required]
            [Display(Name = "fechaFinal")]
            public DateTime fechaFinal { get; set; }

            public String equipos;
        }
    }
}