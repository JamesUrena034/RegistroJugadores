using System.Net.Http.Json;
using RegistroJugadores.Models.ApiDtos;
using RegistroJugadores.Shared;

namespace RegistroJugadoresServer.Services;

public interface IMovimientosApiService
{
    Task<Resource<List<MovimientosResponse>>> GetMovimientosAsync(int partidaId);
    Task<Resource<bool>> PostMovimientoAsync(MovimientosRequest movimiento);
}

public class MovimientosApiService : IMovimientosApiService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "https://gestionhuacalesapi.azurewebsites.net/api/";

    public MovimientosApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Resource<List<MovimientosResponse>>> GetMovimientosAsync(int partidaId)
    {
        try
        {
            var movimientos = await _http.GetFromJsonAsync<List<MovimientosResponse>>($"{BaseUrl}Movimientos/{partidaId}");
            return new Resource<List<MovimientosResponse>>.Success(movimientos ?? new());
        }
        catch (Exception ex)
        {
            return new Resource<List<MovimientosResponse>>.Error($"Error al obtener movimientos: {ex.Message}");
        }
    }

    public async Task<Resource<bool>> PostMovimientoAsync(MovimientosRequest movimiento)
    {
        try
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}Movimientos", movimiento);
            if (!response.IsSuccessStatusCode)
                return new Resource<bool>.Error($"Error HTTP {response.StatusCode}");

            return new Resource<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return new Resource<bool>.Error($"Error al registrar movimiento: {ex.Message}");
        }
    }
}
