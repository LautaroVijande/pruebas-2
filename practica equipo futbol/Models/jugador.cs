namespace practica_equipo_futbol.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int EquipoId { get; set; }  

        public Jugador(string nombre, int numero, int equipoId)
        {
            this.Nombre = nombre;
            this.Numero = numero;
            this.EquipoId = equipoId;
        }

        public override string ToString()
        {
            return $"{Nombre} - Número: {Numero}";
        }
    }
}