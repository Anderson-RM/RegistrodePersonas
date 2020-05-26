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

namespace RegistrodePersonas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
    } 
    public class persona
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; } 
        public string telefono { get; set; } 
        public string cedula { get; set; }
        public string direccion { get; set; }       
        public string nacimiento { get; set; }

    }

    public class contecto : DbContext
    {
        public DbSet<persona> personas { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Movies.db");
        }
    }
}
