/****** Object:  StoredProcedure [dbo].[Carga_Balance_Sumas_y_Saldos]    Script Date: 13/9/2019 20:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

EXEC Carga_Balance_Sumas_y_Saldos 1,'20180701','20190731'

*/

ALTER PROCEDURE [dbo].[Carga_Balance_Sumas_y_Saldos]
(
@intEmpresa smallint,
@strFecha_Desde char(8) = '',
@strFecha_Hasta char(8) = ''
)

AS
set @strFecha_Desde=''
set @strFecha_Hasta=''

Declare
@datFecha_Desde as smalldatetime,
@datFecha_Hasta as smalldatetime

set @datFecha_Desde=null
set @datFecha_Hasta=null

if (rtrim(ltrim(@strFecha_Desde)) <> '')
	Begin
		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
	End

if @datFecha_Desde is null 
	Begin
		select @datFecha_Desde = fInicioEjercicio, @datFecha_Hasta = FCierreEjercicio 
		From datosgenerales
		Where Empresa = @intEmpresa
	End
declare @ultimo_asiento int

set @ultimo_asiento=(SELECT Max(IdAsiento) As UltimoAsiento FROM Asiento WHERE Empresa=@intEmpresa  AND Ano=(SELECT Max(Ano) FROM Asiento WHERE Empresa=@intEmpresa ) GROUP BY Ano)

SELECT (IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,
Cuenta,Descripcion,
Sum( isnull(Debe,0) ) As Debitos,
Sum( isnull(Haber, 0) ) as Creditos,
CASE WHEN (SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) )) > 0 THEN SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) ) ELSE 0 END as Saldo_Deudor,
CASE WHEN (SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) )) < 0 THEN -(SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) )) ELSE 0 END as Saldo_Acreedor,
SUBSTRING(Cuenta,1,2) as SubTotal,
SUBSTRING(Cuenta,1,1) as SubTotal_Cuenta,
@ultimo_asiento as Ultimo_Asiento,
Empresa.RazonSocial Razon_Social

FROM Asiento,ItemAsiento,PlanCuenta, Empresa
WHERE Asiento.Empresa = @intEmpresa 
AND Asiento.Estado<>'Anulado' 
AND ItemAsiento.Empresa=Asiento.Empresa 
AND Asiento=IdAsiento 
AND Asiento.Ano=ItemAsiento.Ano 
AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) 
AND IdCuenta=Cuenta 
AND Asiento.Empresa = Empresa.IdEmpresa 
AND FechaAsiento BETWEEN @datFecha_Desde AND @datFecha_Hasta 
GROUP BY Cuenta,Descripcion, Empresa.RazonSocial 
ORDER BY Cuenta


