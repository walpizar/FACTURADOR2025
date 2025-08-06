using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BMovimiento
    {
        DMovimientos MovimientoD = new DMovimientos();
        public tbMovimientos Guardar(tbMovimientos movimiento)//funcion recibe objeto
        {
            return MovimientoD.Guardar(movimiento);

        }
        public tbCajasMovimientos GuardarInicioCaja(tbCajasMovimientos inicio)//funcion recibe objeto
        {
            return MovimientoD.GuardarInicioCaja(inicio);

        }

        public tbCajasMovimientos GuardarCierreCaja(tbCajasMovimientos cierre)//funcion recibe objeto
        {
            return MovimientoD.GuardarCierreCaja(cierre);

        }

      
        //public tbMovimientos Modificar(tbMovimientos modificarMov)//tabla de creditos no de clientes
        //{
        //    try
        //    {

        //        return MovimientoD.Modificar(modificarMov);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //Obtener la lista de movimientos guardados PAGO PROVEEDORES

        public List<tbMovimientos> getListMovimiento(int estado)
        {
            return MovimientoD.GetListEntities(estado);
        }

        public List<tbMovimientos> getListMovByFecha(DateTime fecha, int sucursal, int caja)
        {
            return MovimientoD.getListMovByFecha(fecha, sucursal, caja);
        }
    
        public List<tbCajasMovimientos> getIncioCajaByFecha(DateTime fechaInicio, DateTime fechaFin, int sucursal, int caja)
        {
            return MovimientoD.getIncioCajaByFecha(fechaInicio, fechaFin, sucursal, caja);
        }

        public List<tbMovimientos> getListMovByFechaAsNotTracking(DateTime fechaInicio,DateTime fechaFin, int sucursal, int caja)
        {
            return MovimientoD.getListMovByFechaAsNotTracking(fechaInicio, fechaFin, sucursal, caja);
        }
        public tbCajasMovimientos verificarCierreCaja(int caja, int sucursal)
        {
            return MovimientoD.verificarCierreCaja(caja, sucursal);
        }


        //public tbMovimientos ModificarLista(List<tbMovimientos> modificarMov)//tabla de creditos no de clientes
        //{
        //    try
        //    {

        //        return MovimientoD.ModificarLista(modificarMov);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

    }
}
