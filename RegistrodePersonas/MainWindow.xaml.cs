using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistrodePersonas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Personas persona = new Personas();      
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = persona;
        }

        private void Limpiar()
        {
            persona = new Personas();
            this.DataContext = persona;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = PersonasBLL.Buscar(int.Parse(personaId.Text));

            if (encontrado != null)
            {
                this.persona = encontrado;

                this.DataContext = encontrado;
            }

            else
                this.persona = new Personas();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private bool Validar()
        {
            bool esValido = true;

            if (nombre.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (direccion.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = PersonasBLL.Guardar(persona);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccione exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonasBLL.Eliminar(Convert.ToInt32(personaId.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string nombre { get; set; } 
        public string telefono { get; set; } 
        public string cedula { get; set; }
        public string direccion { get; set; }       
        public string nacimiento { get; set; }

    }

    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/Personas.db");
        }
    }

    public class PersonasBLL
    {
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                //buscar la entidad que se desea eliminar
                var persona = contexto.Personas.Find(id);
                if (persona != null)
                {
                    contexto.Personas.Remove(persona);//remover la entidad mas las que enel agrego
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Personas Buscar(int Id)
        {
            Contexto contexto = new Contexto();
            Personas Persona;

            try
            {
                Persona = contexto.Personas.Find(Id);
            }
            catch (Exception)
            {

                throw;
            }
            contexto.Dispose();
            return Persona;


        }
        public static bool Guardar(Personas persona)
        {
            if (!Existe(persona.PersonaId))//si no existe insertamos
                return Insertar(persona);
            else
                return Modificar(persona);
        }
        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(persona).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Insertar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {                 
                contexto.Personas.Add(persona);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }


        public static List<Personas> GetList(Expression<Func<Personas, bool>> expresion)
        {
            Contexto contexto = new Contexto();
            List<Personas> lista = new List<Personas>();
            try
            {
                lista = contexto.Personas.Where(expresion).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            contexto.Dispose();
            return lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Personas
                .Any(e => e.PersonaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado; //si
        }
    }
}
