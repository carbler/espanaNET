using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RespuestaDTO<T> : ResponseDTO
    {
        public T Data { get; set; }

    }
}
