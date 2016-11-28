using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.DAL;
using Entidades;
using System.Net.Http;

namespace CapaLogica
{

    public class EquiposBLL
    {
        ResponseDTO response = new ResponseDTO();
        Contexto db = new Contexto();


        public ResponseDTO Insertar(EquiposDTO equipos)
        {
            using (db = new Contexto())
            {
                try
                {
                    // preparar el Equipo para guardar
                    Equipos nuevo = new Equipos();
                    nuevo.Modelo = equipos.Modelo;
                    nuevo.Serial = equipos.Serial;
                    nuevo.Tipo = equipos.Tipo;
                    nuevo.Marca = equipos.Marca;
                    nuevo.Estado = true;
                    nuevo.FechaCompra = equipos.FechaCompra;
                    nuevo.Descripcion = equipos.Descripcion;
                    db.Equipos.Add(nuevo);

                    // preparar la respuesta

                    response.Mensaje = "Equipo Insertado";
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


        public async Task<ResponseDTO> Editar(EquiposDTO actual)
        {
            using (db = new Contexto())
            {
                try
                {
                    Equipos viejo = await db.Equipos.FindAsync(actual.EquiposId);

                    if (viejo != null)
                    {
                        //viejo.Modelo = actual.Modelo;
                        // viejo.Serial = actual.Serial;
                        // viejo.Tipo = actual.Tipo;
                        // viejo.Marca = actual.Marca;

                        //   viejo.FechaCompra = actual.FechaCompra;
                        //    viejo.Descripcion = actual.Descripcion;
                        viejo.Estado = actual.Estado;
                    }

                    //3. Mark entity as modified
                    db.Entry(viejo).State = System.Data.Entity.EntityState.Modified;


                    // preparar la respuesta

                    response.Mensaje = "Equipo Insertado";
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

        public RespuestaDTO<List<EquiposDTO>> getEquipos()
        {
            RespuestaDTO<List<EquiposDTO>> response = new RespuestaDTO<List<EquiposDTO>>();
            response.Mensaje = "Listado de equipos";
            using (db = new Contexto())
            {
                var Equipos = db.Equipos.ToList();
                response.Data = EquiposToEquiposDTO(Equipos);
                return response;


            }

        }

        private List<EquiposDTO> EquiposToEquiposDTO(List<Equipos> equipos)
        {
            List<EquiposDTO> listadoDTO = new List<EquiposDTO>();
            foreach (Equipos equipo in equipos)
            {
                listadoDTO.Add(new EquiposDTO()
                {
                    EquiposId = equipo.EquiposId,
                    Descripcion = equipo.Descripcion,
                    Estado = equipo.Estado,
                    FechaCompra = equipo.FechaCompra,
                    Marca = equipo.Marca,
                    Modelo = equipo.Modelo,
                    Serial = equipo.Serial,
                    Tipo = equipo.Tipo


                });


            }

            return listadoDTO;
        }

    }

}
