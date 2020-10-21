USE [Seipac]
GO
/****** Objeto:  StoredProcedure [dbo].[Carga_Listado_Subdiario_Compras]    Fecha de la secuencia de comandos: 08/15/2019 18:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Carga_Listado_Subdiario_Compras 1, '20190801', '20190816'

ALTER Proc [dbo].[Carga_Listado_Subdiario_Compras]
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