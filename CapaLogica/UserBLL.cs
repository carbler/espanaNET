
using CapaDatos.Infraestructura;
using Entidades;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace CapaLogica
{
    public class UserBLL : BaseContextBBL
    {
        public UserBLL(HttpRequestMessage request) 
            : base(request)
        {
         
        }

        public async Task<UserDTO> getUser(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return setUserDTO(user);
            }
            return null;
        }

        public RespuestaDTO<List<UserDTO>> GetUsers()
        {
            List<UserDTO> usersDTO = new List<UserDTO>();
            List<ApplicationUser> users = UserManager.Users.ToList();
            foreach (ApplicationUser user in users)
            {
                usersDTO.Add(setUserDTO(user));
            }
            return new RespuestaDTO<List<UserDTO>>(){
                Data = usersDTO,
                Mensaje = "Listado de Usuarios"
            };
        }

        public async Task<RespuestaDTO<UserDTO>> createUser(CreateUserDTO createUserModel)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = createUserModel.UserName,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
            };
            RespuestaDTO<UserDTO> response = new RespuestaDTO<UserDTO>();
            response.Error = ApplicationUserManager.Result(await UserManager.CreateAsync(user, createUserModel.Password));

            if (response.Error.Count > 0)
            {
                response.Mensaje = "El Usuario No Ha Sido Creado";

            }
            else
            {
                response.Mensaje = "Se Ha Creado Un Nuevo Usuario";
                response.Data = setUserDTO(user);
            }
            return response;
        }

       
        public async Task<RespuestaDTO<string[]>> AssignRolesToUser(string id, string[] rolesToAssign)
        {
            RespuestaDTO<string[]> response = new RespuestaDTO<string[]>();

            var appUser = await this.UserManager.FindByIdAsync(id);
            response.Data = rolesToAssign;
            if (appUser == null)
            {
                response.Mensaje = "El usuario no existe";
                response.Error.Add(new ErrorDTO()
                {
                   Code = "404",
                   Menssage = " Not Found"
                });
            }

            var currentRoles = await this.UserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.RoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {
                response.Mensaje = "Roles no existe";
                response.Error.Add(new ErrorDTO()
                {
                    Code = "504",
                    Menssage = "Roles inexistentes"
                });

                response.Data = rolesNotExists;

                return response;
            }

            response.Error = ApplicationUserManager.Result( await this.UserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray()));

            if (response.Error.Count > 0)
            {
                response.Mensaje = "No se puedieron remover los roles de usuario";
                response.Error.Add(new ErrorDTO()
                {
                    Code = "504",
                    Menssage = "No se puedieron remover los roles de usuario"
                });

                return response;
            }

            response.Error = ApplicationUserManager.Result( await this.UserManager.AddToRolesAsync(appUser.Id, rolesToAssign));

            if (response.Error.Count > 0)
            {
                response.Mensaje = "No se pudo agregar el rol al usuario";
                response.Error.Add(new ErrorDTO()
                {
                    Code = "504",
                    Menssage = "No se pudo agregar el rol al usuario"
                });

                return response;
            }

            return response;
        }



        private UserDTO setUserDTO(ApplicationUser user)
        {
            var roles = UserManager.GetRolesAsync(user.Id).Result;
            var claims = UserManager.GetClaimsAsync(user.Id).Result;
            var url = new UrlHelper(request).Link("GetUserById", new { id = user.Id });
            return new UserDTO
            {
                Url = url,
                Id = user.Id,
                UserName = user.UserName,
                FullName = string.Format("{0} {1}", user.FirstName, user.LastName),
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Level = user.Level,
                JoinDate = user.JoinDate,
                Roles = roles,
                Claims = claims
            };
        }
    }
}

