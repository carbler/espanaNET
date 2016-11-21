using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using static CapaPresentacion.Models.EquiposBindingModels;
using Entidades;
using CapaLogica;


namespace CapaPresentacion.Controllers
{
   
        [RoutePrefix("api/equipos")]
        public class EquiposController : BaseApiController
        {
            [Route("create")]
            public IHttpActionResult Create(CreateEquiposBindingModels model)
            {


                ResponseDTO result = new EquiposBLL().Insertar(new EquiposDTO()
                {
                    Modelo = model.Modelo,
                    FechaCompra = model.FechaCompra,
                    Serial = model.Serial,
                    Descripcion = model.Descripcion,
                    Tipo = model.Tipo,
                    Marca = model.Marca,
                 
                });

                return Ok(result);

            }

        }
}