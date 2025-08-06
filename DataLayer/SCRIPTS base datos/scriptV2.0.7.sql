alter table tbProducto ALTER COLUMN utilidad1Porc DECIMAL (18, 5);   
go
alter table tbProducto ALTER COLUMN utilidad2Porc DECIMAL (18, 5);  
go
alter table tbProducto ALTER COLUMN utilidad3Porc DECIMAL (18, 5);  
go
IF COL_LENGTH('dbo.tbDocumento', 'estadoCorreo') IS NULL
BEGIN
    ALTER TABLE tbDocumento add estadoCorreo int;	
END
go
update tbDocumento 	set estadoCorreo=0 where estadoCorreo is null;
GO
update tbDocumento 	set estadoCorreo=1 where notificarCorreo=1;
GO
IF COL_LENGTH('dbo.tbParametrosEmpresa', 'promociones') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add promociones bit;	
END
go
update tbParametrosEmpresa 	set promociones=0 where promociones is null;
GO
IF COL_LENGTH('dbo.tbParametrosEmpresa', 'etiquetas') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add etiquetas bit;	
END
go
update tbParametrosEmpresa 	set etiquetas=0 where etiquetas is null;
GO
IF COL_LENGTH('dbo.tbParametrosEmpresa', 'aprobarEliminar') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add aprobarEliminar bit;	
END
go
update tbParametrosEmpresa 	set aprobarEliminar=0 where aprobarEliminar is null;
GO
IF COL_LENGTH('dbo.tbParametrosEmpresa', 'cierreCajaAdmin') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add cierreCajaAdmin bit;	
END
go
update tbParametrosEmpresa 	set cierreCajaAdmin=0 where cierreCajaAdmin is null;
go
IF COL_LENGTH('dbo.tbParametrosEmpresa', 'precioVariable') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add precioVariable bit;	
END
go
update tbParametrosEmpresa 	set precioVariable=0 where precioVariable is null;
go
IF not exists(select * from INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='montoOtroImp' AND TABLE_NAME='tbDetalleCompras') begin 
	ALTER TABLE tbDetalleCompras add  montoOtroImp decimal(18,5)
end;
go
UPDATE tbDetalleCompras
set montoOtroImp=0
where montoOtroImp is null
go
IF not exists(select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='tbPromociones') begin 
CREATE TABLE [dbo].[tbPromociones](
	id int IDENTITY(1,1) NOT NULL primary key,
	fechaIncio datetime NOT NULL,	
	fechaCierre datetime NOT NULL,
	idProducto varchar(50) NOT NULL,
	usuario_crea nchar(30) NOT NULL,
	usuario_ult_mod nchar(30) NOT NULL,
	descuento decimal(18, 5) not null,
	fecha_crea datetime NOT NULL,
	fecha_ult_mod datetime NOT NULL,
	estado bit NOT NULL  
	) 
end;
go
IF not exists(select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='fk_promociones_producto') begin 
	alter table tbPromociones add constraint fk_promociones_producto foreign key (idProducto) references tbProducto (idProducto)
end;
go
IF OBJECT_ID('sp_promocionesPorEstado', 'P') IS NOT NULL 
  DROP PROCEDURE sp_promocionesPorEstado;
go

create procedure sp_promocionesPorEstado
@tipo int=3
as
BEGIN

if @tipo=0
begin
SELECT        (((pro.precio_real*(((pro.utilidad1Porc)/100)+1))-((pro.precio_real*(((pro.utilidad1Porc)/100)+1))*(p.descuento/100)))*((dbo.tbImpuestos.valor/100)+1)) as promo,IIF(p.fechaCierre<GETDATE(), 'VENCIDA', IIF(p.fechaIncio>GETDATE(), 'PENDIENTE', 'ACTIVA')) as estado,pro.idProducto, pro.nombre, pro.precioVenta1, pro.utilidad1Porc, pro.precio_real, p.fechaIncio, p.fechaCierre, 
                         p.descuento, p.id, dbo.tbImpuestos.valor as imp
