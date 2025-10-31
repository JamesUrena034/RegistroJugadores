using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroJugadores.DAL;
using RegistroJugadores.Models;

namespace RegistroJugadores.Services
{
    public class PartidasService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public PartidasService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        private async Task<bool> Existe(int partidaId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Partidas.AnyAsync(p => p.PartidaId == partidaId);
        }

        public async Task<bool> Guardar(Partidas partida)
        {
            if (!await Existe(partida.PartidaId))
                return await Insertar(partida);
            else
                return await Modificar(partida);
        }

        private async Task<bool> Insertar(Partidas partida)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Partidas.Add(partida);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Partidas partida)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Partidas.Update(partida);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Partidas?> Buscar(int partidaId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .Include(p => p.Ganador)
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.TurnoJugador)
                .FirstOrDefaultAsync(p => p.PartidaId == partidaId);
        }

        public async Task<bool> Eliminar(int partidaId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .AsNoTracking()
                .Where(p => p.PartidaId == partidaId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .Include(p => p.Ganador)
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.TurnoJugador)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
