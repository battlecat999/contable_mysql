USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Asientos]    Fecha de la secuencia de comandos: 10/02/2019 14:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Asientos]
@intEmpresa tinyint,
@strLista_Cuentas nvarchar(3999),
@datFecha_Desde smalldatetime,
@datFecha_Hasta smalldatetime

as

Declare
@strSql nvarchar(max),
@strFecha_Desde char(10),
@strFecha_Hasta char(10)

Set @strFecha_Desde = convert(char(16), convert(datetime, @datFecha_Desde) , 121)
Set @strFecha_Hasta = convert(char(16), convert(datetime, @datFecha_Hasta) , 121)

Set @strSql = ''
Set @strSql = 'SELECT a.Empresa, a.FechaAsiento, a.IdAsiento As NroAsiento, isnull(a.NIComprobante, 0) NIComprobante, b.LeyendaItem, isnull(b.Debe, 0) Debe, isnull(b.Haber, 0) Haber, 
b.Cuenta, c.Descripcion 

FROM Asiento a Inner Join ItemAsiento b   
On a.Empresa = b.Empresa 
AND a.Ano = b.Ano 
and a.iDAsiento = b.Asiento

Inner Join PlanCuenta c
On b.Cuenta = c.idCuenta

WHERE a.Empresa= 2
AND rtrim(ltrim(a.Estado)) <> ''Anulado ''

--AND Cuenta IN (''' + @strLista_Cuentas + ''') 
AND a.FechaAsiento BETWEEN ''' + @strFecha_Desde + ''' AND ''' + @strFecha_Hasta + ''' 

ORDER BY b.Cuenta, a.FechaAsiento, a.IdAsiento '

Exec sp_executesql @strSql

--print @strSql 

/*
--select * from Empresa 
select top 10 * from asiento
select top 10 * from ItemAsiento 
select top 1 * from plancuenta 
*/

--sp_help asiento
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Combo_Empresas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Carga_Combo_Empresas]

as

select IdEmpresa, RazonSocial 
From Empresa
Order By RazonSocial
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Estados_Asientos]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Carga_Estados_Asientos]

as

Create Table #tmp
(
Descripcion varchar(15),
Orden tinyint
)

Insert Into #tmp (Descripcion, Orden) Values ('GENERADO', 1);
Insert Into #tmp (Descripcion, Orden) Values ('PENDIENTE', 2);
Insert Into #tmp (Descripcion, Orden) Values ('ANULADO', 3);


Select Descripcion
From #tmp
Order By Orden
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Estados_Comprobante_Despachante]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Estados_Comprobante_Despachante]

as

Create Table #tmp
(
Descripcion varchar(15)
)

Insert Into #tmp (Descripcion) Values ('PENDIENTE');
Insert Into #tmp (Descripcion) Values ('ANULADA');
Insert Into #tmp (Descripcion) Values ('CUMPLIDA');

Select Descripcion
From #tmp
Order By Descripcion desc
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Letras_Comprobantes]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Letras_Comprobantes]

as

Create Table #tmp
(
Letra_Comprobante char(2)
)

Insert Into #tmp (Letra_Comprobante) Values ('A');
Insert Into #tmp (Letra_Comprobante) Values ('B');
Insert Into #tmp (Letra_Comprobante) Values ('C');
Insert Into #tmp (Letra_Comprobante) Values ('M');
Insert Into #tmp (Letra_Comprobante) Values ('X');

