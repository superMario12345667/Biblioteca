using Biblioteca.DTO;
using System.Net.Http.Headers;

namespace Biblioteca.Services
{
    public class EstadoPrestamoService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public EstadoPrestamoService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<EstadoPrestamoResponse>> GetEstadoPrestamos()
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o inválido");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<EstadoPrestamoResponse>>("api/estadoprestamos");

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al obtener estados de préstamos. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener estados de préstamos.");
            }
        }
    }
}
