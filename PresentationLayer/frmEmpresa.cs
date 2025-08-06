using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmEmpresa : Form
    {
        BActividadesEconomicas actIns = new BActividadesEconomicas();
        BEmpresa empresaIns = new BEmpresa();
        tbEmpresa empresaGlobal = new tbEmpresa();
        List<tbEmpresaActividades> listaActividades = new List<tbEmpresaActividades>();
        tbEmpresaActividades empresaAct = new tbEmpresaActividades();
        public frmEmpresa()
        {
            InitializeComponent();
        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {

            //MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
            // Utility.EnableDisableForm(ref tabControl1, false);
            tabControl1.Enabled = false;
            cboTipoFactRegimenSimplificado.Enabled = false;
            tlsBtnBuscar.Enabled = false;
            tlsBtnCancelar.Enabled = false;
            tlsBtnEliminar.Enabled = false;
            tlsBtnSalir.Enabled = true;
            tlsBtnNuevo.Enabled = false;
            tlsBtnGuardar.Enabled = false;
            cargarDatos();

        }

        private void cargarDatos()
        {
            cboTipoFactRegimenSimplificado.DataSource = Enum.GetValues(typeof(Enums.TipoFacturacionElectRegimenSimplificado));
            try
            {
                empresaGlobal.id = Global.Usuario.tbEmpresa.id.Trim();
                empresaGlobal.tipoId = Global.Usuario.tbEmpresa.tipoId;
                empresaGlobal = empresaIns.getEntity(empresaGlobal);
                cargarForm(empresaGlobal);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargarForm(tbEmpresa empresa)
        {

            try
            {

                txtIdEmpresa.Text = empresa.id.Trim();
                if (empresa.tipoId == (int)Enums.TipoId.Fisica)
                {
                    txtNombreEmpresa.Text = empresa.tbPersona.nombre.Trim().ToUpper() + " " + empresa.tbPersona.apellido1.Trim().ToUpper() + " " + empresa.tbPersona.apellido2.Trim().ToUpper();

                }
                else
                {
                    txtNombreEmpresa.Text = empresa.tbPersona.nombre.Trim().ToUpper();

                }



                //datos hacienda
                chkAmbienteDesa.Checked = (bool)!empresa.ambientePruebas;
                txtUsuarioHacienda.Text = empresa.usuarioApiHacienda.Trim();
                txtConstrasenaHacienda.Text = empresa.claveApiHacienda.Trim();

                chkRegimenSimplificado.Checked = empresa.regimenSimplificado;
                if (chkRegimenSimplificado.Checked)
                {
                    cboTipoFactRegimenSimplificado.Text = Enum.GetName(typeof(Enums.TipoFacturacionElectRegimenSimplificado), empresa.tipoFacturacionRegimen);

                }
                else
                {
                    cboTipoFactRegimenSimplificado.ResetText();
                }

                txtCertificado.Text = empresa.certificadoInstalado.Trim();
                txtPinCertificado.Text = empresa.pin.ToString().Trim();

                txtRutaXML.Text = empresa.rutaCertificado.Trim();
                txtXMLCompras.Text = empresa.rutaXMLCompras.Trim();

                txtResolucion.Text = empresa.numeroResolucion.Trim();
                txtFechaResolucion.Text = empresa.fechaResolucio.ToString().Trim();



                chkImprimeDoc.Checked = (bool)empresa.imprimeDoc;
                if (chkImprimeDoc.Checked)
                {
                    txtNombreImpresora.Text = empresa.nombreImpresora.Trim();
                }


                cargarListaActividad(empresa.tbEmpresaActividades);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CargaCertificado()
        {
            try
            {

                frmCertificado iCertificados = new frmCertificado();
                iCertificados.ShowDialog();
                txtCertificado.Text = iCertificados.thumbprint;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string BuscaCertificado()
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog1.SelectedPath + "\\";
                }
                // return iFile.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }


        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accionMenu(e.ClickedItem.Text);
        }

        private void accionMenu(string accion)
        {

            switch (accion)
            {
                case "Guardar":

                    modificar();
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    tlsBtnBuscar.Enabled = false;
                    tlsBtnCancelar.Enabled = false;
                    tlsBtnEliminar.Enabled = false;
                    tlsBtnSalir.Enabled = true;

                    tabControl1.Enabled = false;
                    break;

                case "Nuevo":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxDatos, true);

                    Utility.ResetForm(ref gbxDatos);
                    break;

                case "Modificar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);

                    tlsBtnSalir.Enabled = true;
                    //tlsBtnCancelar.Enabled = false;
                    txtIdEmpresa.Enabled = false;
                    txtNombreEmpresa.Enabled = false;

                    tabControl1.Enabled = true;


                    break;
                case "Eliminar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);

                    break;
                case "Buscar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref gbxDatos, true);

                    break;
                case "Cancelar":

                    Utility.ResetForm(ref gbxDatos);
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    //Utility.EnableDisableForm(ref gbxDatos, false);
                    tabControl1.Enabled = false;
                    tlsBtnBuscar.Enabled = false;
                    tlsBtnCancelar.Enabled = false;
                    tlsBtnEliminar.Enabled = false;
                    tlsBtnSalir.Enabled = true;
                    btnAgregarAct.Enabled = true;
                    btnEliminarAct.Enabled = true;

                    btnAgregarAct.ResetText();
                    txtNombreComercial.ResetText();
                    txtNombreAct.ResetText();
                    txtCorreoCompras.ResetText();
                    txtCorreoEmpresa.ResetText();
                    txtContraseCorreo.ResetText();
                    txtAsuntoCorreo.ResetText();
                    txtCuerpoCorreo.ResetText();




                    cargarDatos();

                    break;
                case "Salir":
                    this.Close();
                    break;
            }

        }


        public void modificar()
        {
            DialogResult resp = MessageBox.Show("Esta seguro que desea modificar los datos?", "Guardar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (resp == DialogResult.Yes)
                {

                    if (validarDatos())
                    {
                        bool isNuevo = false;
                        if (empresaGlobal == null)
                        {
                            empresaGlobal = new tbEmpresa();
                            isNuevo = true;
                        }


                        //tab 1 empresa

                        empresaGlobal.nombreImpresora = txtNombreImpresora.Text.Trim();
                        empresaGlobal.imprimeDoc = chkImprimeDoc.Checked;



                        //tab 2 DATOS HACIENDA

                        empresaGlobal.ambientePruebas = !chkAmbienteDesa.Checked;
                        empresaGlobal.usuarioApiHacienda = txtUsuarioHacienda.Text;
                        empresaGlobal.claveApiHacienda = txtConstrasenaHacienda.Text;


                        empresaGlobal.regimenSimplificado = chkRegimenSimplificado.Checked;
                        empresaGlobal.tipoFacturacionRegimen = empresaGlobal.regimenSimplificado ? (int)cboTipoFactRegimenSimplificado.SelectedValue : 0;


                        empresaGlobal.certificadoInstalado = txtCertificado.Text;
                        empresaGlobal.pin = int.Parse(txtPinCertificado.Text);

                        empresaGlobal.rutaCertificado = txtRutaXML.Text;
                        empresaGlobal.rutaXMLCompras = txtXMLCompras.Text;

                        empresaGlobal.numeroResolucion = txtResolucion.Text;
                        empresaGlobal.fechaResolucio = DateTime.Parse(txtFechaResolucion.Text);

                        guardarActividad();

                        try
                        {
                            if (isNuevo)
                            {
                                // empresaGlobal.tbEmpresaActividades = listaActividades;

                                empresaGlobal = empresaIns.guardar(empresaGlobal);
                            }
                            else
                            {
                                // empresaGlobal.tbEmpresaActividades = listaActividades;

                                empresaGlobal = empresaIns.modificar(empresaGlobal);

                            }

                            cargarDatos();
                            MessageBox.Show("Los datos han sido actualizados en la base de datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Global.Usuario.tbEmpresa = empresaGlobal;
                        }


                        catch (Exception)
                        {

                            MessageBox.Show("No se pudieron guardar los datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (UpdateEntityException ex)
            {
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private bool validarDatos()
        {


            //if (chkRegimenSimplificado.Checked)
            //{
            //    if (cboTipoFactRegimenSimplificado.Text==String.Empty)
            //    {
            //        MessageBox.Show("Debe indicar Tipo Facturación electrónica para el régimen simplificado", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        cboTipoFactRegimenSimplificado.Focus();
            //        return false;
            //    }

            //}
            //else if (txtUsuarioHacienda.Text.Trim()==string.Empty)
            //{

            //    MessageBox.Show("Debe completar el campo de Usuario Hacienda","Faltan datos", MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    txtUsuarioHacienda.Focus();
            //    return false;
            //}
            //else if (txtConstrasenaHacienda.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe completar el campo de Contraseña Hacienda", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtConstrasenaHacienda.Focus();
            //    return false;
            //}
            //else if (txtCertificado.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe completar el campo de Certificado", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtCertificado.Focus();
            //    return false;
            //}
            //else if (txtPinCertificado.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe completar el campo de PIN Certificado", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPinCertificado.Focus();
            //    return false;
            //}
            //else if (txtRutaXML.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe indicar donde se guardarán los XML emitidos por el sistema", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtRutaXML.Focus();
            //    return false;
            //}
            //else if (txtXMLCompras.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe indicar donde se guardarán los XML de Compras", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtXMLCompras.Focus();
            //    return false;
            //}
            //else if (txtResolucion.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe indicar la resolución Hacienda", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtResolucion.Focus();
            //    return false;
            //}
            //else if (txtFechaResolucion.Text.Trim() == string.Empty)
            //{

            //    MessageBox.Show("Debe indicar la fecha de resolución Hacienda", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtFechaResolucion.Focus();
            //    return false;
            //}

            if (chkImprimeDoc.Checked && txtNombreImpresora.Text.Trim() == string.Empty)
            {

                MessageBox.Show("Debe indicar el nombre de la impresora.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombreImpresora.Focus();
                return false;
            }


            return true;


        }

        private void btnCertificado_Click_1(object sender, EventArgs e)
        {
            try
            {
                CargaCertificado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void chkRegimenSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegimenSimplificado.Checked)
            {
                cboTipoFactRegimenSimplificado.Enabled = true;
            }
            else
            {
                cboTipoFactRegimenSimplificado.Enabled = false;
            }
        }

        private void btnRutaCompras_Click(object sender, EventArgs e)
        {
            try
            {
                txtXMLCompras.Text = BuscaCertificado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            try
            {
                txtRutaXML.Text = BuscaCertificado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActividadEconomica_Click(object sender, EventArgs e)
        {
            buscarAct buscar = new buscarAct();


            buscar.pasarDatosEvent += pasarDatos;

            buscar.ShowDialog();

        }

        private void pasarDatos(tbActividades entity)
        {

            cargarDatosActividadEconica(entity);
        }

        private void cargarDatosActividadEconica(tbActividades act)
        {
            txtActEconomica.Text = act.codigoAct;
            txtNombreAct.Text = act.nombreAct.Trim().ToUpper();


        }

        private void txtActEconomica_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string codigo = txtActEconomica.Text.Trim().ToUpper();
                if (codigo != string.Empty)
                {
                    buscarAct();

                }
                else
                {
                    txtActEconomica.ResetText();
                    txtNombreAct.ResetText();

                }

            }
        }

        private void buscarAct()
        {
            try
            {
                tbActividades act = new tbActividades();
                act.codigoAct = txtActEconomica.Text.Trim().ToUpper();
                act = actIns.getById(act);
                if (act != null)
                {
                    txtActEconomica.Text = act.codigoAct;
                    txtNombreAct.Text = act.nombreAct.Trim().ToUpper();
                }
                else
                {
                    txtActEconomica.ResetText();
                    txtNombreAct.ResetText();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargarListaActividad(IEnumerable<tbEmpresaActividades> listaActividades)
        {
            lstvActividades.Items.Clear();
            foreach (var act in listaActividades)
            {
                ListViewItem item = new ListViewItem();
                item.Text = act.CodActividad;
                txtActEconomica.Text = act.CodActividad; buscarAct();
                item.SubItems.Add(txtNombreAct.Text.Trim().ToUpper());
                lstvActividades.Items.Add(item);
            }
            txtActEconomica.ResetText();
            txtNombreAct.ResetText();
        }

        private void resetFormActividadComercial()
        {
            txtNombreAct.ResetText();
            txtActEconomica.Text = string.Empty;
            txtContraseCorreo.Text = string.Empty;
            txtCorreoEmpresa.Text = string.Empty;
            txtCuerpoCorreo.Text = string.Empty;
            txtNombreComercial.Text = string.Empty;
            txtAsuntoCorreo.Text = string.Empty;
            empresaAct = null;
            txtCorreoCompras.ResetText();
            txtClaveAplicacion.ResetText();
        }

        private bool validarDatosActividadEco()
        {

            if (txtActEconomica.Text.Trim() == string.Empty || txtNombreAct.Text == string.Empty)
            {

                MessageBox.Show("Debe indicar la actividad Económica", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtActEconomica.Focus();
                return false;
            }
            else if (listaActividades.Where(x => x.CodActividad.Trim().ToUpper() == txtActEconomica.Text.Trim().ToUpper()).SingleOrDefault() != null)
            {
                MessageBox.Show("Ya existe esa actividad económica asiganada", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtActEconomica.Focus();
                txtNombreAct.ResetText();
                txtActEconomica.ResetText();
                return false;

            }

            else if (txtNombreComercial.Text.Trim() == string.Empty)
            {

                MessageBox.Show("Debe indicar el nombre comercial", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombreComercial.Focus();
                return false;
            }

            else if (txtCorreoEmpresa.Text.Trim() == string.Empty)
            {

                MessageBox.Show("Debe indicar el correo de la empresa", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCorreoEmpresa.Focus();
                return false;
            }
            else if (!Utility.isValidEmail(txtCorreoEmpresa.Text))
            {

                MessageBox.Show("El correo indicado no tiene formato correcto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCorreoEmpresa.Focus();
                return false;
            }
            else if (txtContraseCorreo.Text.Trim() == string.Empty)
            {

                MessageBox.Show("Debe indicar una contraseña para el correo institucional", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContraseCorreo.Focus();
                return false;
            }
            else if (txtAsuntoCorreo.Text.Trim() == string.Empty)
            {

                MessageBox.Show("Debe indicar un asunto para el envio de correos.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAsuntoCorreo.Focus();
                return false;
            }
            else if (txtCuerpoCorreo.Text.Trim() == string.Empty)
            {

                MessageBox.Show("Debe indicar mensaje para el cuerpo de los correos institucionales.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCuerpoCorreo.Focus();
                return false;
            }


            return true;
        }

        private void lstvActividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvActividades.SelectedItems.Count > 0)
            {

                empresaAct = empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim().ToUpper() == lstvActividades.SelectedItems[0].Text.Trim().ToUpper()).SingleOrDefault();


                txtActEconomica.Text = empresaAct.CodActividad.Trim();
                buscarAct();
                txtContraseCorreo.Text = empresaAct.contrasenaCorreo;
                txtCorreoEmpresa.Text = empresaAct.correoElectronicoEmpresa;
                txtCuerpoCorreo.Text = empresaAct.cuerpoCorreo.Trim().ToUpper();
                txtNombreComercial.Text = empresaAct.nombreComercial.Trim().ToUpper();
                txtAsuntoCorreo.Text = empresaAct.subjectCorreo.Trim().ToUpper();
                txtCorreoCompras.Text = empresaAct.correoCompras.Trim();
                btnAgregarAct.Enabled = false;
                btnEliminarAct.Enabled = false;
                chkPrincipal.Checked = (bool)empresaAct.principal;
                txtClaveAplicacion.Text = empresaAct.claveCorreo.Trim();

            }
        }

        private void btnEliminarAct_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea eliminar la actividad económoica?", "Actividad Económica", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                empresaGlobal.tbEmpresaActividades.Remove(empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == lstvActividades.SelectedItems[0].Text.Trim()).SingleOrDefault());
                cargarListaActividad(empresaGlobal.tbEmpresaActividades);
                resetFormActividadComercial();

            }

        }

        private void txtActEconomica_TextChanged(object sender, EventArgs e)
        {

        }

        private void tlsBtnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarAct_Click(object sender, EventArgs e)
        {

            guardarActividad();

        }

        private void guardarActividad()
        {
            if (validarDatosActividadEco())
            {
                if (empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault() == null)
                {
                    tbEmpresaActividades act = new tbEmpresaActividades();
                    act.CodActividad = txtActEconomica.Text;
                    act.contrasenaCorreo = txtContraseCorreo.Text;
                    act.correoElectronicoEmpresa = txtCorreoEmpresa.Text.Trim();
                    act.cuerpoCorreo = txtCuerpoCorreo.Text.ToUpper();
                    act.nombreComercial = txtNombreComercial.Text.ToUpper();
                    act.subjectCorreo = txtAsuntoCorreo.Text.ToUpper();
                    act.idEmpresa = Global.Usuario.tbEmpresa.id;
                    act.tipoId = Global.Usuario.tbEmpresa.tipoId;
                    act.correoCompras = txtCorreoCompras.Text.Trim();
                    act.principal = chkPrincipal.Checked;
                    act.claveCorreo = txtClaveAplicacion.Text.Trim();
                    // act.tbEmpresa = Global.Usuario.tbEmpresa;
                    empresaGlobal.tbEmpresaActividades.Add(act);
                }
                else
                {

                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().contrasenaCorreo = txtContraseCorreo.Text;
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().correoElectronicoEmpresa = txtCorreoEmpresa.Text.Trim();
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().cuerpoCorreo = txtCuerpoCorreo.Text.ToUpper();
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().nombreComercial = txtNombreComercial.Text.ToUpper();
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().subjectCorreo = txtAsuntoCorreo.Text.ToUpper();
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().correoCompras = txtCorreoCompras.Text.Trim();
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().claveCorreo = txtClaveAplicacion.Text.Trim();
                    empresaGlobal.tbEmpresaActividades.Where(x => x.CodActividad.Trim() == txtActEconomica.Text.Trim()).SingleOrDefault().principal = chkPrincipal.Checked;

                }



                cargarListaActividad(empresaGlobal.tbEmpresaActividades);
                resetFormActividadComercial();
            }
        }

        private void txtRutaXML_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://myaccount.google.com/apppasswords?pli=1&rapt=AEjHL4PXBIhhKFKz0Ja5TAtLpnR1u1a_OKPecPakxqMmeVqoUwgEPIV-A7D9jnbD4ZLD8GBXCBFGBoVr7RX5HiQCP7Ke_1-3352kuJGGeabBYKEzWN-Qnao";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la URL: " + ex.Message);
            }
        }
    }
}
