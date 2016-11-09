using System.Collections.Generic;

namespace Entidades
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
            Mensaje = "";
            Error = new List<ErrorDTO>();
        }

        public string Mensaje { get; set; }
        public List<ErrorDTO> Error { get; set; }
        public int FilasAfectadas { get; set; }
    }
}
