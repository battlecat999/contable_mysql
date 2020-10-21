-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: sgi_pop
-- Source Schemata: sgi_pop
-- Created: Wed Oct 21 10:06:09 2020
-- Workbench Version: 8.0.21
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema sgi_pop
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `sgi_pop` ;
CREATE SCHEMA IF NOT EXISTS `sgi_pop` ;

-- ----------------------------------------------------------------------------
-- Table sgi_pop.Asiento
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `sgi_pop`.`Asiento` (
  `Empresa` TINYINT UNSIGNED NOT NULL,
  `Ano` INT NOT NULL,
  `IdAsiento` INT NOT NULL,
  `FechaAsiento` DATETIME NOT NULL,
  `LeyendaAsiento` VARCHAR(50) NULL,
  `NInternoAsiento` INT NULL,
  `Estado` VARCHAR(15) NULL,
  `NIComprobante` VARCHAR(5) NULL);

-- ----------------------------------------------------------------------------
-- Table sgi_pop.DatosGenerales
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `sgi_pop`.`DatosGenerales` (
  `Empresa` TINYINT UNSIGNED NOT NULL,
  `FInicioEjercicio` DATETIME NULL,
  `FCierreEjercicio` DATETIME NULL,
  `FInicioEjercicioAnt` DATETIME NULL,
  `FCierreEjercicioAnt` DATETIME NULL);

-- ----------------------------------------------------------------------------
-- Table sgi_pop.ItemAsiento
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `sgi_pop`.`ItemAsiento` (
  `Empresa` TINYINT UNSIGNED NOT NULL,
  `Ano` INT NOT NULL,
  `Asiento` INT NOT NULL,
  `ItemAsiento` SMALLINT NOT NULL,
  `Cuenta` VARCHAR(8) NOT NULL,
  `LeyendaItem` VARCHAR(30) NULL,
  `Debe` DECIMAL(15,2) NULL,
  `Haber` DECIMAL(15,2) NULL);

