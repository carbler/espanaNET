using Entidades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Infraestructura
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
           : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            return appUserManager;
        }

        public static ApplicationUserManager Create(HttpRequestMessage request)
        {
            return request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public static List<ErrorDTO> Result(IdentityResult result)
        {
            List<ErrorDTO> errors = new List<ErrorDTO>();
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        errors.Add(new ErrorDTO { Code = "", Menssage = error });
                    }
                }
                else
                {
                    errors.Add(new ErrorDTO { Code = "500", Menssage = "info En El Servidor" });
                }
            }
            return errors;
        }
    }
}
