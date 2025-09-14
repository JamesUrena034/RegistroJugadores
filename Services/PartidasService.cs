using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroJugadores.DAL;
using RegistroJugadores.Models;

namespace RegistroJugadores.Services
{
    public class PartidasService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Partidas partida)
        {
            if(partida.Jugador1Id == partida.Jugador2Id)
                throw new ArgumentException("Jugador1Id y Jugador2Id no pueden ser iguales ");

            if (!await Existe(partida.PartidasId))
                return await Insertar(partida);
            else
                return await Modificar(partida);
        }

        public async Task<bool> Existe(int PartidasId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas.AnyAsync(p => p.PartidasId == PartidasId);
        }

        private async Task<bool> Insertar(Partidas partida)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Partidas.Add(partida);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Partidas partida)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            contexto.Update(partida);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Partidas?> Buscar(int PartidasId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .Include(p => p.Ganador)
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.TurnoJugador)
                .FirstOrDefaultAsync(p => p.PartidasId == PartidasId);
        }

        public async Task<bool> Eliminar(int PartidasId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .AsNoTracking()
                .Where(p => p.PartidasId == PartidasId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
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
