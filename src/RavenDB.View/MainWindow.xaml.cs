using RavenDB.Model;
using RavenDB.Repository;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace RavenDB.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int QUANTIDADE_POR_PAGINA = 10;

        public string IdDoClienteSalvo { get; set; }
        public RepositorioDeCliente Repositorio { get; set; }
        public int PaginaAtual { get; set; } = 1;
        public int QuantidadeTotalDePaginas { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Repositorio = new RepositorioDeCliente();
            CarregueElementosDoBancoDeDados();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            var cliente = ChameOEditorDeCliente(new Cliente());
            Repositorio.Cadastrar(cliente);
            CarregueElementosDoBancoDeDados();
        }

        private void CarregueElementosDoBancoDeDados()
        {
            //QueryStatistics estatisticas;
            //lstClientes.DataContext = Repositorio.Liste(PaginaAtual, QUANTIDADE_POR_PAGINA, out estatisticas);
            //QuantidadeTotalDePaginas = (int)Math.Ceiling((decimal)estatisticas.TotalResults / (decimal)QUANTIDADE_POR_PAGINA);
            //txtPaginaAtual.Text = $"Página {PaginaAtual} de {QuantidadeTotalDePaginas}; Total de elmentos: {estatisticas.TotalResults}; Duração da consulta: {estatisticas.DurationInMs} ms";
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista!");
                return;
            }

            var cliente = (Cliente)lstClientes.SelectedItem;

            cliente = Repositorio.Consulte(cliente.Id);

            ChameOEditorDeCliente(cliente);

            Repositorio.Atualizar(cliente);
            CarregueElementosDoBancoDeDados();
        }

        private Cliente ChameOEditorDeCliente(Cliente cliente)
        {
            var formCliente = new FormCliente(cliente);
            formCliente.ShowDialog();
            return formCliente.Cliente;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (lstClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um item na lista!");
                return;
            }

            var cliente = ((Cliente)(lstClientes.SelectedItem));
            Repositorio.Remover(cliente.Id);
            CarregueElementosDoBancoDeDados();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregueElementosDoBancoDeDados();
        }

        private void txtConsulta_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConsulta.Text))
            {
                CarregueElementosDoBancoDeDados();
                return;
            }

            lstClientes.DataContext = Repositorio.ConsultePorTermo(txtConsulta.Text);
        }

        private void txtIdade_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdade.Text))
            {
                CarregueElementosDoBancoDeDados();
                return;
            }

            int idade;
            if (int.TryParse(txtIdade.Text, out idade))
            {
                lstClientes.DataContext = Repositorio.ConsultaPorIdade(idade);
            }
            else
            {
                lstClientes.DataContext = new List<Cliente>();
            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (PaginaAtual > 1)
            {
                PaginaAtual--;
            }

            CarregueElementosDoBancoDeDados();
        }

        private void btnProximo_Click(object sender, RoutedEventArgs e)
        {
            if (PaginaAtual < QuantidadeTotalDePaginas)
            {
                PaginaAtual++;
            }

            CarregueElementosDoBancoDeDados();
        }
    }
}

