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
    public class AlquilerInstitucionBLL
    {
        ResponseDTO response = new ResponseDTO();
        Contexto db;
        public ResponseDTO Insertar(AlquilerInstitucionDTO alquiler)
        {
            using (db = new Contexto())
            {
                try
                {

                    // preparar el cliente para guardar
                    AlquilerInstitucion nuevo = new AlquilerInstitucion();
                    nuevo.AlquilerInstitucionId = alquiler.AlquilerInstitucionId;
                    nuevo.Salon = alquiler.Salon;
                    nuevo.Docente = db.Docentes.Where((e => e.DocenteId == alquiler.Docente)).First();
                    nuevo.Descripcion = alquiler.Descripcion;
                    nuevo.fechaFinal = alquiler.fechaFinal;
                    nuevo.fechaInicial = alquiler.fechaInicial;

                    // String x = DateTime.Now.ToString();
                    // Separando el string con los tipos de equipos
                    String[] tiposEquipo = alquiler.equipos.Split(new Char[] { ',' });
                    int[] cantidadTipo = new int[3];
                    foreach (String tipo in tiposEquipo)
                    {
                        if (tipo != "")
                        {
                            if (tipo == "Proyector")
                            {
                                cantidadTipo[0] = cantidadTipo[0] + 1;
                            }
                            else if (tipo == "Luces")
                            {
                                cantidadTipo[1] = cantidadTipo[0] + 1;

                            }
                            else if (tipo == "Sonido")
                            {
                                cantidadTipo[2] = cantidadTipo[0] + 1;

                            }
                        }
                    }



                    List<Equipos> listadoProyectores = Disponibles(alquiler.fechaInicial, alquiler.fechaFinal, "Proyector");
                    List<Equipos> listadoLuces = Disponibles(alquiler.fechaInicial, alquiler.fechaFinal, "Luces");
                    List<Equipos> listadoSonido = Disponibles(alquiler.fechaInicial, alquiler.fechaFinal, "Sonido");


                    if (cantidadTipo[0] > listadoProyectores.Count)
                    {

                        response.Mensaje = "No hay Proyectores Disponibles";
                        response.FilasAfectadas = 0;
                        response.Error.Add(new ErrorDTO()
                        {
                            Menssage = "No hay Proyectores Disponibles"
                        });

                        return response;
                    }
                    else if (cantidadTipo[1] > listadoLuces.Count)
                    {

                        response.Mensaje = "No hay Luces Disponibles";
                        response.FilasAfectadas = 0;
                        response.Error.Add(new ErrorDTO()
                        {
                            Menssage = "No hay Proyectores Disponibles"
                        });

                        return response;

                    }
                    else if (cantidadTipo[2] > listadoSonido.Count)
                    {

                        response.Mensaje = "No hay Sonido Disponibles";
                        response.FilasAfectadas = 0;
                        response.Error.Add(new ErrorDTO()
                        {
                            Menssage = "No hay Proyectores Disponibles"
                        });

                        return response;

                    }



                    for (var i = 0; i < cantidadTipo[0]; i++)
                    {
                        listadoProyectores[i].AlquilersInstitucion.Add(nuevo);

                    }

                    for (var i = 0; i < cantidadTipo[1]; i++)
                    {
                        listadoLuces[i].AlquilersInstitucion.Add(nuevo);

                    }

                    for (var i = 0; i < cantidadTipo[2]; i++)
                    {
                        listadoSonido[i].AlquilersInstitucion.Add(nuevo);

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


        public RespuestaDTO<List<AlquilerInstitucionDTO>> getAlquileresDocente(int docenteId)
        {
            RespuestaDTO<List<AlquilerInstitucionDTO>> response = new RespuestaDTO<List<AlquilerInstitucionDTO>>();

            using (db = new Contexto())
            {
                var Alquileres = db.AlquilerInstitucion.Where(t =>(t.Docente.DocenteId == docenteId)).ToList();

                response.Mensaje = "Listado Alquileres";
                response.Data = AlquilerInstitucionToAlquilerInstitucionDTO(Alquileres);
                return response;


            }

        }

        public RespuestaDTO<List<AlquilerInstitucionDTO>> getAlquileresInstitucionPorFecha(DateTime fechaInicialPrestamo, DateTime fechaFinalPrestamo)
        {
            RespuestaDTO<List<AlquilerInstitucionDTO>> response = new RespuestaDTO<List<AlquilerInstitucionDTO>>();

            using (db = new Contexto())
            {
                var Alquileres = db.AlquilerInstitucion.Where(t =>
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
                response.Data = AlquilerInstitucionToAlquilerInstitucionDTO(Alquileres);
                return response;


            }

        }


        private List<Equipos> Disponibles(DateTime fechaInicialPrestamo, DateTime fechaFinalPrestamo, String Tipo)
        {
            List<Equipos> EquiposAlquileres = Disponibles2(fechaInicialPrestamo, fechaFinalPrestamo, Tipo);
            List<Equipos> EquiosAlquileresInstitucion = Disponibles3(fechaInicialPrestamo, fechaFinalPrestamo, Tipo);

            List<Equipos> disponibles = new List<Equipos>();
            foreach(Equipos e1 in EquiposAlquileres)
            {
                int contador = 0;
                foreach (Equipos e2 in EquiosAlquileresInstitucion)
                {
                    if(e1.EquiposId == e2.EquiposId)
                    {
                        contador++;
                    }

                }

                if(contador>0)
                {
                    disponibles.Add(e1);

                }
            }

            foreach (Equipos e1 in EquiosAlquileresInstitucion)
            {
                int contador = 0;
                foreach (Equipos e2 in EquiposAlquileres)
                {
                    if (e1.EquiposId == e2.EquiposId)
                    {
                        contador++;
                    }

                }

                if (contador > 0)
                {
                    if (!disponibles.Contains(e1))
                    {
                        disponibles.Add(e1);
                    }

                }
            }


            return disponibles;


        }

       

        private List<Equipos> Disponibles3(DateTime fechaInicialPrestamo, DateTime fechaFinalPrestamo, String Tipo)
        {

            var EquiposDisponibles = db.Equipos.Where(e => e.Tipo == Tipo && e.Estado == true && e.AlquilersInstitucion.Where(t =>
            (
            (t.fechaInicial >= fechaInicialPrestamo && t.fechaInicial <= fechaFinalPrestamo)
            ||
            (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaInicialPrestamo && t.fechaFinal <= fechaFinalPrestamo)
            ||
            (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaFinalPrestamo)
            )
           ).Count() == 0).ToList();

            return EquiposDisponibles;

        }

        public List<Equipos> Disponibles2(DateTime fechaInicialPrestamo, DateTime fechaFinalPrestamo, String Tipo)
        {

            var EquiposDisponibles = db.Equipos.Where(e => e.Tipo == Tipo && e.Estado == true &&  e.Alquilers.Where(t =>
             (
             (t.fechaInicial >= fechaInicialPrestamo && t.fechaInicial <= fechaFinalPrestamo)
             ||
             (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaInicialPrestamo && t.fechaFinal <= fechaFinalPrestamo)
             ||
             (t.fechaInicial <= fechaInicialPrestamo && t.fechaFinal >= fechaFinalPrestamo)
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
        private List<AlquilerInstitucionDTO> AlquilerInstitucionToAlquilerInstitucionDTO(List<AlquilerInstitucion> alquileres)
        {
            List<AlquilerInstitucionDTO> listadoDTO = new List<AlquilerInstitucionDTO>();
            foreach (AlquilerInstitucion alquiler in alquileres)
            {
                listadoDTO.Add(new AlquilerInstitucionDTO()
                {
                    AlquilerInstitucionId = alquiler.AlquilerInstitucionId,
                    Descripcion = alquiler.Descripcion,
                    Salon = alquiler.Salon,
                    Docente = alquiler.Docente.DocenteId,                   
                    fechaInicial = alquiler.fechaInicial,
                    fechaFinal = alquiler.fechaFinal
                    
                });


            }

            return listadoDTO;
        }

    }
}
