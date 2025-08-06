SET NOCOUNT ON;   
DECLARE @id1 nchar(13), @id2 nchar(13),@id3 nchar(13),@id4 nchar(13),
@id5 nchar(13),@id6 nchar(13),@id7 nchar(13),@id8 nchar(13), @id9 nchar(13),
 @nombre nchar(1000),@impuesto int
PRINT '-------- VERICA CABYS --------';  
 
DECLARE pro_cursor CURSOR FOR   
SELECT idCategoria1,idCategoria2,idCategoria3,idCategoria4,idCategoria5,
idCategoria6,idCategoria7,idCategoria8,idCategoria9,nombre, impuesto
FROM TempCompleto  

OPEN pro_cursor  
  
FETCH NEXT FROM pro_cursor  
INTO  @id1,@id2,@id3,@id4,@id5,@id6,@id7,@id8,@id9,@nombre,@impuesto
  
WHILE @@FETCH_STATUS = 0  
BEGIN  

	IF(not EXISTS(SELECT * FROM tbCategoria9Cabys WHERE idCategoria9=@id9))BEGIN
		insert into cabysNuevos values(@id1,@id2,@id3,@id4,@id5,@id6,@id7,@id8,@id9,@nombre,@impuesto)
	END
	
	
	
        -- Get the next vendor.  
    FETCH NEXT FROM pro_cursor     
    INTO @id1,@id2,@id3,@id4,@id5,@id6,@id7,@id8,@id9,@nombre,@impuesto
END   
CLOSE pro_cursor;  
DEALLOCATE pro_cursor; 