Select Letra_Comprobante 
From #tmp 
Order By Letra_Comprobante
GO
/****** Objeto:  StoredProcedure [dbo].[sgm_Carga_Tipos_Comprobantes_CompraBU]    Fecha de la secuencia de comandos: 10/02/2019 14:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[sgm_Carga_Tipos_Comprobantes_CompraBU]

as

Create Table #tmp
(
Tipo_Comprobante char(2)
)

Insert Into #tmp (Tipo_Comprobante) Values ('DA');
Insert Into #tmp (Tipo_Comprobante) Values ('FC');
Insert Into #tmp (Tipo_Comprobante) Values ('NC');
Insert Into #tmp (Tipo_Comprobante) Values ('ND');

Select Tipo_Comprobante
From #tmp
Order By Tipo_Comprobante 

/*
Select TipoComprobante Tipo_Comprobante
From CompraBU 
Group By TipoComprobante 
Order By TipoComprobante 
*/
GO
/****** Objeto:  StoredProcedure [dbo].[Alta_CompraBU]    Fecha de la secuencia de comandos: 10/02/2019 14:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Alta_CompraBU]
@intEmpresa smallint, 
@intIdCompraBU int = null,
@datFechaCompraBU datetime, 
@intProveedor int, 
@strTipoComprobante nvarchar(5), 
@strLetraComprobante nvarchar(2), 
@intCentro_Emision smallint,
@strNComprobante nvarchar(13), 
@strDetalleCompraBU nvarchar(90), 
@decImporteNeto decimal(15,2), 
@decIVA decimal(15,2), 
@decIngresosBrutos decimal(15,2),  
@decTotalPesos decimal(15,2), 
@strEstado nvarchar(255),
@intNRemito int = 0, 
@decIVA27 decimal(15,2) = 0, 
@decIVA105 decimal(15,2) = 0, 
@decResol3337 decimal(15,2) = 0, 
@decSaldoBU decimal(15,2) = 0, 
@intCondicionIVA smallint, 
@intCondicionGanancia smallint = 0, 
@datFechaVencimiento datetime,
@intBienUso smallint = 0, 
@strNCarpeta nvarchar(15),
@decGanancias decimal(15,2) = 0, 
@decDerechosAd decimal(15,2),  
@decIVA9 decimal(15,2) = 0, 
@decOtros decimal(15,2) = 0, 
@decCotizacion decimal(15,2),
@strCAI nvarchar(14), 
@datfeCAI datetime, 
@datfeComp datetime, 
@strAduana nvarchar(3) = '',
@strDestinacion nvarchar(4) = '',
@strDespacho nvarchar(50), 
@strVerifica nvarchar(1),
@strMoneda nvarchar(3), 
@intFiscal smallint = 0, 
@strcodOpe nvarchar(1) = '',
@decPercepcion_IVA decimal(15,2),
@strNombreProveedor varchar(100),
@strCUIT varchar(13)

as

Declare
@strNumero_Comprobante char(13)

Set @strNumero_Comprobante = ''
--Set @strNumero_Comprobante = str(@intCentro_Emision) + '-' + @strNComprobante

Set @strNumero_Comprobante = rtrim(ltrim(dbo.LPAD(@intCentro_Emision, 4, '0'))) + '-' + rtrim(ltrim(dbo.LPAD(rtrim(ltrim(@strNComprobante)), 8, '0')))

if (@intIdCompraBU is null)
	Begin
		Select @intIdCompraBU = Max(IdCompraBU) + 1 From CompraBU

		INSERT INTO CompraBU
		(
		Empresa, IdCompraBU, FechaCompraBU, Proveedor, TipoComprobante, LetraComprobante, NComprobante, DetalleCompraBU, ImporteNeto, IVA, IngresosBrutos, 
		TotalPesos, Estado, NRemito, IVA27, IVA105, Resol3337, SaldoBU, CondicionIVA, CondicionGanancia, FechaVencimiento, BienUso, NCarpeta, 
		Ganancias, DerechosAd, IVA9, Otros, Cotizacion, CAI, feCAI, feComp, Aduana, Destinacion, Despacho, Verifica, Moneda, Fiscal, codOpe, nombreProveedor, CUIT, Percepcion_IVA
		)
		VALUES
		(
		@intEmpresa, @intIdCompraBU, @datFechaCompraBU, @intProveedor, @strTipoComprobante, @strLetraComprobante, @strNumero_Comprobante, @strDetalleCompraBU, @decImporteNeto, @decIVA, @decIngresosBrutos,
		@decTotalPesos, @strEstado, @intNRemito, @decIVA27, @decIVA105, @decResol3337, @decSaldoBU, @intCondicionIVA, @intCondicionGanancia, @datFechaVencimiento, @intBienUso, @strNCarpeta,
		@decGanancias, @decDerechosAd, @decIVA9, @decOtros, @decCotizacion, @strCAI, @datfeCAI, @datfeComp, @strAduana, @strDestinacion, @strDespacho, @strVerifica, @strMoneda, @intFiscal, @strcodOpe,
		@strNombreProveedor, @strCUIT, @decPercepcion_IVA
		)
	End
Else
	Begin
		
		UPDATE CompraBU

		Set FechaCompraBU = @datFechaCompraBU, 
			Proveedor = @intProveedor, 
			TipoComprobante = @strTipoComprobante,  
			LetraComprobante = @strLetraComprobante,  
			NComprobante = @strNumero_Comprobante, 
			DetalleCompraBU = @strDetalleCompraBU,  
			ImporteNeto = @decImporteNeto,  
			IVA =  @decIVA, 
			IngresosBrutos =  @decIngresosBrutos,
			TotalPesos = @decTotalPesos,
			Estado = @strEstado,
			NRemito = @intNRemito,  
			IVA27 = @decIVA27, 
			IVA105 = @decIVA105, 
			Resol3337 = @decResol3337, 
			SaldoBU = @decSaldoBU, 
			CondicionIVA = @intCondicionIVA, 
			CondicionGanancia = @intCondicionGanancia,  
			FechaVencimiento = @datFechaVencimiento,  
			BienUso = @intBienUso,  
			NCarpeta = @strNCarpeta, 
			Ganancias = @decGanancias,  
			DerechosAd = @decDerechosAd,  
			IVA9 = @decIVA9,  
			Otros = @decOtros,  
			Cotizacion = @decCotizacion,  
			CAI = @strCAI,  
			feCAI = @datfeCAI,  
			feComp = @datfeComp,  
			Aduana = @strAduana,  
			Destinacion = @strDestinacion,  
			Despacho = @strDespacho,  
			Verifica = @strVerifica,  
			Moneda = @strMoneda,  
			Fiscal = @intFiscal,  
			codOpe = @strcodOpe,
			Percepcion_IVA = @decPercepcion_IVA

			WHERE Empresa = @intEmpresa
			And IdCompraBU = @intIdCompraBU 
		
	End
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Listado_Mayor_General]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Listado
/*

EXEC [Carga_Listado_Mayor_General] 1,'20180101','20190930'
Carga_Listado_Mayor_General @intEmpresa = 1, @strFecha_Desde = '20190101', @strFecha_Hasta = '20190929'

*/
CREATE PROCEDURE [dbo].[Carga_Listado_Mayor_General]
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
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Listado_Libro_Diario]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Carga_Listado_Libro_Diario @intEmpresa = 1, @datFecha_Desde = '20170701', @datFecha_Hasta = '20190408' 

