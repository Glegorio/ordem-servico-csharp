using System;
using System.Windows.Forms;
using OrdemServico.Entities;
using OrdemServico.Infra.Logging;
using OrdemServico.Services.Implementations;

namespace OrdemServico.UI.Forms
{
    public partial class FrmServicosLista : Form
    {
        private readonly ServicoService _service = new ServicoService();

        public FrmServicosLista()
        {
            InitializeComponent();
        }

        private void FrmServicosLista_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            Carregar();
        }

        private void ConfigurarGrid()
        {
            dgvServicos.AutoGenerateColumns = false;
            dgvServicos.Columns.Clear();

            dgvServicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvServicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 60
            });
            dgvServicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorBase",
                HeaderText = "Valor Base (R$)",
                DataPropertyName = "ValorBase",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvServicos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PercentualImposto",
                HeaderText = "Imposto (%)",
                DataPropertyName = "PercentualImposto",
                Width = 110,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvServicos.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Ativo",
                HeaderText = "Ativo",
                DataPropertyName = "Ativo",
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
        }

        private void Carregar()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var lista = chkSomenteAtivos.Checked
                    ? _service.ListarAtivos()
                    : _service.ListarTodos();

                dgvServicos.DataSource = lista;
                lblTotal.Text = "Total: " + lista.Count + " serviço(s)";
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao carregar servicos", ex, "FrmServicosLista.Carregar");
                MessageBox.Show("Erro ao carregar servicos. Verifique o log.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void chkSomenteAtivos_CheckedChanged(object sender, EventArgs e)
        {
            Carregar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var form = new FrmServicoEdit())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    Carregar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarSelecionado();
        }

        private void dgvServicos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            EditarSelecionado();
        }

        private void EditarSelecionado()
        {
            if (dgvServicos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um servico para editar.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var servico = dgvServicos.CurrentRow.DataBoundItem as Servico;
            if (servico == null) return;

            var atualizado = _service.ObterPorId(servico.Id);
            if (atualizado == null)
            {
                MessageBox.Show("Servico nao encontrado.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Carregar();
                return;
            }

            using (var form = new FrmServicoEdit(atualizado))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    Carregar();
            }
        }
    }
}