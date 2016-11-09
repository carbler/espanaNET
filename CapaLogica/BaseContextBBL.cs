using CapaDatos.Infraestructura;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class BaseContextBBL
    {
        protected ApplicationUserManager UserManager;
        protected ApplicationRoleManager RoleManager;
        protected HttpRequestMessage request;

        public BaseContextBBL(HttpRequestMessage request) {
            this.request = request;
            UserManager = UserManager ?? request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = RoleManager ?? request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        }
    }
}
