USE [Seipac]
GO
/****** Objeto:  Table [dbo].[Empresa]    Fecha de la secuencia de comandos: 09/16/2019 15:21:31 ******/
--DROP TABLE Empresa

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Empresa](
	[IdEmpresa] [tinyint] NOT NULL,
	[RazonSocial] [varchar](30) NOT NULL,
	[Domicilio] [varchar](100) NOT NULL,
	[Localidad] [varchar](30) NOT NULL,
	[Pais] [smallint] NOT NULL,
	[Provincia] [smallint] NOT NULL,
	[CUIT] [varchar](13) NOT NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY NONCLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF