SET NOCOUNT ON;   
DECLARE @id nchar(13),@nombre nvarchar(160), @idPro varchar(50),@conta int=0;
PRINT '-------- VERICA CABYS --------';  
 
DECLARE pro_cursor CURSOR FOR   
SELECT codigoCabys, nombre, idProducto
FROM tbProducto  

OPEN pro_cursor  
  
FETCH NEXT FROM pro_cursor  
INTO  @id, @nombre,@idPro
  
WHILE @@FETCH_STATUS = 0  
BEGIN  

	IF(not EXISTS(SELECT * FROM tbCategoria9Cabys WHERE idCategoria9=@id))BEGIN
	
		PRINT CONCAT (@idPro, ' -> ', @nombre, ' cabys: ',@id);
		set @conta=@conta+1;
	END
	
	

	
        -- Get the next vendor.  
    FETCH NEXT FROM pro_cursor     
    INTO @id, @nombre,@idPro
END   
CLOSE pro_cursor;  
DEALLOCATE pro_cursor; 

	print concat('Cantidad: ',@conta);