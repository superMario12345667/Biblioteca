
using global::Biblioteca.DTO;
using System.Net.Http.Headers;

namespace Biblioteca.Services
{
    public class PrestamoService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public PrestamoService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<PrestamoResponse>> GetPrestamos()
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o inválido");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<PrestamoResponse>>("api/prestamos");

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al obtener préstamos. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener préstamos.");
            }
        }
    }
}
