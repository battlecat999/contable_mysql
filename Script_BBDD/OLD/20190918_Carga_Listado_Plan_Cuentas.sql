--Exec Carga_Listado_Plan_Cuentas @strID_Cuenta_Desde = '11110100'

Create Proc Carga_Listado_Plan_Cuentas 
@strID_Cuenta_Desde varchar(8) = '',
@strID_Cuenta_Hasta varchar(8) = ''

as

if ( rtrim(ltrim(@strID_Cuenta_Desde)) = '' and rtrim(ltrim(@strID_Cuenta_Hasta)) = '') 
	Begin
		SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
		FROM PlanCuenta 
		--WHERE IdCuenta = Case When @strID_Cuenta = '' then IdCuenta Else @strID_Cuenta End
		--and AceptaMovimiento = Case when @intAcepta_Movimientos is null Then AceptaMovimiento Else @intAcepta_Movimientos End
		--and Nivel = Case When @intNivel is Null then Nivel Else @intNivel End
		ORDER BY IdCuenta
	End
Else
	Begin

		if ( rtrim(ltrim(@strID_Cuenta_Desde)) <> '' and rtrim(ltrim(@strID_Cuenta_Hasta)) = '') 
			Begin
				SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
				FROM PlanCuenta 
				WHERE IdCuenta >= @strID_Cuenta_Desde
				ORDER BY IdCuenta
			End
		Else
			Begin

				if ( rtrim(ltrim(@strID_Cuenta_Desde)) = '' and rtrim(ltrim(@strID_Cuenta_Hasta)) <> '') 
					Begin
						SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
						FROM PlanCuenta 
						WHERE IdCuenta <= @strID_Cuenta_Hasta
						ORDER BY IdCuenta
					End
				Else
					Begin

						if ( rtrim(ltrim(@strID_Cuenta_Desde)) <> '' and rtrim(ltrim(@strID_Cuenta_Hasta)) <> '') 
							Begin
								SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
								FROM PlanCuenta 
								WHERE IdCuenta Between @strID_Cuenta_Desde and @strID_Cuenta_Hasta
								ORDER BY IdCuenta
							End

					End

			End

	End
