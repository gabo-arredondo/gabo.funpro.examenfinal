using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace ArredondoPalomino_GabrielGiovani
{
    public partial class Form1 : Form
    {
        #region Constantes
        private const int PUNTOS_ADICIONALES = 2;

        private const int NOTA_MINIMA = 14;
        #endregion

        private Operaciones oper;

        public Form1()
        {
            InitializeComponent();
            oper = new Operaciones();
        }

        #region Getters
        int GetCodigo()
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

        string GetNombre()
        {
            return txtNombre.Text;
        }

        string GetApellido()
        {
            return txtApellido.Text;
        }

        int GetPromedio()
        {
            return int.Parse(txtPromedio.Text);
        }

        Alumno GetAlumno()
        {
            Alumno alumno = new Alumno()
            {
                Codigo = GetCodigo(),
                Nombre = GetNombre(),
                Apellido = GetApellido(),
                Promedio = GetPromedio()
            };

            return alumno;
        }
        #endregion

        #region Metodos
        void escribir()
        {
            var alumno = GetAlumno();
            oper.agregar(alumno);
            limpiarFormulario();
        }

        void cargarGrilla()
        {
            dgvAlumnos.DataSource = null;
            dgvAlumnos.DataSource = oper.listar();
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
            Alumno x = oper.buscar(GetCodigo());


            if (x != null)
            {
                mostrar(x);
            }
        }

        void mostrar(Alumno alumno)
        {
            txtCodigo.Text = alumno.Codigo.ToString();
            txtNombre.Text = alumno.Nombre;
            txtApellido.Text = alumno.Apellido;
            txtPromedio.Text = alumno.Promedio.ToString();
        }

        void eliminar()
        {
            if (txtCodigo.Text.Length != 0)
            {
                Alumno alumnoEliminar = oper.buscar(GetCodigo());
                oper.eliminar(alumnoEliminar);
                cargarGrilla();
                limpiarFormulario();
            }
            else
            {
                MessageBox.Show("Ingrese codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void modificar()
        {
            var codigo = GetCodigo();
            
            var alumno = new Alumno()
            {
                Nombre = GetNombre(),
                Apellido = GetApellido(),
                Promedio = GetPromedio()
            };

            oper.modificar(codigo, alumno);

            cargarGrilla();
            limpiarFormulario();
        }

        void aumentarDosPuntos()
        {
            oper.incrementarPuntosConMinimo(PUNTOS_ADICIONALES, NOTA_MINIMA);
        }
        #endregion

        #region Eventos
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
            cargarGrilla();
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
            mostrar(alumnoMaximoPromedio);
        }

        private void btnMenor_Click(object sender, EventArgs e)
        {
            var alumnoMinimoPromedio = oper.menorPromedio();
            mostrar(alumnoMinimoPromedio);
        }

        private void btnAumentar_Click(object sender, EventArgs e)
        {
            aumentarDosPuntos();
            cargarGrilla();
        }
        #endregion
    }
}
