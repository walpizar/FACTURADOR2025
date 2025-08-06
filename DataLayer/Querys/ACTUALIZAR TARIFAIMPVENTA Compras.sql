
SET NOCOUNT ON;   
DECLARE @id int, @tarifa decimal(18,2); 
  
PRINT '-------- ACTUALIZA tarifaImpVenta COMPRAS --------';  
 
DECLARE compras_cursor CURSOR FOR   
SELECT idDetalle, tarifaImp 
FROM tbDetalleCompras  
where tarifaImpVenta is null and tarifaImp is not null
  
OPEN compras_cursor  
  
FETCH NEXT FROM compras_cursor  
INTO  @id,@tarifa;
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
		update tbDetalleCompras
		set tarifaImpVenta= @tarifa
		where  idDetalle=@id 

       
    FETCH NEXT FROM compras_cursor     
    INTO  @id,@tarifa;
END   
CLOSE compras_cursor;  
DEALLOCATE compras_cursor;  
go