--Carga_Listado_Libro_Diario @intEmpresa = 1, @strFecha_Desde = '20170701', @strFecha_Hasta = '20170731'

--Carga_Listado_Libro_Diario 1,'20170101','20170630', 1, 1000
CREATE proc [dbo].[Carga_Listado_Libro_Diario]
@intEmpresa smallint,
--@datFecha_Desde smalldatetime = null,
--@datFecha_Hasta smalldatetime = null,
@strFecha_Desde char(8) = '',
@strFecha_Hasta char(8) = '',

@intAsiento_Desde int = null,
@intAsiento_Hasta int = null,

@intOrden_Por_Asiento tinyint = 0


as

Declare
@datFecha_Desde smalldatetime,
@datFecha_Hasta smalldatetime,
@datFecha_Inicio_Ejercicio smalldatetime,
@datFecha_Cierre_Ejercicio smalldatetime

if (rtrim(ltrim(@strFecha_Desde)) <> '')
	Begin
		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
	End

Select @datFecha_Inicio_Ejercicio = fInicioEjercicio,
@datFecha_Cierre_Ejercicio = fCierreEjercicio
From datosgenerales
Where Empresa = @intEmpresa


if (rtrim(ltrim(@strFecha_Desde)) <> '')
	Begin 
       SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta, isnull(Debe, 0) Debe, isnull(Haber, 0) Haber, LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante,
	   Empresa.RazonSocial Desc_Empresa

	   FROM Asiento, ItemAsiento, Empresa
	   
	   WHERE Asiento.Estado<>'Anulado' 
	   AND Asiento.Empresa = Empresa.IdEmpresa
	   AND Asiento.Empresa = @intEmpresa 
	   AND ItemAsiento.Empresa = Asiento.Empresa 
	   AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano 
	  AND FechaAsiento BETWEEN @datFecha_Desde AND @datFecha_Hasta 
	
	   ORDER BY Asiento.FechaAsiento,IdAsiento
	End

Else

	Begin
	
		if (@intOrden_Por_Asiento = 1)
			Begin

				if (@intAsiento_Desde is not null and @intAsiento_Hasta is not null)
					Begin
						SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
						FROM Asiento,ItemAsiento 
						WHERE Asiento.Estado <> 'Anulado' 
						AND Asiento.Empresa = @intEmpresa 
						AND ItemAsiento.Empresa=Asiento.Empresa 
						AND Asiento=IdAsiento 
						AND Asiento.Ano=ItemAsiento.Ano 
						AND IdAsiento BETWEEN @intAsiento_Desde AND @intAsiento_Hasta 
						ORDER BY Asiento.FechaAsiento,IdAsiento
					End

				Else
					
					Begin 

						if (@intAsiento_Desde is not null and @intAsiento_Hasta is null)
							Begin
								SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
								FROM Asiento,ItemAsiento 
								WHERE Asiento.Estado <> 'Anulado' 
								AND Asiento.Empresa = @intEmpresa  
								AND ItemAsiento.Empresa=Asiento.Empresa 
								AND Asiento=IdAsiento 
								AND Asiento.Ano=ItemAsiento.Ano 
								AND IdAsiento >= @intAsiento_Desde 
								AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
								ORDER BY Asiento.FechaAsiento,IdAsiento
							End

						Else

							Begin

								if (@intAsiento_Desde is null and @intAsiento_Hasta is not null)
									Begin
										SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
										FROM Asiento,ItemAsiento 
										WHERE Asiento.Estado <> 'Anulado' 
										AND Asiento.Empresa = @intEmpresa  
										AND ItemAsiento.Empresa=Asiento.Empresa 
										AND Asiento=IdAsiento 
										AND Asiento.Ano=ItemAsiento.Ano 
										AND IdAsiento >= @intAsiento_Hasta 
										AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
										ORDER BY Asiento.FechaAsiento,IdAsiento
									End

								else

									Begin

										if (@intAsiento_Desde is null and @intAsiento_Hasta is null)
											Begin
												SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
												FROM Asiento,ItemAsiento 
												WHERE Asiento.Estado <> 'Anulado' 
												AND Asiento.Empresa = @intEmpresa 
												AND ItemAsiento.Empresa = Asiento.Empresa 
												AND Asiento=IdAsiento 
												AND Asiento.Ano=ItemAsiento.Ano 
												AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
												ORDER BY Asiento.FechaAsiento,IdAsiento
											End

										Else

											Begin

												if (@intAsiento_Desde is not null and @intAsiento_Hasta is null)
													Begin
														SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
														FROM Asiento,ItemAsiento 
														WHERE Asiento.Estado <> 'Anulado' 
														AND Asiento.Empresa = @intEmpresa  
														AND ItemAsiento.Empresa=Asiento.Empresa 
														AND Asiento=IdAsiento 
														AND Asiento.Ano=ItemAsiento.Ano 
														AND IdAsiento >= @intAsiento_Desde 
														AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
														ORDER BY Asiento.FechaAsiento,IdAsiento
													End

											End

									End

							End

					End
			
			End
		
		Else
			
			Begin 

				if (@intAsiento_Desde is not null and @intAsiento_Hasta is not null)
					Begin
						SELECT NInternoAsiento As NroAsiento, ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
						FROM Asiento,ItemAsiento 
						WHERE Asiento.Estado <> 'Anulado' 
						AND Asiento.Empresa = @intEmpresa 
						AND ItemAsiento.Empresa=Asiento.Empresa 
						AND Asiento=IdAsiento 
						AND Asiento.Ano=ItemAsiento.Ano 
						AND NInternoAsiento Is Not Null 
						AND NInternoAsiento BETWEEN @intAsiento_Desde AND @intAsiento_Hasta 
						ORDER BY Asiento.FechaAsiento,IdAsiento
					End			

				Else
					
					Begin 

						if (@intAsiento_Desde is not null and @intAsiento_Hasta is null)
							Begin
								SELECT NInternoAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
								FROM Asiento,ItemAsiento 
								WHERE Asiento.Estado <> 'Anulado' 
								AND Asiento.Empresa = @intEmpresa 
								AND ItemAsiento.Empresa=Asiento.Empresa 
								AND Asiento=IdAsiento 
								AND Asiento.Ano=ItemAsiento.Ano 
								AND NInternoAsiento Is Not Null 
								AND NInternoAsiento >= @intAsiento_Desde 
								AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
								ORDER BY Asiento.FechaAsiento,IdAsiento
							End

						Else

							Begin
								
								if (@intAsiento_Desde is null and @intAsiento_Hasta is not null)
									Begin
										SELECT NInternoAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
										FROM Asiento,ItemAsiento 
										WHERE Asiento.Estado <> 'Anulado' 
										AND Asiento.Empresa = @intEmpresa 
										AND ItemAsiento.Empresa=Asiento.Empresa 
										AND Asiento=IdAsiento 
										AND Asiento.Ano=ItemAsiento.Ano 
										AND NInternoAsiento Is Not Null 
										AND NInternoAsiento >= @intAsiento_Hasta
										AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
										ORDER BY Asiento.FechaAsiento,IdAsiento
									End

								Else

									Begin
										
										if (@intAsiento_Desde is null and @intAsiento_Hasta is null)
											Begin
												SELECT NInternoAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
												FROM Asiento,ItemAsiento 
												WHERE Asiento.Estado <> 'Anulado' 
												AND Asiento.Empresa = @intEmpresa 
												AND ItemAsiento.Empresa=Asiento.Empresa 
												AND Asiento=IdAsiento 
												AND Asiento.Ano=ItemAsiento.Ano 
												AND NInternoAsiento Is Not Null 
												AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
												ORDER BY Asiento.FechaAsiento,IdAsiento
											End

									End
							End

					End
			End 

	End
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Asiento]    Fecha de la secuencia de comandos: 10/02/2019 14:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Asiento]
@intEmpresa smallint = null,
@intAnio smallint = null,
@intID_Asiento int = null,
@strNIComprobante varchar(5) = ''

