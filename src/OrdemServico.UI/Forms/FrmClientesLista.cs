using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Logging;
using OrdemServico.Repositories.Common;
using OrdemServico.Services.Exceptions;
using OrdemServico.Services.Implementations;

namespace OrdemServico.UI.Forms
{
    public partial class FrmClientesLista : Form
    {
        private readonly ClienteService _service = new ClienteService();
        private const int TamanhoPagina = 50;

        public FrmClientesLista()
        {
            InitializeComponent();
        }

        private void FrmClientesLista_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CarregarFiltroStatus();
            Buscar();
        }

        private void ConfigurarGrid()
        {
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.Columns.Clear();

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 40
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Documento",
                HeaderText = "Documento",
                DataPropertyName = "Documento",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "E-mail",
                DataPropertyName = "Email",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 30
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefone",
                HeaderText = "Telefone",
                DataPropertyName = "Telefone",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvClientes.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Ativo",
                HeaderText = "Ativo",
                DataPropertyName = "Ativo",
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
        }

        private void CarregarFiltroStatus()
        {
            cmbFiltroAtivo.Items.Add(new StatusItem("Todos", null));
            cmbFiltroAtivo.Items.Add(new StatusItem("Ativos", true));
            cmbFiltroAtivo.Items.Add(new StatusItem("Inativos", false));
            cmbFiltroAtivo.SelectedIndex = 1; 
        }

        private void Buscar()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var filtro = new ClienteFiltro
                {
                    Nome = string.IsNullOrWhiteSpace(txtFiltroNome.Text) ? null : txtFiltroNome.Text.Trim(),
                    Documento = string.IsNullOrWhiteSpace(txtFiltroDocumento.Text) ? null : txtFiltroDocumento.Text.Trim(),
                    Ativo = ((StatusItem)cmbFiltroAtivo.SelectedItem).Ativo
                };

                var resultado = _service.Buscar(filtro, 1, TamanhoPagina);
                dgvClientes.DataSource = resultado.Items;
                lblTotal.Text = "Total: " + resultado.TotalRegistros + " cliente(s)";
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao buscar clientes", ex, "FrmClientesLista.Buscar");
                MessageBox.Show("Erro ao buscar clientes. Verifique o log.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnLimparFiltros_Click(object sender, EventArgs e)
        {
            txtFiltroNome.Clear();
            txtFiltroDocumento.Clear();
            cmbFiltroAtivo.SelectedIndex = 0; 
            Buscar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var form = new FrmClienteEdit())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    Buscar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarSelecionado();
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            EditarSelecionado();
        }

        private void EditarSelecionado()
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Selecione um cliente para editar.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = dgvClientes.CurrentRow.DataBoundItem as Cliente;
            if (cliente == null) return;

            var atualizado = _service.ObterPorId(cliente.Id);
            if (atualizado == null)
            {
                MessageBox.Show("Cliente nao encontrado (pode ter sido excluido).",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Buscar();
                return;
            }

            using (var form = new FrmClienteEdit(atualizado))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    Buscar();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Selecione um cliente para excluir.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = dgvClientes.CurrentRow.DataBoundItem as Cliente;
            if (cliente == null) return;

            var resp = MessageBox.Show(
                "Deseja realmente excluir o cliente '" + cliente.Nome + "'?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                _service.Excluir(cliente.Id);
                MessageBox.Show("Cliente excluido com sucesso.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Buscar();
            }
            catch (RegraNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Não permitido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao excluir cliente", ex, "FrmClientesLista.Excluir");
                MessageBox.Show("Erro ao excluir cliente. Verifique o log.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private class StatusItem
        {
            public string Texto { get; set; }
            public bool? Ativo { get; set; }

            public StatusItem(string texto, bool? ativo)
            {
                Texto = texto;
                Ativo = ativo;
            }

            public override string ToString() { return Texto; }
        }
    }
}