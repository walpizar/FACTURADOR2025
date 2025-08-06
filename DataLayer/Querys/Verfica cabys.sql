SET NOCOUNT ON;  
  
DECLARE @cod nvarchar(50),@nombre nvarchar(160), @cabys nchar(13)='', @existe int=0, @noexiste int=0; 
  
PRINT '-------- VERIFICAR CABYS DE PRODUCTOS --------';  
 


update tbProducto
set codigoCabys= null
where codigoCabys= ''

DECLARE prod_cursor CURSOR FOR   
SELECT idProducto,nombre, codigoCabys  
FROM tbProducto  
where codigoCabys is not null
ORDER BY idProducto; 

  
OPEN prod_cursor  
  
FETCH NEXT FROM prod_cursor  
INTO  @cod,@nombre,@cabys ;

WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	if not Exists(select * from tbCategoria9Cabys where idCategoria9=@cabys)
	begin
	   set @noexiste= @noexiste+1;
	   PRINT CONCAT ('CABYS: ', CONVERT(VARCHAR(50),@cabys));
		PRINT CONCAT('PRODUCTO actualizado codigo: ',@cod,' nombre: ',@nombre);  
		PRINT('---------------------------------------------------------------------------')
		/*update tbProducto
		set codigoCabys=null
		where idProducto=@cod*/

	end;else
	begin 
		set @existe= @existe+1;
	end;
        -- Get the next vendor.  
    FETCH NEXT FROM prod_cursor     
    INTO  @cod,@nombre,@cabys  
END   
CLOSE prod_cursor;  
DEALLOCATE prod_cursor; 
PRINT ('--------------------- TOTAL ----------------------------------');
 PRINT CONCAT ('CABYS EXISTENTES: ', CONVERT(VARCHAR(50),@existe));
  PRINT CONCAT ('CABYS MODIFICADOS INEXISTENTES: ', CONVERT(VARCHAR(50),@noexiste));