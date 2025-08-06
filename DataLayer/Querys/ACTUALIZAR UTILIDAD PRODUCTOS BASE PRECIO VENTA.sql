SET NOCOUNT ON;   
DECLARE @id varchar(50), @precioVenta1 decimal(18,5), @precioVenta2 decimal(18,5), @precioVenta3 decimal(18,5),  @precioReal decimal(18,5)=0,
@valorImp decimal(18,5),@imp int, @utilidad1 decimal(18,5),  @utilidad2 decimal(18,5),  @utilidad3 decimal(18,5), @uti decimal(18,5), @montoUtil1Antes decimal(18,5),
@montoUtil1 decimal(18,5), @montoUtil2 decimal(18,5), @montoUtil3 decimal(18,5),
@cont int=0;
  
PRINT '-------- ACTUALIZA UTILIDAD PRODUCTOS --------';  
 
DECLARE pro_cursor CURSOR FOR   
SELECT idProducto, precio_real, precioVenta1, precioVenta2, precioVenta3, idTipoImpuesto
FROM tbProducto  

OPEN pro_cursor  
  
FETCH NEXT FROM pro_cursor  
INTO  @id,@precioReal,@precioVenta1, @precioVenta2, @precioVenta3,@imp
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	set @valorImp= ((select valor from tbImpuestos where id=@imp)/100)+1;
	set @uti= ((select utilidad1Porc  from tbProducto where idProducto=@id)/100)+1;

	set @montoUtil1Antes=@precioReal*@uti;

	set @montoUtil1= @precioVenta1/@valorImp;
	set @montoUtil2= @precioVenta2/@valorImp;
	set @montoUtil3= @precioVenta3/@valorImp;

	
	
	IF @montoUtil1Antes <> @montoUtil1
	begin

			print concat('PRODUCTO: ',@id);
			print concat('Monto utilidad 1 antes: ',@montoUtil1Antes, ' - Monto utilidad 1 despues: ', @montoUtil1);
			print concat('Imp: ',@valorImp,' PrecioReal: ', @precioReal,' precioVenta1: ', @precioVenta1,' precioVenta2: ', @precioVenta2,' precioVenta3: ', @precioVenta3);
	
			
			set @utilidad1= ((@montoUtil1/ @precioReal)*100)-100;
			set @utilidad2= ((@montoUtil2/ @precioReal)*100)-100;
			set @utilidad3= ((@montoUtil3/ @precioReal)*100)-100;

			update tbProducto
			set utilidad1Porc= @utilidad1, utilidad2Porc=@utilidad2, utilidad3Porc=@utilidad3, precioUtilidad1=@montoUtil1,precioUtilidad2=@montoUtil2, precioUtilidad3=@montoUtil3
			where idProducto=@id;
			
			print 'DESPUES:';
			print concat('precioUtilidad1: ', @montoUtil1,' precioUtilidad2: ', @montoUtil2,' precioUtilidad3: ', @montoUtil3);
			print concat('utilidad1 %: ', @utilidad1,' utilidad2 %: ', @utilidad2,' utilidad3 %: ', @utilidad3);
			print '------------------------------------------'
			set @cont=@cont+1;
	end;
	
        -- Get the next vendor.  
    FETCH NEXT FROM pro_cursor     
    INTO  @id,@precioReal,@precioVenta1, @precioVenta2, @precioVenta3,@imp
END   
CLOSE pro_cursor;  
DEALLOCATE pro_cursor; 
print concat('REGISTROS MODIFICADOS: ',@cont)