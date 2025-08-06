using CommonLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmDashboard : Form
    {
        SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
        SqlCommand cmd;
        SqlDataReader dr;

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            Grafico_top10MenosProductosVentas();
            Grafico_top10ProductosVentas();
            datos();
        }

        private void datos()
        {
            try
            {
                cmd = new SqlCommand("DASHBOARD_CANTIDADES", _SqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter diaInicio = new SqlParameter("@diaInicio", Utility.getDate().Date);
                diaInicio.Direction = ParameterDirection.Input;

                SqlParameter diaFin = new SqlParameter("@diaFin", Utility.getDate().Date);
                diaFin.Direction = ParameterDirection.Input;

                SqlParameter ventasDia = new SqlParameter("@ventasDia", 0);
                ventasDia.Direction = ParameterDirection.Output;

                SqlParameter cantPro = new SqlParameter("@cantPro", 0);
                cantPro.Direction = ParameterDirection.Output;

                SqlParameter cantCliente = new SqlParameter("@cantCliente", 0);
                cantCliente.Direction = ParameterDirection.Output;

                SqlParameter InvMenorO = new SqlParameter("@InvMenorO", 0);
                InvMenorO.Direction = ParameterDirection.Output;

                SqlParameter CantProductosVenDia = new SqlParameter("@CantProductosVenDia", 0);
                CantProductosVenDia.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(diaInicio);
                cmd.Parameters.Add(diaFin);
                cmd.Parameters.Add(ventasDia);
                cmd.Parameters.Add(cantPro);
                cmd.Parameters.Add(cantCliente);
                cmd.Parameters.Add(InvMenorO);
                cmd.Parameters.Add(CantProductosVenDia);
                _SqlConnection.Open();
                cmd.ExecuteNonQuery();


                lblventasDia.Text = cmd.Parameters["@ventasDia"].Value.ToString()==string.Empty?"0": cmd.Parameters["@ventasDia"].Value.ToString();
                lblCantPro.Text = cmd.Parameters["@cantPro"].Value.ToString() == string.Empty ? "0" : cmd.Parameters["@cantPro"].Value.ToString(); ;
                lblCantCliente.Text = cmd.Parameters["@cantCliente"].Value.ToString() == string.Empty ? "0" : cmd.Parameters["@cantCliente"].Value.ToString(); ;
                lblInvMenorO.Text = cmd.Parameters["@InvMenorO"].Value.ToString() == string.Empty ? "0" : cmd.Parameters["@InvMenorO"].Value.ToString(); ;
                lblCantProductosVenDia.Text = cmd.Parameters["@CantProductosVenDia"].Value.ToString() == string.Empty ? "0" : cmd.Parameters["@CantProductosVenDia"].Value.ToString(); ;

            }
            catch (Exception)
            {

                throw;
            }
        }

        ArrayList producto = new ArrayList();
        ArrayList Cantidad = new ArrayList();
        private void Grafico_top10ProductosVentas()
        {
            try
            {
                cmd = new SqlCommand("DASHBOARD_10PRODUCTOVENDIDOS", _SqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                _SqlConnection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    producto.Add(dr.GetString(1));
                    Cantidad.Add(dr.GetInt32(2));
                }
                chartTopProductoVend.Series[0].Points.DataBindXY(producto, Cantidad);
                dr.Close();
                _SqlConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el gráfico", "Cargar gráfico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dr.Close();
                _SqlConnection.Close();
            }
        }

        private void Grafico_top10MenosProductosVentas()
        {
            try
            {
                cmd = new SqlCommand("DASHBOARD_10PRODUCTOMENOSVENDIDOS", _SqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                _SqlConnection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    producto.Add(dr.GetString(1));
                    Cantidad.Add(dr.GetInt32(2));
                }
                chartMenosVendidos.Series[0].Points.DataBindXY(producto, Cantidad);
                dr.Close();
                _SqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el gráfico", "Cargar gráfico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dr.Close();
                _SqlConnection.Close();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           Utility.ObtenerCorreosConAdjuntosXml("walpizar@gmail.com", "Casa8383**");
        }
    }
}
