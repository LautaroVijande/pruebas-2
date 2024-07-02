 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_equipo_futbol.Models
{
    public class EquipoLocal : Equipo, IPartido
    {
        // Constructor
        public EquipoLocal(string nombre, string entrenador, string ciudad) : base(nombre, entrenador, ciudad) { }

        public string SimularPartido()
        {
            return "Simulación del partido como equipo local";
        }

        // Devuelve el nombre del equipo
        public override string ToString()
        {
            return this.Nombre;
        }
    }
}