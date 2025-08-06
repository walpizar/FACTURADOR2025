using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BCategoriaProducto
    {


        //Creamos una instancia global para el acceso a la capa de negocios.
        DCategoriaProducto catProductosIns = new DCategoriaProducto();


        /// <summary>
        /// Recuperamos todas las categorias de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<tbCategoriaProducto> getCategorias(int estado)
        {
            return catProductosIns.GetListEntities(estado);
        }
        public List<tbCategoriaProducto> getCategorias()
        {
            return catProductosIns.GetListEntities();
        }

        public tbCategoriaProducto Getentity(tbCategoriaProducto entity)
        {
            return catProductosIns.GetEntity(entity);
        }

        /// <summary>
        /// Recuperamos las categorias para el reporte.
        /// </summary>
        /// <returns></returns>
        public List<tbCategoriaProducto> getCategoriasReport()
        {
            return catProductosIns.getCategoriasReporte();
        }




        /// <summary>
        /// Actualizamos la informacion de la categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public tbCategoriaProducto actualizarCategoria(tbCategoriaProducto categoria)
        {
            return catProductosIns.Actualizar(categoria);
        }


        /// <summary>
        /// Guardamos la categoria nueva en la base de datos.
        /// </summary>
        /// <param name="categoriaNueva"></param>
        /// <returns></returns>
        public tbCategoriaProducto guardarCategoria(tbCategoriaProducto categoriaNueva)
        {

            tbCategoriaProducto existe = catProductosIns.GetEntity(categoriaNueva);
            if (existe == null)
            {
                //Enviamos la entidad a guardarse.
                return catProductosIns.Guardar(categoriaNueva);

            }
            else
            {
                //Lo enviamos a modificar.


                existe.descripcion = categoriaNueva.descripcion;
                existe.estado = categoriaNueva.estado;
                existe.fecha_ult_mod = categoriaNueva.fecha_ult_mod;
                existe.usuario_ult_mod = categoriaNueva.usuario_ult_mod;
                existe.fotocategoria = categoriaNueva.fotocategoria;




                return catProductosIns.Actualizar(existe);

            }

        }

        ///CABYS
        ///

        //public List<tbCategoria1Cabys> getCat1Cabys()
        //{

        //    return catProductosIns.getCat1Cabys();

        //}

        //public List<tbCategoria2Cabys> getCat2Cabys(string cat1)
        //{

        //    return catProductosIns.getCat2Cabys(cat1);

        //}
        //public List<tbCategoria3Cabys> getCat3Cabys(string cat1, string cat2)
        //{

        //    return catProductosIns.getCat3Cabys(cat1, cat2);

        //}
        //public List<tbCategoria4Cabys> getCat4Cabys(string cat1, string cat2, string cat3)
        //{

        //    return catProductosIns.getCat4Cabys(cat1, cat2, cat3);

        //}
        //public List<tbCategoria5Cabys> getCat5Cabys(string cat1, string cat2, string cat3, string cat4)
        //{

        //    return catProductosIns.getCat5Cabys(cat1, cat2, cat3, cat4);

        //}
        //public List<tbCategoria6Cabys> getCat6Cabys(string cat1, string cat2, string cat3, string cat4, string cat5)
        //{

        //    return catProductosIns.getCat6Cabys(cat1, cat2, cat3, cat4, cat5);

        //}
        //public List<tbCategoria7Cabys> getCat7Cabys(string cat1, string cat2, string cat3, string cat4, string cat5, string cat6)
        //{

        //    return catProductosIns.getCat7Cabys(cat1, cat2, cat3, cat4, cat5, cat6);

        //}
        //public List<tbCategoria8Cabys> getCat8Cabys(string cat1, string cat2, string cat3, string cat4, string cat5, string cat6, string cat7)
        //{

        //    return catProductosIns.getCat8Cabys(cat1, cat2, cat3, cat4, cat5, cat6, cat7);

        //}
        //public List<tbCategoria9Cabys> getCat9Cabys(string cat1, string cat2, string cat3, string cat4, string cat5, string cat6, string cat7, string cat8)
        //{

        //    return catProductosIns.getCat9Cabys(cat1, cat2, cat3, cat4, cat5, cat6, cat7, cat8);

        //}

        //public List<tbCategoria9Cabys> getCat9CabysByText(string text)
        //{

        //    return catProductosIns.getCat9CabysByText(text);

        //}

        //public tbCategoria9Cabys getCat9CabysById(string codigo)
        //{

        //    return catProductosIns.getCat9CabysById(codigo);

        //}





    }
}
