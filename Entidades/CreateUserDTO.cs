using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CreateUserDTO : UserDTO
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
    }
}
