namespace Biblioteca.DTO
{
	
		public class UsuarioSession
		{

			public string NombreUsuario { get; set; } = null!;

			public string Contraseña { get; set; } = null!;

		}


        public class UsuarioResponse
    {
             public int IdUsuario { get; set; }

            public string NombreUsuario { get; set; } = null!;

         public int IdRol { get; set; }
    }
}

