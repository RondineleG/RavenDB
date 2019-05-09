using RavenDB.Model;
using RavenDB.Repository;
using System.Windows;

namespace RavenDB.View
{
    public partial class FormCliente : Window
    {
        public Cliente Cliente { get; set; }

        RepositorioDeCliente repositorio;

        public FormCliente()
        {
            InitializeComponent();
            Cliente = new Cliente();
            this.DataContext = Cliente;
            repositorio = new RepositorioDeCliente();
        }

        public FormCliente(Cliente cliente)
        {
            InitializeComponent();
            this.DataContext = cliente;
            Cliente = cliente;
            repositorio = new RepositorioDeCliente();
            if (cliente.Indicador != null)
            {
                cmbIndicador.SelectedValue = cliente.Indicador.Id;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbIndicador.ItemsSource = repositorio.Liste();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Cliente = (Cliente)this.DataContext;
            Cliente.Indicador = (Cliente)cmbIndicador.SelectedItem;
            if (Cliente.Indicador != null)
            {
                Cliente.IndicadorId = ((Cliente)cmbIndicador.SelectedItem).Id;
            }
            this.Close();
        }
    }
}
