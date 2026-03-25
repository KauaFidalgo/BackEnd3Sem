CREATE DATABASE ConnectPlus

GO
USE ConnectPlus

CREATE TABLE TipoContato (
    IdTipoContato INT PRIMARY KEY IDENTITY,
    Titulo VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Contato (
    IdContato INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(150) NOT NULL,
    FormaContato VARCHAR(150) NOT NULL,
    CaminhoImagem VARCHAR(255),
    IdTipoContato INT NOT NULL,

    FOREIGN KEY (IdTipoContato)
    REFERENCES TipoContato(IdTipoContato)
);
GO