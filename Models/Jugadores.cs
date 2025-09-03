using System.ComponentModel.DataAnnotations;

namespace RegistroJugadores.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre' es obligario.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo 'Partidas' es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El numero de partida debe ser un valor valido.")]
        public int Partidas { get; set; }
    }
}
