USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Listado_Libro_Diario]    Fecha de la secuencia de comandos: 08/27/2019 11:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Carga_Listado_Libro_Diario @intEmpresa = 1, @datFecha_Desde = '20170701', @datFecha_Hasta = '20190408' 

--Carga_Listado_Libro_Diario @intEmpresa = 1, @strFecha_Desde = '20170701', @strFecha_Hasta = '20170731'

--Carga_Listado_Libro_Diario 1,'20170101','20170630', 1, 1000
ALTER proc [dbo].[Carga_Listado_Libro_Diario]
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

