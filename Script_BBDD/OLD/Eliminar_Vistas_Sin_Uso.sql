select name from sysobjects where type='V'

DECLARE @table varchar(80)
declare @t_elimina varchar(400)

DECLARE DELETE_TABLES CURSOR FOR

	select name from sysobjects where type='V' 

	OPEN DELETE_TABLES  
	  
	FETCH NEXT FROM DELETE_TABLES INTO @table
	  
	WHILE @@FETCH_STATUS = 0  
		BEGIN  
			set @t_elimina=N'DROP VIEW dbo.['+@table+']';
			--drop procedure @sp_elimina;
			EXEC(@t_elimina);
			FETCH NEXT FROM DELETE_TABLES INTO @table  
		END   
	CLOSE DELETE_TABLES;  
DEALLOCATE DELETE_TABLES; 