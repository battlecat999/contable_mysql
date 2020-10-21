--exec cargar_datos_empresa 1
create procedure cargar_datos_empresa

@intEmpresa int

as

select * from empresa where idempresa=@intEmpresa

go