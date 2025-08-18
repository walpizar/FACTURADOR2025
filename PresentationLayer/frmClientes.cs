using BusinessLayer;
using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.BusisnessExceptions;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using FacturacionElectronicaLayer.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmClientes : Form
    {
        int bandera = 1;
        tbClientes clienteGlobal;
        Bcliente clienteInst = new Bcliente();
        BProvincias provinciasIns = new BProvincias();
        BTipoId tipoIdIns = new BTipoId();
        BExoneraciones exoneraIns = new BExoneraciones();

        public delegate void pasaDatos(tbClientes entity);
        public event pasaDatos pasarDatosEvent;

        public string id { get; set; }


        List<tbProvincia> provinciasGlo = null;
        List<tbCanton> cantonesGlo = null;
        List<tbDistrito> distritosGlo = null;

        //EL NEW CREA UNA NUEVA INSTANCIA...
        // LAS INSTANCIAS SON OBJETOS NUEVOS....
        public frmClientes()
        {
            InitializeComponent();
        }



        private bool validarCampos()
        // METODO DE VALIDAR PARA APLICAR A CAMPOS OBLIGATORIOS...
        {
            if (cbotipoId.Text == string.Empty)
            {
                cbotipoId.Focus();
                MessageBox.Show("Debe indicar tipo de Identificacion", "mensaje de prueba");
                return false;
            }

            if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica)
            {
                if (mskidentificacion.Text == string.Empty)
                {
                    MessageBox.Show("Debe de ingresar una identificación");
                    mskidentificacion.Focus();
                    return false;

                }
                if (mskidentificacion.Text.Length != 9)
                {
                    MessageBox.Show("La identificación no tiene el formato correcto.");
                    mskidentificacion.Focus();
                    return false;
                }
            }
            else if ((int)cbotipoId.SelectedValue != (int)Enums.TipoId.Fisica)
            {
                if (txtIdentificacion.Text == string.Empty)
                {
                    MessageBox.Show("Debe de ingresar su número de Cédula");
                    txtIdentificacion.Focus();
                    return false;
                }

            }

            else if ((int)cbotipoId.SelectedValue != (int)Enums.TipoId.Fisica)
            {
                if (txtIdentificacion.Text.Length != 10)
                {
                    MessageBox.Show("La cédula jurídica no tiene el formato correcto");
                    txtIdentificacion.Focus();
                    return false;
                }

            }

            if (txtnombre.Text == string.Empty)
            {
                MessageBox.Show("Debe de Ingresar su Nombre");
                txtnombre.Focus();
                return false;
            }
            if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica ||
                (int)cbotipoId.SelectedValue == (int)Enums.TipoId.Dimex)
            {
                if (txtapellido1.Text == string.Empty)
                {
                    MessageBox.Show("Debe debe de ingresar su apellido");
                    txtapellido1.Focus();
                    return false;
                }

                if (txtapellido2.Text == string.Empty)
                {
                    MessageBox.Show("Debe debe de ingresar su apellido");
                    txtapellido2.Focus();
                    return false;
                }

            }


            if (cbotipoCliente.Text == string.Empty)
            {
                cbotipoCliente.Focus();
                MessageBox.Show("Debe indicar tipo cliente");
                return false;
            }


            if (msktelefono.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un numero de telefono");
                msktelefono.Focus();
                return false;
            }
            if (txtcorreo.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un correo eletrónico");
                txtcorreo.Focus();
                return false;
            }
            if (!Utility.isValidEmail(txtcorreo.Text))
            {

                MessageBox.Show("El correo electrónico indicado no tiene un formato válido.");
                txtcorreo.Focus();
                return false;
            }

            if (cboProvincia.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar una provincia");
                cboProvincia.Focus();
                return false;
            }

            if (cboCanton.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un cantón");
                cboCanton.Focus();
                return false;
            }

            if (cboDistrito.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un distrito");
                cboDistrito.Focus();
                return false;
            }

            if (cboPrecioAplicar.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un precio aplicar");
                cboPrecioAplicar.Focus();
                return false;
            }

            if (txtDescMaxInt.Text == string.Empty)
            {

                txtDescMaxInt.Text = "0";
            }
            if (txtCreditoMaxInt.Text == string.Empty)
            {

                txtCreditoMaxInt.Text = "0";
            }
            if (txtPlazoMaxInt.Text == string.Empty)
            {

                txtPlazoMaxInt.Text = "0";
            }
            if (chkAplicaExo.Checked)
            {

                if (cboExoneracion.Text == string.Empty)
                {
                    MessageBox.Show("Debe indicar un tipo de exoneración");
                    cboExoneracion.Focus();
                    return false;

                }

                if (txtDocExo.Text == string.Empty)
                {
                    MessageBox.Show("Debe indicar el número de documento de exoneración");
                    txtDocExo.Focus();
                    return false;

                }
                if (txtInstitucionExo.Text == string.Empty)
                {
                    MessageBox.Show("Debe indicar la institución que emitió la exoneración");
                    txtInstitucionExo.Focus();
                    return false;

                }
                if (dtpFechaEmisionExo.Text == string.Empty)
                {
                    MessageBox.Show("Debe indicar la fecha de emisión de la exoneración");
                    dtpFechaEmisionExo.Focus();
                    return false;

                }
                if (cboPorcExo.Text == string.Empty)
                {
                    MessageBox.Show("Debe indicar el porcentaje de exoneración");
                    cboPorcExo.Focus();
                    return false;

                }

            }

            return true;
        }



        private bool guardarCliente()
        {//SE CREAN LAS INSTANCIAS
            tbClientes cliente = new tbClientes();
            tbPersona persona = new tbPersona();
            bool processOk = false;
            try
            {
                if (validarCampos())
                {//SE VALIDAN LOS CAMPOS OBLIGATORIOS...
                 // SE LLENAN LOS DATOS DE PERSONA PRIMERO... PRIMERO SE ES PERSONA Y LUEGO CLIENTE...
                    persona.tipoId = (int)cbotipoId.SelectedValue;


                    if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica ||
                        (int)cbotipoId.SelectedValue == (int)Enums.TipoId.Dimex)
                    {
                        if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica)
                        {
                            persona.identificacion = mskidentificacion.Text.Trim();

                        }
                        else
                        {
                            persona.identificacion = txtIdentificacion.Text.Trim();

                        }
                        //   persona.identificacion = mskidentificacion.Text.Trim();
                        persona.apellido1 = txtapellido1.Text.ToUpper().Trim();
                        persona.apellido2 = txtapellido2.Text.ToUpper().Trim();
                        persona.fechaNac = DateTime.Parse(dtpfechaNa.Text);
                        if (rbtmasc.Checked)
                        {
                            persona.sexo = 1;
                        }
                        else
                        {
                            persona.sexo = 2;
                        }
                    }
                    else
                    {
                        persona.identificacion = txtIdentificacion.Text.Trim();
                    }
                    persona.nombre = txtnombre.Text.ToUpper().Trim();


                    persona.correoElectronico = txtcorreo.Text.Trim();
                    persona.telefono = int.Parse(msktelefono.Text);
                    persona.provincia = cboProvincia.SelectedValue.ToString();
                    persona.canton = cboCanton.SelectedValue.ToString();
                    persona.distrito = cboDistrito.SelectedValue.ToString();
                    persona.codigoPaisTel = "506";
                    persona.otrasSenas = txtOtrasSenas.Text;
                    persona.barrio = cboBarrios.SelectedValue.ToString();

                    // AQUI ES DONDE QUE ESA PERSONA TAMBIEN ES CLIENTE...
                    cliente.tbPersona = persona;
                    cliente.contribuyente = false;
                    cliente.id = persona.identificacion;
                    cliente.tipoId = persona.tipoId;
                    cliente.tipoCliente = (int)cbotipoCliente.SelectedValue;
                    cliente.tbPersona.otrasSenas = txtOtrasSenas.Text.ToUpper().Trim();
                    cliente.exonera = chkAplicaExo.Checked;

                    if (chkAplicaExo.Checked)
                    {
                        cliente.institucionExo = txtInstitucionExo.Text.ToUpper().Trim();
                        if (cboExoneracion.Text != string.Empty)
                        {
                            cliente.idExonercion = (int)cboExoneracion.SelectedValue;
                            //if (cliente.idExonercion==null)
                            //{
                            //    cliente.idExonercion = 3;
                            //}
                        }
                        cliente.FechaEmisionExo = dtpFechaEmisionExo.Value;
                        cliente.numeroDocumentoExo = txtDocExo.Text;
                        cliente.porcExo = int.Parse(cboPorcExo.Text);
                    }


                    cliente.correoElectConta = txtCorreoContabilidad.Text;
                    cliente.creditoMax = int.Parse(txtCreditoMaxInt.Text);
                    cliente.descuentoMax = int.Parse(txtDescMaxInt.Text.Trim());
                    cliente.aplicaDescAuto = chkDescAuto.Checked;
                    cliente.descripcion = txtObserv.Text;
                    cliente.encargadoConta = txtEncargadoTrib.Text;
                    cliente.plazoCreditoMax = int.Parse(txtPlazoMaxInt.Text);
                    cliente.nombreTributario = txtNombreTribut.Text;
                    cliente.precioAplicar = (int)int.Parse(cboPrecioAplicar.Text.Substring(0, 1));


                    cliente.estado = true;
                    cliente.fecha_crea = Utility.getDate();
                    cliente.fecha_ult_mod = Utility.getDate();
                    cliente.usuario_crea = Global.Usuario.nombreUsuario;
                    cliente.usuario_ult_crea = Global.Usuario.nombreUsuario;
                    // CON NUESTRA INSTACIA LLAMAMOS AL METODO GUARDAR.... Y LE MANDAMOS A CLIENTE...

                    cliente = clienteInst.Guardar(cliente);

                    if (id != string.Empty)
                    {
                        clienteGlobal = cliente;
                    }
                    processOk = true;
                    MessageBox.Show("Los datos han sido almacenada en la base de datos.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }

            catch (EntityDisableStateException ex)
            {

                //LOS CATCH EVITAN QUE LOS APLICATIVOS SE CAIGAN....
                DialogResult result = MessageBox.Show("Datos ya existe en la base datos, ¿Desea actualizarlos?", "Datos Existentes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {


                    clienteGlobal = cliente;
                    actualizarCliente();
                    processOk = actualizarCliente();
                }

            }
            catch (EntityExistException ex)
            {
                MessageBox.Show(ex.Message);
                processOk = false;
            }
            catch (Exception ex)
            {
                // AQUI ES DONDE LAS EXCEPCIONES SON ATRAPADAS Y SE MUESTRA EL MENSAJE PERSONALIZADO...
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                processOk = false;
            }

            return processOk;
        }


        private bool Eliminar()
        {
            //ELIMINAR SOLO MODIFICA EL ESTADO... 
            // Y ACTUALIZA LOS DATOS AUDITORIA....
            bool processOk = false;
            try
            {
                DialogResult result = MessageBox.Show("¿Se encuentra seguro de eliminar los datos?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                {
                    if (result == DialogResult.Yes)
                    {

                        clienteGlobal.estado = false;
                        clienteGlobal.fecha_ult_mod = Utility.getDate();
                        clienteGlobal.usuario_ult_crea = Global.Usuario.nombreUsuario;
                        clienteGlobal = clienteInst.Modificar(clienteGlobal);
                        processOk = true;

                        MessageBox.Show("Los datos han sido eliminados de la base datos.", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (SaveEntityException ex)
            {
                processOk = false;
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

            return processOk;

        }


        private void cbotipoId_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiarTiposId();


        }

        private void cambiarTiposId()
        {
            if (cbotipoId.SelectedValue != null)
            {
                if ((int)cbotipoId.SelectedValue != (int)Enums.TipoId.Fisica)
                {
                    txtIdentificacion.Mask = (int)cbotipoId.SelectedValue == (int)Enums.TipoId.Juridica ? "##########" : "";
                    mskidentificacion.Visible = false;
                    txtIdentificacion.Visible = true;
                    txtIdentificacion.Enabled = true;
                    if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Dimex)
                    {

                        txtapellido1.Enabled = true;
                        txtapellido2.Enabled = true;
                        gboSexo.Enabled = true;
                        dtpfechaNa.Enabled = true;
                    }
                    else
                    {
                        txtapellido1.Enabled = false;
                        txtapellido2.Enabled = false;
                        gboSexo.Enabled = false;
                        dtpfechaNa.Enabled = false;
                    }




                }
                else
                {
                    mskidentificacion.Enabled = true;
                    mskidentificacion.Visible = true;
                    txtIdentificacion.Visible = false;
                    txtapellido1.Enabled = true;
                    txtapellido2.Enabled = true;
                    gboSexo.Enabled = true;
                    dtpfechaNa.Enabled = true;
                }

            }
        }



        private void accionMenu(string accion)
        {
            // ESTAS SON LAS DISTINTAS ACCIONES QUE HARA EL MENU EN LOS DISTINTOS EVENTOS EVENTOS
            // SE ESTAN LLAMANDO DESDE ...MENUGENERICO...FORMULARIOGENERICO...
            switch (accion)
            {
                case "Guardar":

                    if (Guardar(bandera))

                    {
                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                        Utility.EnableDisableForm(ref gbxCliente, false);
                        Utility.ResetForm(ref gbxCliente);
                        txtapellido1.Enabled = false;
                        txtapellido2.Enabled = false;
                        txtIdentificacion.Enabled = false;
                        mskidentificacion.Enabled = false;
                        cboPorcExo.SelectedIndex = 0;
                        txtDocExo.ResetText();

                    }
                    break;
                case "Nuevo":
                    bandera = 1;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxCliente, true);
                    Utility.ResetForm(ref gbxCliente);

                    cboPorcExo.SelectedIndex = 0;
                    cbotipoId.SelectedIndex = 0;
                    cboExoneracion.SelectedValue = "";
                    cbotipoCliente.SelectedIndex = 0;
                    btnBuscarCliente.Enabled = true;
                    rbtmasc.Checked = true;
                    rbtfem.Checked = false;
                    gbxExoneracion.Enabled = false;
                    txtDocExo.ResetText();
                    limpiar();
                    break;

                case "Modificar":
                    bandera = 2;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxCliente, true);
                    mskidentificacion.Enabled = false; txtIdentificacion.Enabled = false;
                    cambiarTiposId();
                    cbotipoId.Enabled = false;
                    txtIdentificacion.Enabled = false;
                    mskidentificacion.Enabled = false;
                    btnBuscarCliente.Enabled = false;
                    if ((bool)clienteGlobal.exonera)
                    {

                        gbxExoneracion.Enabled = true;
                    }
                    else
                    {
                        gbxExoneracion.Enabled = false;
                    }
                    break;
                case "Eliminar":
                    bandera = 3;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    mskidentificacion.Enabled = false; txtIdentificacion.Enabled = false;
                    cbotipoId.Enabled = false;
                    btnBuscarCliente.Enabled = false;
                    break;
                case "Buscar":
                    buscar();
                    if (clienteGlobal == null)
                    {
                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                        Utility.EnableDisableForm(ref gbxCliente, false);
                        Utility.ResetForm(ref gbxCliente);
                    }
                    else
                    {
                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                        Utility.EnableDisableForm(ref gbxCliente, false);
                    }

                    break;
                case "Cancelar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                    Utility.EnableDisableForm(ref gbxCliente, false);
                    Utility.ResetForm(ref gbxCliente);
                    txtDocExo.ResetText();
                    cboPorcExo.SelectedIndex = 0;
                    txtapellido1.Enabled = false;
                    txtapellido2.Enabled = false;
                    txtIdentificacion.Enabled = false;
                    mskidentificacion.Enabled = false;
                    break;
                case "Salir":
                    this.Close();
                    break;
            }

        }


        private bool Guardar(int trans)
        {//PROCESSOK VARIABLE PARA QUE NO SE DESACTIVE EL FORMULARIO EN CASO DE QUE FALTE UN DATO OBLIGATORIO...
            bool processOk = false;
            switch (trans)
            {

                case 1:
                    processOk = guardarCliente();
                    break;

                case 2:
                    processOk = actualizarCliente();
                    break;

                case 3:
                    processOk = Eliminar();
                    break;
            }

            return processOk;
        }
        private void dataBuscar(tbClientes cliente)
        {// metodo al cual le apunta el puntero... o delegado...
            clienteGlobal = cliente;
            try
            {//BUSCAR JALA LOS DATOS DE LAS DIFERENTEN TABLAS Y LAS METE EN LA VARIABLE CLIENTE GLOBAL...
                //LO HACE DESDE EL FORMULARIO DE BUSCAR... 
                if (clienteGlobal != null)
                {
                    if (clienteGlobal.id.Trim() != string.Empty)
                    {
                        {
                            //CargarCombos();
                            if (cliente.tipoId != (int)Enums.TipoId.Fisica)
                            {

                                txtIdentificacion.Text = clienteGlobal.tbPersona.identificacion.ToString().Trim();
                                if ((cliente.tipoId == (int)Enums.TipoId.Dimex))
                                {
                                    txtapellido1.Text = clienteGlobal.tbPersona.apellido1.ToString().Trim();
                                    txtapellido2.Text = clienteGlobal.tbPersona.apellido2.ToString().Trim();
                                }

                            }
                            else
                            {
                                mskidentificacion.Text = clienteGlobal.tbPersona.identificacion.Trim();
                                txtapellido1.Text = clienteGlobal.tbPersona.apellido1.ToString().Trim();
                                txtapellido2.Text = clienteGlobal.tbPersona.apellido2.ToString().Trim();
                            }
                            cbotipoId.SelectedValue = clienteGlobal.tipoId;
                            txtnombre.Text = clienteGlobal.tbPersona.nombre.ToString().Trim();

                            cbotipoCliente.SelectedValue = clienteGlobal.tipoCliente;

                            if (clienteGlobal.tbPersona.sexo == 1)
                            {
                                rbtmasc.Checked = true;
                            }
                            else if (clienteGlobal.tbPersona.sexo == 2)
                            {
                                rbtfem.Checked = true;
                            }
                            cbotipoCliente.Text = clienteGlobal.tbTipoClientes.nombre.ToString().Trim();//////////////
                            dtpfechaNa.Text = clienteGlobal.tbPersona.fechaNac.ToString().Trim();
                            txtcorreo.Text = clienteGlobal.tbPersona.correoElectronico.ToString().Trim();
                            msktelefono.Text = clienteGlobal.tbPersona.telefono.ToString().Trim();
                            txtOtrasSenas.Text = clienteGlobal.tbPersona.otrasSenas.ToString().Trim();
                            cboProvincia.SelectedValue = clienteGlobal.tbPersona.provincia;
                            cboCanton.SelectedValue = clienteGlobal.tbPersona.canton;
                            cboCanton.Refresh();
                            cboDistrito.SelectedValue = clienteGlobal.tbPersona.distrito;
                            cboBarrios.SelectedValue = clienteGlobal.tbPersona.barrio;
                            cboPrecioAplicar.SelectedIndex = clienteGlobal.precioAplicar - 1;
                            chkAplicaExo.Checked = (bool)clienteGlobal.exonera;
                            if (chkAplicaExo.Checked)
                            {
                                gbxExoneracion.Enabled = true;
                                txtInstitucionExo.Text = clienteGlobal.institucionExo.ToUpper().Trim();
                                cboExoneracion.SelectedValue = clienteGlobal.idExonercion;
                                dtpFechaEmisionExo.Value = clienteGlobal.FechaEmisionExo.Value;
                                txtDocExo.Text = clienteGlobal.numeroDocumentoExo.Trim();
                                cboPorcExo.SelectedItem = clienteGlobal.porcExo.ToString();

                            }
                            else
                            {
                                gbxExoneracion.Enabled = false;
                                txtInstitucionExo.Text = string.Empty;
                                cboExoneracion.ResetText();
                                cboPorcExo.SelectedIndex = 0;
                                dtpFechaEmisionExo.ResetText();
                                txtDocExo.Text = string.Empty;
                            }


                            cboPrecioAplicar.SelectedValue = clienteGlobal.precioAplicar;
                            txtCreditoMaxInt.Text = clienteGlobal.creditoMax.ToString();
                            txtDescMaxInt.Text = clienteGlobal.descuentoMax.ToString();
                            txtPlazoMaxInt.Text = clienteGlobal.plazoCreditoMax.ToString();
                            chkDescAuto.Checked = (bool)clienteGlobal.aplicaDescAuto;
                            txtObserv.Text = clienteGlobal.descripcion;

                            txtEncargadoTrib.Text = clienteGlobal.encargadoConta;
                            txtCorreoContabilidad.Text = clienteGlobal.correoElectConta;
                            txtNombreTribut.Text = clienteGlobal.nombreTributario;

                        }
                    }
                }
            }
            catch (ListEntityException ex)
            {

                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void buscar()
        {//FORMA NUEVA CON EL DELEGADO...

            FrmBuscar buscar = new FrmBuscar();
            buscar.pasarDatosEvent += dataBuscar;
            buscar.ShowDialog();


            //ESTA ERA LA FORMA SIN EL DELEGADO.....
            //FrmBuscar buscar = new FrmBuscar();
            //buscar.ShowDialog();
            //clienteGlobal = FrmBuscar.clienteGlo;


        }


        private bool actualizarCliente()
        {
            bool processOk = false;
            try
            {
                if (validarCampos())
                {
                    //NO SE PUEDE MODIFICAR DE NINGUNA FORMA ID, TIPOiD POR SER LLAVES PRIMARIAS...
                    //clienteGlobal.tbPersona.identificacion = cbotipoId.ToString().Trim();...EJEMPLO DE LO QUE  NO SE HACE..
                    if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica ||
                      (int)cbotipoId.SelectedValue == (int)Enums.TipoId.Dimex)
                    {


                        clienteGlobal.tbPersona.apellido1 = txtapellido1.Text.ToUpper().Trim();
                        clienteGlobal.tbPersona.apellido2 = txtapellido2.Text.ToUpper().Trim();
                        clienteGlobal.tbPersona.fechaNac = DateTime.Parse(dtpfechaNa.Text);
                        if (rbtmasc.Checked)
                        {
                            clienteGlobal.tbPersona.sexo = 1;
                        }
                        else
                        {
                            clienteGlobal.tbPersona.sexo = 2;
                        }
                    }

                    clienteGlobal.tbPersona.nombre = txtnombre.Text.ToUpper().Trim();


                    clienteGlobal.tbPersona.correoElectronico = txtcorreo.Text.Trim();
                    clienteGlobal.tbPersona.telefono = int.Parse(msktelefono.Text);
                    clienteGlobal.tbPersona.provincia = cboProvincia.SelectedValue.ToString();
                    clienteGlobal.tbPersona.canton = cboCanton.SelectedValue.ToString();
                    clienteGlobal.tbPersona.distrito = cboDistrito.SelectedValue.ToString();
                    clienteGlobal.tbPersona.codigoPaisTel = "506";
                    clienteGlobal.tbPersona.otrasSenas = txtOtrasSenas.Text.Trim();
                    clienteGlobal.tbPersona.barrio = cboBarrios.SelectedValue.ToString();

                    clienteGlobal.tipoCliente = (int)cbotipoCliente.SelectedValue;
                    clienteGlobal.tbPersona.otrasSenas = txtOtrasSenas.Text.ToUpper().Trim();


                    clienteGlobal.exonera = chkAplicaExo.Checked;
                    if ((bool)clienteGlobal.exonera)
                    {
                        clienteGlobal.institucionExo = txtInstitucionExo.Text.ToUpper().Trim();
                        if (cboExoneracion.Text != string.Empty)
                        {
                            clienteGlobal.idExonercion = (int)cboExoneracion.SelectedValue;

                        }
                        clienteGlobal.FechaEmisionExo = dtpFechaEmisionExo.Value;
                        clienteGlobal.numeroDocumentoExo = txtDocExo.Text.Trim();
                        clienteGlobal.porcExo = int.Parse(cboPorcExo.Text);

                    }




                    clienteGlobal.correoElectConta = txtCorreoContabilidad.Text.Trim();
                    clienteGlobal.creditoMax = int.Parse(txtCreditoMaxInt.Text.Trim());
                    clienteGlobal.descuentoMax = int.Parse(txtDescMaxInt.Text.Trim());
                    clienteGlobal.aplicaDescAuto = chkDescAuto.Checked;
                    clienteGlobal.descripcion = txtObserv.Text.Trim();
                    clienteGlobal.encargadoConta = txtEncargadoTrib.Text.Trim();
                    clienteGlobal.plazoCreditoMax = int.Parse(txtPlazoMaxInt.Text.Trim());
                    clienteGlobal.nombreTributario = txtNombreTribut.Text;
                    clienteGlobal.precioAplicar = (int)int.Parse(cboPrecioAplicar.Text.Substring(0, 1));

                    clienteGlobal.usuario_ult_crea = Global.Usuario.nombreUsuario;
                    clienteGlobal.fecha_ult_mod = Utility.getDate();
                    clienteGlobal = clienteInst.Modificar(clienteGlobal);
                    MessageBox.Show("Los datos han sido actualizados en la base de datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    processOk = true;
                }
            }
            catch (UpdateEntityException ex)
            {
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                processOk = false;

            }

            return processOk;
        }


        private void frmClientes_Load(object sender, EventArgs e)
        {// AQUI ES DONDE  SE INICIAN LOS COMANDOS DEL DE TODO EL APLICATIVO....

            CargarCombos();
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
            Utility.EnableDisableForm(ref gbxCliente, false);
            // Y SE COMIENZAN A  LLAMAR LOS METODOS....

            if (id != null && id != string.Empty)
            {

                MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                Utility.EnableDisableForm(ref gbxCliente, true);
                limpiar();
                mskidentificacion.Text = id;
                buscarClienteTribunal();

            }


        }

        private void limpiar()
        {
            chkAplicaExo.Checked = false;
            cambiarComboExoneracion();
            cboPrecioAplicar.SelectedIndex = 0;
            decimal desMax = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().descuentoBase;
            int desc = (int)desMax;
            txtDescMaxInt.Text = desc.ToString();
            txtCreditoMaxInt.Text = "100000";
            txtPlazoMaxInt.Text = Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().plazoMaximoCredito.ToString();
            chkDescAuto.Checked = false;
        }

        private void CargarCombos()
        {
            cargarTipoClientes();

            provinciasGlo = provinciasIns.getListTipoing((int)Enums.EstadoBusqueda.Activo);

            cboProvincia.ValueMember = "Cod";
            cboProvincia.DisplayMember = "Nombre";
            cboProvincia.DataSource = provinciasGlo;

            cbotipoId.ValueMember = "id";
            cbotipoId.DisplayMember = "nombre";
            cbotipoId.DataSource = tipoIdIns.getListaTipoId();


            cboExoneracion.ValueMember = "id";
            cboExoneracion.DisplayMember = "nombre";
            cboExoneracion.DataSource = exoneraIns.getListaExoneraciones();


        }

        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accionMenu(e.ClickedItem.Text);
        }


        private void cargarTipoClientes()
        {
            BTipoCliente clienteInst = new BTipoCliente();
            //METODO PARA RECUPERAR CLIENTES Y CARGAR EL COMBO CON LOS DATOS DE DE TIPOCLIENTE
            //DATASOURCE PARA TRAER LOS DATOS DE LA TABLA....
            //VALUEMENBER JALA DESDE EL ID...
            //DISPLAYMEMBER ES EL MDATO QUE SE VA A MOSTRAR EN EL COMBO....
            cbotipoCliente.DataSource = clienteInst.GetListEntities((int)Enums.EstadoBusqueda.Activo);
            cbotipoCliente.ValueMember = "id";
            cbotipoCliente.DisplayMember = "nombre";
        }


        private void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCantones();
        }

        private void cargarCantones()
        {
            if (cboProvincia.SelectedValue != null)
            {

                List<tbCanton> cantones = new List<tbCanton>();

                foreach (tbProvincia pro in provinciasGlo)
                {
                    if (pro.Cod == cboProvincia.SelectedValue.ToString())
                    {

                        foreach (tbCanton can in pro.tbCanton)
                        {
                            cantones.Add(can);
                        }

                        cantonesGlo = cantones;
                        cboCanton.DataSource = cantones;
                        cboCanton.ValueMember = "Canton";
                        cboCanton.DisplayMember = "Nombre";
                    }
                }
            }
        }

        private void cboCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCanton.SelectedValue != null)
            {

                List<tbDistrito> distritos = new List<tbDistrito>();

                foreach (tbCanton can in cantonesGlo)
                {
                    if (can.Canton == cboCanton.SelectedValue.ToString())
                    {
                        foreach (tbDistrito dis in can.tbDistrito)
                        {
                            distritos.Add(dis);
                        }

                        distritosGlo = distritos;
                        cboDistrito.DataSource = distritos;
                        cboDistrito.ValueMember = "Distrito";
                        cboDistrito.DisplayMember = "Nombre";

                    }
                }

            }
        }

        private void cboDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDistrito.SelectedValue != null)
            {

                List<tbBarrios> barrios = new List<tbBarrios>();

                foreach (tbDistrito dis in distritosGlo)
                {
                    if (dis.Distrito == cboDistrito.SelectedValue.ToString())
                    {
                        if (dis.tbBarrios.Count != 0)
                        {
                            foreach (tbBarrios barrio in dis.tbBarrios)
                            {
                                barrios.Add(barrio);
                            }


                            cboBarrios.DataSource = barrios;
                            cboBarrios.ValueMember = "barrio";
                            cboBarrios.DisplayMember = "nombre";
                        }


                    }
                }

            }
        }
       
        private async void buscarClienteTribunal()
        {

            if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica)
            {
                ClienteResponseDTO cliente = await ConsultasAPI.ObtenerCliente(mskidentificacion.Text);



                if( cliente == null)
                {
                    MessageBox.Show("No se encontraron datos del cliente en el Tribunal, verifique la identificación.", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var (nombre, apellido1, apellido2) = Utility.ObtenerNombreYApellidos(cliente.Nombre);
               

                if (cliente != null)
                {
                    
                    txtapellido1.Text = apellido1.Trim(); 
                    txtapellido2.Text = apellido2.Trim();
                    txtnombre.Text = nombre.Trim();
                  


                    //if (int.Parse(cliente.SEXO.Trim()) == 1)
                    //{
                    //    rbtmasc.Checked = true;
                    //}
                    //else
                    //{
                    //    rbtfem.Checked = true;
                    //}
                    //cboProvincia.SelectedValue = cliente.CODIGOPOSTAL.Substring(0, 1);
                    //int canton = int.Parse(cliente.CODIGOPOSTAL.Substring(1, 2));
                    //cboCanton.SelectedValue = canton.ToString().PadRight(2, ' ');
                    //cboCanton.Refresh();

                    //int distrito = int.Parse(cliente.CODIGOPOSTAL.Substring(3, 3));
                    //cboDistrito.SelectedValue = distrito.ToString().PadRight(2, ' '); ;



                }
            }
            else
            {
                ClienteResponseDTO respuesta = await ConsultasAPI.ObtenerCliente(txtIdentificacion.Text);
                if (respuesta != null)
                {
                    txtnombre.Text = respuesta.Nombre.ToString();
                    txtapellido1.Text = string.Empty;
                    txtapellido2.Text = string.Empty;
                }
              
            }

        }
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {

            buscarClienteTribunal();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cambiarComboExoneracion();

        }

        private void cambiarComboExoneracion()
        {
            if (chkAplicaExo.Checked)
            {

                gbxExoneracion.Enabled = true;
                cboExoneracion.SelectedIndex = 0;
            }
            else
            {
                gbxExoneracion.Enabled = false;
                cboExoneracion.SelectedIndex = 0;
                txtInstitucionExo.Text = string.Empty;
                dtpFechaEmisionExo.ResetText();

            }
        }

        private void frmClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (id != null)
            {
                if (clienteGlobal != null)
                {
                    pasarDatosEvent(clienteGlobal);
                }

            }
        }

        private void txtDocExo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                obtenerDatosExoneracion();

            }
        }

        private void obtenerDatosExoneracion()
        {
            try
            {
                if (txtDocExo.Text!= string.Empty)
                {
                    Comunicacion comunicacion = new Comunicacion();
                    comunicacion.ConsultaExoneracionXAutorizacion(txtDocExo.Text.Trim());

                    if(comunicacion.exoneracion != null)
                    {
                        txtDocExo.Text = comunicacion.exoneracion.numeroDocumento;
                        txtInstitucionExo.Text= comunicacion.exoneracion.nombreInstitucion.Trim();
                        dtpFechaEmisionExo.Text = comunicacion.exoneracion.fechaEmision;
                        dtpVencimiento.Text = comunicacion.exoneracion.fechaVencimiento;
                        cboPorcExo.Text = comunicacion.exoneracion.porcentajeExoneracion;
                        cboExoneracion.SelectedValue = int.Parse(comunicacion.exoneracion.tipoDoc.codigo);
                    }
                    else
                    {
                        txtDocExo.Text = string.Empty;
                        txtInstitucionExo.Text = string.Empty;
                        dtpFechaEmisionExo.Text = string.Empty;
                        dtpVencimiento.Text = string.Empty;
                        cboPorcExo.Text = "13";
                        cboExoneracion.SelectedValue = 0;
                        MessageBox.Show("No se encontraron datos con ese número de documento, verifique.", "Exoneración", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            


            }
            catch (Exception)
            {

                MessageBox.Show("Error al obtener los datos de Exoneración", "Exoneración", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void btnObtnerExo_Click(object sender, EventArgs e)
        {
            obtenerDatosExoneracion();
        }
    }
}

