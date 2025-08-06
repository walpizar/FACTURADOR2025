SET NOCOUNT ON;  
  
DECLARE @cod nvarchar(50), @idDetalle int, @idOrden int
  

 
DECLARE detalle_cursor CURSOR FOR   
SELECT idOrdenCompra,idDetalle, idProducto
FROM tbDetalleOrdenCompra ;

  
OPEN detalle_cursor  
  
FETCH NEXT FROM detalle_cursor  
INTO @idOrden,@idDetalle,@cod 
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	update tbDetalleOrdenCompra
	set precioCosto=(select precio_real from tbProducto where idProducto=@cod)
	where idOrdenCompra= @idOrden and idDetalle= @idDetalle
	
    FETCH NEXT FROM detalle_cursor      
    INTO @idOrden, @idDetalle,@cod 
END   
CLOSE detalle_cursor ;  
DEALLOCATE detalle_cursor ;    