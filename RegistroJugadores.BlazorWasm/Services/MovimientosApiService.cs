using System.Net.Http.Json;
using RegistroJugadores.BlazorWasm.Models.ApiDtos;
using RegistroJugadores.BlazorWasm.Shared;

namespace RegistroJugadores.Services;
public interface IMovimientosApiService
{
    Task<Resource<List<MovimientosResponse>>> GetMovimientosAsync(int partidaId);
    Task<Resource<bool>> PostMovimientoAsync(MovimientosRequest movimiento);
}
public class MovimientosApiService : IMovimientosApiService
{
    private readonly HttpClient _http;

    public MovimientosApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Resource<List<MovimientosResponse>>> GetMovimientosAsync(int partidaId)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<MovimientosResponse>>($"api/Movimientos/{partidaId}");
            return new Resource<List<MovimientosResponse>>.Success(data ?? new());
        }
        catch (Exception ex)
        {
            return new Resource<List<MovimientosResponse>>.Error(ex.Message);
        }
    }
    public async Task<Resource<bool>> PostMovimientoAsync(MovimientosRequest movimiento)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync("api/Movimientos", movimiento);
            return resp.IsSuccessStatusCode
                ? new Resource<bool>.Success(true)
                : new Resource<bool>.Error($"Error HTTP: {resp.StatusCode}");
        }
        catch (Exception ex)
        {
            return new Resource<bool>.Error(ex.Message);
        }
    }
}