as

SELECT 
a.IdEmpresa, a.RazonSocial Razon_Social,
b.Ano Anio, b.IdAsiento, b.FechaAsiento Fecha_Asiento, b.LeyendaAsiento Leyenda_Asiento, isnull(b.NInternoAsiento, 0) NInternoAsiento, b.Estado, b.NIComprobante Numero_Comprobante
FROM Empresa a Inner Join Asiento b
On a.IdEmpresa = b.Empresa
WHERE a.IdEmpresa = Case When @intEmpresa is null then a.IdEmpresa Else @intEmpresa End 
AND b.Ano = Case When @intAnio is null then b.Ano Else @intAnio End
and b.IdAsiento = Case When @intID_Asiento is null then b.IdAsiento Else @intID_Asiento End
and b.NIComprobante = Case When rtrim(ltrim(@strNIComprobante)) = '' then b.NIComprobante Else rtrim(ltrim(@strNIComprobante)) End
ORDER BY b.IdAsiento DESC
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Item_Asiento]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec Carga_Item_Asiento @intEmpresa = 1, @intAnio = 2018, @intAsiento = 738, @strNumero_Comprobante = '04236'

--exec Carga_Item_Asiento @intEmpresa = 1, @intAnio = 2018, @strNumero_Comprobante = '04236'


CREATE Proc [dbo].[Carga_Item_Asiento]
@intEmpresa smallint = null,
@intAnio smallint = null,
@intAsiento smallint = null,
@strNumero_Comprobante varchar(5) = ''

as

select a.Empresa, a.Ano Anio, a.Asiento, a.ItemAsiento, a.Cuenta, a.LeyendaItem, isnull(a.Debe, 0) Debe, isnull(a.Haber, 0) Haber,
b.Descripcion Nombre,
isnull(c.NIComprobante, 0) NIComprobante,
rtrim(ltrim(c.Estado)) Estado

From itemasiento a Inner Join PlanCuenta b
On a.Cuenta = b.IdCuenta
Inner Join Asiento c

On a.Empresa = c.Empresa
and a.Ano = c.Ano
and a.Asiento = c.IdAsiento

Where a.Empresa = @intEmpresa
and a.Ano = @intAnio 
and a.Asiento = Case When @intAsiento is null then a.Asiento Else @intAsiento End
and rtrim(ltrim(c.NIComprobante)) = Case When rtrim(ltrim(@strNumero_Comprobante)) = '' then rtrim(ltrim(c.NIComprobante)) Else rtrim(ltrim(@strNumero_Comprobante)) End
Order By a.Empresa, a.Ano, a.Asiento, a.ItemAsiento


