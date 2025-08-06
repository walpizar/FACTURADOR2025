using CommonLayer;
using CommonLayer.Logs;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmNotaCredito : Form
    {
        BusinessLayer.BFacturacion factIns = new BusinessLayer.BFacturacion();
        public List<tbDetalleDocumento> _docDetalleCopia { get; set; }
        public List<tbDetalleDocumento> _docDetalleNotaCredito { get; set; }
        public List<tbDocumento> _NC { get; set; }
        public tbDocumento _doc { get; set; }

        clsDetalleNC _detalleNC;

        public frmNotaCredito()
        {
            InitializeComponent();
        }

        private void frmNotaCredito_Load(object sender, EventArgs e)

        {
            try
            {
                _docDetalleNotaCredito = new List<tbDetalleDocumento>();
                _docDetalleCopia = factIns.getEntityDetails(_doc);

                _NC = (List<tbDocumento>)factIns.getNCByRef(_doc.clave);

                _docDetalleCopia = aplicarNCDocs(_docDetalleCopia);
                calcularMontosT(_docDetalleCopia);
                cargarForm();

                formatoGrid();
                cargarTotales();
                agregarProductoGrid();
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }



        }

        private List<tbDetalleDocumento> aplicarNCDocs(List<tbDetalleDocumento> docDetalleCopia)
        {

            List<tbDetalleDocumento> auxList = new List<tbDetalleDocumento>();

            foreach (var doc in docDetalleCopia)
            {
                bool bandera = false;
                tbDetalleDocumento detalle = factIns.getEntityDetails(_doc).Where(x => x.idProducto.Trim() == doc.idProducto.Trim()).SingleOrDefault();

                foreach (var nota in _NC)
                {

                    var detalleNC = nota.tbDetalleDocumento.Where(x => x.idProducto == doc.idProducto).SingleOrDefault();
                    if (detalleNC != null)
                    {
                        bandera = true;
                        if (detalle.cantidad != detalleNC.cantidad)
                        {

                            detalle.cantidad = detalle.cantidad - detalleNC.cantidad;
                            auxList.Add(detalle);
                        }

                    }
                }

                if (!bandera && detalle != null)
                {
                    auxList.Add(detalle);
                }




            }




            return auxList;
        }

        private void cargarForm()
        {
            lblTipoDoc.Text = Enum.GetName(typeof(Enums.TipoDocumento), _doc.tipoDocumento).ToUpper();
            txtIdFactura.Text = _doc.id.ToString().Trim();

            txtCorreo.Text = _doc.correo1 == null ? string.Empty : _doc.correo1.ToString().Trim();

            txtCorreo2.Text = _doc.correo2 == null ? string.Empty : _doc.correo2.ToString().Trim();

        }

        private void formatoGrid()
        {
            dtgvDetalleFactura.BorderStyle = BorderStyle.None;
            dtgvDetalleFactura.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgvDetalleFactura.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgvDetalleFactura.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dtgvDetalleFactura.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtgvDetalleFactura.BackgroundColor = Color.White;

            dtgvDetalleFactura.EnableHeadersVisualStyles = false;
            dtgvDetalleFactura.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgvDetalleFactura.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dtgvDetalleFactura.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            // dtgvDetalleFactura.DefaultCellStyle.Font.Size = 20; 
        }
        private void agregarProductoGrid()
        {
            decimal cantidadProd = 0, utilidad = 0;
            dtgvDetalleFactura.Rows.Clear();
            _docDetalleCopia = _docDetalleCopia.OrderBy(x => x.numLinea).ToList();
            foreach (tbDetalleDocumento detalle in _docDetalleCopia)
            {
                utilidad = 0;
                cantidadProd += detalle.cantidad;
                DataGridViewRow row = (DataGridViewRow)dtgvDetalleFactura.Rows[0].Clone();
                row.Cells[0].Value = detalle.numLinea.ToString().Trim();
                row.Cells[1].Value = detalle.idProducto.ToString().Trim();
                row.Cells[2].Value = detalle.tbProducto.nombre.Trim();
                if (detalle.precio == 0)
                {
                    row.Cells[3].Value = "0.00";
                }
                else
                {
                    row.Cells[3].Value = string.Format("{0:N2}", detalle.precio);
                }
                //calcula la utilidad del prodcuto
                if (detalle.tbProducto.precio_real == 0)
                {
                    utilidad = 0;
                }
                else
                {
                    utilidad = ((detalle.precio / detalle.tbProducto.precio_real) - 1) * 100;
                }

                row.Cells[4].Value = string.Format("{0:N2}", detalle.cantidad);

                dtgvDetalleFactura.Rows.Add(row);

            }
            lblTotalProducto.Text = cantidadProd.ToString("#.#");
            lblCantidadLineas.Text = _docDetalleCopia.Count.ToString();
            if (_docDetalleCopia.Count != 0)
            {
                dtgvDetalleFactura.Rows[_docDetalleCopia.Count - 1].Selected = true;
            }


        }
        private void cargarTotales()
        {

            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0;

            foreach (tbDetalleDocumento detalle in _docDetalleCopia)
            {
                detalle.totalLinea = (detalle.montoTotal - detalle.montoTotalDesc) + detalle.montoTotalImp - detalle.montoTotalExo;
                total += detalle.totalLinea;
                desc += detalle.montoTotalDesc;
                iva += detalle.montoTotalImp;
                exo += detalle.montoTotalExo;
                subtotal += detalle.montoTotal;

            }
            txtSubtotal.Text = string.Format("{0:N2}", subtotal);
            txtDescuento.Text = desc == 0 ? "0.00" : string.Format("{0:N2}", desc);
            txtIva.Text = iva == 0 ? "0.00" : string.Format("{0:N2}", iva);
            txtTotal.Text = total == 0 ? "0.00" : string.Format("{0:N2}", total);
            txtExoneracion.Text = exo == 0 ? "0.00" : string.Format("{0:N2}", exo);

        }

        private void dtgvDetalleFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                string codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (codigoProducto != string.Empty)
                {

                    DialogResult result = MessageBox.Show("¿Desea eliminar el producto " + dtgvDetalleFactura.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() + " del documento?", "Eliminar linea", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        eliminarProducto(codigoProducto, 1);

                    }
                }
            }
        }




        private void cargarGridNota()
        {
            decimal cantidadProd = 0, utilidad = 0;
            dtgvNotaCredito.Rows.Clear();
            _docDetalleNotaCredito = _docDetalleNotaCredito.OrderBy(x => x.numLinea).ToList();
            foreach (tbDetalleDocumento detalle in _docDetalleNotaCredito)
            {
                utilidad = 0;
                cantidadProd += detalle.cantidad;
                DataGridViewRow row = (DataGridViewRow)dtgvNotaCredito.Rows[0].Clone();
                row.Cells[0].Value = detalle.numLinea.ToString().Trim();
                row.Cells[1].Value = detalle.idProducto.ToString().Trim();
                row.Cells[2].Value = detalle.tbProducto.nombre.Trim();
                if (detalle.precio == 0)
                {
                    row.Cells[3].Value = "0.00";
                }
                else
                {
                    row.Cells[3].Value = string.Format("{0:N2}", detalle.precio);
                }
                //calcula la utilidad del prodcuto
                if (detalle.tbProducto.precio_real == 0)
                {
                    utilidad = 0;
                }
                else
                {
                    utilidad = ((detalle.precio / detalle.tbProducto.precio_real) - 1) * 100;
                }
                row.Cells[4].Value = string.Format("{0:N2}", detalle.cantidad);


                dtgvNotaCredito.Rows.Add(row);
                // dtgvDetalleFactura.Rows[listaDetalleDocumento.Count-1].Selected=true;
            }
            lblTotalProdNota.Text = cantidadProd.ToString("#.#");
            lblLineaNota.Text = _docDetalleNotaCredito.Count.ToString();
            if (_docDetalleNotaCredito.Count != 0)
            {
                dtgvNotaCredito.Rows[_docDetalleNotaCredito.Count - 1].Selected = true;
            }

        }

        private void calcularMontosT(List<tbDetalleDocumento> lista)
        {
            try
            {
                asignarLineasNumero(lista);
                calcularSubtotales(lista);
                calcularDescuentos(lista);
                calcularImpuestos(lista);
                calcularTotales(lista);

            }
            catch (Exception)
            {

                MessageBox.Show("Error al calcular montos de facturación.", "Calcular montos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void asignarLineasNumero(List<tbDetalleDocumento> list)
        {
            int linea = 0;
            foreach (tbDetalleDocumento det in list)
            {
                linea++;
                det.numLinea = linea;
            }

        }
        private void calcularSubtotales(List<tbDetalleDocumento> list)
        {
            foreach (tbDetalleDocumento det in list)
            {
                det.montoTotal = det.precio * det.cantidad;
            }
        }

        private void calcularDescuentos(List<tbDetalleDocumento> list)
        {

            foreach (tbDetalleDocumento det in list)
            {
                det.montoTotalDesc = det.montoTotal * (det.descuento / 100);

            }
        }
        private void calcularImpuestos(List<tbDetalleDocumento> list)
        {

            foreach (tbDetalleDocumento detalle in list)
            {

                detalle.montoTotalImp = (detalle.montoTotal - detalle.montoTotalDesc) * ((decimal)detalle.porcImp / 100);
                detalle.montoTotalExo = (detalle.montoTotal - detalle.montoTotalDesc) * ((decimal)detalle.porcExo / 100);
            }
        }


        private void calcularTotales(List<tbDetalleDocumento> list)
        {

            foreach (tbDetalleDocumento detalle in list)
            {
                detalle.totalLinea = (detalle.montoTotal - detalle.montoTotalDesc) + detalle.montoTotalImp - detalle.montoTotalExo;


            }

        }



        private void imprimirTotales()
        {
            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0, sm = 0;

            foreach (tbDetalleDocumento detalle in _docDetalleCopia)
            {
                if (detalle.idProducto != "SM")
                {
                    total += detalle.totalLinea;
                    desc += detalle.montoTotalDesc;
                    iva += detalle.montoTotalImp;
                    exo += detalle.montoTotalExo;
                    subtotal += detalle.montoTotal;
                }
            }

            txtSubtotal.Text = string.Format("{0:N2}", subtotal);
            txtDescuento.Text = string.Format("{0:N2}", desc);
            txtIva.Text = string.Format("{0:N2}", iva);
            txtExoneracion.Text = string.Format("{0:N2}", exo);
            txtTotal.Text = string.Format("{0:N2}", (subtotal - desc + iva) - exo);

            txtTotal.Text = string.Format("{0:N2}", (total + sm));


            if (txtSubtotal.Text == string.Empty)
            {
                txtSubtotal.Text = "0";

            }
            if (txtTotal.Text == string.Empty)
            {
                txtTotal.Text = "0";

            }

            //if (txtPorcetaje.Text == string.Empty)
            //{
            //    txtPorcetaje.Text = "0";

            //}
            if (txtDescuento.Text == string.Empty)
            {
                txtDescuento.Text = "0";

            }
            if (txtIva.Text == string.Empty)
            {
                txtIva.Text = "0";

            }
            if (txtExoneracion.Text == string.Empty)
            {
                txtExoneracion.Text = "0";

            }


        }
        private void imprimirTotalesNota()
        {
            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0, sm = 0;

            foreach (tbDetalleDocumento detalle in _docDetalleNotaCredito)
            {
                if (detalle.idProducto != "SM")
                {
                    total += detalle.totalLinea;
                    desc += detalle.montoTotalDesc;
                    iva += detalle.montoTotalImp;
                    exo += detalle.montoTotalExo;
                    subtotal += detalle.montoTotal;
                }
            }

            txtSubTotalNota.Text = string.Format("{0:N2}", subtotal);
            txtDescuentoNota.Text = string.Format("{0:N2}", desc);
            txtIvaNota.Text = string.Format("{0:N2}", iva);
            txtExoNota.Text = string.Format("{0:N2}", exo);
            txtTotalNota.Text = string.Format("{0:N2}", (subtotal - desc + iva) - exo);

            txtTotalNota.Text = string.Format("{0:N2}", (total + sm));


            if (txtSubTotalNota.Text == string.Empty)
            {
                txtSubTotalNota.Text = "0";

            }
            if (txtTotalNota.Text == string.Empty)
            {
                txtTotalNota.Text = "0";

            }

            //if (txtPorcetaje.Text == string.Empty)
            //{
            //    txtPorcetaje.Text = "0";

            //}
            if (txtDescuentoNota.Text == string.Empty)
            {
                txtDescuentoNota.Text = "0";

            }
            if (txtIvaNota.Text == string.Empty)
            {
                txtIvaNota.Text = "0";

            }
            if (txtExoNota.Text == string.Empty)
            {
                txtExoNota.Text = "0";

            }


        }




        private void dtgvDetalleFactura_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double subtotal = 0;
            decimal cantidad = 0;
            string codigoProducto = string.Empty;
            decimal precioProd = 0;


            try
            {
                //cuando cambia la cantidad
                if (dtgvDetalleFactura.Columns[e.ColumnIndex].Name == "colCant")
                {
                    if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value != null)
                    {
                        tbProducto prod;
                        codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value.ToString();

                        if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[4].Value != null)
                        {

                            cantidad = decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (cantidad < 1)
                            {
                                // dtgvDetalleFactura.Rows[e.RowIndex].Cells[4].Value = string.Format("{0:N2}", _docDetalleCopia.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().cantidad);

                                dtgvDetalleFactura.CancelEdit();
                                dtgvDetalleFactura.RefreshEdit();
                                MessageBox.Show("La cantidad ingresada debe ser mayor que 0", "Cantidad de Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);


                            }
                            else
                            {
                                if (cantidad > _doc.tbDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().cantidad)
                                {

                                    dtgvDetalleFactura.Rows[e.RowIndex].Cells[4].DetachEditingControl();

                                    //dtgvDetalleFactura.Rows[e.RowIndex].Cells[4].Value = string.Format("{0:N2}", _docDetalleCopia.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().cantidad);
                                    //dtgvDetalleFactura.CancelEdit();
                                    //dtgvDetalleFactura.RefreshEdit();
                                    MessageBox.Show("La cantidad ingresada no puede ser mayor a la cantidad ingresada al documento original, cantidad: " + _doc.tbDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().cantidad, "Cantidad de Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {

                                    dtgvDetalleFactura.Rows[e.RowIndex].Cells[4].Value = string.Format("{0:N2}", cantidad);
                                    actualizarListas(codigoProducto, cantidad);


                                }


                            }


                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void actualizarListas(string codigoProducto, decimal cantidad)
        {

            try
            {
                //lista nota de credito
                decimal cantidadTotal = _doc.tbDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().cantidad;

                foreach (var nc in _NC)
                {
                    var detalleNc = nc.tbDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault();
                    cantidadTotal -= detalleNc == null ? 0 : detalleNc.cantidad;

                }

                decimal cantidadDism = cantidadTotal - cantidad;


                tbDetalleDocumento detalle = factIns.getEntityDetails(_doc).Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault();
                detalle.cantidad = cantidadDism;

                foreach (var item in _docDetalleNotaCredito)
                {
                    if (item.idProducto.Trim() == codigoProducto)
                    {
                        _docDetalleNotaCredito.Remove(item);
                        break;
                    }

                }
                if (cantidadDism != 0)
                {
                    _docDetalleNotaCredito.Add(detalle);
                }

                //lista original               

                dtgvDetalleFactura.CancelEdit();
                dtgvDetalleFactura.RefreshEdit();
                _docDetalleCopia.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().cantidad = cantidad;

                calcularMontosT(_docDetalleNotaCredito);
                calcularMontosT(_docDetalleCopia);

                imprimirTotales();
                imprimirTotalesNota();

                agregarProductoGrid();
                cargarGridNota();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al actualizar listas.", "Actualizar listas", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dtgvNotaCredito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                string codigoProducto = dtgvNotaCredito.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (codigoProducto != string.Empty)
                {

                    DialogResult result = MessageBox.Show("¿Desea eliminar el producto " + dtgvNotaCredito.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() + " de la nota de crédito?", "Eliminar linea", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        eliminarProducto(codigoProducto, 2);

                    }
                }
            }
        }
        private void eliminarProducto(string codigoProducto, int tipo)
        {
            try
            {

                foreach (var item in _docDetalleCopia)
                {
                    if (item.idProducto.Trim() == codigoProducto)
                    {
                        _docDetalleCopia.Remove(item);
                        break;
                    }

                }

                foreach (var item in _docDetalleNotaCredito)
                {
                    if (item.idProducto.Trim() == codigoProducto)
                    {
                        _docDetalleNotaCredito.Remove(item);
                        break;
                    }

                }

                tbDetalleDocumento detalle = factIns.getEntityDetails(_doc).Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault();

                if (tipo == 1)
                {
                    _docDetalleNotaCredito.Add(detalle);
                    aplicarNCDocs(_docDetalleNotaCredito);
                }
                else
                {
                    _docDetalleCopia.Add(detalle);
                    aplicarNCDocs(_docDetalleCopia);
                }


                calcularMontosT(_docDetalleCopia);
                calcularMontosT(_docDetalleNotaCredito);

                imprimirTotales();
                imprimirTotalesNota();

                agregarProductoGrid();
                cargarGridNota();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al eliminar el producto del documento", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        //private void eliminarProductoNota(string codigoProducto)
        //{
        //    try
        //    {               

        //        foreach (var item in _docDetalleNotaCredito)
        //        {
        //            if (item.idProducto.Trim() == codigoProducto)
        //            {
        //                _docDetalleNotaCredito.Remove(item);
        //                break;
        //            }

        //        }

        //        foreach (var item in _docDetalleCopia)
        //        {
        //            if (item.idProducto.Trim() == codigoProducto)
        //            {
        //                _docDetalleCopia.Remove(item);
        //                break;
        //            }

        //        }

        //       //vuelvo a obtenerla de la bd para agregarla como nueva
        //        tbDetalleDocumento detalle = factIns.getEntityDetails(_doc).Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault();





        //        calcularMontosT(_docDetalleCopia);
        //        calcularMontosT(_docDetalleNotaCredito);

        //        imprimirTotales();
        //        imprimirTotalesNota();

        //        agregarProductoGrid();
        //        cargarGridNota();
        //    }
        //    catch (Exception)
        //    {

        //        MessageBox.Show("Error al eliminar el producto del documento", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnCancelarFactura_Click(object sender, EventArgs e)
        {
            bool bandera = true;
            try
            {
                if (_docDetalleNotaCredito.Count != 0)
                {
                    DialogResult result = MessageBox.Show("Se generará una NOTA DE CRÉDITO, para modificar el documento Original, Desea continuar?", "Nota de Crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        frmRazon razon = new frmRazon();
                        razon._doc = _doc;
                        razon.pasarDatosEvent += dataBuscar;
                        razon.ShowDialog();
                        if (_detalleNC != null)
                        {

                            var doc = eliminarFactura();
                            _doc = doc;
                            MessageBox.Show("Se ha generado la Nota de Crédito Correctamente.", "Guardar Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (doc != null & doc.reporteElectronic == true)
                            {
                                BackgroundWorker tarea = new BackgroundWorker();

                                tarea.DoWork += reportarFacturacionElectronica;
                                tarea.RunWorkerAsync();
                                //reportarFacturacionElectronica();

                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Debe indicar la razon de la nota de crédito", "Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }



                    }

                }
                else
                {
                    MessageBox.Show("No hay producto para confeccionar la Nota de Crédio", "Guardar Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo generar la nota de crédito", "Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void dataBuscar(clsDetalleNC detalle)
        {
            _detalleNC = detalle;
        }

        private void reportarFacturacionElectronica(object sender, DoWorkEventArgs e)
        {

            if (Utility.AccesoInternet())
            {
                try
                {
                    _doc = factIns.FacturarElectronicamente(_doc);

                    System.Threading.Thread.Sleep(3000);

                    factIns.consultarFacturaElectronicaPorClave(_doc.clave);

                    //envio correo electrónico
                    if (_doc.notificarCorreo)
                    {
                        List<string> correos = new List<string>();


                        if (_doc.correo1 != null && _doc.correo1.Trim() != string.Empty)
                        {
                            correos.Add(_doc.correo1);

                        }

                        if (_doc.correo2 != null && _doc.correo2.Trim() != string.Empty)
                        {
                            correos.Add(_doc.correo2);

                        }

                        if (correos.Count != 0)
                        {
                            enviarCorreo(_doc, correos);
                        }

                    }




                }
                catch (Exception ex)
                {

                    MessageBox.Show("No se pudo emitir el documento NOTA CREDITO", "Error al procesar", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("No hay acceso a internet, No se enviará el documento a Hacienda, validar en la pantalla de validación, para su envio correspodiente.", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void enviarCorreo(tbDocumento doc, List<string> correos)
        {
            try
            {
                bool enviado = false;
                //se solicita respuesta, y se confecciona el correo a enviar

                clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.factura);
                enviado = CorreoElectronico.enviarCorreo(docCorreo);

                if (enviado)
                {
                    MessageBox.Show("Se envió correctamente el correo electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Se produjo un error al enviar el Correo Electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                MessageBox.Show("Se produjo un error al enviar el Correo Electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private tbDocumento eliminarFactura()
        {
            try
            {
                tbDocumento notaCredito = new tbDocumento();

                notaCredito.correo1 = _doc.correo1;
                notaCredito.correo2 = _doc.correo2;
                notaCredito.estado = true;
                notaCredito.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
                notaCredito.fecha = Utility.getDate();
                notaCredito.fecha_crea = Utility.getDate();
                notaCredito.fecha_ult_mod = Utility.getDate();
                notaCredito.idCliente = _doc.idCliente;
                notaCredito.reporteAceptaHacienda = false;
                notaCredito.idEmpresa = _doc.idEmpresa;
                notaCredito.reporteElectronic = _doc.reporteElectronic;
                notaCredito.tipoVenta = _doc.tipoVenta;
                notaCredito.tipoPago = _doc.tipoPago;
                notaCredito.tipoMoneda = _doc.tipoMoneda;
                notaCredito.tipoCambio = (int)_doc.tipoCambio;
                notaCredito.plazo = (int)_doc.plazo;

                notaCredito.codigoActividad = _doc.codigoActividad;

                notaCredito.sucursal = _doc.sucursal;
                notaCredito.caja = _doc.caja;


                if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Factura && !_doc.reporteElectronic)
                {
                    notaCredito.tipoDocumento = (int)Enums.TipoDocumento.NotaCredito;

                }
                else
                {
                    notaCredito.tipoDocumento = (int)Enums.TipoDocumento.NotaCreditoElectronica;

                }

                notaCredito.tipoIdCliente = _doc.tipoIdCliente;
                notaCredito.tipoIdEmpresa = _doc.tipoIdEmpresa;
                notaCredito.usuario_crea = _doc.usuario_crea;
                notaCredito.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();

                //datos de nota de credito
                notaCredito.tipoDocRef = _doc.tipoDocumento;
                notaCredito.claveRef = _doc.clave;
                notaCredito.fechaRef = _doc.fecha;
                notaCredito.codigoRef = (int)Enums.TipoRef.AnulaDocumentoReferencia;

                notaCredito.razon = _detalleNC.razon.Trim().ToUpper();
                notaCredito.observaciones = _detalleNC.razon.Trim().ToUpper();
                notaCredito.tipoPago = _detalleNC.tipoPago;
                notaCredito.refPago = _detalleNC.referencia;


                List<tbDetalleDocumento> lista = new List<tbDetalleDocumento>();
                foreach (var item in _docDetalleNotaCredito)
                {
                    tbDetalleDocumento detalle = new tbDetalleDocumento();
                    detalle.precioCosto = item.precioCosto;
                    detalle.cantidad = item.cantidad;
                    detalle.descuento = item.descuento;
                    detalle.idProducto = item.idProducto;
                    detalle.montoTotal = item.montoTotal;
                    detalle.montoTotalDesc = item.montoTotalDesc;
                    detalle.montoTotalExo = item.montoTotalExo;
                    detalle.montoTotalImp = item.montoTotalImp;
                    detalle.numLinea = item.numLinea;
                    detalle.porcExo = item.porcExo;
                    detalle.porcImp = item.porcImp;
                    detalle.precio = item.precio;
                    detalle.totalLinea = item.totalLinea;
                    lista.Add(detalle);

                }
                notaCredito.tbDetalleDocumento = lista;
                notaCredito = factIns.guadar(notaCredito);
                return notaCredito;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private tbDocumento eliminarFactura()
        //{
        //    try
        //    {
        //        tbDocumento notaCredito = new tbDocumento();


        //        notaCredito.notificarCorreo = chkEnviar.Checked;
        //        notaCredito.estado = true;
        //        notaCredito.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
        //        notaCredito.fecha = Utility.getDate();
        //        notaCredito.fecha_crea = Utility.getDate();
        //        notaCredito.fecha_ult_mod = Utility.getDate();
        //        notaCredito.idCliente = _doc.idCliente;              
        //        notaCredito.reporteAceptaHacienda = false;
        //        notaCredito.idEmpresa = _doc.idEmpresa;
        //        notaCredito.reporteElectronic = _doc.reporteElectronic;
        //        notaCredito.tipoVenta = _doc.tipoVenta;
        //        notaCredito.tipoPago = _doc.tipoPago;
        //        notaCredito.tipoMoneda = _doc.tipoMoneda;
        //        notaCredito.tipoCambio = (int)_doc.tipoCambio;

        //        if ((bool)notaCredito.notificarCorreo)
        //        {
        //            notaCredito.correo1 = txtCorreo.Text == string.Empty ? null : txtCorreo.Text.Trim();
        //            notaCredito.correo2 = txtCorreo2.Text == string.Empty ? null : txtCorreo2.Text.Trim();

        //        }

        //        if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Factura && !_doc.reporteElectronic)
        //        {
        //            notaCredito.tipoDocumento = (int)Enums.TipoDocumento.NotaCredito;

        //        }
        //        else
        //        {
        //            notaCredito.tipoDocumento = (int)Enums.TipoDocumento.NotaCreditoElectronica;

        //        }

        //        notaCredito.tipoIdCliente = _doc.tipoIdCliente;
        //        notaCredito.tipoIdEmpresa = _doc.tipoIdEmpresa;
        //        notaCredito.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();
        //        notaCredito.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();

        //        //datos de nota de credito
        //        notaCredito.tipoDocRef = _doc.tipoDocumento;
        //        notaCredito.claveRef = _doc.clave;
        //        notaCredito.fechaRef = _doc.fecha;
        //        notaCredito.codigoRef = (int)Enums.TipoRef.CorrigeMonto;
        //        notaCredito.razon = "Corregir Monto";
        //        notaCredito.observaciones = "Corregir Monto";

        //        List<tbDetalleDocumento> lista = new List<tbDetalleDocumento>();
        //        foreach (var item in _docDetalleNotaCredito)
        //        {
        //            tbDetalleDocumento detalle = new tbDetalleDocumento();
        //            detalle.precioCosto = item.precioCosto;
        //            detalle.cantidad = item.cantidad;
        //            detalle.descuento = item.descuento;
        //            detalle.idProducto = item.idProducto;
        //            detalle.montoTotal = item.montoTotal;
        //            detalle.montoTotalDesc = item.montoTotalDesc;
        //            detalle.montoTotalExo = item.montoTotalExo;
        //            detalle.montoTotalImp = item.montoTotalImp;
        //            detalle.numLinea = item.numLinea;
        //            detalle.precio = item.precio;
        //            detalle.porcExo = item.porcExo;
        //            detalle.porcImp = item.porcImp;
        //            detalle.totalLinea = item.totalLinea;
        //            lista.Add(detalle);

        //        }
        //        notaCredito.tbDetalleDocumento = lista;
        //        notaCredito = factIns.guadar(notaCredito);
        //        return notaCredito;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
