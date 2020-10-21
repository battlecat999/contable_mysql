create Proc [dbo].[Elimina_ItemAsiento] 
@intEmpresa tinyint,
@intAnio int,
@intAsiento int

as

Delete ItemAsiento 
Where Empresa = @intEmpresa
and Ano = @intAnio
and Asiento = @intAsiento
