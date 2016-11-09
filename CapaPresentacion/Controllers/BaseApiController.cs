using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CapaPresentacion.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController() { }

        protected IHttpActionResult GetErrorResult(ResponseDTO result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            else
            {
                if (result.Error != null)
                {
                    foreach (ErrorDTO error in result.Error)
                    {
                        if (error.Code == "500")
                        {
                            return InternalServerError();
                        }
                        ModelState.AddModelError(error.Code, error.Menssage);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }
        }
    }
}