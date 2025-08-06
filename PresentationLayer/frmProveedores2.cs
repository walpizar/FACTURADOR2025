using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BusisnessExceptions;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmProveedores2 : Form
    {
        Bcliente clienteInst = new Bcliente();
        int bandera = 1;
        tbProveedores proveedorGlobal = new tbProveedores();
        BProveedores proveedorIns = new BProveedores();
        BProvincias provinciasIns = new BProvincias();
        BTipoId tipoIdIns = new BTipoId();
        List<tbProvincia> provinciasGlo = null;
        List<tbCanton> cantonesGlo = null;
        List<tbDistrito> distritosGlo = null;

        public frmProveedores2()
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


            if (msktelefono.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un numero de telefono");
                msktelefono.Focus();
                return false;
            }
            if (txtCorreo.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un correo eletrónico");
                txtCorreo.Focus();
                return false;
            }

            if (!Utility.isValidEmail(txtCorreo.Text))
            {

                MessageBox.Show("El correo electrónico indicado no tiene un formato válido.");
                txtCorreo.Focus();
                return false;
            }

            if (txtContactoProv.Text == string.Empty)
            {

                MessageBox.Show("Debe Ingresar un contacto del Proveedor");
                txtContactoProv.Focus();
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

            return true;
        }



        private bool guardarProveedor()
        {//SE CREAN LAS INSTANCIAS
            tbProveedores proveedor = new tbProveedores();
            tbPersona persona = new tbPersona();
            bool processOk = false;
            try
            {
                if (validarCampos() == true)
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


                    persona.telefono = int.Parse(msktelefono.Text);

                    persona.provincia = cboProvincia.SelectedValue.ToString();
                    persona.canton = cboCanton.SelectedValue.ToString();
                    persona.distrito = cboDistrito.SelectedValue.ToString();
                    persona.codigoPaisTel = "506";
                    persona.otrasSenas = txtOtrasSenas.Text;
                    persona.barrio = cboBarrios.SelectedValue.ToString();
                    persona.correoElectronico = txtCorreo.Text;
                    // AQUI ES DONDE QUE ESA PERSONA TAMBIEN ES CLIENTE...
                    proveedor.tbPersona = persona;
                    proveedor.id = persona.identificacion;
                    proveedor.tipoId = persona.tipoId;

                    proveedor.fax = mskFax.Text;
                    proveedor.descripcion = txtObserv.Text;
                    proveedor.cuentaBancaria = txtCuentaBanco.Text;
                    proveedor.contactoProveedor = txtContactoProv.Text.ToUpper().Trim();

                    proveedor.encargadoConta = txtEncargadoTrib.Text.Trim();
                    proveedor.correoElectConta = txtCorreoContabilidad.Text;
                    proveedor.nombreTributario = txtNombreTribut.Text;



                    proveedor.estado = true;
                    proveedor.fecha_crea = Utility.getDate();
                    proveedor.fecha_ult_mod = Utility.getDate();
                    proveedor.usuario_crea = Global.Usuario.nombreUsuario;
                    proveedor.usuario_ult_mod = Global.Usuario.nombreUsuario;
                    // CON NUESTRA INSTACIA LLAMAMOS AL METODO GUARDAR.... Y LE MANDAMOS A CLIENTE...

                    proveedor = proveedorIns.guardar(proveedor);
                    processOk = true;
                    MessageBox.Show("Los datos han sido almacenada en la base de datos.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (EntityDisableStateException ex)
            {


                DialogResult result = MessageBox.Show("Proveedor ya existe en la base datos. ¿Desea actualizarlos?", "Datos Existentes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {


                    proveedorGlobal = proveedor;
                    processOk = actualizarProveedor();
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

                        proveedorGlobal.estado = false;
                        proveedorGlobal.fecha_ult_mod = Utility.getDate();
                        proveedorGlobal.usuario_ult_mod = Global.Usuario.nombreUsuario;
                        proveedorGlobal = proveedorIns.Modificar(proveedorGlobal);
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
                        Utility.EnableDisableForm(ref gbxProveedor, false);
                        Utility.ResetForm(ref gbxProveedor);
                        mskidentificacion.Enabled = false;
                        txtIdentificacion.Enabled = false;
                        txtapellido1.Enabled = false;
                        txtapellido2.Enabled = false;

                    }
                    break;
                case "Nuevo":
                    bandera = 1;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxProveedor, true);
                    Utility.ResetForm(ref gbxProveedor);

                    cbotipoId.SelectedIndex = 0;

                    rbtmasc.Checked = true;
                    rbtfem.Checked = false;
                    break;

                case "Modificar":
                    bandera = 2;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxProveedor, true);
                    mskidentificacion.Enabled = false; mskidentificacion.Enabled = false;
                    cbotipoId.Enabled = false;
                    cambiarTiposId();
                    mskidentificacion.Enabled = false;
                    txtIdentificacion.Enabled = false;
                    break;
                case "Eliminar":
                    bandera = 3;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    cbotipoId.Enabled = false;
                    mskidentificacion.Enabled = false;
                    txtIdentificacion.Enabled = false;
                    break;
                case "Buscar":

                    if (buscar())
                    {

                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                        Utility.EnableDisableForm(ref gbxProveedor, false);

                    }

                    break;
                case "Cancelar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                    Utility.EnableDisableForm(ref gbxProveedor, false);
                    Utility.ResetForm(ref gbxProveedor);
                    mskidentificacion.Enabled = false;
                    txtIdentificacion.Enabled = false;
                    txtapellido1.Enabled = false;
                    txtapellido2.Enabled = false;
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
                    processOk = guardarProveedor();
                    break;

                case 2:
                    processOk = actualizarProveedor();
                    break;

                case 3:
                    processOk = Eliminar();
                    break;
            }

            return processOk;
        }
        private void dataBuscar(tbProveedores prov)
        {// metodo al cual le apunta el puntero... o delegado...
            proveedorGlobal = prov;
            try
            {//BUSCAR JALA LOS DATOS DE LAS DIFERENTEN TABLAS Y LAS METE EN LA VARIABLE CLIENTE GLOBAL...
                //LO HACE DESDE EL FORMULARIO DE BUSCAR... 
                if (proveedorGlobal != null)
                {
                    if (proveedorGlobal.tipoId != 0)
                    {
                        {
                            // cargarCombos();
                            cbotipoId.SelectedValue = proveedorGlobal.tipoId;

                            if (prov.tipoId != (int)Enums.TipoId.Fisica)
                            {

                                txtIdentificacion.Text = proveedorGlobal.tbPersona.identificacion.ToString().Trim();

                                if (prov.tipoId == (int)Enums.TipoId.Dimex)
                                {
                                    txtIdentificacion.Text = proveedorGlobal.tbPersona.identificacion.ToString().Trim();
                                    txtapellido1.Text = proveedorGlobal.tbPersona.apellido1.ToString().Trim();
                                    txtapellido2.Text = proveedorGlobal.tbPersona.apellido2.ToString().Trim();

                                }
                            }
                            else
                            {
                                mskidentificacion.Text = proveedorGlobal.tbPersona.identificacion.Trim();
                                txtapellido1.Text = proveedorGlobal.tbPersona.apellido1.ToString().Trim();
                                txtapellido2.Text = proveedorGlobal.tbPersona.apellido2.ToString().Trim();

                            }

                            txtnombre.Text = proveedorGlobal.tbPersona.nombre.ToString().Trim();


                            if (proveedorGlobal.tbPersona.sexo == 1)
                            {
                                rbtmasc.Checked = true;
                            }
                            else if (proveedorGlobal.tbPersona.sexo == 2)
                            {
                                rbtfem.Checked = true;
                            }
                            if (proveedorGlobal.tbPersona.correoElectronico != null && proveedorGlobal.tbPersona.correoElectronico != string.Empty)
                            {
                                txtCorreo.Text = proveedorGlobal.tbPersona.correoElectronico.Trim();

                            }
                            dtpfechaNa.Text = proveedorGlobal.tbPersona.fechaNac.ToString().Trim();
                            txtContactoProv.Text = proveedorGlobal.contactoProveedor.Trim();
                            msktelefono.Text = proveedorGlobal.tbPersona.telefono.ToString().Trim();
                            if (proveedorGlobal.tbPersona.otrasSenas != null)
                            {
                                txtOtrasSenas.Text = proveedorGlobal.tbPersona.otrasSenas.ToString().Trim();

                            }

                            cboProvincia.SelectedValue = proveedorGlobal.tbPersona.provincia;
                            cboCanton.SelectedValue = proveedorGlobal.tbPersona.canton;
                            cboCanton.Refresh();
                            cboDistrito.SelectedValue = proveedorGlobal.tbPersona.distrito;
                            cboBarrios.SelectedValue = proveedorGlobal.tbPersona.barrio;

                            txtContactoProv.Text = proveedorGlobal.contactoProveedor;
                            mskFax.Text = proveedorGlobal.fax;
                            txtCuentaBanco.Text = proveedorGlobal.cuentaBancaria;
                            txtObserv.Text = proveedorGlobal.descripcion;
                            txtEncargadoTrib.Text = proveedorGlobal.encargadoConta;
                            txtCorreoContabilidad.Text = proveedorGlobal.correoElectConta;
                            txtNombreTribut.Text = proveedorGlobal.nombreTributario;
                        }
                    }
                }
            }
            catch (ListEntityException ex)
            {

                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private bool buscar()
        {//FORMA NUEVA CON EL DELEGADO...

            frmBuscarProveedores buscar = new frmBuscarProveedores();
            buscar.pasarDatosEvent += dataBuscar;
            buscar.ShowDialog();
            if (proveedorGlobal == null || proveedorGlobal.id == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private bool actualizarProveedor()
        {
            bool processOk = false;
            try
            {
                if (validarCampos())
                {
                    //NO SE PUEDE MODIFICAR DE NINGUNA FORMA ID, TIPOiD POR SER LLAVES PRIMARIAS...
                    //proveedorGlobal.tbPersona.identificacion = cbotipoId.ToString().Trim();...EJEMPLO DE LO QUE  NO SE HACE..
                    if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica ||
                    (int)cbotipoId.SelectedValue == (int)Enums.TipoId.Dimex)
                    {


                        proveedorGlobal.tbPersona.apellido1 = txtapellido1.Text.ToUpper().Trim();
                        proveedorGlobal.tbPersona.apellido2 = txtapellido2.Text.ToUpper().Trim();
                        proveedorGlobal.tbPersona.fechaNac = DateTime.Parse(dtpfechaNa.Text);
                        if (rbtmasc.Checked)
                        {
                            proveedorGlobal.tbPersona.sexo = 1;
                        }
                        else
                        {
                            proveedorGlobal.tbPersona.sexo = 2;
                        }
                    }


                    proveedorGlobal.tbPersona.nombre = txtnombre.Text.ToUpper().Trim();


                    proveedorGlobal.tbPersona.correoElectronico = txtContactoProv.Text.Trim();
                    proveedorGlobal.tbPersona.telefono = int.Parse(msktelefono.Text);
                    proveedorGlobal.tbPersona.provincia = cboProvincia.SelectedValue.ToString();
                    proveedorGlobal.tbPersona.canton = cboCanton.SelectedValue.ToString();
                    proveedorGlobal.tbPersona.distrito = cboDistrito.SelectedValue.ToString();
                    proveedorGlobal.tbPersona.codigoPaisTel = "506";
                    proveedorGlobal.tbPersona.otrasSenas = txtOtrasSenas.Text.Trim();
                    proveedorGlobal.tbPersona.barrio = cboBarrios.SelectedValue.ToString();
                    proveedorGlobal.tbPersona.otrasSenas = txtOtrasSenas.Text.ToUpper().Trim();
                    proveedorGlobal.tbPersona.correoElectronico = txtCorreo.Text.Trim();

                    proveedorGlobal.fax = mskFax.Text;
                    proveedorGlobal.cuentaBancaria = txtCuentaBanco.Text.Trim().ToUpper();
                    proveedorGlobal.contactoProveedor = txtContactoProv.Text.ToUpper().Trim();
                    proveedorGlobal.correoElectConta = txtCorreoContabilidad.Text.Trim();
                    proveedorGlobal.descripcion = txtObserv.Text.Trim();
                    proveedorGlobal.encargadoConta = txtEncargadoTrib.Text.Trim();
                    proveedorGlobal.nombreTributario = txtNombreTribut.Text;

                    proveedorGlobal.usuario_ult_mod = Global.Usuario.nombreUsuario;
                    proveedorGlobal.fecha_ult_mod = Utility.getDate();
                    proveedorGlobal = proveedorIns.Modificar(proveedorGlobal);
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

        private void cargarCombos()
        {


            provinciasGlo = provinciasIns.getListTipoing((int)Enums.EstadoBusqueda.Activo);

            cboProvincia.ValueMember = "Cod";
            cboProvincia.DisplayMember = "Nombre";
            cboProvincia.DataSource = provinciasGlo;

            cbotipoId.ValueMember = "id";
            cbotipoId.DisplayMember = "nombre";
            cbotipoId.DataSource = tipoIdIns.getListaTipoId();

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

        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accionMenu(e.ClickedItem.Text);
        }

        private void frmProveedores2_Load(object sender, EventArgs e)
        {
            cargarCombos();
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
            Utility.EnableDisableForm(ref gbxProveedor, false);
            // Y SE COMIENZAN A  LLAMAR LOS METODOS....
        }

        private void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCantones();
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

        //private void BtnBuscarCliente_Click(object sender, EventArgs e)
        //{
        //    if ((int)cbotipoId.SelectedValue == (int)Enums.TipoId.Fisica)
        //    {
        //        tbPersonasTribunalS cliente = clienteInst.GetClienteByIdTribunal(mskidentificacion.Text);
        //        if (cliente != null)
        //        {
        //            txtapellido1.Text = cliente.APELLIDO1.Trim();
        //            txtapellido2.Text = cliente.APELLIDO2.Trim();
        //            txtnombre.Text = cliente.NOMBRE.Trim();
        //            if (int.Parse(cliente.SEXO.Trim()) == 1)
        //            {
        //                rbtmasc.Checked = true;
        //            }
        //            else
        //            {
        //                rbtfem.Checked = true;
        //            }
        //            cboProvincia.SelectedValue = cliente.CODIGOPOSTAL.Substring(0, 1);
        //            int canton = int.Parse(cliente.CODIGOPOSTAL.Substring(1, 2));
        //            cboCanton.SelectedValue = canton.ToString().PadRight(2, ' ');
        //            cboCanton.Refresh();

        //            int distrito = int.Parse(cliente.CODIGOPOSTAL.Substring(3, 3));
        //            cboDistrito.SelectedValue = distrito.ToString().PadRight(2, ' '); ;



        //        }
        //    }
        //}

        private void cbotipoId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cambiarTiposId();
        }

        private void txtIdentificacion_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
