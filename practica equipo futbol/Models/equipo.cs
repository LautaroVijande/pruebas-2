using System;
using System.Collections.Generic;

namespace practica_equipo_futbol.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Entrenador { get; set; }
        public string Ciudad { get; set; }  
        public List<Jugador> Jugadores { get; set; }

        public Equipo(string nombre, string entrenador, string ciudad)
        {
            this.Nombre = nombre;
            this.Entrenador = entrenador;
            this.Ciudad = ciudad;  
            Jugadores = new List<Jugador>();
        }

        public void AgregarJugador(Jugador jugador)
        {
            Jugadores.Add(jugador);
        }

        public bool BuscarJugador(string nombre)
        {
            return Jugadores.Exists(j => j.Nombre == nombre);
        }

        public void BorrarJugador(Jugador jugador)
        {
            Jugadores.Remove(jugador);
            Console.WriteLine($"Se eliminó el jugador {jugador} correctamente");
        }
    }
}