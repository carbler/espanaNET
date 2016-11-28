using CapaLogica;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CapaPresentacion.Controllers
{
    [RoutePrefix("api/docentes")]
    public class DocenteController : BaseApiController
    {
        [Authorize]
        [Route("create")]
        public IHttpActionResult Create(DocenteDTO model)
        {

            ResponseDTO result = new DocenteBLL().Insertar(model);
            return Ok(result);

        }
        [Authorize]
        [Route("institucion/{username}")]
        public IHttpActionResult GetUserByName(string username)
        {
            var respuesta = new DocenteBLL().getDocentes(username);

            if (respuesta != null)
            {
                return Ok(respuesta);
            }
            return NotFound();

        }
    }
}