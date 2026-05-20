CREATE DATABASE RoyalGames
GO

USE RoyalGames
GO

CREATE TABLE Usuario (
	UsuarioID INT PRIMARY KEY IDENTITY, 
	Nome VARCHAR(60) NOT NULL,
	Email VARCHAR (150) UNIQUE NOT NULL,
	Senha VARBINARY(32) NOT NULL,
	StatusUsuario BIT DEFAULT 1
);
GO 

CREATE TABLE Jogo (
	JogoID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR (100) UNIQUE NOT NULL,
	Preco DECIMAL (10,2) NOT NULL,
	Descricao NVARCHAR (MAX) NOT NULL,
	Imagem VARBINARY (MAX) NOT NULL,
	StatusJogo BIT DEFAULT 1,
	UsuarioID INT FOREIGN KEY REFERENCES Usuario (UsuarioID)
);
GO

CREATE TABLE Genero(
	GeneroID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR (50) NOT NULL
);
GO

CREATE TABLE ClassificacaoIndicativa(
	ClassificacaoIndicativaID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR (50) NOT NULL
);
GO

CREATE TABLE Plataforma(
	PlataformaID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR (50) NOT NULL
);
GO

CREATE TABLE JogoGenero(
	JogoID INT NOT NULL,
	GeneroID INT NOT NULL,	
	CONSTRAINT PK_JogoGenero PRIMARY KEY (JogoID, GeneroID),
	CONSTRAINT FK_JogoGenero_Jogo FOREIGN KEY (JogoID) 
		REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoGenero_Genero FOREIGN KEY (GeneroID) 
		REFERENCES Genero(GeneroID) ON DELETE CASCADE
);
GO

CREATE TABLE JogoPlataforma(
	JogoID INT NOT NULL,
	PlataformaID INT NOT NULL,	
	CONSTRAINT PK_JogoPlataforma PRIMARY KEY (JogoID, PlataformaID),
	CONSTRAINT FK_JogoPlataforma_Jogo FOREIGN KEY (JogoID) 
		REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoPlataforma_Plataforma FOREIGN KEY (PlataformaID) 
		REFERENCES Plataforma(PlataformaID) ON DELETE CASCADE
);
GO

CREATE TABLE JogoClassificacaoIndicativa(
	JogoID INT NOT NULL,
	ClassificacaoIndicativaID INT NOT NULL,	
	CONSTRAINT PK_JogoClassificacaoIndicativa PRIMARY KEY (JogoID, ClassificacaoIndicativaID),
	CONSTRAINT FK_JogoClassificacaoIndicativa_Jogo FOREIGN KEY (JogoID) 
		REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoClassificacaoIndicativa_ClassificacaoIndicativa FOREIGN KEY (ClassificacaoIndicativaID) 
		REFERENCES ClassificacaoIndicativa(ClassificacaoIndicativaID) ON DELETE CASCADE
);
GO

CREATE TABLE Promocao(
	PromocaoID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100) NOT NULL,
	DataExpiracao DATETIME2(0) NOT NULL, 
	StatusPromocao BIT DEFAULT 1 NOT NULL
);
GO

CREATE TABLE JogoPromocao(
	PromocaoID INT NOT NULL,
	JogoID INT NOT NULL,
	PrecoAtual DECIMAL(10,2) NOT NULL,

	CONSTRAINT PK_JogoPromocao PRIMARY KEY (JogoID, PromocaoID),
	CONSTRAINT FK_JogoPromocao_Jogo FOREIGN KEY (JogoID)  
		REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoPromocao_Promocao  FOREIGN KEY (PromocaoID) 
		REFERENCES Promocao(PromocaoID) ON DELETE CASCADE
);
GO

CREATE TABLE Log_AlteracaoJogo(
	Log_AlteracaoJogoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME2(0) NOT NULL,
	NomeAnterior VARCHAR(100),
	PrecoAnterior DECIMAL(10, 2),

	JogoID INT FOREIGN KEY REFERENCES Jogo(JogoID)
);
GO
	CREATE TRIGGER trg_ExclusaoUsuario
	ON Usuario
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE a SET StatusUsuario = 0
		FROM Usuario a
		INNER JOIN deleted d 
			ON d.UsuarioID = a.UsuarioID;
	END
	GO

	CREATE TRIGGER trg_AlteracaoJogo
	ON Jogo
	AFTER UPDATE
	AS
	BEGIN
		INSERT INTO Log_AlteracaoJogo(DataAlteracao, JogoID, NomeAnterior, PrecoAnterior)
		SELECT GETDATE(), JogoID, Nome, Preco FROM deleted 
	END
	GO

	CREATE TRIGGER trg_ExclusaoJogo
	ON Jogo
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE p SET StatusJogo = 0
		FROM Jogo p
		INNER JOIN deleted d 
			ON d.JogoID = p.JogoID;
	END
	GO

--- DML
INSERT INTO Usuario (Nome, Email, Senha)
	VALUES 
	('Carlos Lima', 'carlos@vhburguer.com', HASHBYTES('SHA2_256', 'admin@123'));
GO



INSERT INTO Genero (Nome)
VALUES
('Ação'),
('RPG'),
('Multiplayer');

GO

INSERT INTO Jogo (Nome, Preco, Descricao, Imagem, UsuarioID)
VALUES
('Shadow Blade', 59.90, 'Jogo de a��o com combates r�pidos e ambienta��o sombria.', CONVERT(VARBINARY(MAX), 'imagem aleatoria'), 1),
('Legends of Etheria', 79.90, 'RPG de mundo aberto com hist�ria �pica e escolhas que afetam o final.', CONVERT(VARBINARY(MAX), 'imagem aleatoria'), 1),
('Pixel Racers Online', 39.90, 'Jogo de corrida arcade com modo multiplayer online.', CONVERT(VARBINARY(MAX), 'imagem aleatoria'), 1);

GO

SELECT * FROM Jogo;

INSERT INTO JogoGenero (JogoID, GeneroID)
VALUES
(1, 1), 
(2, 2), 
(3, 3), 
(2, 1), 
(3, 1); 

GO

INSERT INTO Promocao (Nome, DataExpiracao)
VALUES
('Promo��o Semana Gamer', '2026-03-01 23:59:59'),
('Oferta Multiplayer Madness', '2026-02-20 23:59:59');

GO

INSERT INTO JogoPromocao (JogoID, PromocaoID, PrecoAtual)
VALUES
(1, 1, 44.90), 
(2, 1, 59.90), 
(3, 2, 19.90); 

GO
