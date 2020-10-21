USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Plan_Cuenta]    Fecha de la secuencia de comandos: 10/02/2019 14:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Carga_Plan_Cuenta]

as

SELECT IdCuenta, Descripcion 
FROM PlanCuenta 
WHERE AceptaMovimiento=1 
ORDER BY IdCuenta