-- ----------------------------------------------------------------------------
-- Table sgi_pop.CuentasTMP_Listados
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `sgi_pop`.`CuentasTMP_Listados` (
  `nroCuentaTMP` VARCHAR(50) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table sgi_pop.Empresa
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `sgi_pop`.`Empresa` (
  `IdEmpresa` TINYINT UNSIGNED NOT NULL,
  `RazonSocial` VARCHAR(30) NOT NULL,
  `Domicilio` VARCHAR(30) NOT NULL,
  `Localidad` VARCHAR(30) NOT NULL,
  `Pais` SMALLINT NOT NULL,
  `Provincia` SMALLINT NOT NULL,
  `CUIT` VARCHAR(13) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table sgi_pop.PlanCuenta
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `sgi_pop`.`PlanCuenta` (
  `IdCuenta` VARCHAR(8) NOT NULL,
  `Descripcion` VARCHAR(50) NOT NULL,
  `Nivel` INT NOT NULL,
  `AceptaMovimiento` TINYINT UNSIGNED NOT NULL);

-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Listado_Mayor_General
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- --Listado
-- /*
-- 
-- EXEC [Carga_Listado_Mayor_General] 1,'20180101','20190930'
-- Carga_Listado_Mayor_General @intEmpresa = 1, @strFecha_Desde = '20190101', @strFecha_Hasta = '20190929'
-- 
-- */
-- CREATE PROCEDURE [dbo].[Carga_Listado_Mayor_General]
-- (
-- @intEmpresa int,
-- /*
-- @FechaDesde as smalldatetime=null,
-- @FechaHasta as smalldatetime=null
-- */
-- @strFecha_Desde char(8) = '',
-- @strFecha_Hasta char(8) = ''
-- 
-- )
-- AS
-- 
-- Declare
-- @datFecha_Inicio_Ejercicio smalldatetime,
-- @datFecha_Desde smalldatetime,
-- @datFecha_Hasta smalldatetime
-- 
-- Set @datFecha_Desde = null
-- 
-- 
-- if (rtrim(ltrim(@strFecha_Desde)) <> '')
-- 	Begin
-- 		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
-- 		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
-- 	End
-- 
-- Select @datFecha_Inicio_Ejercicio = fInicioEjercicio
-- From datosgenerales
-- Where Empresa = @intEmpresa
-- 
-- if @datFecha_Desde is null 
-- 	Begin
-- 		
-- select @datFecha_Desde = fInicioEjercicio, @datFecha_Hasta = FCierreEjercicio 
-- 		From datosgenerales
-- 		Where Empresa = @intEmpresa
-- 	End
-- 
-- SELECT a.FechaAsiento, 
-- a.IdAsiento As NroAsiento,
-- a.NIComprobante,
-- b.LeyendaItem,
-- ISNULL(b.Debe,0) as DEBE,
-- ISNULL(b.Haber,0)
--  as HABER,
-- b.Cuenta,
-- c.Descripcion,
-- convert(char(6), cast(Year(a.FechaAsiento) as char(4)) + dbo.LPAD(Month(a.FechaAsiento), 2, '0')) Anio_Mes ,
-- 
-- isnull(Saldos.Saldo, 0) Saldo,
-- d.RazonSocial
-- 
-- FROM Asiento a Inner Join ItemAsiento b 
-- On a.Empresa = b.Empresa
-- AND a.Ano= b.Ano 
-- and a.idAsiento = b.Asiento 
-- --and a.idCuenta = b.Cuenta
-- 
-- Inner Join PlanCuenta c
-- On b.Cuenta = c.idCuenta 
-- 
-- Left Join (select a.Empresa, ia.cuenta, sum(isnull(ia.debe,0) - isnull(ia.haber,0)) as saldo --,convert(varchar(10),@FechaHasta,103)  as fecha 
-- from itemasiento as ia inner join asiento as a 
-- on ia.asiento = a.idasiento 
-- --where a.fechaasiento between @FechaDesde  and @FechaHasta 
-- where a.fechaasiento between @datFecha_Inicio_Ejercicio  and (@datFecha_Desde - 1)
-- and ia.cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados)  
-- and a.empresa=@intEmpresa  
-- and a.Estado<>'Anulado' 
-- group by a.Empresa, ia.cuenta
-- ) as Saldos
-- On b.Cuenta = Saldos.Cuenta
-- and b.Empresa = Saldos.Empresa
-- 
-- Inner Join Empresa d
-- On a.Empresa = d.IdEmpresa
-- 
-- WHERE a.Empresa= @intEmpresa
-- AND a.Estado <> 'Anulado' 
-- 
-- --AND Asiento=IdAsiento 
-- --AND Asiento.Ano=ItemAsiento.Ano 
-- AND b.Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) 
-- --AND IdCuenta=ItemAsiento.Cuenta 
-- AND a.FechaAsiento BETWEEN @datFecha_Desde AND @datFecha_Hasta
-- 
-- 
-- --and Saldos.Cuenta =* ItemAsiento.Cuenta
-- --and Saldos.Empresa =* ItemAsiento.Empresa
-- 
-- ORDER BY b.Cuenta, a.FechaAsiento, cast(Year(a.FechaAsiento) as char(4)) + dbo.LPAD(Month(a.FechaAsiento), 2, '0'), a.IdAsiento
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Listado_Libro_Diario
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- --Carga_Listado_Libro_Diario @intEmpresa = 1, @datFecha_Desde = '20170701', @datFecha_Hasta = '20190408' 
-- 
-- --Carga_Listado_Libro_Diario @intEmpresa = 1, @strFecha_Desde = '20170701', @strFecha_Hasta = '20170731'
-- 
-- --Carga_Listado_Libro_Diario 1,'20170101','20170630', 1, 1000
-- CREATE proc [dbo].[Carga_Listado_Libro_Diario]
-- @intEmpresa smallint,
-- --@datFecha_Desde smalldatetime = null,
-- --@datFecha_Hasta smalldatetime = null,
-- @strFecha_Desde char(8) = '',
-- @strFecha_Hasta char(8) = '',
-- 
-- @intAsiento_Desde int = null,
-- @intAsiento_Hasta int = null,
-- 
-- @intOrden_Por_Asiento tinyint = 0
-- 
-- 
-- as
-- 
-- Declare
-- @datFecha_Desde smalldatetime,
-- @datFecha_Hasta smalldatetime,
-- @datFecha_Inicio_Ejercicio smalldatetime,
-- @datFecha_Cierre_Ejercicio smalldatetime
-- 
-- if (rtrim(ltrim(@strFecha_Desde)) <> '')
-- 	Begin
-- 		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
-- 		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
-- 	End
-- 
-- Select @datFecha_Inicio_Ejercicio = fInicioEjercicio,
-- @datFecha_Cierre_Ejercicio = fCierreEjercicio
-- From datosgenerales
-- Where Empresa = @intEmpresa
-- 
-- 
-- if (rtrim(ltrim(@strFecha_Desde)) <> '')
-- 	Begin 
--        SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta, isnull(Debe, 0) Debe, isnull(Haber, 0) Haber, LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante,
-- 	   Empresa.RazonSocial Desc_Empresa
-- 
-- 	   FROM Asiento, ItemAsiento, Empresa
-- 	   
-- 	   WHERE Asiento.Estado<>'Anulado' 
-- 	   AND Asiento.Empresa = Empresa.IdEmpresa
-- 	   AND Asiento.Empresa = @intEmpresa 
-- 	   AND ItemAsiento.Empresa = Asiento.Empresa 
-- 	   AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano 
-- 	  AND FechaAsiento BETWEEN @datFecha_Desde AND @datFecha_Hasta 
-- 	
-- 	   ORDER BY Asiento.FechaAsiento,IdAsiento
-- 	End
-- 
-- Else
-- 
-- 	Begin
-- 	
-- 		if (@intOrden_Por_Asiento = 1)
-- 			Begin
-- 
-- 				if (@intAsiento_Desde is not null and @intAsiento_Hasta is not null)
-- 					Begin
-- 						SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
-- 						FROM Asiento,ItemAsiento 
-- 						WHERE Asiento.Estado <> 'Anulado' 
-- 						AND Asiento.Empresa = @intEmpresa 
-- 						AND ItemAsiento.Empresa=Asiento.Empresa 
-- 						AND Asiento=IdAsiento 
-- 						AND Asiento.Ano=ItemAsiento.Ano 
-- 						AND IdAsiento BETWEEN @intAsiento_Desde AND @intAsiento_Hasta 
-- 						ORDER BY Asiento.FechaAsiento,IdAsiento
-- 					End
-- 
-- 				Else
-- 					
-- 					Begin 
-- 
-- 						if (@intAsiento_Desde is not null and @intAsiento_Hasta is null)
-- 							Begin
-- 								SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
-- 								FROM Asiento,ItemAsiento 
-- 								WHERE Asiento.Estado <> 'Anulado' 
-- 								AND Asiento.Empresa = @intEmpresa  
-- 								AND ItemAsiento.Empresa=Asiento.Empresa 
-- 								AND Asiento=IdAsiento 
-- 								AND Asiento.Ano=ItemAsiento.Ano 
-- 								AND IdAsiento >= @intAsiento_Desde 
-- 								AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
-- 								ORDER BY Asiento.FechaAsiento,IdAsiento
-- 							End
-- 
-- 						Else
-- 
-- 							Begin
-- 
-- 								if (@intAsiento_Desde is null and @intAsiento_Hasta is not null)
-- 									Begin
-- 										SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Haber,LeyendaItem,LeyendaAsiento,FechaAsiento,NIComprobante 
-- 										FROM Asiento,ItemAsiento 
-- 										WHERE Asiento.Estado <> 'Anulado' 
-- 										AND Asiento.Empresa = @intEmpresa  
-- 										AND ItemAsiento.Empresa=Asiento.Empresa 
-- 										AND Asiento=IdAsiento 
-- 										AND Asiento.Ano=ItemAsiento.Ano 
-- 										AND IdAsiento >= @intAsiento_Hasta 
-- 										AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
-- 										ORDER BY Asiento.FechaAsiento,IdAsiento
-- 									End
-- 
-- 								else
-- 
-- 									Begin
-- 
-- 										if (@intAsiento_Desde is null and @intAsiento_Hasta is null)
-- 											Begin
-- 												SELECT IdAsiento As NroAsiento,ItemAsiento,Cuenta,Debe,Hab$$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Asiento
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE Proc [dbo].[Carga_Asiento]
-- @intEmpresa smallint = null,
-- @intAnio smallint = null,
-- @intID_Asiento int = null,
-- @strNIComprobante varchar(5) = ''
-- 
-- as
-- 
-- SELECT 
-- a.IdEmpresa, a.RazonSocial Razon_Social,
-- b.Ano Anio, b.IdAsiento, b.FechaAsiento Fecha_Asiento, b.LeyendaAsiento Leyenda_Asiento, isnull(b.NInternoAsiento, 0) NInternoAsiento, b.Estado, b.NIComprobante Numero_Comprobante
-- FROM Empresa a Inner Join Asiento b
-- On a.IdEmpresa = b.Empresa
-- WHERE a.IdEmpresa = Case When @intEmpresa is null then a.IdEmpresa Else @intEmpresa End 
-- AND b.Ano = Case When @intAnio is null then b.Ano Else @intAnio End
-- and b.IdAsiento = Case When @intID_Asiento is null then b.IdAsiento Else @intID_Asiento End
-- and b.NIComprobante = Case When rtrim(ltrim(@strNIComprobante)) = '' then b.NIComprobante Else rtrim(ltrim(@strNIComprobante)) End
-- ORDER BY b.IdAsiento DESC
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Item_Asiento
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- --exec Carga_Item_Asiento @intEmpresa = 1, @intAnio = 2018, @intAsiento = 738, @strNumero_Comprobante = '04236'
-- 
-- --exec Carga_Item_Asiento @intEmpresa = 1, @intAnio = 2018, @strNumero_Comprobante = '04236'
-- 
-- 
-- CREATE Proc [dbo].[Carga_Item_Asiento]
-- @intEmpresa smallint = null,
-- @intAnio smallint = null,
-- @intAsiento smallint = null,
-- @strNumero_Comprobante varchar(5) = ''
-- 
-- as
-- 
-- select a.Empresa, a.Ano Anio, a.Asiento, a.ItemAsiento, a.Cuenta, a.LeyendaItem, isnull(a.Debe, 0) Debe, isnull(a.Haber, 0) Haber,
-- b.Descripcion Nombre,
-- isnull(c.NIComprobante, 0) NIComprobante,
-- rtrim(ltrim(c.Estado)) Estado
-- 
-- From itemasiento a Inner Join PlanCuenta b
-- On a.Cuenta = b.IdCuenta
-- Inner Join Asiento c
-- 
-- On a.Empresa = c.Empresa
-- and a.Ano = c.Ano
-- and a.Asiento = c.IdAsiento
-- 
-- Where a.Empresa = @intEmpresa
-- and a.Ano = @intAnio 
-- and a.Asiento = Case When @intAsiento is null then a.Asiento Else @intAsiento End
-- and rtrim(ltrim(c.NIComprobante)) = Case When rtrim(ltrim(@strNumero_Comprobante)) = '' then rtrim(ltrim(c.NIComprobante)) Else rtrim(ltrim(@strNumero_Comprobante)) End
-- Order By a.Empresa, a.Ano, a.Asiento, a.ItemAsiento
-- 
-- 
-- /*
-- SELECT ItemAsiento, Cuenta, Descripcion,LeyendaItem,Debe,Haber,Empresa,Cuenta,Ano FROM ItemAsiento INNER JOIN PlanCuenta ON ItemAsiento.Cuenta=PlanCuenta.IdCuenta WHERE Empresa=" & rsEmpresas![IdEmpresa] & " AND Asiento=" & cboAsiento.Text & " AND Ano="
--  & lblAño.Caption
--  */
-- 
-- /*
--  select top 1 * from asiento where niComprobante = '04236'
--  select top 1 * from itemasiento
-- 
-- */
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Combo_Asientos_Desde
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- --Carga_Combo_Asientos_Desde @intEmpresa = 1, @intOrden_Por_Asiento = 0
-- 
-- CREATE Proc [dbo].[Carga_Combo_Asientos_Desde]
-- @intEmpresa smallint,
-- @intOrden_Por_Asiento tinyint = 0,
-- @intIdAsiento_Desde int = null
-- 
-- as
-- 
-- Declare
-- @datFecha_Inicio_Ejercicio smalldatetime,
-- @datFecha_Cierre_Ejercicio smalldatetime
-- 
-- Select @datFecha_Inicio_Ejercicio = fInicioEjercicio,
-- @datFecha_Cierre_Ejercicio = fCierreEjercicio
-- From datosgenerales
-- Where Empresa = @intEmpresa
-- 
-- 
-- If (@intOrden_Por_Asiento = 1)
-- 	Begin
-- 		
-- 		if (@intIdAsiento_Desde is null)
-- 			Begin
-- 				SELECT IdAsiento
-- 				FROM Asiento 
-- 				WHERE Empresa = @intEmpresa 
-- 				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
-- 				ORDER BY IdAsiento
-- 			End
-- 		Else
-- 			Begin
-- 				SELECT IdAsiento 
-- 				FROM Asiento 
-- 				WHERE Empresa = @intEmpresa 
-- 				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
-- 				and IdAsiento <> @intIdAsiento_Desde
-- 				ORDER BY IdAsiento
-- 			End
-- 	End
-- Else
-- 	Begin
-- 
-- 		if (@intIdAsiento_Desde is null)
-- 			Begin
-- 				SELECT NInternoAsiento IdAsiento--, *
-- 				FROM Asiento 
-- 				WHERE Empresa = @intEmpresa 
-- 				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
-- 				and NInternoAsiento is not null
-- 				ORDER BY NInternoAsiento
-- 			End
-- 		Else
-- 			Begin
-- 				SELECT NInternoAsiento IdAsiento--, *
-- 				FROM Asiento 
-- 				WHERE Empresa = @intEmpresa 
-- 				AND FechaAsiento BETWEEN @datFecha_Inicio_Ejercicio AND @datFecha_Cierre_Ejercicio 
-- 				and IdAsiento <> @intIdAsiento_Desde
-- 				and NInternoAsiento is not null
-- 				ORDER BY NInternoAsiento
-- 			End
-- 	End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Balance_Sumas_y_Saldos
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- /*
-- 
-- EXEC Carga_Balance_Sumas_y_Saldos 1,'20180701','20190731'
-- 
-- */
-- 
-- CREATE PROCEDURE [dbo].[Carga_Balance_Sumas_y_Saldos]
-- (
-- @intEmpresa smallint,
-- @strFecha_Desde char(8) = '',
-- @strFecha_Hasta char(8) = ''
-- )
-- 
-- AS
-- set @strFecha_Desde=''
-- set @strFecha_Hasta=''
-- 
-- Declare
-- @datFecha_Desde as smalldatetime,
-- @datFecha_Hasta as smalldatetime
-- 
-- set @datFecha_Desde=null
-- set @datFecha_Hasta=null
-- 
-- if (rtrim(ltrim(@strFecha_Desde)) <> '')
-- 	Begin
-- 		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
-- 		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
-- 	End
-- 
-- if @datFecha_Desde is null 
-- 	Begin
-- 		select @datFecha_Desde = fInicioEjercicio, @datFecha_Hasta = FCierreEjercicio 
-- 		From datosgenerales
-- 		Where Empresa = @intEmpresa
-- 	End
-- declare @ultimo_asiento int
-- 
-- set @ultimo_asiento=(SELECT Max(IdAsiento) As UltimoAsiento FROM Asiento WHERE Empresa=@intEmpresa  AND Ano=(SELECT Max(Ano) FROM Asiento WHERE Empresa=@intEmpresa ) GROUP BY Ano)
-- 
-- SELECT (IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,
-- Cuenta,Descripcion,
-- Sum( isnull(Debe,0) ) As Debitos,
-- Sum( isnull(Haber, 0) ) as Creditos,
-- CASE WHEN (SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) )) > 0 THEN SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) ) ELSE 0 END as Saldo_Deudor,
-- CASE WHEN (SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) )) < 0 THEN -(SUM( isnull(Debe, 0) )-SUM( isnull(Haber, 0) )) ELSE 0 END as Saldo_Acreedor,
-- SUBSTRING(Cuenta,1,2) as SubTotal,
-- SUBSTRING(Cuenta,1,1) as SubTotal_Cuenta,
-- @ultimo_asiento as Ultimo_Asiento,
-- Empresa.RazonSocial Razon_Social
-- 
-- FROM Asiento,ItemAsiento,PlanCuenta, Empresa
-- WHERE Asiento.Empresa = @intEmpresa 
-- AND Asiento.Estado<>'Anulado' 
-- AND ItemAsiento.Empresa=Asiento.Empresa 
-- AND Asiento=IdAsiento 
-- AND Asiento.Ano=ItemAsiento.Ano 
-- AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) 
-- AND IdCuenta=Cuenta 
-- AND Asiento.Empresa = Empresa.IdEmpresa 
-- AND FechaAsiento BETWEEN @datFecha_Desde AND @datFecha_Hasta 
-- GROUP BY Cuenta,Descripcion, Empresa.RazonSocial 
-- ORDER BY Cuenta
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Alta_Asiento
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE Proc [dbo].[Alta_Asiento]
-- @intEmpresa tinyint,
-- @intAnio int,
-- @intAsiento int = null,
-- @datFecha_Asiento smalldatetime,
-- @strLeyenda_Asiento varchar(50) = '',
-- @intNumero_Interno_Asiento int = null,
-- @strEstado varchar(15) = '',
-- @strNumero_Comprobante varchar(5) = ''
-- 
-- as
-- 
-- Declare 
-- @intNumero_Asiento int 
-- 
-- if (@intAsiento is null)
-- 	Begin
-- 
-- 		If exists (Select 1 From Asiento Where Empresa = @intEmpresa And Ano = @intAnio)
-- 			Begin 
-- 				Select @intNumero_Asiento = Max(IdAsiento) + 1 From Asiento Where Empresa = @intEmpresa And Ano = @intAnio
-- 			End
-- 		Else
-- 			Begin 
-- 				Set @intNumero_Asiento = 1
-- 			End
-- 
-- 		Insert Into Asiento
-- 		(Empresa, Ano, IdAsiento, FechaAsiento, LeyendaAsiento, NInternoAsiento, Estado, NIComprobante)
-- 		Values
-- 		(@intEmpresa, @intAnio, @intNumero_Asiento, @datFecha_Asiento, @strLeyenda_Asiento, null, @strEstado, @strNumero_Comprobante)
-- 
-- 	End
-- Else
-- 	Begin
-- 		Set @intNumero_Asiento = @intAsiento
-- 
-- 		update Asiento 
-- 		Set FechaAsiento = @datFecha_Asiento,
-- 		LeyendaAsiento = @strLeyenda_Asiento,
-- 		Estado = @strEstado
-- 		Where Empresa = @intEmpresa 
-- 		And Ano = @intAnio
-- 		and idAsiento = @intNumero_Asiento
-- 	End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Alta_ItemAsiento
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE proc [dbo].[Alta_ItemAsiento]
-- @intEmpresa tinyint,
-- @intAnio int,
-- @intAsiento int = null,
-- --@intItem smallint,
-- @strCuenta varchar(8),
-- @strLeyenda_Item varchar(30),
-- @decDebe float,
-- @decHaber float,
-- @strNumero_Comprobante varchar(5) = ''
-- 
-- as
-- 
-- Declare
-- @intItem smallint,
-- @intNumero_Asiento int
-- 
-- If Not Exists (Select 1 From ItemAsiento Where Empresa = @intEmpresa and Ano = @intAnio and Asiento = @intAsiento)
-- 	Begin
-- 		Set @intItem = 1
-- 	End
-- Else
-- 	Begin
-- 		Select @intItem = max(ItemAsiento) + 1
-- 		From ItemAsiento 
-- 		Where Empresa = @intEmpresa
-- 		and Ano = @intAnio
-- 		and Asiento = @intAsiento
-- 	End
-- 
-- if (@intAsiento is null)
-- 	Begin
-- 		Select @intNumero_Asiento = IdAsiento From Asiento Where Empresa = @intEmpresa And Ano = @intAnio and rtrim(ltrim(NIComprobante)) = rtrim(ltrim(@strNumero_Comprobante))
-- 	End
-- else
-- 	Begin
-- 		Set @intNumero_Asiento = @intAsiento
-- 	End
-- 
-- Insert Into ItemAsiento
-- (Empresa, Ano, Asiento, ItemAsiento, Cuenta, LeyendaItem, Debe, Haber)
-- Values
-- (@intEmpresa, @intAnio, @intNumero_Asiento, @intItem, @strCuenta, @strLeyenda_Item, @decDebe, @decHaber)
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Listado_Subdiario_Compras
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- --Carga_Listado_Subdiario_Compras 1, '20190801', '20190816'
-- 
-- CREATE Proc [dbo].[Carga_Listado_Subdiario_Compras]
-- @intEmpresa tinyint = null,
-- /*
-- @FechaDesde as smalldatetime=null,
-- @FechaHasta as smalldatetime=null
-- */
-- @strFecha_Desde char(8) = '',
-- @strFecha_Hasta char(8) = ''
-- 
-- as
-- 
-- Declare
-- @datFecha_Desde smalldatetime,
-- @datFecha_Hasta smalldatetime
-- 
-- Set @datFecha_Desde = null
-- 
-- if (rtrim(ltrim(@strFecha_Desde)) <> '')
-- 	Begin
-- 		Set @datFecha_Desde = convert(smalldatetime, @strFecha_Desde)
-- 		Set @datFecha_Hasta = convert(smalldatetime, @strFecha_Hasta)
-- 	End
-- 
-- --Set @intEmpresa = 1
-- 
-- if @datFecha_Desde is null 
-- 	Begin
-- 		select @datFecha_Desde = fInicioEjercicio, @datFecha_Hasta = FCierreEjercicio 
-- 		From datosgenerales
-- 		Where Empresa = @intEmpresa
-- 	End
-- 
-- select a.FechaCompraBU Fecha, 
-- rtrim(ltrim(isnull(a.LetraComprobante,''))) Letra,
-- rtrim(ltrim(isnull(a.TipoComprobante,''))) + ' ' + rtrim(ltrim(isnull(a.LetraComprobante,''))) + '-' + rtrim(ltrim(isnull(a.NComprobante,''))) Comprobante, 
-- isnull(a.nCarpeta, '') Numero, ISNULL(a.nombreProveedor,'') Razon_Social_Proveedor, ISNULL(a.CUIT,'') CUIT,
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(ImporteNeto, 0) * -1 Else Isnull(ImporteNeto, 0) End Neto, 
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(IVA, 0) * -1 Else Isnull(IVA, 0) End IVA_21,
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(a.iva27, 0) * -1 Else Isnull(a.iva27, 0) End IVA_27,
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(a.IVA105, 0) * -1 Else Isnull(a.IVA105, 0) End IVA_105,
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then Isnull(a.IngresosBrutos, 0) * -1 Else Isnull(a.IngresosBrutos, 0) End IIBB, 
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then isnull(a.Ganancias, 0) * -1 Else isnull(a.Ganancias, 0) End Imp_Ganancias,
-- Case When rtrim(ltrim(a.TipoComprobante)) = 'NC' then isnull(a.Percepcion_IVA, 0) * -1 Else isnull(a.Percepcion_IVA, 0) End Percepcion_IVA,
-- b.RazonSocial Razon_Social_Empresa
-- From comprabu a Inner Join Empresa b
-- On a.Empresa = b.idEmpresa
-- Where a.Empresa = @intEmpresa
-- and isnull(a.nCarpeta, '') <> ''
-- and rtrim(ltrim(a.Estado)) <> 'Anulada'
-- and a.FechaCompraBU Between @datFecha_Desde and @datFecha_Hasta
-- Order by a.NCarpeta
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Saldos_Mayor_General
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- /*
-- 
-- EXEC [Carga_Saldos_Mayor_General] 2,'20180101','20180930'
-- 
-- */
-- create PROCEDURE [dbo].[Carga_Saldos_Mayor_General]
-- (
-- @codEmpresa integer,
-- @FechaDesde as smalldatetime=null,
-- @FechaHasta as smalldatetime=null
-- )
-- AS
-- 
-- 
-- 
-- if @FechaDesde is null 
-- 	Begin
-- 		select @FechaDesde = fInicioEjercicio, @FechaHasta = FCierreEjercicio 
-- 		From datosgenerales
-- 		Where Empresa = @codEmpresa
-- 	End
-- select cuenta, sum(isnull(ia.debe,0) - isnull(ia.haber,0)) as saldo ,convert(varchar(10),@FechaHasta,103)  as fecha 
-- from itemasiento as ia inner join asiento as a on ia.asiento = a.idasiento 
-- where a.fechaasiento between @FechaDesde  and @FechaHasta 
-- and ia.cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados)  
-- and a.empresa=@codEmpresa  and a.Estado<>'Anulado' group by ia.cuenta
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_TOTALES_Balance_Sumas_y_Saldos
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- /*
-- 
-- EXEC Carga_TOTALES_Balance_Sumas_y_Saldos 1,'20180101','20180131'
-- 
-- */
-- 
-- CREATE PROCEDURE [dbo].[Carga_TOTALES_Balance_Sumas_y_Saldos]
-- @codEmpresa integer,
-- @FechaDesde as smalldatetime=null,
-- @FechaHasta as smalldatetime=null
-- 
-- AS
-- 
-- 
-- 
-- if @FechaDesde is null 
-- 	Begin
-- 		select @FechaDesde = fInicioEjercicio, @FechaHasta = FCierreEjercicio 
-- 		From datosgenerales
-- 		Where Empresa = @codEmpresa
-- 	End
-- SELECT 1 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Activo' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
-- WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
-- IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta LIKE '1%'
-- 
-- UNION SELECT 2 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Pasivo' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
-- WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
-- IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta LIKE '2%'
-- 
-- UNION SELECT 3 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Patrimonio Neto' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
-- WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
-- IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta LIKE '3%'
-- 
-- UNION SELECT 4 As Orden,(IsNull(Sum(Haber),0)-IsNull(Sum(Debe),0)) As Saldo,'Total Resultado' As Label,IsNull(Sum(Debe),0) As Debitos,IsNull(Sum(Haber),0) as Creditos FROM Asiento,ItemAsiento,PlanCuenta 
-- WHERE Asiento.Empresa=@codEmpresa AND Asiento.Estado<>'Anulado' AND ItemAsiento.Empresa=Asiento.Empresa AND Asiento=IdAsiento AND Asiento.Ano=ItemAsiento.Ano AND Cuenta IN (SELECT nroCuentaTMP FROM CuentasTMP_Listados) AND 
-- IdCuenta=Cuenta AND FechaAsiento BETWEEN @FechaDesde AND @FechaHasta AND Cuenta Not LIKE '3%' 
-- ORDER BY Orden
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Datos_Generales
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- Create Proc [dbo].[Carga_Datos_Generales]
-- @intEmpresa smallint = Null
-- 
-- as
-- 
-- select Empresa ID_Empresa, FInicioEjercicio Fecha_Inicio_Ejercicio, fCierreEjercicio Fecha_Cierre_Ejercicio, 
-- FInicioEjercicioAnt Fecha_Inicio_Ejercicio_Anterior, FCierreEjercicioAnt Fecha_Cierre_Ejercicio_Anterior 
-- From DatosGenerales
-- Where Empresa = Case When @intEmpresa is null then Empresa Else @intEmpresa End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Alta_Datos_Generales
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- Create proc [dbo].[Alta_Datos_Generales]
-- @intEmpresa smallint,
-- @datFecha_Inicio_Ejercicio smalldatetime,
-- @datFecha_Fin_Ejercicio smalldatetime,
-- @datFecha_Inicio_Ejercicio_Ciclo_Anterior smalldatetime,
-- @datFecha_Fin_Ejercicio_Cliclo_Anterior smalldatetime
-- 
-- as
-- 
-- if exists (Select 1 From DatosGenerales Where Empresa = @intEmpresa)
-- 	Begin
-- 		Update DatosGenerales
-- 		Set FInicioEjercicio = @datFecha_Inicio_Ejercicio,
-- 		fCierreEjercicio = @datFecha_Fin_Ejercicio,
-- 		FInicioEjercicioAnt = @datFecha_Inicio_Ejercicio_Ciclo_Anterior,
-- 		FCierreEjercicioAnt = @datFecha_Fin_Ejercicio_Cliclo_Anterior
-- 		Where Empresa = @intEmpresa
-- 	End 
-- Else
-- 	Begin
-- 		Insert Into DatosGenerales
-- 		(Empresa, FInicioEjercicio, fCierreEjercicio, FInicioEjercicioAnt, FCierreEjercicioAnt)
-- 		Values
-- 		(@intEmpresa, @datFecha_Inicio_Ejercicio, @datFecha_Fin_Ejercicio, @datFecha_Inicio_Ejercicio_Ciclo_Anterior, @datFecha_Fin_Ejercicio_Cliclo_Anterior)
-- 	End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Elimina_ItemAsiento
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- create Proc [dbo].[Elimina_ItemAsiento] 
-- @intEmpresa tinyint,
-- @intAnio int,
-- @intAsiento int
-- 
-- as
-- 
-- Delete ItemAsiento 
-- Where Empresa = @intEmpresa
-- and Ano = @intAnio
-- and Asiento = @intAsiento
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Alta_Empresa
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE procedure [dbo].[Alta_Empresa]
-- @intEmpresa int,
-- @strRazonSocial varchar(30),
-- @strDomicilio varchar(30),
-- @strLocalidad varchar(100),
-- @intPais int,
-- @intProvincia int,
-- @strCUIT varchar(13)
-- 
-- as 
-- 
-- declare  @nextID int 
-- set @nextID=(select isnull(max(idempresa),0)+1 from empresa)
-- 
-- 
-- INSERT INTO Empresa (IdEmpresa, RazonSocial, Domicilio, Localidad, Pais, Provincia,CUIT)
-- VALUES (@nextID, @strRazonSocial, @strDomicilio, @strLocalidad, @intPais, @intProvincia, @strCUIT)
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Actualiza_Empresa
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- create procedure [dbo].[Actualiza_Empresa]
-- @intEmpresa int,
-- @strRazonSocial varchar(30),
-- @strDomicilio varchar(30),
-- @strLocalidad varchar(100),
-- @intPais int,
-- @intProvincia int,
-- @strCUIT varchar(13)
-- 
-- as 
-- 
-- update empresa set
-- RazonSocial=@strRazonSocial,
-- Domicilio=@strDomicilio,
-- Localidad=@strLocalidad,
-- Pais=@intPais,
-- Provincia=@intProvincia,
-- CUIT=@strCUIT
-- WHERE idEmpresa=@intEmpresa
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.cargar_datos_empresa
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- create procedure [dbo].[cargar_datos_empresa]
-- 
-- @intEmpresa int
-- 
-- as
-- 
-- select * from empresa where idempresa=@intEmpresa
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Listado_Plan_Cuentas
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- --Exec Carga_Listado_Plan_Cuentas @strID_Cuenta_Desde = '11110100'
-- 
-- Create Proc [dbo].[Carga_Listado_Plan_Cuentas] 
-- @strID_Cuenta_Desde varchar(8) = '',
-- @strID_Cuenta_Hasta varchar(8) = ''
-- 
-- as
-- 
-- if ( rtrim(ltrim(@strID_Cuenta_Desde)) = '' and rtrim(ltrim(@strID_Cuenta_Hasta)) = '') 
-- 	Begin
-- 		SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
-- 		FROM PlanCuenta 
-- 		--WHERE IdCuenta = Case When @strID_Cuenta = '' then IdCuenta Else @strID_Cuenta End
-- 		--and AceptaMovimiento = Case when @intAcepta_Movimientos is null Then AceptaMovimiento Else @intAcepta_Movimientos End
-- 		--and Nivel = Case When @intNivel is Null then Nivel Else @intNivel End
-- 		ORDER BY IdCuenta
-- 	End
-- Else
-- 	Begin
-- 
-- 		if ( rtrim(ltrim(@strID_Cuenta_Desde)) <> '' and rtrim(ltrim(@strID_Cuenta_Hasta)) = '') 
-- 			Begin
-- 				SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
-- 				FROM PlanCuenta 
-- 				WHERE IdCuenta >= @strID_Cuenta_Desde
-- 				ORDER BY IdCuenta
-- 			End
-- 		Else
-- 			Begin
-- 
-- 				if ( rtrim(ltrim(@strID_Cuenta_Desde)) = '' and rtrim(ltrim(@strID_Cuenta_Hasta)) <> '') 
-- 					Begin
-- 						SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
-- 						FROM PlanCuenta 
-- 						WHERE IdCuenta <= @strID_Cuenta_Hasta
-- 						ORDER BY IdCuenta
-- 					End
-- 				Else
-- 					Begin
-- 
-- 						if ( rtrim(ltrim(@strID_Cuenta_Desde)) <> '' and rtrim(ltrim(@strID_Cuenta_Hasta)) <> '') 
-- 							Begin
-- 								SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
-- 								FROM PlanCuenta 
-- 								WHERE IdCuenta Between @strID_Cuenta_Desde and @strID_Cuenta_Hasta
-- 								ORDER BY IdCuenta
-- 							End
-- 
-- 					End
-- 
-- 			End
-- 
-- 	End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Elimina_Plan_Cuentas
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- Create Proc [dbo].[Elimina_Plan_Cuentas]
-- @strID_Cuenta varchar(8)
-- 
-- as
-- 
-- Delete PlanCuenta where IdCuenta = @strID_Cuenta
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Actualiza_Plan_Cuentas
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE proc [dbo].[Actualiza_Plan_Cuentas]
-- @strID_Cuenta varchar(8),
-- @strDescripcion varchar(50),
-- @intNivel smallint,
-- @intAcepta_Movimientos smallint
-- 
-- as
-- 
-- if exists (Select 1 From PlanCuenta Where idCuenta = @strID_Cuenta) 
-- 	Begin
-- 		UPDATE PlanCuenta 
-- 		Set Descripcion = @strDescripcion,
-- 		Nivel = @intNivel,
-- 		AceptaMovimiento = @intAcepta_Movimientos 
-- 		Where IdCuenta = @strID_Cuenta 
-- 	End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Alta_Plan_Cuentas
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE proc [dbo].[Alta_Plan_Cuentas]
-- @strID_Cuenta varchar(8),
-- @strDescripcion varchar(50),
-- @intNivel smallint,
-- @intAcepta_Movimientos smallint
-- 
-- as
-- 
-- if exists (Select 1 From PlanCuenta Where IdCuenta = @strID_Cuenta) 
-- 	Begin
-- 
-- 		raiserror('El Plan de Cuenta ya existe', 20, -1) with log
-- 
-- 	End
-- 
-- Else 
-- 
-- 	Begin 
-- 
-- 		Insert Into PlanCuenta 
-- 		(IdCuenta, Descripcion, Nivel, AceptaMovimiento)
-- 		Values
-- 		(@strID_Cuenta, @strDescripcion, @intNivel, @intAcepta_Movimientos)
-- 
-- 	End
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Plan_Cuenta
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- Create proc [dbo].[Carga_Plan_Cuenta]
-- 
-- as
-- 
-- SELECT IdCuenta, Descripcion 
-- FROM PlanCuenta 
-- WHERE AceptaMovimiento=1 
-- ORDER BY IdCuenta
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Plan_Cuentas
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE Proc [dbo].[Carga_Plan_Cuentas]
-- @strID_Cuenta varchar(8) = '',
-- @intNivel smallint = Null,
-- @intAcepta_Movimientos smallint = Null
-- 
-- as
-- 
-- SELECT IdCuenta, Descripcion, isnull(Nivel, 0) Nivel, isnull(AceptaMovimiento , 0) Acepta_Movimiento 
-- FROM PlanCuenta 
-- WHERE IdCuenta = Case When @strID_Cuenta = '' then IdCuenta Else @strID_Cuenta End
-- and AceptaMovimiento = Case when @intAcepta_Movimientos is null Then AceptaMovimiento Else @intAcepta_Movimientos End
-- and Nivel = Case When @intNivel is Null then Nivel Else @intNivel End
-- ORDER BY IdCuenta
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Asientos
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE Proc [dbo].[Carga_Asientos]
-- @intEmpresa tinyint,
-- @strLista_Cuentas nvarchar(3999),
-- @datFecha_Desde smalldatetime,
-- @datFecha_Hasta smalldatetime
-- 
-- as
-- 
-- Declare
-- @strSql nvarchar(max),
-- @strFecha_Desde char(10),
-- @strFecha_Hasta char(10)
-- 
-- Set @strFecha_Desde = convert(char(16), convert(datetime, @datFecha_Desde) , 121)
-- Set @strFecha_Hasta = convert(char(16), convert(datetime, @datFecha_Hasta) , 121)
-- 
-- Set @strSql = ''
-- Set @strSql = 'SELECT a.Empresa, a.FechaAsiento, a.IdAsiento As NroAsiento, isnull(a.NIComprobante, 0) NIComprobante, b.LeyendaItem, isnull(b.Debe, 0) Debe, isnull(b.Haber, 0) Haber, 
-- b.Cuenta, c.Descripcion 
-- 
-- FROM Asiento a Inner Join ItemAsiento b   
-- On a.Empresa = b.Empresa 
-- AND a.Ano = b.Ano 
-- and a.iDAsiento = b.Asiento
-- 
-- Inner Join PlanCuenta c
-- On b.Cuenta = c.idCuenta
-- 
-- WHERE a.Empresa= 2
-- AND rtrim(ltrim(a.Estado)) <> ''Anulado ''
-- 
-- --AND Cuenta IN (''' + @strLista_Cuentas + ''') 
-- AND a.FechaAsiento BETWEEN ''' + @strFecha_Desde + ''' AND ''' + @strFecha_Hasta + ''' 
-- 
-- ORDER BY b.Cuenta, a.FechaAsiento, a.IdAsiento '
-- 
-- Exec sp_executesql @strSql
-- 
-- --print @strSql 
-- 
-- /*
-- --select * from Empresa 
-- select top 10 * from asiento
-- select top 10 * from ItemAsiento 
-- select top 1 * from plancuenta 
-- */
-- 
-- --sp_help asiento
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Combo_Empresas
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE proc [dbo].[Carga_Combo_Empresas]
-- 
-- as
-- 
-- select IdEmpresa, RazonSocial 
-- From Empresa
-- Order By RazonSocial
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.Carga_Estados_Asientos
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE proc [dbo].[Carga_Estados_Asientos]
-- 
-- as
-- 
-- Create Table #tmp
-- (
-- Descripcion varchar(15),
-- Orden tinyint
-- )
-- 
-- Insert Into #tmp (Descripcion, Orden) Values ('GENERADO', 1);
-- Insert Into #tmp (Descripcion, Orden) Values ('PENDIENTE', 2);
-- Insert Into #tmp (Descripcion, Orden) Values ('ANULADO', 3);
-- 
-- 
-- Select Descripcion
-- From #tmp
-- Order By Orden
-- $$
-- 
-- DELIMITER ;
-- 
-- ----------------------------------------------------------------------------
-- Routine sgi_pop.LPAD
-- ----------------------------------------------------------------------------
-- DELIMITER $$
-- 
-- DELIMITER $$
-- USE `sgi_pop`$$
-- CREATE FUNCTION [dbo].[LPAD]
-- (
--     @string VARCHAR(MAX), -- Initial string
--     @length INT,          -- Size of final string
--     @pad CHAR             -- Pad character
-- )
-- RETURNS VARCHAR(MAX)
-- AS
-- BEGIN
--     RETURN REPLICATE(@pad, @length - LEN(@string)) + @string;
-- END
-- $$
-- 
-- DELIMITER ;
-- SET FOREIGN_KEY_CHECKS = 1;
