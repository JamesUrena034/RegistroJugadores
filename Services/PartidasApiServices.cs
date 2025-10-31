using System.Net.Http.Json;
using RegistroJugadores.Models.ApiDtos;
using RegistroJugadores.Shared;

namespace RegistroJugadoresServer.Services;

public interface IPartidasApiService
{
    Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId);
    Task<Resource<PartidaResponse>> UnirsePorRolAsync(int partidaId, int jugadorId, string rol);
}

public class PartidasApiService : IPartidasApiService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "https://gestionhuacalesapi.azurewebsites.net/api/";

    public PartidasApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId)
    {
        try
        {
            var partida = await _http.GetFromJsonAsync<PartidaResponse>($"{BaseUrl}Partidas/{partidaId}");
            return partida != null
                ? new Resource<PartidaResponse>.Success(partida)
                : new Resource<PartidaResponse>.Error("No se encontró la partida.");
        }
        catch (HttpRequestException ex)
        {
            return new Resource<PartidaResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new Resource<PartidaResponse>.Error($"Error inesperado: {ex.Message}");
        }
    }

    public async Task<Resource<PartidaResponse>> UnirsePorRolAsync(int partidaId, int jugadorId, string rol)
    {
        try
        {
            var partidaResult = await GetPartidaAsync(partidaId);
            if (partidaResult is Resource<PartidaResponse>.Error err)
                return new Resource<PartidaResponse>.Error(err.Message ?? "No se pudo obtener la partida.");

            var partida = (partidaResult as Resource<PartidaResponse>.Success)?.Data;
            if (partida == null)
                return new Resource<PartidaResponse>.Error("No se encontró la partida.");

            if (rol == "X" && partida.Jugador1Id != 0)
                return new Resource<PartidaResponse>.Error("El jugador X ya está ocupado.");
            if (rol == "O" && partida.Jugador2Id != null)
                return new Resource<PartidaResponse>.Error("El jugador O ya está ocupado.");

            var body = new
            {
                partidaId = partidaId,
                jugador1Id = rol == "X" ? jugadorId : partida.Jugador1Id,
                jugador2Id = rol == "O" ? jugadorId : partida.Jugador2Id
            };

            var response = await _http.PutAsJsonAsync($"{BaseUrl}Partidas/{partidaId}", body);

            if (!response.IsSuccessStatusCode)
                return new Resource<PartidaResponse>.Error($"Error del servidor: {response.StatusCode}");

            // En caso de 204 o sin cuerpo, consideramos éxito
            PartidaResponse? updated = null;
            if (response.Content.Headers.ContentLength > 0)
                updated = await response.Content.ReadFromJsonAsync<PartidaResponse>();

            return new Resource<PartidaResponse>.Success(updated ?? new PartidaResponse
            {
                PartidaId = partidaId,
                Jugador1Id = rol == "X" ? jugadorId : partida.Jugador1Id,
                Jugador2Id = rol == "O" ? jugadorId : partida.Jugador2Id
            });
        }
        catch (HttpRequestException ex)
        {
            return new Resource<PartidaResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new Resource<PartidaResponse>.Error($"Error inesperado: {ex.Message}");
        }
    }
}
