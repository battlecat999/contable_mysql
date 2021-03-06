/****** Object:  StoredProcedure [dbo].[Actualiza_Plan_Cuentas]    Script Date: 2/8/2019 15:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Actualiza_Plan_Cuentas]
@strID_Cuenta varchar(8),
@strDescripcion varchar(50),
@intNivel smallint,
@intAcepta_Movimientos smallint

as

if exists (Select 1 From PlanCuenta Where idCuenta = @strID_Cuenta) 
	Begin
		UPDATE PlanCuenta 
		Set Descripcion = @strDescripcion,
		Nivel = @intNivel,
		AceptaMovimiento = @intAcepta_Movimientos 
		Where IdCuenta = @strID_Cuenta 
	End

GO
/****** Object:  StoredProcedure [dbo].[Alta_Plan_Cuentas]    Script Date: 2/8/2019 15:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Alta_Plan_Cuentas]
@strID_Cuenta varchar(8),
@strDescripcion varchar(50),
@intNivel smallint,
@intAcepta_Movimientos smallint

as

if exists (Select 1 From PlanCuenta Where IdCuenta = @strID_Cuenta) 
	Begin

		raiserror('El Plan de Cuenta ya existe', 20, -1) with log

	End

Else 

	Begin 

		Insert Into PlanCuenta 
		(IdCuenta, Descripcion, Nivel, AceptaMovimiento)
		Values
		(@strID_Cuenta, @strDescripcion, @intNivel, @intAcepta_Movimientos)

	End

GO
/****** Object:  StoredProcedure [dbo].[Carga_Plan_Cuentas]    Script Date: 2/8/2019 15:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [dbo].[Carga_Plan_Cuentas]
@strID_Cuenta varchar(8) = '',
@intNivel smallint = Null,
@intAcepta_Movimientos smallint = 1

as

SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento,
rtrim(ltrim(idCuenta)) + ' ' + rtrim(ltrim(Descripcion)) Descripcion_Ampliada
FROM PlanCuenta 
WHERE IdCuenta = Case When @strID_Cuenta = '' then IdCuenta Else @strID_Cuenta End
and AceptaMovimiento = Case when @intAcepta_Movimientos is null Then AceptaMovimiento Else @intAcepta_Movimientos End
and Nivel = Case When @intNivel is Null then Nivel Else @intNivel End
ORDER BY IdCuenta

GO
/****** Object:  StoredProcedure [dbo].[Elimina_Plan_Cuentas]    Script Date: 2/8/2019 15:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Elimina_Plan_Cuentas]
@strID_Cuenta varchar(8)

as

Delete PlanCuenta where IdCuenta = @strID_Cuenta 
GO
