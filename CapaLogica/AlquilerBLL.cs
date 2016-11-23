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
     
        public ResponseDTO Insertar(AlquilerDTO alquiler)
        {
            using (Contexto db = new Contexto())
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
                    String[] tiposEquipo = alquiler.equipos.Split(new Char[] {'.'});

                   
                    foreach (String tipo in tiposEquipo)
                    {
                    //   DbRawSqlQuery<Equipos> x = consulta(alquiler.fechaFinal.ToString(),alquiler.fechaFinal.ToString(), tipo);
                       // nuevo.Equipos.Add(x.First());
                    }
                  


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

        public List<Equipos> GetRecords()
        {
            using (Contexto db = new Contexto())
            {
                return db.Equipos
                    .Select(t =>
                        new Equipos
                        {
                            EquiposId  = t.EquiposId
                        }
                    ).ToList();
            }
        }
        /*
        public DbRawSqlQuery<Equipos> consulta(String fechaInicial, String fechaFinal, String Tipo)
        {
            using (Contexto db = new Contexto())
            {
                var sql = @"SELECT *from Equipos where not EquiposId in (select ea.Equipos_EquiposId from Alquiler a, EquiposAlquiler ea where a.AlquilerId=ea.Alquiler_AlquilerId and @fechaInicial BETWEEN a.fechaInicial and a.fechaFinal and @fechaFinal BETWEEN a.fechaInicial AND a.fechaFinal union select ea.Equipos_EquiposId from Alquiler a, EquiposAlquiler ea where a.AlquilerId=ea.Alquiler_AlquilerId and a.fechaInicial BETWEEN @fechaInicial and @fechaFinal AND a.fechaFinal BETWEEN @fechaInicial and @fechaFinal) and Tipo=@Tipo";
                /*
                object[] parameters = new object[] {
                     new SqlParameter("@fechaInicial", fechaInicial),
                     new SqlParameter("@Tipo", Tipo),
                     new SqlParameter("@fechaFinal", fechaFinal) };
                return db.Database.SqlQuery<Equipos>(sql, parameters);

               */
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
    }
}
