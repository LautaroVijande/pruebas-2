using System;

namespace practica_equipo_futbol.Models
{
    //Clase Partido
    public class Partido : IPartido
    {
        private Equipo equipoLocal;
        private Equipo equipoVisitante;

        public Partido(Equipo equipoLocal, Equipo equipoVisitante)
        {
            this.equipoLocal = equipoLocal;
            this.equipoVisitante = equipoVisitante;
        }

        public string SimularPartido()
        {
            Random rnd = new Random();
            int golesLocal = rnd.Next(0, 5);
            int golesVisitante = rnd.Next(0, 5);

            return $"{equipoLocal.Nombre} {golesLocal} - {golesVisitante} {equipoVisitante.Nombre}";
        }
    }
}