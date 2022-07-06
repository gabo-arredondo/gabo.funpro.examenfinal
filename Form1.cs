using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArredondoPalomino_GabrielGiovani
{
    public partial class Form1 : Form
    {
        Operaciones oper = new Operaciones();

        public Form1()
        {
            InitializeComponent();
        }
        int Codigo()
        {
            try
            {
                return int.Parse(txtCodigo.Text);

            }
            catch
            {
                MessageBox.Show("Ingrese codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
        }

        string Nombre()
        {
            return txtNombre.Text;
        }

        string Apellido()
        {
            return txtApellido.Text;
        }

        int Promedio()
        {
            return int.Parse(txtPromedio.Text);
        }

        void escribir()
        {
            Alumno a = new Alumno()
            {
                Codigo = Codigo(),
                Nombre = Nombre(),
                Apellido = Apellido(),
                Promedio = Promedio()
            };
            oper.escribirArchivo(a);
            limpiarFormulario();
        }

        void leer()
        {
            dgvAlumnos.DataSource = null;
            dgvAlumnos.DataSource = oper.leerArchivo();
        }

        void limpiarFormulario()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtPromedio.Text = "";
        }

        void buscar()
        {
            Alumno x = oper.buscar(Codigo());


            if (x != null)
            {
                txtNombre.Text = x.Nombre;
                txtApellido.Text = x.Apellido;
                txtPromedio.Text = x.Promedio.ToString();

            }
        }


        void eliminar()
        {
            if (txtCodigo.Text.Length != 0)
            {
                Alumno x = oper.buscar(Codigo());
                oper.eliminar(x);
                leer();
                limpiarFormulario();
            }
            else
            {
                MessageBox.Show("Ingrese codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        void modificar()
        {
            Alumno x = oper.buscar(Codigo());
            if (x != null)
            {
                x.Nombre = Nombre();
                x.Apellido = Apellido();
                x.Promedio = Promedio();
                ArrayList arrclientes = oper.obtenerarrcliente();
                TextWriter tw = new StreamWriter("alumnos.txt", false);
                tw.Write(string.Empty);
                tw.Close();
                oper.escribirArchivo(arrclientes);
                leer();
                limpiarFormulario();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            escribir();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            leer();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void btnMayor_Click(object sender, EventArgs e)
        {
            var alumnoMaximoPromedio = oper.mayorPromedio();
        }

        private void btnMenor_Click(object sender, EventArgs e)
        {
            var alumnoMinimoPromedio = oper.menorPromedio();
        }
    }
}
