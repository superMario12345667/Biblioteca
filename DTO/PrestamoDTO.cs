namespace Biblioteca.DTO
{
    public class PrestamoResponse
    {
        public int IdPrestamo { get; set; }

        public int IdUsuario { get; set; }

        public int IdLibro { get; set; }

        public DateOnly FechaPrestamo { get; set; }

        public DateOnly FechaDevolucion { get; set; }

        public int IdEstadoPrestamo { get; set; }
    }
}
