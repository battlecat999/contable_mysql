
DECLARE @procedure varchar(80)
declare @sp_elimina varchar(400)

DECLARE DELETE_SP CURSOR FOR
	--SELECT name AS procedure_name   
	--    ,SCHEMA_NAME(schema_id) AS schema_name  
	--    ,type_desc  
	--    ,create_date  
	--    ,modify_date
	SELECT name AS procedure_name   
	FROM sys.procedures
	WHERE name not like 'dt_%'; 

	OPEN DELETE_SP  
	  
	FETCH NEXT FROM DELETE_SP INTO @procedure
	  
	WHILE @@FETCH_STATUS = 0  
		BEGIN  
			set @sp_elimina=N'DROP PROCEDURE dbo.['+@procedure+']';
			--drop procedure @sp_elimina;
			EXEC(@sp_elimina);
			FETCH NEXT FROM DELETE_SP INTO @procedure  
		END   
	CLOSE DELETE_SP;  
DEALLOCATE DELETE_SP;  