/*
SELECT ItemAsiento, Cuenta, Descripcion,LeyendaItem,Debe,Haber,Empresa,Cuenta,Ano FROM ItemAsiento INNER JOIN PlanCuenta ON ItemAsiento.Cuenta=PlanCuenta.IdCuenta WHERE Empresa=" & rsEmpresas![IdEmpresa] & " AND Asiento=" & cboAsiento.Text & " AND Ano="
 & lblAño.Caption
 */

/*
 select top 1 * from asiento where niComprobante = '04236'
 select top 1 * from itemasiento

*/
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Combo_Asientos_Desde]    Fecha de la secuencia de comandos: 10/02/2019 14:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Carga_Combo_Asientos_Desde @intEmpresa = 1, @intOrden_Por_Asiento = 0

CREATE Proc [dbo].[Carga_Combo_Asientos_Desde]
@intEmpresa smallint,
@intOrden_Por_Asiento tinyint = 0,
@intIdAsiento_Desde int = null

as

Declare
@datFecha_Inicio_Ejercicio smalldatetime,
@datFecha_Cierre_Ejercicio smalldatetime

Select @datFecha_Inicio_Ejercicio = fInicioEjercicio,
@datFecha_Cierre_Ejercicio = fCierreEjercicio
From datosgenerales
Where Empresa = @intEmpresa


If (@intOrden_Por_Asiento = 1)
	Begin
		
		if (@intIdAsiento_Desde is null)
			Begin
				SELECT IdAsiento
				FROM Asiento 
				WHERE Empresa = @intEmpresa 
				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
				ORDER BY IdAsiento
			End
		Else
			Begin
				SELECT IdAsiento 
				FROM Asiento 
				WHERE Empresa = @intEmpresa 
				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
				and IdAsiento <> @intIdAsiento_Desde
				ORDER BY IdAsiento
			End
	End
Else
	Begin

		if (@intIdAsiento_Desde is null)
			Begin
				SELECT NInternoAsiento IdAsiento--, *
				FROM Asiento 
				WHERE Empresa = @intEmpresa 
				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
				and NInternoAsiento is not null
				ORDER BY NInternoAsiento
			End
		Else
			Begin
				SELECT NInternoAsiento IdAsiento--, *
				FROM Asiento 
				WHERE Empresa = @intEmpresa 
				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
				and IdAsiento <> @intIdAsiento_Desde
				and NInternoAsiento is not null
				ORDER BY NInternoAsiento
			End
	End
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Balance_Sumas_y_Saldos]    Fecha de la secuencia de comandos: 10/02/2019 14:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

EXEC Carga_Balance_Sumas_y_Saldos 1,'20180701','20190731'

*/

