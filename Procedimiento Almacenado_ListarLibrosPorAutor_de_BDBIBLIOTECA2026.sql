USE BDBIBLIOTECA2026;
GO

CREATE PROCEDURE usp_ListarLibrosPorAutor
    @Autor VARCHAR(100)
AS
BEGIN
    SELECT Id, NombreLibro, Autor, NombreEditorial, ISBN
    FROM dbo.Libros
    WHERE Autor LIKE '%' + @Autor + '%';
END
GO