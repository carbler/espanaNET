using CapaLogica;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static CapaPresentacion.Models.AccountBindingModels;

namespace CapaPresentacion.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(new UserBLL(Request).GetUsers());
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await new UserBLL(Request).getUser(Id);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RespuestaDTO<UserDTO> user = await new UserBLL(Request).createUser(new CreateUserDTO
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
                Password = createUserModel.Password
            });


            if (user.Error.Count > 0)
            {
                return GetErrorResult(user);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Data.Id }));

            return Created(locationHeader, user);
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            RespuestaDTO<string[]> response = await new UserBLL(Request).AssignRolesToUser(id, rolesToAssign);
          
            if (response.Error.Count > 0)
            {
                return GetErrorResult(response);
            }
            
            return Ok(response);
        }
    }


}