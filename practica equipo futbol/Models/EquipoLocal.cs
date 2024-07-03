 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_equipo_futbol.Models
{
    public class EquipoLocal : Equipo, IPartido
    {
        public EquipoLocal(string nombre, string entrenador, string ciudad) : base(nombre, entrenador, ciudad) { }

        public string SimularPartido()
        {
            return "Simulación del partido como equipo local";
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}