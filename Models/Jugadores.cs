using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroJugadores.Models;

public class Jugadores
{
    [Key]
    public int JugadorId { get; set; }

    [Required(ErrorMessage = "El campo 'Nombres' es obligatorio.")]
    public string Nombres { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "El número de partidas debe ser mayor o igual a 1")]
    public int Partidas { get; set; }
   
}
