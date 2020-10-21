
--Listado
/*

EXEC [Carga_Listado_Mayor_General] 1,'20180101','20190930'
Carga_Listado_Mayor_General @intEmpresa = 1, @strFecha_Desde = '20190101', @strFecha_Hasta = '20190929'

*/
alter PROCEDURE [dbo].[Carga_Listado_Mayor_General]
(
@intEmpresa int,
/*
@FechaDesde as smalldatetime=null,
@FechaHasta as smalldatetime=null
*/
@strFecha_Desde char(8) = '',
@strFecha_Hasta char(8) = ''

)
AS

Declare
@datFecha_Inicio_Ejercicio smalldatetime,
@datFecha_Desde smalldatetime,
@datFecha_Hasta smalldatetime

Set @datFecha_Desde = null


if (rtrim(ltrim(@strFecha_Desde)) <> '')
	Begin
		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
	End

Select @datFecha_Inicio_Ejercicio = fInicioEjercicio
From datosgenerales
Where Empresa = @intEmpresa

if @datFecha_Desde is null 
	Begin
		
select @datFecha_Desde = fInicioEjercicio, @datFecha_Hasta = FCierreEjercicio 
		From datosgenerales
		Where Empresa = @intEmpresa
	End

SELECT a.FechaAsiento, 
a.IdAsiento As NroAsiento,
a.NIComprobante,
b.LeyendaItem,
ISNULL(b.Debe,0) as DEBE,
ISNULL(b.Haber,0)
 as HABER,
b.Cuenta,
c.Descripcion,
convert(char(6), cast(Year(a.FechaAsiento) as char(4)) + dbo.LPAD(Month(a.FechaAsiento), 2, '0')) Anio_Mes ,

isnull(Saldos.Saldo, 0) Saldo,
d.RazonSocial

FROM Asiento a Inner Join ItemAsiento b 
On a.Empresa = b.Empresa
AND a.Ano= b.Ano 
and a.idAsiento = b.Asiento 
--and a.idCuenta = b.Cuenta

Inner Join PlanCuenta c
On b.Cuenta = c.idCuenta 

Left Join (select a.Empresa, ia.cuenta, sum(isnull(ia.debe,0) - isnull(ia.haber,0)) as saldo --,convert(varchar(10),@FechaHasta,103)  as fecha 
from itemasiento as ia inner join asiento as a 
on ia.asiento = a.idasiento 
--where a.fechaasiento between @FechaDesde  and @FechaHasta 
where a.fechaasiento between @datFecha_Inicio_Ejercicio  and (@datFecha_Desde - 1)
and ia.cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados)  
and a.empresa=@intEmpresa  
and a.Estado<>'Anulado' 
group by a.Empresa, ia.cuenta
) as Saldos
On b.Cuenta = Saldos.Cuenta
and b.Empresa = Saldos.Empresa

Inner Join Empresa d
On a.Empresa = d.IdEmpresa

WHERE a.Empresa= @intEmpresa
AND a.Estado <> 'Anulado' 

--AND Asiento=IdAsiento 
--AND Asiento.Ano=ItemAsiento.Ano 
AND b.Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) 
--AND IdCuenta=ItemAsiento.Cuenta 
AND a.FechaAsiento BETWEEN @datFecha_Desde AND @datFecha_Hasta


--and Saldos.Cuenta =* ItemAsiento.Cuenta
--and Saldos.Empresa =* ItemAsiento.Empresa

ORDER BY b.Cuenta, a.FechaAsiento, cast(Year(a.FechaAsiento) as char(4)) + dbo.LPAD(Month(a.FechaAsiento), 2, '0'), a.IdAsiento



