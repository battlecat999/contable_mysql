alter procedure Alta_Empresa
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
