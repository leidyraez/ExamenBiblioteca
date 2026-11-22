using System.Data;
using Microsoft.Data.SqlClient;
using ExamenBiblioteca.Models;

namespace ExamenBiblioteca.Services
{
    public class LibroService : ILibroService
    {
        private readonly string _cadenaConexion;

        public LibroService(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("ConexionBiblioteca")!;
        }

        public IEnumerable<Libro> ListarLibros()
        {
            var lista = new List<Libro>();

            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("sp_ListarLibros", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Libro
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            NombreLibro = dr["NombreLibro"].ToString()!,
                            Autor = dr["Autor"].ToString()!,
                            NombreEditorial = dr["NombreEditorial"].ToString()!,
                            ISBN = dr["ISBN"].ToString()!
                        });
                    }
                }
            }
            return lista;
        }

        public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
        {
            var lista = new List<Libro>();

            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("usp_ListarLibrosPorAutor", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Autor", autor ?? string.Empty);

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Libro
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            NombreLibro = dr["NombreLibro"].ToString()!,
                            Autor = dr["Autor"].ToString()!,
                            NombreEditorial = dr["NombreEditorial"].ToString()!,
                            ISBN = dr["ISBN"].ToString()!
                        });
                    }
                }
            }
            return lista;
        }

        public Libro? ObtenerPorId(int id)
        {
            return ListarLibros().FirstOrDefault(x => x.Id == id);
        }

        public void RegistrarLibro(Libro libro)
        {
            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("sp_RegistrarLibro", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreLibro", libro.NombreLibro);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@NombreEditorial", libro.NombreEditorial);
                cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarLibro(Libro libro)
        {
            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("sp_ActualizarLibro", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", libro.Id);
                cmd.Parameters.AddWithValue("@NombreLibro", libro.NombreLibro);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@NombreEditorial", libro.NombreEditorial);
                cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarLibro(int id)
        {
            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("sp_EliminarLibro", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}