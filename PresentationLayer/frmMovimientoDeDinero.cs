using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Windows.Forms;





namespace PresentationLayer
{
    public partial class frmMovimientoDeDinero : Form
    {
        //BMovimientoDeDinero moviDineInst = new BMovimientoDeDinero();
        BMovimiento moviDineInst = new BMovimiento();

        public int tipoMov { set; get; }

        public static tbMovimientos MoviDineGlobal = new tbMovimientos();

        public frmMovimientoDeDinero()
        {
            InitializeComponent();
        }

        private void frmMovimientoDeDinero_Load(object sender, EventArgs e)
        {
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
            Utility.EnableDisableForm(ref gbxMovimientoDeDinero, false);
            tlsBtnBuscar.Enabled = false;
            lblMov.Text = Enum.GetName(typeof(Enums.tipoMovimiento), tipoMov).ToString();
        }
        public void accionMenu(string accion)
        {
            switch (accion)
            {
                case "Guardar":

                    if (guardar())
                    {
                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                        Utility.EnableDisableForm(ref gbxMovimientoDeDinero, false);
                        tlsBtnBuscar.Enabled = false;
                        Utility.ResetForm(ref gbxMovimientoDeDinero);
                    }
                    break;

                case "Nuevo":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxMovimientoDeDinero, true);

                    dtpFecha.Enabled = false;
                    tlsBtnBuscar.Enabled = false;
                    Utility.ResetForm(ref gbxMovimientoDeDinero);
                    break;

                case "Cancelar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                    Utility.EnableDisableForm(ref gbxMovimientoDeDinero, false);
                    Utility.ResetForm(ref gbxMovimientoDeDinero);
                    tlsBtnBuscar.Enabled = false;
                    break;

                case "Salir":
                    this.Close();
                    break;
            }
        }


        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accionMenu(e.ClickedItem.Text);
        }

        private bool guardar()
        {

            tbMovimientos movimientoDinero = new tbMovimientos();
            if (validar())
            {
                try
                {
                    movimientoDinero.fecha = Utility.getDate();
                    movimientoDinero.motivo = txtMotivoMov.Text.ToUpper();
                    movimientoDinero.idTipoMov = tipoMov;
                    movimientoDinero.total = Convert.ToDecimal(txtTotal.Text);
                    movimientoDinero.caja = Global.Configuracion.caja;
                    movimientoDinero.sucursal = Global.Configuracion.sucursal;
                    //auditoria
                    movimientoDinero.estado = true;
                    movimientoDinero.fecha_crea = Utility.getDate();
                    movimientoDinero.fecha_ult_mod = Utility.getDate();
                    movimientoDinero.usuario_crea = Global.Usuario.nombreUsuario.ToUpper().Trim();
                    movimientoDinero.usuario_ult_mod = Global.Usuario.nombreUsuario.ToUpper().Trim();

                    tbMovimientos tipo = moviDineInst.Guardar(movimientoDinero);

                    if(Global.Configuracion.imprime== (int)Enums.Estado.Activo &&
                        Global.Configuracion.imprimeDoc == 1)
                    {
                        DialogResult result = MessageBox.Show("Desea imprimir comprobante del movimiento", "Comprobante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            
                            clsImpresionFactura imprimir = new clsImpresionFactura(movimientoDinero);
                            imprimir.printMovimientos();
                        }

                    }

                    MessageBox.Show("El movimiento de Dinero se guardado correctamente");

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool validar()
        {
            //empty indica quev el campo esta vacio 
            if (txtTotal.Text == string.Empty)
            {
                MessageBox.Show("Indique el total de el movimiento a realizar");
                txtTotal.Focus();
                return false;
            }
            if (tipoMov == (int)Enums.tipoMovimiento.SalidaDinero)
            {
                if (txtMotivoMov.Text == string.Empty)
                {
                    MessageBox.Show("Indique un motivo del movimento a realizar");
                    //focus es mara que al validar y haga falta el campo el me coloque el puntero donde falto el dato 
                    txtMotivoMov.Focus();
                    return false;
                }

            }
            return true;
        }

        private void gbxMovimientoDeDinero_Enter(object sender, EventArgs e)
        {

        }



        private void txtTotal_Validated(object sender, EventArgs e)
        {
            if (Utility.isNumeroDecimal(txtTotal.Text))
            {
                txtTotal.Text = string.Format("{0:N2}", decimal.Parse(txtTotal.Text));
            }
        }
    }
}
