SET NOCOUNT ON;   
DECLARE @id varchar(50), @precioVenta decimal(18,5),   @precio decimal(18,5)=0;
  
PRINT '-------- ACTUALIZA PRECIOS PRODUCTOS --------';  
 
DECLARE pro_cursor CURSOR FOR   
SELECT idProducto,
FROM tbProducto  

OPEN pro_cursor  
  
FETCH NEXT FROM pro_cursor  
INTO  @id,@precioVenta
  
WHILE @@FETCH_STATUS = 0  
BEGIN  

	set @precio= (select precioVenta1 from tbProducto where idProducto=@id)/1.13;
	update tbProducto
	set precio_real=  @precio, precioUtilidad1=@precio, precioUtilidad2=@precio, precioUtilidad3=@precio,
	utilidad1Porc=0, utilidad2Porc=0,utilidad3Porc=0, idTipoImpuesto=8, esExento=0, precioVenta2=@precioVenta, precioVenta3=@precioVenta
	where idProducto=@id
	
        -- Get the next vendor.  
    FETCH NEXT FROM pro_cursor     
    INTO @id,@precioVenta
END   
CLOSE pro_cursor;  
DEALLOCATE pro_cursor; 