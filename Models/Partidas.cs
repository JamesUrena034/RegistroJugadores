using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroJugadores.Models
{
    public class Partidas
    {
        [Key]
        public int PartidasId { get; set; }

        [Required(ErrorMessage = "Jugador 1 es obligatorio")]
        public int Jugador1Id { get; set; }

        [ForeignKey("Jugador1Id")]
        public Jugadores? Jugador1 { get; set; }

        [Required(ErrorMessage = "Jugador 2 es obligatorio")]
        public int Jugador2Id { get; set; }

        [ForeignKey("Jugador2Id")]
        public Jugadores? Jugador2 { get; set; }
         
        public int? GanadorId { get; set; }

        [ForeignKey("GanadorId")]
        public Jugadores? Ganador { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "El turno es obligatorio")]
        public int TurnoJugadorId { get; set; }

        [ForeignKey("TurnoJugadorId")]
        public Jugadores? TurnoJugador { get; set; }

        [Required(ErrorMessage = "El estado de la partida es obligatorio")]
        public string EstadoPartida { get; set; } = string.Empty;
    }
}
