using CapaLogica;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static CapaPresentacion.Models.RoleBindingModels;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {

        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string Id)
        {

           RespuestaDTO<RoleDTO> response = await new RoleBLL(Request).GetRole(Id);
           
            if (response.Error.Count > 0)
            {
                return GetErrorResult(response);
            }
            return Ok(response);
        }

        [Route("", Name = "GetAllRoles")]
        public  IHttpActionResult GetAllRoles()
        {
            RespuestaDTO<List<RoleDTO>> response =  new RoleBLL(Request).GetAllRoles();
            return Ok(response);
        }

        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {

            RespuestaDTO<RoleDTO> result = await new RoleBLL(Request).Create(new RoleDTO()
            {
                Name = model.Name
            });

            if (result.Error.Count > 0)
            {
                return GetErrorResult(result);
            }

            Uri locationHeader = new Uri(Url.Link("GetRoleById", new { id = result.Data.Id }));

            return Created(locationHeader, result);

        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string Id)
        {

            RespuestaDTO<RoleDTO> response = await new RoleBLL(Request).DeleteRole(Id);

            if (response.Error.Count > 0)
            {
                return GetErrorResult(response);
            }
            return Ok(response);
        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModelDTO model)
        {
            RespuestaDTO<UsersInRoleModelDTO> response = await new RoleBLL(Request).ManageUsersInRole(model);
            if (response.Error.Count > 0)
            {
                GetErrorResult(response);
            }
            return Ok(response);
        }
    }
}