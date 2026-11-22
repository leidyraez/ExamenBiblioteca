USE BDBIBLIOTECA2026;
GO

CREATE PROCEDURE sp_ListarLibros
AS
BEGIN
    SELECT Id, NombreLibro, Autor, NombreEditorial, ISBN FROM dbo.Libros;
END
GO

CREATE PROCEDURE sp_RegistrarLibro
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISBN VARCHAR(20)
AS
BEGIN
    INSERT INTO dbo.Libros (NombreLibro, Autor, NombreEditorial, ISBN)
    VALUES (@NombreLibro, @Autor, @NombreEditorial, @ISBN);
END
GO

CREATE PROCEDURE sp_ActualizarLibro
    @Id INT,
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISBN VARCHAR(20)
AS
BEGIN
    UPDATE dbo.Libros
    SET NombreLibro = @NombreLibro,
        Autor = @Autor,
        NombreEditorial = @NombreEditorial,
        ISBN = @ISBN
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_EliminarLibro
    @Id INT
AS
BEGIN
    DELETE FROM dbo.Libros WHERE Id = @Id;
END
GO