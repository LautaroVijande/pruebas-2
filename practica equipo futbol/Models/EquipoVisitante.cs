using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_equipo_futbol.Models
{
    public class EquipoVisitante : Equipo, IPartido
    {
        public EquipoVisitante(string nombre, string entrenador, string ciudad) : base(nombre, entrenador, ciudad) { }

        public string SimularPartido()
        {
            return "Simulación del partido como equipo visitante";
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
