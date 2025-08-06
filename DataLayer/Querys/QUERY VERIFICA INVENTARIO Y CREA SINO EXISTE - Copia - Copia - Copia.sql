SET NOCOUNT ON;  
  
DECLARE @cod nvarchar(50), @contar int=0; 
  
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
	
	if not Exists(select * from tbInventario where idProducto=@cod)
	begin
		PRINT CONCAT('INSERTADO PRODUCTO: ',@cod);  
		insert into tbInventario values(@cod,0,0,0,1,'12/12/2020','12/12/2020','admin','admin');
	end;
        -- Get the next vendor.  
    FETCH NEXT FROM prod_cursor     
    INTO @cod
END   
CLOSE prod_cursor;  
DEALLOCATE prod_cursor;  