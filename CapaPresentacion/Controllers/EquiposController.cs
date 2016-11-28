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
            [Authorize]
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

        

                [Authorize]
                [Route("editar")]
                public async Task<IHttpActionResult> Editar(CreateEquiposBindingModels model)
                {


                    ResponseDTO result = await new EquiposBLL().Editar(new EquiposDTO()
                    {
                        Modelo = model.Modelo,
                        FechaCompra = model.FechaCompra,
                        Serial = model.Serial,
                        Descripcion = model.Descripcion,
                        Tipo = model.Tipo,
                        Marca = model.Marca,
                        Estado = model.Estado,
                        EquiposId = model.EquiposId

                    });

                    return Ok(result);
                }

                    [Authorize]
                    [Route("listado", Name = "GetEquipos")]
                    public async Task<IHttpActionResult> GetUser()
                    {
                         var equipos = new EquiposBLL().getEquipos(); 

                        if (equipos != null)
                        {
                            return Ok(equipos);
                        }
                        return NotFound();
                    }

    }
}

