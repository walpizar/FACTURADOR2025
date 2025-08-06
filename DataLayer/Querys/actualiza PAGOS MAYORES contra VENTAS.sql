SET NOCOUNT ON;  
  
DECLARE @id int, @tipoId int,@tipoVenta int, @fecha datetime, @total decimal(18, 5);
  
PRINT '-------- CURSOR PAGOS MAYORES A FACTURAS --------';  
 
DECLARE doc_cursor CURSOR FOR   
SELECT d.id, dd.idTipoDoc, d.tipoVenta,convert(date,d.fecha) as fecha, SUM(dd.totalLinea) total
FROM tbDocumento d inner join tbDetalleDocumento dd
on d.id=dd.idDoc and d.tipoDocumento=dd.idTipoDoc
group by d.id, dd.idTipoDoc, d.tipoVenta, d.fecha
having fecha>convert(date,'2021-03-01')

  
OPEN doc_cursor  
  
FETCH NEXT FROM doc_cursor  
INTO  @id, @tipoId,@tipoVenta, @fecha, @total
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
	declare @pagaTotal decimal(18, 5)=0;
	if Exists(select * from tbPagos where idDoc= @id AND tipoDoc=@tipoId)
	begin


		set @pagaTotal= (select sum(monto) from tbPagos where idDoc= @id AND tipoDoc=@tipoId);

		if(@pagaTotal>@total)
		begin 
			declare @cantPagos int;
			set @cantPagos= (select count(*) from tbPagos where idDoc= @id AND tipoDoc=@tipoId);
			PRINT CONCAT('id :', @id, ' tipoId:',@tipoId, ' TipoVenta:',@tipoVenta,' Fecha:',@fecha, ' Total: ',@total, ' Pago:', @pagaTotal, ' CantidadPAgos:',  @cantPagos);		
	
	
			 if(@cantPagos=1)
			 begin
				 update tbPagos
				 set monto= @total
				 where idDoc= @id AND tipoDoc=@tipoId
				 PRINT 'PAGO ACTUALIZADO';
			 end;
		end;
		
	end;
   -- Get the next vendor.  
    FETCH NEXT FROM doc_cursor     
    INTO  @id, @tipoId,@tipoVenta, @fecha, @total
END   
CLOSE doc_cursor;  
DEALLOCATE doc_cursor;  