FROM            dbo.tbPromociones p INNER JOIN
                         dbo.tbProducto pro ON pro.idProducto = p.idProducto INNER JOIN
                         dbo.tbImpuestos ON pro.idTipoImpuesto = dbo.tbImpuestos.id
	


end
else if @tipo=1/*PROMOCIONES ACTIVAS*/
begin 
SELECT        (((pro.precio_real*(((pro.utilidad1Porc)/100)+1))-((pro.precio_real*(((pro.utilidad1Porc)/100)+1))*(p.descuento/100)))*((dbo.tbImpuestos.valor/100)+1)) as promo,IIF(p.fechaCierre<GETDATE(), 'VENCIDA', IIF(p.fechaIncio>GETDATE(), 'PENDIENTE', 'ACTIVA')) as estado,pro.idProducto, pro.nombre, pro.precioVenta1, pro.utilidad1Porc, pro.precio_real, p.fechaIncio, p.fechaCierre, 
                         p.descuento, p.id, dbo.tbImpuestos.valor as imp
FROM            dbo.tbPromociones p INNER JOIN
                         dbo.tbProducto pro ON pro.idProducto = p.idProducto INNER JOIN
                         dbo.tbImpuestos ON pro.idTipoImpuesto = dbo.tbImpuestos.id
	where getdate()>= p.fechaIncio and getdate()<=p.fechaCierre

end
else if @tipo=2 /*PROMOCIONES PENDIENTES*/
begin 
SELECT        (((pro.precio_real*(((pro.utilidad1Porc)/100)+1))-((pro.precio_real*(((pro.utilidad1Porc)/100)+1))*(p.descuento/100)))*((dbo.tbImpuestos.valor/100)+1)) as promo,IIF(p.fechaCierre<GETDATE(), 'VENCIDA', IIF(p.fechaIncio>GETDATE(), 'PENDIENTE', 'ACTIVA')) as estado,pro.idProducto, pro.nombre, pro.precioVenta1, pro.utilidad1Porc, pro.precio_real, p.fechaIncio, p.fechaCierre, 
                         p.descuento, p.id, dbo.tbImpuestos.valor as imp
FROM            dbo.tbPromociones p INNER JOIN
                         dbo.tbProducto pro ON pro.idProducto = p.idProducto INNER JOIN
                         dbo.tbImpuestos ON pro.idTipoImpuesto = dbo.tbImpuestos.id
	where getdate()< p.fechaIncio 
end
else if @tipo=3 /*PROMOCIONES VENCIDAS*/
begin 
SELECT        (((pro.precio_real*(((pro.utilidad1Porc)/100)+1))-((pro.precio_real*(((pro.utilidad1Porc)/100)+1))*(p.descuento/100)))*((dbo.tbImpuestos.valor/100)+1)) as promo,IIF(p.fechaCierre<GETDATE(), 'VENCIDA', IIF(p.fechaIncio>GETDATE(), 'PENDIENTE', 'ACTIVA')) as estado,pro.idProducto, pro.nombre, pro.precioVenta1, pro.utilidad1Porc, pro.precio_real, p.fechaIncio, p.fechaCierre, 
                         p.descuento, p.id, dbo.tbImpuestos.valor as imp
FROM            dbo.tbPromociones p INNER JOIN
                         dbo.tbProducto pro ON pro.idProducto = p.idProducto INNER JOIN
                         dbo.tbImpuestos ON pro.idTipoImpuesto = dbo.tbImpuestos.id
	where getdate()> p.fechaCierre

end

END
GO
IF not exists(select * from tbTipoDocumento where id=25) begin 	
	INSERT INTO [dbSISSODINA].[dbo].tbTipoDocumento VALUES (25,'ORDEN DE COMPRA')
end;
GO
IF not exists(select * from tbTipoDocumento where id=26) begin 	
	INSERT INTO [dbSISSODINA].[dbo].tbTipoDocumento VALUES (26,'GASTOS')
end;
GO
IF not exists(select * from tbTipoDocumento where id=27) begin 	
	INSERT INTO [dbSISSODINA].[dbo].tbTipoDocumento VALUES (27,'NOTA DE CRÉDITO GASTOS')
