USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[CargaPaises]    Fecha de la secuencia de comandos: 09/16/2019 13:51:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREaATE PROCEDURE [dbo].[Carga_Paises]
AS

select idPais, NombrePais from pais