using System;
using System.Collections;
using System.IO;

namespace ArredondoPalomino_GabrielGiovani
{
    class Operaciones
    {
        private TextWriter tw;

        private ArrayList arralumnos;

        public Operaciones()
        {
            arralumnos = new ArrayList();
            cargarListaAlumnos();
        }

        #region BaseDatos
        public Alumno buscar(int codigo)
        {
            foreach (Alumno alumnoBuscar in arralumnos)
            {
                if (alumnoBuscar.Codigo == codigo)
                {
                    return alumnoBuscar;
                }
            }
            return null;
        }

        public void agregar(Alumno alumno)
        {
            arralumnos.Add(alumno);
            escribirUnAlumnoEnArchivo(alumno);
        }

        public void modificar(int codigo, Alumno alumno)
        {
            Alumno alumnoBuscar = buscar(codigo);
            
            if (alumnoBuscar != null)
            {
                alumnoBuscar.Nombre = alumno.Nombre;
                alumnoBuscar.Apellido = alumno.Apellido;
                alumnoBuscar.Promedio = alumno.Promedio;
            }
            escribirListaAlumnosEnArchivo();
        }

        public void eliminar(Alumno alumno)
        {
            arralumnos.Remove(alumno);
            escribirListaAlumnosEnArchivo();
        }

        public ArrayList listar()
        {
            return arralumnos;
        }

        public ArrayList listarSuperiores(int notaInferiorMax)
        {
            var arrAlumnosSuperiores = new ArrayList();

            foreach (Alumno alumno in arralumnos)
            {
                if (alumno.Promedio > notaInferiorMax)
                {
                    arrAlumnosSuperiores.Add(alumno);
                }
            }

            return arrAlumnosSuperiores;
        }
        #endregion

        public Alumno mayorPromedio()
        {
            var alumnoMaximoPromedio = new Alumno()
            {
                Promedio = int.MinValue
            };

            foreach (Alumno alumno in arralumnos)
            {
                if (alumno.Promedio > alumnoMaximoPromedio.Promedio)
                {
                    alumnoMaximoPromedio = alumno;
                }
            }
            return alumnoMaximoPromedio;
        }

        public Alumno menorPromedio()
        {
            var alumnoMinimoPromedio = new Alumno()
            {
                Promedio = int.MaxValue
            };

            foreach (Alumno alumno in arralumnos)
            {
                if (alumno.Promedio < alumnoMinimoPromedio.Promedio)
                {
                    alumnoMinimoPromedio = alumno;
                }
            }
            return alumnoMinimoPromedio;
        }

        public void incrementarPuntosConMinimo(int puntosAdicionales, int notaMinima)
        {
            foreach (Alumno alumno in arralumnos)
            {
                if (alumno.Promedio > notaMinima)
                {
                    alumno.Promedio += puntosAdicionales;
                }
            }
        }

        #region ManejoArchivos
        public void cargarListaAlumnos()
        {
            TextReader tr = new StreamReader("alumnos.txt");
            try
            {
                string[] datos;
                string cadena = tr.ReadLine();

                while (!string.IsNullOrEmpty(cadena))
                {
                    try
                    {
                        datos = cadena.Split(',');

                        int codigo = int.Parse(datos[0]);
                        string nombre = datos[1];
                        string apellido = datos[2];
                        int promedio = int.Parse(datos[3]);

                        var al = new Alumno()
                        {
                            Codigo = codigo,
                            Nombre = nombre,
                            Apellido = apellido,
                            Promedio = promedio
                        };

                        arralumnos.Add(al);

                        cadena = tr.ReadLine();
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                tr.Close();
            }
        }
        
        public void escribirUnAlumnoEnArchivo(Alumno a)
        {
            try
            {
                tw = new StreamWriter("alumnos.txt", true);
                tw.Write(a.Codigo + "," + a.Nombre + "," + a.Apellido + "," + a.Promedio + "\n");
                tw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void escribirListaAlumnosEnArchivo()
        {
            try
            {
                tw = new StreamWriter("alumnos.txt", false);
                foreach (Alumno a in arralumnos)
                {
                    tw.Write(a.Codigo + "," + a.Nombre + "," + a.Apellido + "," + a.Promedio + "\n");
                }
                tw.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
