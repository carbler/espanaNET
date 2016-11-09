using CapaDatos.DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace CapaLogica
{
   public class AlquilerBLL 
    {
        ResponseDTO response = new ResponseDTO();
        Contexto db = new Contexto();
    


        public ResponseDTO Insertar(AlquilerDTO alquiler)
        {
            using (db = new Contexto())
            {
                try
                {
                    // preparar el cliente para guardar
                    Alquiler nuevo = new Alquiler();
                    nuevo.AlquilerId = alquiler.AlquilerId;
                    nuevo.Direccion = alquiler.Direccion;
                    nuevo.nombreCliente = alquiler.nombreCliente;
                    nuevo.Servicios = alquiler.Servicios;
                    nuevo.Telefono = alquiler.Telefono;
                    db.Alquiler.Add(nuevo);

                    // preparar la respuesta

                    response.Mensaje = "Alquiler Insertado";
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
