using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using static CapaPresentacion.Models.AlquilerInstitucionBindingModels;
using Entidades;
using CapaLogica;

namespace CapaPresentacion.Controllers
{


    [RoutePrefix("api/alquilerInstitucion")]
    public class AlquilerInstitucionController : BaseApiController
    {
        [Route("create")]
        public IHttpActionResult Create(CreateAlquilerInstitucionBindingModel model)
        {


            ResponseDTO result = new AlquilerInstitucionBLL().Insertar(new AlquilerInstitucionDTO()
            {
                Salon = model.Salon,
                Descripcion = model.Descripcion,
                Docente = model.Docente,
                fechaFinal = model.fechaFinal,
                fechaInicial = model.fechaInicial,
                equipos = model.equipos
            });

            return Ok(result);

        }

        [Route("AlquileresFecha")]
        public IHttpActionResult AlquileresFecha(CreateAlquilerInstitucionBindingModel model)
        {


            var respuesta = new AlquilerInstitucionBLL().getAlquileresInstitucionPorFecha(model.fechaInicial, model.fechaFinal);

            return Ok(respuesta);

        }


        [Route("AlquileresDocente")]
        public IHttpActionResult AlquileresDocente(CreateAlquilerInstitucionBindingModel model)
        {

            var respuesta = new AlquilerInstitucionBLL().getAlquileresDocente(model.Docente);

            return Ok(respuesta);

        }

    }

 
}