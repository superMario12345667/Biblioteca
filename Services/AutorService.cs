using Biblioteca.DTO;
using System.Net.Http.Headers;
namespace Biblioteca.Services
{
    public class AutorService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public AutorService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public async Task<List<AutorResponse>> GetAutores()
        {

            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<AutorResponse>>("api/autores");

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener autores. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener autores.");
            }
        }
    }
}
