using CapaDatos.DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
   public class DocenteBLL
    {
        ResponseDTO response = new ResponseDTO();
        Contexto db;
        public ResponseDTO Insertar(DocenteDTO docente)
        {
            using (db = new Contexto())
            {
                try
                {

                    // preparar el cliente para guardar
                    Docente nuevo = new Docente();
                    nuevo.Nombre = docente.Nombre;
                    nuevo.Apellidos = docente.Apellidos;
                    nuevo.Correo = docente.Correo;
                    nuevo.Telefono = docente.Telefono;
                    nuevo.Institucion = docente.Institucion;

                   



                     db.Docentes.Add(nuevo);

                    // preparar la respuesta

                    response.Mensaje = "Docente Insertado";
                    response.FilasAfectadas = db.SaveChanges();

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    response.Mensaje = ex.Message;
                    response.FilasAfectadas = 0;

                }
                catch (Exception ex)
                {
                    response.Mensaje = ex.Message;
                    response.FilasAfectadas = 0;

                }

                return response;


            }

        }

        public RespuestaDTO<List<DocenteDTO>> getDocentes(string idInstitucion)
        {
            RespuestaDTO<List<DocenteDTO>> response = new RespuestaDTO<List<DocenteDTO>>();
            response.Mensaje = "Listado de docentes";
            using (db = new Contexto())
            {
                var docentes = db.Docentes.Where(d => d.Institucion == idInstitucion).ToList();
                response.Data = DocenteToDocenteDTO(docentes);
                return response;


            }

        }

        private List<DocenteDTO> DocenteToDocenteDTO(List<Docente> docentes)
        {
            List<DocenteDTO> listadoDTO = new List<DocenteDTO>();
            foreach (Docente d in docentes)
            {
                listadoDTO.Add(new DocenteDTO()
                {

                 DocenteId = d.DocenteId,
                 Nombre = d.Nombre,
                 Apellidos = d.Apellidos,
                 Correo = d.Correo,
                 Telefono = d.Telefono,
                 Institucion = d.Institucion
                });


            }

            return listadoDTO;
        }
    }
}
