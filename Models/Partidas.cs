using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroJugadores.Models
{
    public class Partidas
    {
        [Key]
        public int PartidasId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe Seleccionar un Jugador Valido")]
        public int Jugador1Id { get; set; }
        public int? Jugador2Id { get; set; }
        [Required(ErrorMessage = " Debe Espeficicar el estado de la Partida")]
        [StringLength(20)]
        public string EstadoPartida { get; set; } = null!;
        public int? GanadorId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "'Debe especificar el turno de un jugador Valido")]
        public int TurnoJugadorId { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime? FechaFin { get; set; }

        [ForeignKey("Jugador1Id")]
        [InverseProperty("PartidasJugador1")]
        public virtual Jugadores Jugador1 { get; set; } = null!;

        [ForeignKey("Jugador2Id")]
        [InverseProperty("PartidasJugador2")]
        public virtual Jugadores? Jugador2 { get; set; }

        [ForeignKey("GanadorId")]
        [InverseProperty("PartidasGanadas")]
        public virtual Jugadores? Ganador { get; set; }

        [ForeignKey("TurnoJugadorId")]
        [InverseProperty("PartidasTurno")]
        public virtual Jugadores TurnoJugador { get; set; } = null!;

    }
}
