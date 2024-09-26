using Biblioteca.DTO;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;

namespace Biblioteca.Services
{
	public class AuthService
	{
		private readonly ProtectedLocalStorage _LocalStorage;
		private readonly HttpClient _httpClient;
		private string? _token;

		public AuthService(ProtectedLocalStorage localStorage, HttpClient httpClient)
		{
			_LocalStorage = localStorage;
			_httpClient = httpClient;
		}

		//Enviar datos a endpoin
		public async Task<string> Login(UsuarioSession usuarioSession)
		{
			var response = await _httpClient.PostAsJsonAsync("api/usuarios/login", usuarioSession);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<string>();
				return result;
			}
			return null;
		}

		//Guardar Token en el navegador
	
		public async Task SetToken( string token)
		{
			_token = token;
			await _LocalStorage.SetAsync("token", token);
		}

		//Obtener token del navegador

		public async Task<string> GetToken()
		{
			var localStorageResult = await _LocalStorage.GetAsync<string>("token");

			if (string.IsNullOrEmpty(_token))
			{
				
				if (!localStorageResult.Success || string.IsNullOrEmpty(localStorageResult.Value))
				{
					_token = null;
					return null;
				}
				_token = localStorageResult.Value;
			}
			return _token;
		}

		//Verificar si el usuario  esta authenticado

		public async Task <bool> IsAuthenticated()
		{
			var token = await GetToken();

			return !string.IsNullOrEmpty(token) && !IsTokenExpire(token);
		}


		//Verificar si el token a expirado 
		 public bool IsTokenExpire(string token)
		{
			var jwtToken = new JwtSecurityToken(token);
			return jwtToken.ValidTo <DateTime.UtcNow;
		}

		// Cerrar Sesion

		public async Task logout()
		{
			_token = null;
			await _LocalStorage.DeleteAsync("token");
		}
		
	}
}