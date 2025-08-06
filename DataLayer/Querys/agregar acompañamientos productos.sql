SET NOCOUNT ON;   
DECLARE @id varchar(50), @idAco varchar(50)
  
PRINT '-------- ACOMPAÑAMIETOS --------';  
 
DECLARE pro_cursor CURSOR FOR   
SELECT idProducto
FROM tbProducto where cocina= 1  

OPEN pro_cursor  


  
FETCH NEXT FROM pro_cursor  
INTO  @id
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
		print @id;

		DECLARE aco_cursor CURSOR FOR  
		select id from tbAcompanamiento where estado=1
		OPEN aco_cursor

		FETCH NEXT FROM aco_cursor 
		INTO  @idAco
		WHILE @@FETCH_STATUS = 0  
		BEGIN

			INSERT INTO tbProdutoAcompa values(@idAco, @id);

			FETCH NEXT FROM aco_cursor     
			INTO @idAco
		END
		CLOSE aco_cursor;  
		DEALLOCATE aco_cursor;
        -- Get the next vendor.  
    FETCH NEXT FROM pro_cursor     
    INTO @id

END   
CLOSE pro_cursor;  
DEALLOCATE pro_cursor; 

/*delete from tbProdutoAcompa*/