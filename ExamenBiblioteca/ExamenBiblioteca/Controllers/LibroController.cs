using Microsoft.AspNetCore.Mvc;
using ExamenBiblioteca.Models;
using ExamenBiblioteca.Services;

namespace ExamenBiblioteca.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;

        // Inyección de dependencias usando la INTERFAZ
        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        // Método IEnumerable exigido en el examen (Pregunta 1)
        public IEnumerable<Libro> ObtenerLibros()
        {
            return _libroService.ListarLibros();
        }

        // Método IEnumerable exigido en el examen (Pregunta 2)
        public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
        {
            return _libroService.ListarLibrosPorAutor(autor);
        }

        // Método IActionResult para consultar por autor y retornar resultados a la vista (Pregunta 2)
        public IActionResult BuscarPorAutor(string? autor)
        {
            ViewBag.AutorBuscado = autor;

            try
            {
                if (string.IsNullOrWhiteSpace(autor))
                {
                    return View(new List<Libro>());
                }

                var resultados = ListarLibrosPorAutor(autor);
                return View(resultados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al buscar libros por autor: " + ex.Message;
                return View(new List<Libro>());
            }
        }

        public IActionResult Index(string? mensaje = null, string? error = null)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;

            try
            {
                var libros = ObtenerLibros();
                return View(libros);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar los libros: " + ex.Message;
                return View(new List<Libro>());
            }
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View(libro);
            }

            try
            {
                _libroService.RegistrarLibro(libro);
                return RedirectToAction(nameof(Index), new { mensaje = "Libro registrado correctamente." });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudo registrar el libro: " + ex.Message;
                return View(libro);
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var libro = _libroService.ObtenerPorId(id);
            if (libro == null)
            {
                return RedirectToAction(nameof(Index), new { error = "El libro no existe." });
            }
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View(libro);
            }

            try
            {
                _libroService.ActualizarLibro(libro);
                return RedirectToAction(nameof(Index), new { mensaje = "Libro actualizado correctamente." });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudo actualizar el libro: " + ex.Message;
                return View(libro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _libroService.EliminarLibro(id);
                return RedirectToAction(nameof(Index), new { mensaje = "Libro eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index), new { error = "No se pudo eliminar el libro: " + ex.Message });
            }
        }
    }
}