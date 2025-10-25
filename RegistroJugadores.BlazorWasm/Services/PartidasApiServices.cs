using System.Net.Http.Json;
using RegistroJugadores.BlazorWasm.Shared;
using RegistroJugadores.BlazorWasm.Models.ApiDtos;

namespace RegistroJugadores.Services;

public interface IPartidasApiService
{
    Task<Resource<List<PartidaResponse>>> GetPartidasAsync();
    Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId);
    Task<Resource<PartidaResponse>> PostPartida(int jugador1, int? jugador2 = null);
    Task<Resource<PartidaResponse>> UnirseAPartidaAsync(int partidaId, int jugador1Id, int jugador2Id);
}

public class PartidasApiService(HttpClient httpClient) : IPartidasApiService
{
    private const string BaseUrl = "https://gestionhuacalesapi.azurewebsites.net/api/";

    public async Task<Resource<List<PartidaResponse>>> GetPartidasAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<PartidaResponse>>($"{BaseUrl}Partidas");
            return new Resource<List<PartidaResponse>>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<List<PartidaResponse>>.Error(ex.Message);
        }
    }

    public async Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<PartidaResponse>($"{BaseUrl}Partidas/{partidaId}");
            if (response == null)
                return new Resource<PartidaResponse>.Error("Partida no encontrada.");
            return new Resource<PartidaResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return new Resource<PartidaResponse>.Error(ex.Message);
        }
    }

    public async Task<Resource<PartidaResponse>> PostPartida(int jugador1, int? jugador2 = null)
    {
        var request = new PartidaRequest(jugador1, jugador2);
        try
        {
            var response = await httpClient.PostAsJsonAsync($"{BaseUrl}Partidas", request);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<PartidaResponse>();
            if (created == null)
                return new Resource<PartidaResponse>.Error("Error al procesar la respuesta del servidor.");
            return new Resource<PartidaResponse>.Success(created);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<PartidaResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<PartidaResponse>.Error("Respuesta inválida del servidor.");
        }
    }

    public async Task<Resource<PartidaResponse>> UnirseAPartidaAsync(int partidaId, int jugador1Id, int jugador2Id)
    {
        try
        {
            var body = new
            {
                partidaId = partidaId,
                jugador1Id = jugador1Id,
                jugador2Id = jugador2Id
            };

            var response = await httpClient.PutAsJsonAsync($"{BaseUrl}Partidas/{partidaId}", body);
            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentLength == 0)
                return new Resource<PartidaResponse>.Success(new PartidaResponse
                {
                    PartidaId = partidaId,
                    Jugador1Id = jugador1Id,
                    Jugador2Id = jugador2Id
                });

            var updated = await response.Content.ReadFromJsonAsync<PartidaResponse>();
            return new Resource<PartidaResponse>.Success(updated!);
        }
        catch (Exception ex)
        {
            return new Resource<PartidaResponse>.Error($"Error al unirse a la partida: {ex.Message}");
        }
    }

}