end;
GO
IF OBJECT_ID('spReporteComprasPorFechaEsp', 'P') IS NOT NULL 
  DROP PROCEDURE spReporteComprasPorFechaEsp;
go
GO
create PROCEDURE [dbo].[spReporteComprasPorFechaEsp]
@diaInicio datetime, 
@diaFin datetime,
@tipo int
AS
BEGIN
	if @tipo!=26
	begin
		SELECT        TOP (100) PERCENT T.id, T.consecutivo, T.numFactura, T.tipoCambio, T.fecha, T.fechaCompra, T.idProveedor,t.nombreProveedor, T.tipoIdProveedor, T.plazo, T.claveRef, T.cantidad, T.precio, T.montoTotal, T.montoTotaDesc, T.tarifaImp, 
								 T.montoTotalImp, T.montoTotalExo, T.montoTotalLinea, dbo.tbTipoDocumento.nombre AS documento, dbo.tbTipoMoneda.siglas, dbo.tbActividades.nombreAct, T.codigoActividadEmpresa AS CodActividad, T.tipoCompra, 
								 dbo.tbTipoVenta.nombre AS tipoCompra, dbo.tbTipoPago.nombre AS tipoPago, @diaInicio AS fechaIncio, @diaFin AS FechaFin, @tipo as tipoDoc
		FROM            dbo.tbTipoMoneda INNER JOIN
									 (SELECT        doc.id, doc.tipoDoc, doc.codigoActividadEmpresa, doc.consecutivo, doc.numFactura, doc.tipoMoneda, doc.tipoCambio, doc.fecha, doc.fechaCompra, doc.idProveedor, doc.nombreProveedor, doc.tipoIdProveedor, doc.tipoCompra, 
																 doc.plazo, doc.tipoPago, doc.claveRef, dt.idProducto, dt.numLinea, dt.cantidad, dt.precio, dt.montoTotal, dt.montoTotaDesc, dt.tarifaImp, dt.montoTotalImp, dt.montoTotalExo, dt.montoTotalLinea
									   FROM            dbo.tbCompras AS doc INNER JOIN
																 dbo.tbDetalleCompras AS dt ON doc.id = dt.idCompra AND doc.tipoDoc = dt.TipoCompra
									   WHERE        (doc.estado = 1) AND (doc.tipoDoc IN (21))
									   UNION ALL
									   SELECT        doc.id, doc.tipoDoc, doc.codigoActividadEmpresa, doc.consecutivo, doc.numFactura, doc.tipoMoneda, doc.tipoCambio, doc.fecha, doc.fechaCompra, doc.idProveedor, doc.nombreProveedor, doc.tipoIdProveedor, doc.tipoCompra, 
																doc.plazo, doc.tipoPago, doc.claveRef, dt.idProducto, dt.numLinea, dt.cantidad, dt.precio, dt.montoTotal * - 1 AS montoTotal, dt.montoTotaDesc * - 1 AS totalDesc, dt.tarifaImp, dt.montoTotalImp * - 1 AS totalImp, 
																dt.montoTotalExo * - 1 AS totalExo, dt.montoTotalLinea * - 1 AS totalLinea
									   FROM            dbo.tbCompras AS doc INNER JOIN
																dbo.tbDetalleCompras AS dt ON doc.id = dt.idCompra AND doc.tipoDoc = dt.TipoCompra
									   WHERE        (doc.estado = 1) AND (doc.tipoDoc IN (23))) AS T ON dbo.tbTipoMoneda.id = T.tipoMoneda INNER JOIN
								 dbo.tbActividades ON T.codigoActividadEmpresa = dbo.tbActividades.codigoAct INNER JOIN
								 dbo.tbTipoDocumento ON T.tipoDoc = dbo.tbTipoDocumento.id INNER JOIN
								 dbo.tbTipoVenta ON T.tipoCompra = dbo.tbTipoVenta.id INNER JOIN
								 dbo.tbTipoPago ON T.tipoPago = dbo.tbTipoPago.id
		WHERE        (CONVERT(date, T.fechaCompra) >= CONVERT(date, @diaInicio)) AND (CONVERT(date, T.fechaCompra) <= CONVERT(date, @diaFin))
		ORDER BY T.fechaCompra
	end
	else
	begin 
				SELECT        TOP (100) PERCENT T.id, T.consecutivo, T.numFactura, T.tipoCambio, T.fecha, T.fechaCompra, T.idProveedor,t.nombreProveedor, T.tipoIdProveedor, T.plazo, T.claveRef, T.cantidad, T.precio, T.montoTotal, T.montoTotaDesc, T.tarifaImp, 
								 T.montoTotalImp, T.montoTotalExo, T.montoTotalLinea, dbo.tbTipoDocumento.nombre AS documento, dbo.tbTipoMoneda.siglas, dbo.tbActividades.nombreAct, T.codigoActividadEmpresa AS CodActividad, T.tipoCompra, 
								 dbo.tbTipoVenta.nombre AS tipoCompra, dbo.tbTipoPago.nombre AS tipoPago, @diaInicio AS fechaIncio, @diaFin AS FechaFin, @tipo as tipoDoc
		FROM            dbo.tbTipoMoneda INNER JOIN
									 (SELECT        doc.id, doc.tipoDoc, doc.codigoActividadEmpresa, doc.consecutivo, doc.numFactura, doc.tipoMoneda, doc.tipoCambio, doc.fecha, doc.fechaCompra, doc.idProveedor, doc.nombreProveedor, doc.tipoIdProveedor, doc.tipoCompra, 
																 doc.plazo, doc.tipoPago, doc.claveRef, dt.idProducto, dt.numLinea, dt.cantidad, dt.precio, dt.montoTotal, dt.montoTotaDesc, dt.tarifaImp, dt.montoTotalImp, dt.montoTotalExo, dt.montoTotalLinea
									   FROM            dbo.tbCompras AS doc INNER JOIN
																 dbo.tbDetalleCompras AS dt ON doc.id = dt.idCompra AND doc.tipoDoc = dt.TipoCompra
									   WHERE        (doc.estado = 1) AND (doc.tipoDoc IN (26))
									   UNION ALL
									   SELECT        doc.id, doc.tipoDoc, doc.codigoActividadEmpresa, doc.consecutivo, doc.numFactura, doc.tipoMoneda, doc.tipoCambio, doc.fecha, doc.fechaCompra, doc.idProveedor, doc.nombreProveedor, doc.tipoIdProveedor, doc.tipoCompra, 
																doc.plazo, doc.tipoPago, doc.claveRef, dt.idProducto, dt.numLinea, dt.cantidad, dt.precio, dt.montoTotal * - 1 AS montoTotal, dt.montoTotaDesc * - 1 AS totalDesc, dt.tarifaImp, dt.montoTotalImp * - 1 AS totalImp, 
																dt.montoTotalExo * - 1 AS totalExo, dt.montoTotalLinea * - 1 AS totalLinea
									   FROM            dbo.tbCompras AS doc INNER JOIN
																dbo.tbDetalleCompras AS dt ON doc.id = dt.idCompra AND doc.tipoDoc = dt.TipoCompra
									   WHERE        (doc.estado = 1) AND (doc.tipoDoc IN (27))) AS T ON dbo.tbTipoMoneda.id = T.tipoMoneda INNER JOIN
								 dbo.tbActividades ON T.codigoActividadEmpresa = dbo.tbActividades.codigoAct INNER JOIN
								 dbo.tbTipoDocumento ON T.tipoDoc = dbo.tbTipoDocumento.id INNER JOIN
								 dbo.tbTipoVenta ON T.tipoCompra = dbo.tbTipoVenta.id INNER JOIN
								 dbo.tbTipoPago ON T.tipoPago = dbo.tbTipoPago.id
		WHERE        (CONVERT(date, T.fechaCompra) >= CONVERT(date, @diaInicio)) AND (CONVERT(date, T.fechaCompra) <= CONVERT(date, @diaFin))
		ORDER BY T.fechaCompra
	end
end
GO
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
