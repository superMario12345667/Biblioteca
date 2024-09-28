using Biblioteca.DTO;
using System.Net.Http.Headers;

namespace Biblioteca.Services
{
    public class RolesUsuarioService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public RolesUsuarioService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<RolesUsuarioResponse>> GetRolesUsuarios()
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o inválido");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<RolesUsuarioResponse>>("api/rolesusuarios");

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al obtener roles de usuario. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener roles de usuario.");
            }
        }
    }
}
