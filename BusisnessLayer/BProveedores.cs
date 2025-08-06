﻿using CommonLayer.Exceptions.BusisnessExceptions;
using DataLayer;
using EntityLayer;
using System.Collections.Generic;





namespace BusinessLayer
{


    public class BProveedores
    {
        DProveedores proveedorIns = new DProveedores();

        public tbProveedores guardar(tbProveedores proveedor)
        {
            //para poder acceder aDproveedores tengo que instanciar poruqe quité los static

            tbProveedores pro = proveedorIns.GetEntity(proveedor);//verifica si existe en la base de datos 
            if (pro == null)
            {

                return proveedorIns.Guardar(proveedor);
            }
            else
            {
                if (pro.estado == true)
                {
                    throw new EntityExistException("Proveedor");
                }
                else
                {
                    throw new EntityDisableStateException("Proveedor");

                }

                //exist = true;
                //return proveedor;
            }
        }


        //public static bool guardar(tbProveedores proveedor)
        //{


        //        return DProveedores.guardar(proveedor);

        //}

        public List<tbProveedores> getListProveedor(int estado)
        {
            return proveedorIns.GetListEntities(estado);
        }

        public tbProveedores getProveedorById(string id)
        {
            return proveedorIns.getProveedorById(id);
        }


        public tbProveedores Modificar(tbProveedores proveedor)
        {


            return proveedorIns.Actualizar(proveedor);
        }


        public tbProveedores eliminar(tbProveedores proveedor)
        {
            return proveedorIns.Actualizar(proveedor);
        }

    }


}
