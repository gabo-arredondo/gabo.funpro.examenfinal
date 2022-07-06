using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArredondoPalomino_GabrielGiovani
{
    class Operaciones
    {
        private TextWriter tw;

        private ArrayList arralumnos;

        public Operaciones()
        {
            arralumnos = new ArrayList();
        }

        public void escribirArchivo(Alumno a)
        {
            try
            {
                tw = new StreamWriter("alumnos.txt", true);
                tw.Write(a.Codigo + "," + a.Nombre + "," + a.Apellido +"," + a.Promedio + "\n");
                tw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void escribirArchivo(ArrayList alumnos)
        {
            try
            {
                tw = new StreamWriter("alumnos.txt", true);
                foreach (Alumno a in alumnos)
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

        public ArrayList leerArchivo(string grupo = "todos")
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

                        var al = new Alumno()
                        {
                            Codigo = int.Parse(datos[0]),
                            Nombre = datos[1],
                            Apellido = datos[2],
                            Promedio = int.Parse(datos[3])
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

            return arralumnos;
        }

        public Alumno buscar(int codigo)
        {
            foreach (Alumno x in leerArchivo())
            {
                if (x.Codigo == codigo)
                {
                    return x;
                }
            }
            return null;
        }

        public void eliminar(Alumno a)
        {
            arralumnos.Remove(a);
            TextWriter tw = new StreamWriter("alumnos.txt", false);
            tw.Write(string.Empty);
            tw.Close();
            escribirArchivo(arralumnos);
        }
        
        public ArrayList obtenerarrcliente()
        {
            return arralumnos;
        }

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
    }
}
