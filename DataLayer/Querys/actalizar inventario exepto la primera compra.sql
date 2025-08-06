SET NOCOUNT ON;  
  
DECLARE @id_producto varchar(50),
	@fecha date=convert(date,'2020-08-23'),
	@cant decimal(18,3)=0,
	@cantAntes decimal(18,3)=0,
	@cantDespues decimal(18,3)=0,
	@conta int=0;  
  
PRINT '-------- Products Report --------';  
  
DECLARE productos_cursor CURSOR FOR   
select idProducto from tbProducto where fecha_crea<@fecha 
  
OPEN productos_cursor  
  
FETCH NEXT FROM productos_cursor  
INTO  @id_producto
  
WHILE @@FETCH_STATUS = 0  
BEGIN     


	print '--------------producto nuevo------------------------'
	set @conta=1;	
    PRINT 'producto id--> '+@id_producto  
  
    -- Declare an inner cursor based     
    -- on vendor_id from the outer cursor.  

    DECLARE compras_cursor CURSOR FOR   
	select cantidad 
	from tbDetalleCompras 
	where idProducto= @id_producto order by idCompra

 
  
    OPEN compras_cursor 
    FETCH NEXT FROM compras_cursor INTO @cant
		
  

    IF @@FETCH_STATUS <> 0   
        PRINT '         <<None>>'       
  	
    WHILE @@FETCH_STATUS = 0  
    BEGIN  
		  print @conta;
		if	@conta<>1 begin
			set @cantAntes=(select cantidad from tbInventario where idProducto=@id_producto);
			print 'actual-->'+convert(varchar(50),@cantAntes) ;
			print 'Aumentar-->'+convert(varchar(50),@cant) ;
			update tbInventario
			set cantidad=cantidad+@cant
			where idProducto=@id_producto	
			set @cantAntes=(select cantidad from tbInventario where idProducto=@id_producto);
			print 'despues-->'+convert(varchar(50),@cantAntes) ;
		end
		
		set @conta=@conta+1;
        FETCH NEXT FROM compras_cursor  INTO @cant 
    END  
  
    CLOSE compras_cursor  
    DEALLOCATE compras_cursor  
        -- Get the next vendor.  
    
	
	FETCH NEXT FROM productos_cursor     
    INTO @id_producto  
END   
CLOSE productos_cursor;  
DEALLOCATE productos_cursor; 