CREATE PROCEDURE [dbo].[Carga_Balance_Sumas_y_Saldos]
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
GO
/****** Objeto:  StoredProcedure [dbo].[Alta_Asiento]    Fecha de la secuencia de comandos: 10/02/2019 14:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Alta_Asiento]
@intEmpresa tinyint,
@intAnio int,
@intAsiento int = null,
@datFecha_Asiento smalldatetime,
@strLeyenda_Asiento varchar(50) = '',
@intNumero_Interno_Asiento int = null,
@strEstado varchar(15) = '',
@strNumero_Comprobante varchar(5) = ''

as

Declare 
@intNumero_Asiento int 

if (@intAsiento is null)
	Begin

		If exists (Select 1 From Asiento Where Empresa = @intEmpresa And Ano = @intAnio)
			Begin 
				Select @intNumero_Asiento = Max(IdAsiento) + 1 From Asiento Where Empresa = @intEmpresa And Ano = @intAnio
			End
		Else
			Begin 
				Set @intNumero_Asiento = 1
			End

		Insert Into Asiento
		(Empresa, Ano, IdAsiento, FechaAsiento, LeyendaAsiento, NInternoAsiento, Estado, NIComprobante)
		Values
		(@intEmpresa, @intAnio, @intNumero_Asiento, @datFecha_Asiento, @strLeyenda_Asiento, null, @strEstado, @strNumero_Comprobante)

	End
Else
	Begin
		Set @intNumero_Asiento = @intAsiento

		update Asiento 
		Set FechaAsiento = @datFecha_Asiento,
		LeyendaAsiento = @strLeyenda_Asiento,
		Estado = @strEstado
		Where Empresa = @intEmpresa 
		And Ano = @intAnio
		and idAsiento = @intNumero_Asiento
	End
GO
/****** Objeto:  StoredProcedure [dbo].[Alta_ItemAsiento]    Fecha de la secuencia de comandos: 10/02/2019 14:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Alta_ItemAsiento]
@intEmpresa tinyint,
@intAnio int,
@intAsiento int = null,
--@intItem smallint,
@strCuenta varchar(8),
@strLeyenda_Item varchar(30),
@decDebe float,
@decHaber float,
@strNumero_Comprobante varchar(5) = ''

as

Declare
@intItem smallint,
@intNumero_Asiento int

If Not Exists (Select 1 From ItemAsiento Where Empresa = @intEmpresa and Ano = @intAnio and Asiento = @intAsiento)
	Begin
		Set @intItem = 1
	End
Else
	Begin
		Select @intItem = max(ItemAsiento) + 1
		From ItemAsiento 
		Where Empresa = @intEmpresa
		and Ano = @intAnio
		and Asiento = @intAsiento
	End

if (@intAsiento is null)
	Begin
		Select @intNumero_Asiento = IdAsiento From Asiento Where Empresa = @intEmpresa And Ano = @intAnio and rtrim(ltrim(NIComprobante)) = rtrim(ltrim(@strNumero_Comprobante))
	End
else
	Begin
		Set @intNumero_Asiento = @intAsiento
	End

Insert Into ItemAsiento
(Empresa, Ano, Asiento, ItemAsiento, Cuenta, LeyendaItem, Debe, Haber)
Values
(@intEmpresa, @intAnio, @intNumero_Asiento, @intItem, @strCuenta, @strLeyenda_Item, @decDebe, @decHaber)
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_CompraBU]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec Carga_CompraBU @intEmpresa = 1, @intIdCompraBU = 28667

CREATE Proc [dbo].[Carga_CompraBU]
@intEmpresa smallint, 
@intIdCompraBU int = null

as

If (@intIdCompraBU is Null)
	Begin
		Select 
		Empresa, IdCompraBU, FechaCompraBU, Proveedor, TipoComprobante, LetraComprobante, NComprobante, DetalleCompraBU, ImporteNeto, IVA, IngresosBrutos, 
		TotalPesos, Estado, NRemito, IVA27, IVA105, Resol3337, SaldoBU, CondicionIVA, CondicionGanancia, FechaVencimiento, BienUso, NCarpeta, 
		Ganancias, DerechosAd, IVA9, Otros, Cotizacion, CAI, feCAI, feComp, Aduana, Destinacion, Despacho, Verifica, Moneda, Fiscal, codOpe,cuit
		From CompraBU
		WHERE Empresa = @intEmpresa
		--And IdCompraBU = Case When @intIdCompraBU is null Then IdCompraBU Else @intIdCompraBU End
		And NCarpeta Is Not Null
		Order By Empresa, IdCompraBU Desc
	End
Else
	Begin
		Select 
		Empresa, IdCompraBU, FechaCompraBU, Proveedor, TipoComprobante, isnull(LetraComprobante, '') LetraComprobante, 
--		left(NComprobante, 4) Centro_Emision, Right(NComprobante, 8) NComprobante, 

		Case when CHARINDEX('-', NComprobante) <> 0 Then
			left( rtrim(ltrim(NComprobante)), (CHARINDEX('-', rtrim(ltrim(NComprobante)) ) - 1) ) 
		Else '0' 
		End Centro_Emision,
		Case when CHARINDEX('-', NComprobante) <> 0 Then
			right( rtrim(ltrim(NComprobante)), (CHARINDEX('-', rtrim(ltrim(NComprobante)) ) ) ) 
		Else rtrim(ltrim(NComprobante)) 
		End  NComprobante,

		DetalleCompraBU, ImporteNeto, IVA, 
		isnull(IngresosBrutos, 0) IngresosBrutos,
		TotalPesos, Estado, NRemito, isnull(IVA27, 0) IVA27, isnull(IVA105, 0) IVA105, isnull(Resol3337, 0) Resol3337, SaldoBU, 
		isnull(CondicionIVA, 0) CondicionIVA,
		isnull(CondicionGanancia, 0) CondicionGanancia, 
		FechaVencimiento, 
		isnull(BienUso, 0) BienUso, 
		isnull(NCarpeta, 0) NCarpeta,
		Ganancias, DerechosAd, IVA9, Otros, 
		isnull(Cotizacion, 0) Cotizacion,
		CAI, 
		isnull(feCAI,'') feCAI,
		isnull(feComp, '') feComp, 
		isnull(Aduana, '') Aduana, 
		isnull(Destinacion, '') Destinacion, Despacho, Verifica, Moneda, Fiscal, codOpe,
		isnull(Percepcion_IVA, 0) Percepcion_IVA,cuit

		From CompraBU
		WHERE Empresa = @intEmpresa
		And IdCompraBU = Case When @intIdCompraBU is null Then IdCompraBU Else @intIdCompraBU End
		--And NCarpeta Is Not Null
		Order By Empresa, IdCompraBU 
	End

/*
select --CHARINDEX('-', rtrim(ltrim(NComprobante))), 
NComprobante,
left( rtrim(ltrim(NComprobante)), (CHARINDEX('-',  rtrim(ltrim(NComprobante))) - 1) ), 
right( rtrim(ltrim(NComprobante)), (CHARINDEX('-',  rtrim(ltrim(NComprobante))) ) ),
* 
from comprabu where empresa = 1 and idcomprabu = 28886
*/
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Listado_Subdiario_Compras]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Carga_Listado_Subdiario_Compras 1, '20190801', '20190816'

CREATE Proc [dbo].[Carga_Listado_Subdiario_Compras]
@intEmpresa tinyint = null,
/*
@FechaDesde as smalldatetime=null,
@FechaHasta as smalldatetime=null
*/
@strFecha_Desde char(8) = '',
@strFecha_Hasta char(8) = ''

as

Declare
@datFecha_Desde smalldatetime,
@datFecha_Hasta smalldatetime

Set @datFecha_Desde = null

if (rtrim(ltrim(@strFecha_Desde)) <> '')
	Begin
		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
	End

--Set @intEmpresa = 1

if @datFecha_Desde is null 
	Begin
		select @datFecha_Desde = fInicioEjercicio, @datFecha_Hasta = FCierreEjercicio 
		From datosgenerales
		Where Empresa = @intEmpresa
	End

