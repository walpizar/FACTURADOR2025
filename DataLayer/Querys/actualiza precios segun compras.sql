SET NOCOUNT ON;  
  
DECLARE @idProducto nvarchar(50), @cantidad decimal(18, 3),
@Linea decimal(18, 5), @utilidad decimal(18, 3); 
  
PRINT '-------- ACTUALIZAR --------';  
 
DECLARE prod_cursor CURSOR FOR   
SELECT idProducto, cantidad, montoTotalLinea, utilidad  
FROM tbDetalleCompras  
ORDER BY idProducto; 

  
OPEN prod_cursor  
  
FETCH NEXT FROM prod_cursor  
INTO  @idProducto, @cantidad,@Linea, @utilidad
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	declare @precioCosto1 decimal(18, 5)=0;
	if Exists(select * from tbProducto where idProducto= @idProducto)
	begin
		PRINT CONCAT('acutalizar : ', @idProducto);		
		set @precioCosto1 =(@Linea/@cantidad)/(((select valor from tbImpuestos where id=(select idTipoImpuesto from tbProducto  where idProducto= @idProducto))/100)+1);
			PRINT CONCAT('precioCosto : ', @precioCosto1);	
		update tbProducto
		set precio_real= @precioCosto1,
		utilidad1Porc=@utilidad,
		precioUtilidad1= @precioCosto1*((@utilidad/100)+1),
		precioUtilidad2= @precioCosto1*(((select utilidad2Porc from tbProducto where idProducto= @idProducto)/100)+1),
		precioUtilidad3=  @precioCosto1*(((select utilidad3Porc from tbProducto where idProducto= @idProducto)/100)+1),
		precioVenta1=  (@precioCosto1*((@utilidad/100)+1))*(((select valor from tbImpuestos where id=(select idTipoImpuesto from tbProducto  where idProducto= @idProducto))/100)+1),
		precioVenta2=  (@precioCosto1*(((select utilidad2Porc from tbProducto where idProducto= @idProducto)/100)+1))*(((select valor from tbImpuestos where id=(select idTipoImpuesto from tbProducto  where idProducto= @idProducto))/100)+1),
		precioVenta3=  (@precioCosto1*(((select utilidad3Porc from tbProducto where idProducto= @idProducto)/100)+1))*(((select valor from tbImpuestos where id=(select idTipoImpuesto from tbProducto  where idProducto= @idProducto))/100)+1)
		where idProducto=@idProducto	
		
	end;
   
   
   -- Get the next vendor.  
    FETCH NEXT FROM prod_cursor     
    INTO @idProducto, @cantidad,@Linea, @utilidad
END   
CLOSE prod_cursor;  
DEALLOCATE prod_cursor;  