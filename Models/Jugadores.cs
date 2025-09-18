using System.ComponentModel.DataAnnotations;

namespace RegistroJugadores.Models;

public class Jugadores
{
    [Key]
    public int JugadorId { get; set; }

    [Required(ErrorMessage = "El campo 'Nombres' es obligatorio.")]
    public string Nombres { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "El número de partidas debe ser mayor o igual a 1")]
    public int Victorias { get; set; }
    public int Derrotas { get; set; }
    public int Empates { get; set; }
}
