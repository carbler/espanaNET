using CapaDatos.Infraestructura;
using Entidades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace CapaLogica
{
    public class RoleBLL : BaseContextBBL
    {
        public RoleBLL(HttpRequestMessage request) : base(request)
        {
        }

        public async Task<RespuestaDTO<RoleDTO>> GetRole(string Id)
        {
            var role = await this.RoleManager.FindByIdAsync(Id);
            var response = new RespuestaDTO<RoleDTO>();
            response.Data = setRoleDTO(role);
            if (role != null)
            {
                response.Mensaje = "Role Encontrado";
                return response;
            }
            response.Mensaje = "Role No Existe";
            response.Error.Add(new ErrorDTO()
            {
                Code = "404",
                Menssage = "Not Found"
            });
            
            return response;
        }

        public RespuestaDTO<List<RoleDTO>> GetAllRoles()
        {
            var roles = RoleManager.Roles.ToList<IdentityRole>();
            var RoleDTO = new List<RoleDTO>();
            foreach (IdentityRole role in roles)
            {
                RoleDTO.Add( setRoleDTO(role));
            }
            return new RespuestaDTO<List<RoleDTO>>()
            {
                Data = RoleDTO,
                Mensaje = "Listado de Usuarios"
            };
        }

        public async Task<RespuestaDTO<RoleDTO>> Create(RoleDTO model)
        {
      
            var role = new IdentityRole { Name = model.Name };
            RespuestaDTO<RoleDTO> response = new RespuestaDTO<RoleDTO>();
            response.Error = ApplicationUserManager.Result( await this.RoleManager.CreateAsync(role));
            response.Data = setRoleDTO(role);
            if (response.Error.Count > 0)
            {
                response.Mensaje = "El Role No Ha Sido Creado";
            }
            else {
                response.Mensaje = "Role Creado Con Exito";
            }
            return response;
        }


        public async Task<RespuestaDTO<RoleDTO>> DeleteRole(string Id)
        {

            var role = await this.RoleManager.FindByIdAsync(Id);
            RespuestaDTO<RoleDTO> response = new RespuestaDTO<RoleDTO>();
            if (role != null)
            {
               response.Error = ApplicationUserManager.Result(await this.RoleManager.DeleteAsync(role));
               response.Mensaje = "Rol Eliminado Correctamente";
                response.Data = setRoleDTO(role);
                return response;
            }
            response.Mensaje = "El rol no ha sido eliminado";
            response.Error.Add(new ErrorDTO() {
                Code = "404",
                Menssage = "Role No Encontrado"
            });
            return response;

        }

        public async Task<RespuestaDTO<UsersInRoleModelDTO>> ManageUsersInRole(UsersInRoleModelDTO model)
        {
            var role = await this.RoleManager.FindByIdAsync(model.Id);
            RespuestaDTO<UsersInRoleModelDTO> response = new RespuestaDTO<UsersInRoleModelDTO>();
            response.Data = model;
            
            if (role == null)
            {
                response.Mensaje = "El Role Con Id" + model.Id + " No Existe ";
                response.Error.Add(new ErrorDTO() {
                    Code = "404",
                    Menssage = "Role no existe"
                });
                return response;
            }

            foreach (string user in model.EnrolledUsers)
            {
                var appUser = await this.UserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    response.Error.Add(new ErrorDTO()
                    {
                        Code = "404",
                        Menssage = "El Usuario" + user +" No Ha Sido Encontrado"
                    });
                    continue;
                }

                if (!this.UserManager.IsInRole(user, role.Name))
                {
                    response.Error = ApplicationUserManager.Result(await this.UserManager.AddToRoleAsync(user, role.Name));

                    if (response.Error.Count > 0)
                    {
                        response.Mensaje = "El Role No Ha Sido Agredo Al Usuario";
                    }
                    else
                    {
                        response.Mensaje = "El Role Ha Sido Agredo Al Usuario";
                    }
                }
            }

            foreach (string user in model.RemovedUsers)
            {
                var appUser = await this.UserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    response.Error.Add(new ErrorDTO()
                    {
                        Code = "404",
                        Menssage = "El Usuario No Ha Sido Encontrado"
                    });
                    continue;
                }

                response.Error = ApplicationUserManager.Result( await this.UserManager.RemoveFromRoleAsync(user, role.Name));

                if (response.Error.Count > 0)
                {
                    response.Mensaje += " El Rol No Ha Sido Removido Del Usuario";
                }
                else
                {
                    response.Mensaje += "\n El Role Ha Sido Removido Del Usuario";
                }
            }
            return response;
        }
            
        private RoleDTO setRoleDTO(IdentityRole role) {
            return new RoleDTO()
            {
                Url = new UrlHelper(request).Link("GetRoleById", new { id = role.Id }),
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
