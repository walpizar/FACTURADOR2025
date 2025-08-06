SET NOCOUNT ON;  
  
DECLARE @id int,
	@tipo int,
	@total decimal(18,5)=0, 
	@abonos decimal(18,5)=0

  
PRINT '-------- DOCUMENTOS REPARAR --------';  
  
DECLARE documentos_cursor CURSOR FOR   
select d.id, d.tipoDocumento,sum(dt.totalLinea)as total from tbDocumento d 
inner join tbDetalleDocumento dt 
on d.id =dt.idDoc and d.tipoDocumento=dt.idTipoDoc
where d.estadoFactura=2
group by d.id, d.tipoDocumento
  
OPEN documentos_cursor  
  
FETCH NEXT FROM documentos_cursor  
INTO  @id,@tipo,@total
  
WHILE @@FETCH_STATUS = 0  
BEGIN     

	set @abonos=0;

	
	select @abonos=s.montoSuma from (select d.id, d.tipoDocumento, sum(p.monto)as montoSuma from tbDocumento d 
	inner join tbPagos p on d.id =p.idDoc and d.tipoDocumento=p.tipoDoc
	where d.id=@id and d.tipoDocumento=@tipo
	group by d.id, d.tipoDocumento)s

	IF (@total-@abonos)<1 and (@total-@abonos)>0 
	BEGIN
	  print('--------------');
	
	    print('PRODUCTO: Id: '+ CAST(@id AS VARCHAR(50))+ ', tipo: '+CAST(@tipo AS VARCHAR(50))+ ', MONTO FACTURA: '+CAST(@total AS VARCHAR(50)) );
	
		print('MONTO PAGO: '+ CAST(@abonos AS VARCHAR(50)) );
		print('diferencia: '+ CAST( (@total-@abonos) AS VARCHAR(50)) );

		
		update tbPagos
		set monto= monto+(@total-@abonos)
		where idAbono= (select max(idAbono) from tbPagos a
		where a.idDoc=@id and a.tipoDoc=@tipo);

		update tbDocumento
		set estadoFactura=1
		where id=@id and tipoDocumento=@tipo 
	 
	END
	ELSE if (@total-@abonos) < 0
	begin 
	    print('--------------');
		PRINT('MAS ABONOS');
	   print('PRODUCTO: Id: '+ CAST(@id AS VARCHAR(50))+ ', tipo: '+CAST(@tipo AS VARCHAR(50))+ ', MONTO FACTURA: '+CAST(@total AS VARCHAR(50)) );
		print('diferencia - 0: '+ CAST( (@total-@abonos) AS VARCHAR(50)) );
		
		update tbPagos
		set monto= monto-(@total-@abonos)
		where idAbono= (select max(idAbono) from tbPagos a
		where a.idDoc=@id and a.tipoDoc=@tipo);

		update tbDocumento
		set estadoFactura=1
		where id=@id and tipoDocumento=@tipo 
	end;
	FETCH NEXT FROM documentos_cursor     
	INTO @id,@tipo,@total;
END   
CLOSE documentos_cursor;  
DEALLOCATE documentos_cursor; 