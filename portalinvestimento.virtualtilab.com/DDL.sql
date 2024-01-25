USE [FIAPPortalInvest]
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 05/01/2024 16:36:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Aplicacao]
DROP TABLE [dbo].[Investimento]
DROP TABLE [dbo].[TipoInvestimento]
DROP TABLE [dbo].[Usuario]
DROP TABLE [dbo].[Rentabilidade_Investimento]
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nome] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Senha] [varchar](100) NOT NULL, 
	[Tipo_Acesso] [smallint] NOT NULL, 
	[CPF] varchar(50) not null, 
	Codigo_Conta int NOT NULL, 
	Digito_Conta int NOT NULL, 
	Saldo_Carteira decimal(16,4) NOT NULL, 

	Deleted bit, 
	Slug varchar(100) null, 
	Publish_Date datetime null, 
	Last_Changed datetime null, 
	Status smallint null, 

	CONSTRAINT AK_Usuario_Email UNIQUE(Email)  ,
	CONSTRAINT AK_Usuario_Conta UNIQUE(Codigo_Conta,Digito_Conta)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TipoInvestimento]
(
	Id int not null primary key, 
	Tipo_Investimento varchar(50) not null, 
	Last_Changed datetime null, 
	Status smallint null 
)
GO

CREATE TABLE [dbo].[Investimento](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Id_Tipo_Investimento] int NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Codigo] [varchar](50) NOT NULL,
	[Descricao] [varchar](500) NOT NULL,
	Taxa_ADM decimal(16,4) NOT NULL, 
	Aporte_Minimo decimal(16,4) NOT NULL, 
	Rentabilidade_Ultimo_3meses  decimal(16,4) NOT NULL, 
	Rentabilidade_Ultimo_12meses decimal(16,4) NOT NULL, 
	Rentabilidade_Ultimo_24meses decimal(16,4) NOT NULL, 
	
	Deleted bit, 
	Slug varchar(100) null, 
	Publish_Date datetime null, 
	Last_Changed datetime null, 
	Status smallint null, 

	 FOREIGN KEY ([Id_Tipo_Investimento]) REFERENCES TipoInvestimento(Id)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Aplicacao](
	[Id_Usuario] [int] NOT NULL ,
	[Id_Investimento] [int] NOT NULL ,
	[Valor_Aplicacao] decimal(16,4) NOT NULL,
	[Data_Aplicacao] datetime NOT NULL,
	[Rentabilidade] decimal(16,4) not null,
	[Ultima_Rentabilidade_Calculada] datetime null,

	Deleted bit, 
	Slug varchar(100) null, 
	Publish_Date datetime null, 
	Last_Changed datetime null, 
	Status smallint null, 

	CONSTRAINT AK_Aplicacao_Investimento UNIQUE (Id_Usuario, Id_Investimento),
	FOREIGN KEY ([Id_Investimento]) REFERENCES Investimento(Id),
	FOREIGN KEY ([Id_Usuario]) REFERENCES Usuario(Id)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Rentabilidade_Investimento](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Id_Investimento] [int] NOT NULL ,
	[Valor_Rentabilidade] decimal(16,4) NOT NULL,
	[Data_Calculo] datetime NOT NULL,

	Deleted bit, 
	Slug varchar(100) null, 
	Publish_Date datetime null, 
	Last_Changed datetime null, 
	Status smallint null, 

	FOREIGN KEY ([Id_Investimento]) REFERENCES Investimento(Id),
	CONSTRAINT AK_Rentabilidade_Investimento UNIQUE ([Id_Investimento], [Data_Calculo]),
) ON [PRIMARY]
GO



