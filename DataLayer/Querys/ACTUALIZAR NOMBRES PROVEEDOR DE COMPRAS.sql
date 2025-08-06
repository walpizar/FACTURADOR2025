ALTER TABLE tbDetalleCompras add  nomenclatura varchar(5)
go
ALTER TABLE tbCompras add  nombreProveedor varchar(100)
go
ALTER TABLE tbCompras DROP CONSTRAINT FK_tbCompras_tbProveedores;
go
SET NOCOUNT ON;   
DECLARE @id int, @tipoDoc int, @idProveedor nchar(30), @tipoId int; 
  
PRINT '-------- ACTUALIZA NOMBREPROVEEDOR COMPRAS --------';  
 
DECLARE compras_cursor CURSOR FOR   
SELECT id,tipoDoc, idProveedor, tipoIdEmpresa  
FROM tbCompras  
where nombreProveedor is null or nombreProveedor=''
  
OPEN compras_cursor  
  
FETCH NEXT FROM compras_cursor  
INTO  @id,@tipoDoc,@idProveedor,@tipoId
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	if Exists(select * from tbProveedores where id=@idProveedor and tipoId=@tipoId)
	begin
		update tbCompras
		set nombreProveedor=(select concat(RTRIM (pre.nombre),' ',RTRIM (pre.apellido1),' ',RTRIM (pre.apellido2)) from tbProveedores pro
		inner join tbPersona pre on pro.id=pre.identificacion and pro.tipoId=pre.tipoId where pro.id=@idProveedor and pro.tipoId=@tipoId)
		where  id=@id and tipoDoc=@tipoDoc

	end;
        -- Get the next vendor.  
    FETCH NEXT FROM compras_cursor     
    INTO @id,@tipoDoc,@idProveedor,@tipoId 
END   
CLOSE compras_cursor;  
DEALLOCATE compras_cursor;  
go

