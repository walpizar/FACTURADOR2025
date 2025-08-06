IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ServiciosFechas')
DROP PROCEDURE sp_ServiciosFechas
GO
CREATE PROCEDURE sp_ServiciosFechas 
 @diaInicio datetime, @diaFin datetime, @idProducto varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
   select dt.idProducto,p.nombre,@diaInicio as fechaIncio,@diaFin as fechaFin, count(dt.idProducto) cantidad, sum(dt.totalLinea)total
   from tbDocumento d inner join tbDetalleDocumento dt on d.id=dt.idDoc and d.tipoDocumento=dt.idTipoDoc 
   inner join tbProducto p on p.idProducto=dt.idProducto 
   where convert(date,d.fecha)>= convert(date,@diaInicio) and  convert(date,d.fecha)>= convert(date,@diaFin)
   and P.idProducto=@idProducto
   group by dt.idProducto, p.nombre
END;
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spVentasProduco2Fechas')
DROP PROCEDURE spVentasProduco2Fechas
GO
CREATE PROCEDURE [dbo].[spVentasProduco2Fechas]
@diaInicio datetime, 
@diaFin datetime
AS
BEGIN
SELECT dbo.tbDetalleDocumento.idProducto,  dbo.tbProducto.nombre, sum(IIF(dbo.tbDetalleDocumento.idTipoDoc=3 or dbo.tbDetalleDocumento.idTipoDoc=23, dbo.tbDetalleDocumento.cantidad*-1 , dbo.tbDetalleDocumento.cantidad)) as cantidad, @diaInicio as fechaInicio,@diaFin as FechaFin, dbo.tbCategoriaProducto.nombre
FROM     dbo.tbDetalleDocumento INNER JOIN
                  dbo.tbProducto ON dbo.tbDetalleDocumento.idProducto = dbo.tbProducto.idProducto INNER JOIN
                  dbo.tbDocumento ON dbo.tbDetalleDocumento.idDoc = dbo.tbDocumento.id AND dbo.tbDetalleDocumento.idTipoDoc = dbo.tbDocumento.tipoDocumento INNER JOIN
				  dbo.tbCategoriaProducto ON dbo.tbProducto.id_categoria= dbo.tbCategoriaProducto.id
where   (dbo.tbDocumento.tipoDocumento = 4 OR dbo.tbDocumento.tipoDocumento = 3 or
					dbo.tbDocumento.tipoDocumento = 23 or
                  dbo.tbDocumento.tipoDocumento = 1 OR
                  dbo.tbDocumento.tipoDocumento = 22) AND (dbo.tbDocumento.estado = 1) AND (CONVERT(date, dbo.tbDocumento.fecha)>= CONVERT(date, @diaInicio)) and (CONVERT(date, dbo.tbDocumento.fecha)<= CONVERT(date, @diaFin))
group by dbo.tbDetalleDocumento.idProducto, dbo.tbProducto.nombre, dbo.tbCategoriaProducto.nombre
having sum(IIF(dbo.tbDetalleDocumento.idTipoDoc=3 or dbo.tbDetalleDocumento.idTipoDoc=23, dbo.tbDetalleDocumento.cantidad*-1 , dbo.tbDetalleDocumento.cantidad))>0
end
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spVentasDetalladaFechasEsp')
DROP PROCEDURE spVentasDetalladaFechasEsp
GO
CREATE PROCEDURE [dbo].[spVentasDetalladaFechasEsp]
@diaInicio datetime, 
@diaFin datetime
AS
BEGIN
SELECT        TOP (100) PERCENT T.id,T.usuario_crea, T.consecutivo,T.cantidad ,T.precio AS precioProducto, T.fecha,dbo.tbProducto.nombre as nombreProducto, T.montoTotal, T.montoTotalDesc, T.porcImp, T.montoTotalImp, T.porcExo, T.montoTotalExo, T.totalLinea, 
                          dbo.tbTipoPago.nombre AS tipoPago, dbo.tbTipoVenta.nombre AS tipoVenta, T.idProducto,dbo.tbTipoDocumento.nombre as tipoDoc, T.descuento, @diaInicio AS fechaInicio, @diaFin as fechaFin
FROM            dbo.tbTipoPago INNER JOIN
                             (SELECT        doc.usuario_crea, doc.id, doc.tipoDocumento, doc.consecutivo, doc.fecha, doc.tipoVenta, doc.plazo, doc.tipoPago, doc.claveRef, dt.idProducto, dt.numLinea, dt.cantidad, dt.precioCosto, 
                                                         dt.precio, dt.montoTotal, dt.descuento, dt.montoTotalDesc, dt.porcImp, dt.montoTotalImp, dt.porcExo, dt.montoTotalExo, dt.totalLinea
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                         dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (1, 22, 4))
                               UNION ALL
                               SELECT        doc.usuario_crea,doc.id, doc.tipoDocumento, doc.consecutivo,  doc.fecha, doc.tipoVenta, doc.plazo, doc.tipoPago, doc.claveRef, dt.idProducto, dt.numLinea, dt.cantidad, dt.precioCosto, 
                                                        dt.precio, dt.montoTotal * - 1 AS Expr2, dt.descuento, dt.montoTotalDesc * - 1 AS Expr3, dt.porcImp, dt.montoTotalImp * - 1 AS Expr4, dt.porcExo, dt.montoTotalExo * - 1 AS Expr5, dt.totalLinea * - 1 AS Expr1
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                        dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (3, 23))) AS T ON dbo.tbTipoPago.id = T.tipoPago INNER JOIN
                         dbo.tbTipoVenta ON T.tipoVenta = dbo.tbTipoVenta.id INNER JOIN						
                         dbo.tbTipoDocumento ON T.tipoDocumento = dbo.tbTipoDocumento.id INNER JOIN
						 dbo.tbProducto ON T.idProducto = dbo.tbProducto.idProducto
