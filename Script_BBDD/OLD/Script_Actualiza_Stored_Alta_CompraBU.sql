alter Proc [dbo].[Alta_CompraBU]
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