select a.FechaCompraBU Fecha, 
rtrim(ltrim(isnull(a.LetraComprobante,''))) Letra,
rtrim(ltrim(isnull(a.TipoComprobante,''))) + ' ' + rtrim(ltrim(isnull(a.LetraComprobante,''))) + '-' + rtrim(ltrim(isnull(a.NComprobante,''))) Comprobante, 
isnull(a.nCarpeta, '') Numero, ISNULL(a.nombreProveedor,'') Razon_Social_Proveedor, ISNULL(a.CUIT,'') CUIT,
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(ImporteNeto, 0) * -1 Else Isnull(ImporteNeto, 0) End Neto, 
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(IVA, 0) * -1 Else Isnull(IVA, 0) End IVA_21,
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(a.iva27, 0) * -1 Else Isnull(a.iva27, 0) End IVA_27,
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(a.IVA105, 0) * -1 Else Isnull(a.IVA105, 0) End IVA_105,
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(a.IngresosBrutos, 0) * -1 Else Isnull(a.IngresosBrutos, 0) End IIBB, 
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then isnull(a.Ganancias, 0) * -1 Else isnull(a.Ganancias, 0) End Imp_Ganancias,
Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then isnull(a.Percepcion_IVA, 0) * -1 Else isnull(a.Percepcion_IVA, 0) End Percepcion_IVA,
b.RazonSocial Razon_Social_Empresa
From comprabu a Inner Join Empresa b
On a.Empresa = b.idEmpresa
Where a.Empresa = @intEmpresa
and isnull(a.nCarpeta, '') <> ''
and rtrim(ltrim(a.Estado)) <> 'Anulada'
and a.FechaCompraBU Between @datFecha_Desde and @datFecha_Hasta
Order by a.NCarpeta
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Condiciones_Ganancias]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Carga_Condiciones_Ganancias]

as

Select IdGanancia, Descripcion, isnull(TaxInscripto, 0) TaxInscripto, isnull(ImporteExento, 0) ImporteExento, isnull(TaxNoInscripto, 0) TaxNoInscripto  
From CondicionGanancia
Order By Descripcion
GO
/****** Objeto:  StoredProcedure [dbo].[sgm_Carga_Condiciones_IVA]    Fecha de la secuencia de comandos: 10/02/2019 14:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[sgm_Carga_Condiciones_IVA]

as

select IdCondicionIVA, Descripcion, isnull(Estado, '') Estado, isnull(TaxInscripto, 0) TaxInscripto, isnull(ImporteExento, 0) ImporteExento, isnull(TaxNoInscripto, 0) TaxNoInscripto 
From CondicionIVA
Order By Descripcion
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Saldos_Mayor_General]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

EXEC [Carga_Saldos_Mayor_General] 2,'20180101','20180930'

*/
create PROCEDURE [dbo].[Carga_Saldos_Mayor_General]
(
@codEmpresa integer,
@FechaDesde as smalldatetime=null,
@FechaHasta as smalldatetime=null
)
AS



if @FechaDesde is null 
	Begin
		select @FechaDesde = fInicioEjercicio, @FechaHasta = FCierreEjercicio 
		From datosgenerales
		Where Empresa = @codEmpresa
	End
select cuenta, sum(isnull(ia.debe,0) - isnull(ia.haber,0)) as saldo ,convert(varchar(10),@FechaHasta,103)  as fecha 
from itemasiento as ia inner join asiento as a on ia.asiento = a.idasiento 
where a.fechaasiento between @FechaDesde  and @FechaHasta 
and ia.cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados)  
and a.empresa=@codEmpresa  and a.Estado<>'Anulado' group by ia.cuenta
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_TOTALES_Balance_Sumas_y_Saldos]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

EXEC Carga_TOTALES_Balance_Sumas_y_Saldos 1,'20180101','20180131'

*/

CREATE PROCEDURE [dbo].[Carga_TOTALES_Balance_Sumas_y_Saldos]
@codEmpresa integer,
@FechaDesde as smalldatetime=null,
@FechaHasta as smalldatetime=null

AS



if @FechaDesde is null 
	Begin
		select @FechaDesde = fInicioEjercicio, @FechaHasta = FCierreEjercicio 
		From datosgenerales
		Where Empresa = @codEmpresa
	End
SELECT 1 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Activo' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta LIKE '1%'

UNION SELECT 2 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Pasivo' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta LIKE '2%'

UNION SELECT 3 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Patrimonio Neto' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta LIKE '3%'

UNION SELECT 4 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Resultado' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta Not LIKE '3%' 
ORDER BY Orden
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Datos_Generales]    Fecha de la secuencia de comandos: 10/02/2019 14:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Carga_Datos_Generales]
@intEmpresa smallint = Null

as

