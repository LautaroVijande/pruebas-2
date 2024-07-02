using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;


namespace practica_equipo_futbol.Models
{
    public class EquipoBD
    {
        private string connectionString = "Data Source= GCNB-470\\LAUTAROSERVER;Initial Catalog=gestion_equipos_futbol;Integrated Security=True;";

        public List<Equipo> GetEquipos()
        {
            List<Equipo> equipos = new List<Equipo>();
            string query = "SELECT idEquipos, nombre, entrenador, ciudad FROM equipos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    string entrenador = reader.GetString(reader.GetOrdinal("entrenador"));
                    string ciudad = reader.IsDBNull(reader.GetOrdinal("ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("ciudad"));

                    Equipo equipo = new Equipo(nombre, entrenador, ciudad)
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id"))
                    };
                    equipos.Add(equipo);
                }

                reader.Close();
                connection.Close();
            }

            return equipos;
        }

       
        /* public List<Jugador> GetJugadoresPorEquipoId(int equipoId)
        {
            List<Jugador> jugadores = new List<Jugador>();
            string query = "SELECT idJugador, nombre, equipos, numero FROM Jugadores";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@equipoId", equipoId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Jugador jugador = new Jugador
                        (
                       reader.GetString("nombre"),
                       reader.GetInt32("numero"),
                      
                        )
                    {
                       // Id = reader.GetInt32("idJugador")
                    };
                    jugadores.Add(jugador);
                }

                reader.Close();
                connection.Close();
            }

            return jugadores;
        }*/

        public List<Equipo> GetEquiposConEntrenadores()
        {
            List<Equipo> equipos = new List<Equipo>();
            string query = "SELECT idEquipos, nombre, entrenador, ciudad FROM Equipos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    string entrenador = reader.GetString(reader.GetOrdinal("entrenador"));
                    string ciudad = reader.IsDBNull(reader.GetOrdinal("ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("ciudad"));

                    Equipo equipo = new Equipo(nombre, entrenador, ciudad)
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("idEquipos"))
                    };
                    equipos.Add(equipo);
                }

                reader.Close();
                connection.Close();
            }

            return equipos;
        }

        public int GetNumeroJugadoresPorEquipo(int equipoId)
        {
            int numeroJugadores = 0;
            string query = "SELECT COUNT(*) FROM Jugadores WHERE idEquipos = @idEquipos;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idEquipos", equipoId);
                connection.Open();
                numeroJugadores = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }

            return numeroJugadores;
        }

        public void UpdateCiudadEquipo(int equipoId, string ciudad)
        {
            string query = "UPDATE Equipos SET ciudad = @ciudad WHERE idEquipos = @idEquipos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ciudad", ciudad);
                command.Parameters.AddWithValue("@idEquipos", equipoId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<EquipoConJugadores> GetEquiposYJugadoresOrdenados()
        {
            List<EquipoConJugadores> equiposConJugadores = new List<EquipoConJugadores>();
            string query = @"
        SELECT e.idEquipos AS EquipoId, e.nombre AS EquipoNombre, e.entrenador AS EquipoEntrenador, e.ciudad AS EquipoCiudad
        FROM Equipos e
        LEFT JOIN Jugadores j ON  e.idEquipos = j.idEquipos
        ORDER BY e.nombre, j.nombre";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int equipoId = reader.GetInt32(reader.GetOrdinal("EquipoId"));
                    string equipoNombre = reader.GetString(reader.GetOrdinal("EquipoNombre"));
                    string equipoEntrenador = reader.GetString(reader.GetOrdinal("EquipoEntrenador"));
                    string equipoCiudad = reader.IsDBNull(reader.GetOrdinal("EquipoCiudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("EquipoCiudad"));

                    EquipoConJugadores equipoConJugadores = equiposConJugadores.Find(e => e.Equipo.Id == equipoId);
                    if (equipoConJugadores == null)
                    {
                        equipoConJugadores = new EquipoConJugadores
                        {
                            Equipo = new Equipo(equipoNombre, equipoEntrenador, equipoCiudad) { Id = equipoId },
                            Jugadores = new List<Jugador>()
                        };
                        equiposConJugadores.Add(equipoConJugadores);
                    }

                }

                reader.Close();
                connection.Close();
            }

            return equiposConJugadores;
        }

        public void CambiarEquipoJugador(int jugadorId, int nuevoEquipoId)
        {
            string query = "UPDATE Jugadores SET idEquipos = @nuevoEquipoId WHERE idJugador = @idJugador ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nuevoEquipoId", nuevoEquipoId);
                command.Parameters.AddWithValue("@idJugador", jugadorId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        
        public int GetNumeroJugadoresPorEquipoEspecifico(int equipoId)
        {
            int numeroJugadoresEspecificos = 0;
            string query = "SELECT COUNT(*) FROM Jugadores WHERE idEquipos = 10";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                numeroJugadoresEspecificos = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }

            return numeroJugadoresEspecificos;
        }


        /*public List<Equipo> GetEquiposSinJugadores()
        {
            List<Equipo> equiposSinJugadores = new List<Equipo>();
            string query = @"
        SELECT e.idEquipos, e.nombre, e.entrenador, e.ciudad
        FROM Equipos e
        LEFT JOIN jugadores j ON e.idEquipos = j.idEquipos
        WHERE j.Equipos IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    string entrenador = reader.GetString(reader.GetOrdinal("entrenador"));
                    string ciudad = reader.IsDBNull(reader.GetOrdinal("ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("ciudad"));

                    Equipo equipo = new Equipo(nombre, entrenador, ciudad)
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("idEntrenador"))
                    };
                    equiposSinJugadores.Add(equipo);
                }

                reader.Close();
                connection.Close();
            }

            return equiposSinJugadores;
        }*/

        
    }
}