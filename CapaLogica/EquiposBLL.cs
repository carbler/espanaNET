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
                    nuevo.nombreEquipo = equipos.nombreEquipo;
                    nuevo.Tipo = equipos.Tipo;
                    nuevo.Marca = equipos.Marca;
                    nuevo.Estado = equipos.Estado;
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
    }

}
