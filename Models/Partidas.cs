using System.ComponentModel.DataAnnotations;

namespace RegistroJugadores.Models
{
    public class Partidas
    {
        [Key]
        public int PartidasId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe Seleccionar un Jugador Valido")]
        public int JugadorId { get; set; }
        public int? Jugador2Id { get; set; }
        [Required(ErrorMessage = " Debe Espeficicar el Estado ")]


    }
}
