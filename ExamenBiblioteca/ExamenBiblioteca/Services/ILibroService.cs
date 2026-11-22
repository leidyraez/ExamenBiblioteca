using ExamenBiblioteca.Models;

namespace ExamenBiblioteca.Services
{
    public interface ILibroService
    {
        IEnumerable<Libro> ListarLibros();
        IEnumerable<Libro> ListarLibrosPorAutor(string autor);
        Libro? ObtenerPorId(int id);
        void RegistrarLibro(Libro libro);
        void ActualizarLibro(Libro libro);
        void EliminarLibro(int id);
    }
}
