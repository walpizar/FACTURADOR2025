SET NOCOUNT ON;  
  
DECLARE @cod nvarchar(50), @conta int=0;
  
PRINT '-------- INVENTARIO --------';  
 
DECLARE prod_cursor CURSOR FOR   
SELECT idProducto  
FROM tbProducto  
ORDER BY idProducto; 

  
OPEN prod_cursor  
  
FETCH NEXT FROM prod_cursor  
INTO  @cod 
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	declare @antigua varchar(50)=0, @nueva varchar(50)=0;
	if Exists(select * from tempo where idProducto=@cod)
	begin
	
	
		set @antigua= (select cantidad from tbInventario where idProducto=@cod);
		set  @nueva= (select cantidad from tempo where idProducto=@cod);
		
		if(@nueva <>@antigua )
		begin 
			set @conta= @conta+1;
			PRINT CONCAT('PRODUCTO: ',@cod,' CANTIDADA ANTIGUA: ',@antigua,' CANTIDAD NUEVA: ',@nueva);
				
			/*update tbInventario
			set cantidad= @nueva,
			fecha_ult_mod= GETDATE()
			where idProducto=@cod	*/	

		end;
		
	
		
	end;
        -- Get the next vendor.  
    FETCH NEXT FROM prod_cursor     
    INTO @cod
END   
CLOSE prod_cursor;  
DEALLOCATE prod_cursor; 
PRINT CONCAT('ACTUALIZADOS: ',@conta);