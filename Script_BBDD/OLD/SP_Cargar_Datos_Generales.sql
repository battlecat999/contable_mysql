USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Datos_Generales]    Fecha de la secuencia de comandos: 10/02/2019 14:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Datos_Generales]
@intEmpresa smallint = Null

as

select Empresa ID_Empresa, FInicioEjercicio Fecha_Inicio_Ejercicio, fCierreEjercicio Fecha_Cierre_Ejercicio, 
FInicioEjercicioAnt Fecha_Inicio_Ejercicio_Anterior, FCierreEjercicioAnt Fecha_Cierre_Ejercicio_Anterior 
From DatosGenerales
Where Empresa = Case When @intEmpresa is null then Empresa Else @intEmpresa End 




