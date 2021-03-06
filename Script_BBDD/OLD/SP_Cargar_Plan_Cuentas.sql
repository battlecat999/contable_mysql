USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Plan_Cuentas]    Fecha de la secuencia de comandos: 10/02/2019 14:16:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Proc [dbo].[Carga_Plan_Cuentas]
@strID_Cuenta varchar(8) = '',
@intNivel smallint = Null,
@intAcepta_Movimientos smallint = Null

as

SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
FROM PlanCuenta 
WHERE IdCuenta = Case When @strID_Cuenta = '' then IdCuenta Else @strID_Cuenta End
and AceptaMovimiento = Case when @intAcepta_Movimientos is null Then AceptaMovimiento Else @intAcepta_Movimientos End
and Nivel = Case When @intNivel is Null then Nivel Else @intNivel End
ORDER BY IdCuenta