WHERE  (CONVERT(date, T.fecha)>= CONVERT(date, @diaInicio)) and (CONVERT(date, T.fecha)<= CONVERT(date, @diaFin)) 
ORDER BY T.fecha asc, t.numLinea
END
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DASHBOARD_10PRODUCTOVENDIDOS')
DROP PROCEDURE DASHBOARD_10PRODUCTOVENDIDOS
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DASHBOARD_CANTIDADES')
DROP PROCEDURE DASHBOARD_CANTIDADES
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DASHBOARD_10PRODUCTOMENOSVENDIDOS')
DROP PROCEDURE DASHBOARD_10PRODUCTOMENOSVENDIDOS
GO
CREATE PROCEDURE [dbo].[DASHBOARD_10PRODUCTOVENDIDOS]
/*10 productos mas vendidos*/
AS
BEGIN
SELECT top 10  T.idProducto,p.nombre,sum(T.cantidad)  as cantidad
FROM         
                             (SELECT         dt.idProducto,doc.fecha, count(dt.idProducto) as cantidad
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                         dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (1, 22, 4))AND (idProducto<>'SM')
							   group by dt.idProducto, doc.fecha
                               UNION ALL
                               SELECT        dt.idProducto,doc.fecha, count(dt.idProducto)*-1 as cantidad 
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                        dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (3, 23)) and (idProducto<>'SM')
							      group by dt.idProducto, doc.fecha) AS T   INNER JOIN tbProducto p ON t.idProducto=p.idProducto
group by T.idProducto  ,p.nombre
order by count(1)desc
END
GO
CREATE PROCEDURE [dbo].[DASHBOARD_CANTIDADES]

	@diaInicio datetime, @diaFin datetime, @ventasDia decimal(18,5) OUTPUT,
	@cantPro int OUTPUT, @cantCliente int OUTPUT, @InvMenorO int OUTPUT,
	@CantProductosVenDia int OUTPUT
AS	

BEGIN

	SET NOCOUNT ON;

/*TOTAL VENTAS DIARIA*/
SELECT      @ventasDia=   sum(T.totalLinea)
FROM         
                             (SELECT        doc.id, dt.totalLinea, doc.fecha
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                         dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (1, 22, 4))
                               UNION ALL
                               SELECT        doc.id, dt.totalLinea * - 1 AS totalLinea, doc.fecha
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                        dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (3, 23))) AS T 
                        
						WHERE  (CONVERT(date, T.fecha)>= CONVERT(date, @diaInicio)) and (CONVERT(date, T.fecha)<= CONVERT(date, @diaFin)) 

/*CANTIDAD DE PRODUCTOS VENDIDOS EN EL DIA*/
SELECT      @CantProductosVenDia=   sum(T.cantidad)
FROM         
                             (SELECT         dt.idProducto,doc.fecha, count(dt.idProducto) as cantidad
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                         dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (1, 22, 4))
							   group by dt.idProducto, doc.fecha
                               UNION ALL
                               SELECT        dt.idProducto,doc.fecha, count(dt.idProducto)*-1 as cantidad 
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                        dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (3, 23))
							      group by dt.idProducto, doc.fecha) AS T                         
						WHERE  (CONVERT(date, T.fecha)>= CONVERT(date, @diaInicio)) and (CONVERT(date, T.fecha)<= CONVERT(date, @diaFin)) 

/*cantidad de productos*/
SELECT @cantPro=COUNT(*) from tbProducto where estado=1;
/*cantidad de clientes*/
SELECT @cantCliente=COUNT(*) from tbClientes where estado=1;
/*productos con invetarios negativo*/
SELECT @InvMenorO=COUNT(*) from tbProducto P inner join tbInventario I on P.idProducto=I.idProducto where P.estado=1 and I.cantidad<0;
END
GO
CREATE PROCEDURE [dbo].[DASHBOARD_10PRODUCTOMENOSVENDIDOS]
/*10 productos MENOS vendidos*/
AS
BEGIN
SELECT top 10  T.idProducto,p.nombre,sum(T.cantidad)  as cantidad
FROM         
                             (SELECT         dt.idProducto,doc.fecha, count(dt.idProducto) as cantidad
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                         dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (1, 22, 4))AND (idProducto<>'SM')
							   group by dt.idProducto, doc.fecha
                               UNION ALL
                               SELECT        dt.idProducto,doc.fecha, count(dt.idProducto)*-1 as cantidad 
                               FROM            dbo.tbDocumento AS doc INNER JOIN
                                                        dbo.tbDetalleDocumento AS dt ON doc.id = dt.idDoc AND doc.tipoDocumento = dt.idTipoDoc
                               WHERE        (doc.estado = 1) AND (doc.tipoDocumento IN (3, 23)) and (idProducto<>'SM')
							      group by dt.idProducto, doc.fecha) AS T   INNER JOIN tbProducto p ON t.idProducto=p.idProducto
group by T.idProducto  ,p.nombre
order by count(1)ASC
END

