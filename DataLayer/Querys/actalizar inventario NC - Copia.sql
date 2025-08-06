SET NOCOUNT ON;  
  
DECLARE @cod nvarchar(50), @cantidad decimal =0, @contar int=0; 
  
PRINT '-------- DEVOLVER INVENTARIO NC --------';  
 
DECLARE prod_cursor CURSOR FOR   
SELECT idProducto, cantidad
FROM tbDetalleDocumento
where idDoc=31 and idTipoDoc=3
ORDER BY idProducto; 

  
OPEN prod_cursor  
  
FETCH NEXT FROM prod_cursor  
INTO  @cod, @cantidad
  
WHILE @@FETCH_STATUS = 0  
BEGIN 
	declare @CANT decimal=0;
	print('Producto: '+@cod);
	print('Cantidad a disminuir: ');
	print @cantidad;
	set @CANT=(select cantidad from tbInventario where idProducto=@cod);
	print('Cantidad antes: ');
	print @CANT;
	
	update tbInventario
	set cantidad=cantidad-@cantidad
	where idProducto=@cod

	set @CANT=(select cantidad from tbInventario where idProducto=@cod);
    print('Cantidad despues: ');
	print(@CANT);
        -- Get the next vendor.  
    FETCH NEXT FROM prod_cursor     
    INTO @cod, @cantidad
END   
CLOSE prod_cursor;  
DEALLOCATE prod_cursor;  
