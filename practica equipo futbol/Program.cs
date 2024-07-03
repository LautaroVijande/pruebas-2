using practica_equipo_futbol.Models;
using System;
using System.Collections.Generic;

namespace practica_equipo_futbol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            // Cargar equipo nuevo con parametros
            Equipo equipo1 = new Equipo("Union de Santa Fe", "Mariano Coppola","SANTA FE");

            // Cargar jugadores nuevos con parametros
            Jugador jugador1 = new Jugador("Lautaro\n", 9, 13);
            Jugador jugador2 = new Jugador("Thiago\n", 5, 13);

            // Agregar los jugadores
            equipo1.AgregarJugador(jugador1);
            equipo1.AgregarJugador(jugador2);

            // Mostrar información de datos de las tablas en la BBDD
            Console.WriteLine($"Equipo: {equipo1.Nombre}");
            Console.WriteLine($"Entrenador: {equipo1.Entrenador}");
            Console.WriteLine("Jugadores:");
            foreach (var jugador in equipo1.Jugadores)
            {
                Console.WriteLine($"Jugador: {jugador.Nombre}, Número: {jugador.Numero}");
            }

            // Buscar los jugadores con el nombre Lautaro y Thiago
            bool buscarJugador1 = equipo1.BuscarJugador("Lautaro");
            bool buscarJugador2 = equipo1.BuscarJugador("Thiago");

            // Mostrar información sobre la búsqueda que se nombra en el paso anterior
            Console.WriteLine($"Jugador Lautaro: {buscarJugador1}");
            Console.WriteLine($"Jugador Thiago: {buscarJugador2}");

            // Eliminar jugador con datos
            Jugador jugadorABorrar = equipo1.Jugadores.Find(j => j.Nombre == "Lautaro");
            if (jugadorABorrar != null)
            {
                // Si se encontró, eliminarlo del equipo
                equipo1.BorrarJugador(jugadorABorrar);
            }

            // Crear equipo visitante y local
            EquipoLocal equipoLocal = new EquipoLocal("Los Chacras", "Luis", "Ituzaingo");
            EquipoVisitante equipoVisitante = new EquipoVisitante("Los Charros", "Marinao", "Merlo");

            // Crear una instancia de la clase Partido con los equipos local y visitante
            Partido partido = new Partido(equipoLocal, equipoVisitante);

            // Simular el partido y obtener el resultado
            string resultadoPartido = partido.SimularPartido();

            // Mostrar el resultado del partido
            Console.WriteLine($"Resultado del partido: {resultadoPartido}");
            

            EquipoBD equipoBD = new EquipoBD();

           
            
            Console.WriteLine("\n\n");

            //Obtener la lista de todos los equipos y sus entrenadores
            var equiposConEntrenadores = equipoBD.GetEquiposConEntrenadores();
            Console.WriteLine("Lista de todos los equipos y sus entrenadores: \n");
            foreach (var equipo in equiposConEntrenadores)
            {
                Console.WriteLine($"ID: {equipo.Id}, Nombre: {equipo.Nombre}, Entrenador: {equipo.Entrenador}, Ciudad: {equipo.Ciudad}");
            }

            Console.WriteLine("\n\n");

            //Lista ordenada de jugadores y equipos
            var equiposConJugadores = equipoBD.GetEquiposYJugadoresOrdenados();

            Console.WriteLine("Lista de equipos y sus jugadores ordenados por nombre del equipo y del jugador:");
            foreach (var equipoConJugadores in equiposConJugadores)
            {
                var equipo = equipoConJugadores.Equipo;
                Console.WriteLine($"\nEquipo: {equipo.Nombre}, Entrenador: {equipo.Entrenador}, Ciudad: {equipo.Ciudad}");

                foreach (var jugador in equipoConJugadores.Jugadores)
                {
                    Console.WriteLine($"\tJugador: {jugador.Nombre}, Número: {jugador.Numero}");
                }
            }

            Console.WriteLine("\n\n");

            //Mostrar número de jugadores por equipo
            Console.WriteLine("\nNúmero de jugadores por equipo: \n");
            foreach (var equipo in equiposConEntrenadores)
            {
                int numeroJugadores = equipoBD.GetNumeroJugadoresPorEquipo(equipo.Id);
                Console.WriteLine($"Equipo: {equipo.Nombre}, Número de Jugadores: {numeroJugadores}");
            }

            Console.WriteLine("\n\n");
            Console.WriteLine("5. Cambiar equipo del jugador");
            int jugadorId = 9;
            int nuevoEquipoId = 13;
            equipoBD.CambiarEquipoJugador(jugadorId, nuevoEquipoId);

            Console.WriteLine($"Jugador {jugadorId} Se asigno al Equipo {nuevoEquipoId}.");

            
           
            // Obtener la lista de equipos que no tienen jugadores
            /*var equiposSinJugadores = equipoBD.GetEquiposSinJugadores();
            Console.WriteLine("\nEquipos que no tienen jugadores: \n");
            foreach (var equipo in equiposSinJugadores)
            {
                Console.WriteLine($"ID: {equipo.Id}, Nombre: {equipo.Nombre}, Entrenador: {equipo.Entrenador}");
            }*/

            //  Obtener la lista de jugadores de un equipo específico
            /* Console.WriteLine("Ingrese el ID del equipo para ver sus jugadores:");
             int equipoId = int.Parse(Console.ReadLine());
             var jugadores = equipoBD.GetNumeroJugadoresPorEquipoEspecifico(equipoId);

             Console.WriteLine($"\nJugadores del equipo con ID {equipoId}:");
             foreach (var jugador in jugadores)
             {
                 Console.WriteLine($"ID: {jugador.Id}, Nombre: {jugador.Nombre}, Número: {jugador.Numero}");
             }*/

        }
    }
}
        
    
