create procedure Actualiza_Empresa
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