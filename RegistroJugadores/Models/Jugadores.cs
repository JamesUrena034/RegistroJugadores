using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroJugadores.Models;

public class Jugadores
{
    [Key]
    public int JugadorId { get; set; }

    [Required(ErrorMessage = "El campo 'Nombres' es obligatorio.")]
    public string Nombres { get; set; } = null!;

    public int Victorias { get; set; } = 0;
    public int Derrotas { get; set; } = 0;
    public int Empates { get; set; } = 0;

    [InverseProperty(nameof(Models.Movimientos.Jugadores))]
    public virtual ICollection<Movimientos> Movimientos { get; set; } = new List<Movimientos>();

}