select Empresa ID_Empresa, FInicioEjercicio Fecha_Inicio_Ejercicio, fCierreEjercicio Fecha_Cierre_Ejercicio, 
FInicioEjercicioAnt Fecha_Inicio_Ejercicio_Anterior, FCierreEjercicioAnt Fecha_Cierre_Ejercicio_Anterior 
From DatosGenerales
Where Empresa = Case When @intEmpresa is null then Empresa Else @intEmpresa End
GO
/****** Objeto:  StoredProcedure [dbo].[Alta_Datos_Generales]    Fecha de la secuencia de comandos: 10/02/2019 14:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Alta_Datos_Generales]
@intEmpresa smallint,
@datFecha_Inicio_Ejercicio smalldatetime,
@datFecha_Fin_Ejercicio smalldatetime,
@datFecha_Inicio_Ejercicio_Ciclo_Anterior smalldatetime,
@datFecha_Fin_Ejercicio_Cliclo_Anterior smalldatetime

as

if exists (Select 1 From DatosGenerales Where Empresa = @intEmpresa)
	Begin
		Update DatosGenerales
		Set FInicioEjercicio = @datFecha_Inicio_Ejercicio,
		fCierreEjercicio = @datFecha_Fin_Ejercicio,
		FInicioEjercicioAnt = @datFecha_Inicio_Ejercicio_Ciclo_Anterior,
		FCierreEjercicioAnt = @datFecha_Fin_Ejercicio_Cliclo_Anterior
		Where Empresa = @intEmpresa
	End 
Else
	Begin
		Insert Into DatosGenerales
		(Empresa, FInicioEjercicio, fCierreEjercicio, FInicioEjercicioAnt, FCierreEjercicioAnt)
		Values
		(@intEmpresa, @datFecha_Inicio_Ejercicio, @datFecha_Fin_Ejercicio, @datFecha_Inicio_Ejercicio_Ciclo_Anterior, @datFecha_Fin_Ejercicio_Cliclo_Anterior)
	End
GO
/****** Objeto:  StoredProcedure [dbo].[Elimina_ItemAsiento]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[Elimina_ItemAsiento] 
@intEmpresa tinyint,
@intAnio int,
@intAsiento int

as

Delete ItemAsiento 
Where Empresa = @intEmpresa
and Ano = @intAnio
and Asiento = @intAsiento
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Monedas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Carga_Monedas]

as

Select Codigo, Descripcion 
From moneda
Order By Descripcion
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Aduanas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Aduanas] 
@strCodigo nvarchar(6) = ''

as

Select rtrim(ltrim(Codigo)) Codigo, Descripcion 
From Aduana 
Where Codigo = Case When ltrim(rtrim(@strCodigo)) = '' then Codigo Else @strCodigo End
Order By Descripcion
GO
/****** Objeto:  StoredProcedure [dbo].[Alta_Empresa]    Fecha de la secuencia de comandos: 10/02/2019 14:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Alta_Empresa]
@intEmpresa int,
@strRazonSocial varchar(30),
@strDomicilio varchar(30),
@strLocalidad varchar(100),
@intPais int,
@intProvincia int,
@strCUIT varchar(13)

as 

declare  @nextID int 
set @nextID=(select isnull(max(idempresa),0)+1 from empresa)


INSERT INTO Empresa (IdEmpresa, RazonSocial, Domicilio, Localidad, Pais, Provincia,CUIT)
VALUES (@nextID, @strRazonSocial, @strDomicilio, @strLocalidad, @intPais, @intProvincia, @strCUIT)
GO
/****** Objeto:  StoredProcedure [dbo].[Actualiza_Empresa]    Fecha de la secuencia de comandos: 10/02/2019 14:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Actualiza_Empresa]
@intEmpresa int,
@strRazonSocial varchar(30),
@strDomicilio varchar(30),
@strLocalidad varchar(100),
@intPais int,
@intProvincia int,
@strCUIT varchar(13)

as 

update empresa set
RazonSocial=@strRazonSocial,
Domicilio=@strDomicilio,
Localidad=@strLocalidad,
Pais=@intPais,
Provincia=@intProvincia,
CUIT=@strCUIT
WHERE idEmpresa=@intEmpresa
GO
/****** Objeto:  StoredProcedure [dbo].[cargar_datos_empresa]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[cargar_datos_empresa]

@intEmpresa int

as

select * from empresa where idempresa=@intEmpresa
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Listado_Plan_Cuentas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec Carga_Listado_Plan_Cuentas @strID_Cuenta_Desde = '11110100'

Create Proc [dbo].[Carga_Listado_Plan_Cuentas] 
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
GO
/****** Objeto:  StoredProcedure [dbo].[Actualiza_Plan_Cuentas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:02 ******/
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
/****** Objeto:  StoredProcedure [dbo].[Alta_Plan_Cuentas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:06 ******/
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
/****** Objeto:  StoredProcedure [dbo].[Elimina_Plan_Cuentas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Elimina_Plan_Cuentas]
@strID_Cuenta varchar(8)

as

Delete PlanCuenta where IdCuenta = @strID_Cuenta
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Plan_Cuenta]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Carga_Plan_Cuenta]

as

SELECT IdCuenta, Descripcion 
FROM PlanCuenta 
WHERE AceptaMovimiento=1 
ORDER BY IdCuenta
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Plan_Cuentas]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Plan_Cuentas]
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
GO
/****** Objeto:  StoredProcedure [dbo].[CargaPaises]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CargaPaises]
AS

select idPais, NombrePais from pais
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Paises]    Fecha de la secuencia de comandos: 10/02/2019 14:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Carga_Paises]
AS

select idPais, NombrePais from pais
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Proveedores]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Carga_Proveedores]
@intProveedor int = null

as

SELECT IdProveedor Codigo, RazonSocial Razon_Social, CUIT,Domicilio, Localidad, CodigoPostal, Pais, Provincia, Telefono, Fax, EMail, Tipo, Despachante, 
Estado, CondicionIVA, CondicionGanancia, CondicionIIBB, IB, Cuit
FROM Proveedor
Where IdProveedor = Case When @intProveedor is null Then IdProveedor Else @intProveedor End
Order By RazonSocial
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Provincias]    Fecha de la secuencia de comandos: 10/02/2019 14:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Carga_Provincias]

AS

select IdProvincia, NombreProvincia from provincia order by nombreProvincia
GO
