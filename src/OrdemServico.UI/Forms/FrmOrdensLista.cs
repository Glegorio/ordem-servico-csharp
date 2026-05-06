using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Logging;
using OrdemServico.Repositories.Common;
using OrdemServico.Services.Implementations;
using OS = OrdemServico.Entities.OrdemServico;

namespace OrdemServico.UI.Forms
{
    public partial class FrmOrdensLista : Form
    {
        private readonly OrdemServicoService _osService = new OrdemServicoService();
        private readonly ClienteService _clienteService = new ClienteService();

        private const int TamanhoPagina = 20;
        private int _paginaAtual = 1;
        private int _totalPaginas = 1;

        public FrmOrdensLista()
        {
            InitializeComponent();
        }

        private void FrmOrdensLista_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CarregarFiltroClientes();
            CarregarFiltroStatus();
            Buscar();
        }

        private void ConfigurarGrid()
        {
            dgvOrdens.AutoGenerateColumns = false;
            dgvOrdens.Columns.Clear();

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "OS",
                DataPropertyName = "Id",
                Width = 70,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataAbertura",
                HeaderText = "Abertura",
                DataPropertyName = "DataAbertura",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClienteNome",
                HeaderText = "Cliente",
                DataPropertyName = "ClienteNome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 50
            });
            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorTotal",
                HeaderText = "Valor Total (R$)",
                DataPropertyName = "ValorTotal",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Versao",
                HeaderText = "Versão",
                DataPropertyName = "Versao",
                Width = 70,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
        }

        private void CarregarFiltroClientes()
        {
            cmbFiltroCliente.Items.Clear();
            cmbFiltroCliente.Items.Add(new ComboItemCliente(0, "(Todos os clientes)"));

            var resultado = _clienteService.Buscar(new ClienteFiltro { Ativo = true }, 1, 1000);
            foreach (var c in resultado.Items)
                cmbFiltroCliente.Items.Add(new ComboItemCliente(c.Id, c.Nome));

            cmbFiltroCliente.SelectedIndex = 0;
        }

        private void CarregarFiltroStatus()
        {
            cmbFiltroStatus.Items.Clear();
            cmbFiltroStatus.Items.Add(new ComboItemStatus(null, "(Todos)"));
            cmbFiltroStatus.Items.Add(new ComboItemStatus(StatusOS.Aberta, "Aberta"));
            cmbFiltroStatus.Items.Add(new ComboItemStatus(StatusOS.EmAndamento, "Em andamento"));
            cmbFiltroStatus.Items.Add(new ComboItemStatus(StatusOS.Concluida, "Concluída"));
            cmbFiltroStatus.Items.Add(new ComboItemStatus(StatusOS.Cancelada, "Cancelada"));
            cmbFiltroStatus.SelectedIndex = 0;
        }

        private void Buscar()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var filtro = new OrdemServicoFiltro
                {
                    ClienteId = ((ComboItemCliente)cmbFiltroCliente.SelectedItem).Id == 0
                        ? (long?)null
                        : ((ComboItemCliente)cmbFiltroCliente.SelectedItem).Id,
                    Status = ((ComboItemStatus)cmbFiltroStatus.SelectedItem).Status,
                    DataInicio = dtFiltroInicio.Checked ? (DateTime?)dtFiltroInicio.Value.Date : null,
                    DataFim = dtFiltroFim.Checked ? (DateTime?)dtFiltroFim.Value.Date.AddDays(1).AddSeconds(-1) : null
                };

                var resultado = _osService.Buscar(filtro, _paginaAtual, TamanhoPagina);
                dgvOrdens.DataSource = resultado.Items;
                _totalPaginas = resultado.TotalPaginas == 0 ? 1 : resultado.TotalPaginas;
                lblPagina.Text = "Página " + _paginaAtual + " de " + _totalPaginas;
                lblTotal.Text = "Total: " + resultado.TotalRegistros + " OS";

                btnPaginaAnterior.Enabled = _paginaAtual > 1;
                btnPaginaProxima.Enabled = _paginaAtual < _totalPaginas;
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao buscar OS", ex, "FrmOrdensLista.Buscar");
                MessageBox.Show("Erro ao buscar OS. Verifique o log.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _paginaAtual = 1;
            Buscar();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            cmbFiltroCliente.SelectedIndex = 0;
            cmbFiltroStatus.SelectedIndex = 0;
            dtFiltroInicio.Checked = false;
            dtFiltroFim.Checked = false;
            _paginaAtual = 1;
            Buscar();
        }

        private void btnPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaAtual > 1)
            {
                _paginaAtual--;
                Buscar();
            }
        }

        private void btnPaginaProxima_Click(object sender, EventArgs e)
        {
            if (_paginaAtual < _totalPaginas)
            {
                _paginaAtual++;
                Buscar();
            }
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            using (var form = new FrmOrdemEdit())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    Buscar();
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            AbrirSelecionada();
        }

        private void dgvOrdens_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            AbrirSelecionada();
        }

        private void AbrirSelecionada()
        {
            if (dgvOrdens.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma OS para abrir.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var os = dgvOrdens.CurrentRow.DataBoundItem as OS;
            if (os == null) return;

            using (var form = new FrmOrdemEdit(os.Id))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    Buscar();
            }
        }

        // ----- Classes auxiliares para os combos -----

        private class ComboItemCliente
        {
            public long Id { get; set; }
            public string Nome { get; set; }

            public ComboItemCliente(long id, string nome)
            {
                Id = id;
                Nome = nome;
            }

            public override string ToString() { return Nome; }
        }

        private class ComboItemStatus
        {
            public StatusOS? Status { get; set; }
            public string Texto { get; set; }

            public ComboItemStatus(StatusOS? status, string texto)
            {
                Status = status;
                Texto = texto;
            }

            public override string ToString() { return Texto; }
        }
    }
}