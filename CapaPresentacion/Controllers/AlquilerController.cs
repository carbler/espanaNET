using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using static CapaPresentacion.Models.AlquilerBindingModels;
using Entidades;
using CapaLogica;

namespace CapaPresentacion.Controllers
{

    
    [RoutePrefix("api/alquiler")]
    public class AlquilerController : BaseApiController
    {
        [Route("create")]
        public IHttpActionResult Create(CreateAlquilerBindingModel model)
        {


            ResponseDTO result = new AlquilerBLL().Insertar(new AlquilerDTO()
            {
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                nombreCliente = model.nombreCliente,
                fechaFinal = model.fechaFinal,
                fechaInicial = model.fechaInicial,
                equipos = model.equipos
               });

            return Ok(result);

        }

        [Route("AlquileresFecha")]
        public IHttpActionResult AlquileresFecha(CreateAlquilerBindingModel model)
        {
           
            
            var respuesta = new AlquilerBLL().getAlquileresPorFecha(model.fechaInicial, model.fechaFinal);
         
            return Ok(respuesta);

        }

        [Authorize]
        [Route("Hoy")]
        public  IHttpActionResult GetAlquileresHoy()
        {
            DateTime ini = DateTime.Now;
          

            DateTime Inicial =  new DateTime(ini.Year, ini.Month, ini.Day, 0, 0, 0);
            DateTime Final = new DateTime(ini.Year, ini.Month, ini.Day, 23, 59, 0);




            var respuesta =  new AlquilerBLL().getAlquileresPorFecha(Inicial,Final);

            return Ok(respuesta);

        }
    }
}