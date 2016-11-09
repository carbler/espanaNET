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
                    nombreEquipo = model.nombreEquipo,
                    Tipo = model.nombreEquipo,
                    Marca = model.Marca,
                    Estado = model.Estado
                });

                return Ok(result);

            }

        }
}