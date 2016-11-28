using CapaDatos.DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace CapaLogica
{
    public class AlquilerBLL
    {
        ResponseDTO response = new ResponseDTO();
        Contexto db;
        public ResponseDTO Insertar(AlquilerDTO alquiler)
        {
            using (db= new Contexto())
            {
                try
                {

                    // preparar el cliente para guardar
                    Alquiler nuevo = new Alquiler();
                    nuevo.AlquilerId = alquiler.AlquilerId;
                    nuevo.Direccion = alquiler.Direccion;
                    nuevo.nombreCliente = alquiler.nombreCliente;
                    nuevo.Telefono = alquiler.Telefono;
                    nuevo.fechaFinal = alquiler.fechaFinal;
                    nuevo.fechaInicial = alquiler.fechaInicial;

                    // String x = DateTime.Now.ToString();
                    // Separando el string con los tipos de equipos
                    String[] tiposEquipo = alquiler.equipos.Split(new Char[] { '.' });


                    foreach (String tipo  in tiposEquipo)
                    {
                        List<Equipos> x = Disponibles(alquiler.fechaInicial, alquiler.fechaFinal, tipo);
                       // nuevo.Equipos.Add(x.FirstOrDefault());

                        if(x.Count > 0)
                        {
                            x[0].Alquilers.Add(nuevo);
                        }else
                        {

                            response.Mensaje = "No hay equipos Disponibles";
                            response.FilasAfectadas = 0;

                            return response;
                        }
                    }
                        
                   

                    //  db.Alquiler.Add(nuevo);

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

  
        /*
        public List<Equipos> Consulta(DateTime fechaInicial, DateTime fechaFinal, String Tipo)
        {
            using (Contexto db = new Contexto())
            {
                var sql = @"SELECT *from Equipos where not EquiposId in (select ea.Equipos_EquiposId from Alquiler a, EquiposAlquiler ea where a.AlquilerId=ea.Alquiler_AlquilerId and @fechaInicial BETWEEN a.fechaInicial and a.fechaFinal and @fechaFinal BETWEEN a.fechaInicial AND a.fechaFinal union select ea.Equipos_EquiposId from Alquiler a, EquiposAlquiler ea where a.AlquilerId=ea.Alquiler_AlquilerId and a.fechaInicial BETWEEN @fechaInicial and @fechaFinal AND a.fechaFinal BETWEEN @fechaInicial and @fechaFinal) and Tipo=@Tipo";

                object[] parameters = new object[] {
                     new SqlParameter("@fechaInicial", fechaInicial),
                     new SqlParameter("@Tipo", Tipo),
                     new SqlParameter("@fechaFinal", fechaFinal) };
                List<Equipos> ListaEquipos = db.Database.SqlQuery<Equipos>(sql, parameters).ToList();
                return ListaEquipos;
                /*
                ObjectQuery<Equipos> contactQuery =
                new ObjectQuery<Equipos>(sql, db.Database)

                 // The following query returns a collection of Contact objects.
                 ObjectQuery<Equipos> query = new ObjectQuery<Equipos>(sql, db, MergeOption.NoTracking);
                 query.Parameters.Add(new ObjectParameter("ln", "Zhou"));

                 // Add parameters to the collection.
                 contactQuery.Parameters.Add(new ObjectParameter("@fechaInicial", fechaInicial));
                 contactQuery.Parameters.Add(new ObjectParameter("@Tipo",Tipo));
                 contactQuery.Parameters.Add(new ObjectParameter("@fechaFinal", fechaFinal));
              
            }






        }

      */

        public RespuestaDTO<List<AlquilerDTO>> getAlquileresPorFecha(DateTime fechaInicialPrestamo, DateTime fechaFinalPrestamo)
        {
            RespuestaDTO<List<AlquilerDTO>> response = new RespuestaDTO<List<AlquilerDTO>>();
            
            using (db = new Contexto())
            {
                var Alquileres = db.Alquiler.Where(t =>
                        (
                        (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaInicialPrestamo)
                        ||
                        (t.fechaInicial <= fechaFinalPrestamo && t.fechaFinal >= fechaFinalPrestamo)
                        ||
                        (t.fechaInicial < fechaInicialPrestamo && t.fechaFinal > fechaFinalPrestamo)
                        ||
                        (fechaInicialPrestamo < t.fechaInicial && fechaFinalPrestamo > t.fechaFinal)
                        )).ToList();

                response.Mensaje = "Listado Alquileres";
                response.Data = AlquilerToAlquilerDTO(Alquileres);
                return response ;


            }
           
        }

        public List<Equipos> Disponibles(DateTime fechaInicialPrestamo, DateTime fechaFinalPrestamo, String Tipo)
        {
          
                var EquiposDisponibles = db.Equipos.Where(e => e.Tipo == Tipo && e.Alquilers.Where(t =>
                 (
                 (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaInicialPrestamo)
                 ||
                 (t.fechaInicial <= fechaFinalPrestamo && t.fechaFinal >= fechaFinalPrestamo)
                 ||
                 (t.fechaInicial < fechaInicialPrestamo && t.fechaFinal > fechaFinalPrestamo)
                 ||
                 (fechaInicialPrestamo < t.fechaInicial && fechaFinalPrestamo > t.fechaFinal)
                 )
                ).Count() == 0).ToList();

                return EquiposDisponibles;

        }

        /*
        public List<Equipos> AlquileresDocente(, DateTime fechaFinalPrestamo, String Tipo)
        {

            var EquiposDisponibles = db.Equipos.Where(e => e.Tipo == Tipo && e.Alquilers.Where(t =>
             (
             (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaInicialPrestamo)
             ||
             (t.fechaInicial <= fechaFinalPrestamo && t.fechaFinal >= fechaFinalPrestamo)
             ||
             (t.fechaInicial < fechaInicialPrestamo && t.fechaFinal > fechaFinalPrestamo)
             ||
             (fechaInicialPrestamo < t.fechaInicial && fechaFinalPrestamo > t.fechaFinal)
             )
            ).Count() == 0).ToList();

            return EquiposDisponibles;

        }

    */
        private List<AlquilerDTO> AlquilerToAlquilerDTO(List<Alquiler> alquileres){
            List<AlquilerDTO> listadoDTO = new List<AlquilerDTO>();
            foreach (Alquiler alquiler  in alquileres)
            {
                listadoDTO.Add(new AlquilerDTO()
                {
                    AlquilerId = alquiler.AlquilerId,
                    Direccion = alquiler.Direccion,
                    nombreCliente = alquiler.nombreCliente,
                    fechaInicial = alquiler.fechaInicial,
                    fechaFinal = alquiler.fechaFinal,
                    Telefono = alquiler.Telefono
                    

                });


            }

            return listadoDTO;
       }

    }
}
