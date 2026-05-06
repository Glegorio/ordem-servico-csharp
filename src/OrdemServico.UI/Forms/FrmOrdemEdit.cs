using System;
using System.Windows.Forms;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Logging;
using OrdemServico.Repositories.Common;
using OrdemServico.Services.Exceptions;
using OrdemServico.Services.Implementations;
using OS = OrdemServico.Entities.OrdemServico;

namespace OrdemServico.UI.Forms
{
    public partial class FrmOrdemEdit : Form
    {
        private readonly OrdemServicoService _osService = new OrdemServicoService();
        private readonly ClienteService _clienteService = new ClienteService();
        private readonly ServicoService _servicoService = new ServicoService();

        private OS _os;
        private readonly bool _modoNova;
        private readonly long _osId;

        public FrmOrdemEdit()
        {
            InitializeComponent();
            _modoNova = true;
            this.Text = "Nova Ordem de Serviço";
        }

        public FrmOrdemEdit(long osId)
        {
            InitializeComponent();
            _modoNova = false;
            _osId = osId;
            this.Text = "Ordem de Serviço #" + osId;
        }

        private void FrmOrdemEdit_Load(object sender, EventArgs e)
        {
            ConfigurarGridItens();
            CarregarClientes();
            CarregarServicos();

            if (_modoNova)
                ExibirNovaOS();
            else
                CarregarOS();
        }

        private void ConfigurarGridItens()
        {
            dgvItens.AutoGenerateColumns = false;
            dgvItens.Columns.Clear();

            dgvItens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ServicoNome",
                HeaderText = "Serviço",
                DataPropertyName = "ServicoNome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 50
            });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantidade",
                HeaderText = "Qtd",
                DataPropertyName = "Quantidade",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N3",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorUnitario",
                HeaderText = "Valor Unit. (R$)",
                DataPropertyName = "ValorUnitario",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PercentualImpostoAplicado",
                HeaderText = "Imposto (%)",
                DataPropertyName = "PercentualImpostoAplicado",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorTotalItem",
                HeaderText = "Total Item (R$)",
                DataPropertyName = "ValorTotalItem",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
                }
            });
        }

        private void CarregarClientes()
        {
            cmbCliente.Items.Clear();
            var resultado = _clienteService.Buscar(new ClienteFiltro { Ativo = true }, 1, 1000);
            foreach (var c in resultado.Items)
                cmbCliente.Items.Add(new ComboItemCliente(c.Id, c.Nome));
        }

        private void CarregarServicos()
        {
            cmbServico.Items.Clear();
            var ativos = _servicoService.ListarAtivos();
            foreach (var s in ativos)
                cmbServico.Items.Add(new ComboItemServico(s.Id, s.Nome, s.ValorBase));
            if (cmbServico.Items.Count > 0)
                cmbServico.SelectedIndex = 0;
        }

        private void ExibirNovaOS()
        {
            lblNumeroOS.Text = "(nova)";
            lblStatus.Text = "—";
            lblVersao.Text = "—";
            lblDataAbertura.Text = "—";
            lblDataConclusao.Text = "—";
            lblValorTotal.Text = "R$ 0,00";

            cmbCliente.Enabled = true;
            txtObservacao.Enabled = true;
            cmbCliente.SelectedIndex = cmbCliente.Items.Count > 0 ? 0 : -1;

            HabilitarOperacoes(false);
            btnIniciar.Text = "Criar OS";
            btnIniciar.Enabled = true;
            btnConcluir.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void CarregarOS()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _os = _osService.ObterCompleta(_osId);
                ExibirOS();
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao carregar OS", ex, "FrmOrdemEdit.CarregarOS");
                MessageBox.Show("Erro ao carregar OS: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ExibirOS()
        {
            lblNumeroOS.Text = _os.Id.ToString();
            lblStatus.Text = StatusTexto(_os.Status);
            lblVersao.Text = _os.Versao.ToString();
            lblDataAbertura.Text = _os.DataAbertura.ToString("dd/MM/yyyy HH:mm");
            lblDataConclusao.Text = _os.DataConclusao.HasValue
                ? _os.DataConclusao.Value.ToString("dd/MM/yyyy HH:mm")
                : "—";
            lblValorTotal.Text = "R$ " + _os.ValorTotal.ToString("N2");
            txtObservacao.Text = _os.Observacao ?? "";

            // Cliente: seleciona no combo
            for (int i = 0; i < cmbCliente.Items.Count; i++)
            {
                if (((ComboItemCliente)cmbCliente.Items[i]).Id == _os.ClienteId)
                {
                    cmbCliente.SelectedIndex = i;
                    break;
                }
            }
            cmbCliente.Enabled = false; // nao pode trocar cliente apos criada
            txtObservacao.Enabled = (_os.Status == StatusOS.Aberta || _os.Status == StatusOS.EmAndamento);

            dgvItens.DataSource = null;
            dgvItens.DataSource = _os.Itens;

            AtualizarBotoesPorStatus();
        }

        private void AtualizarBotoesPorStatus()
        {
            bool editavel = (_os.Status == StatusOS.Aberta || _os.Status == StatusOS.EmAndamento);

            HabilitarOperacoes(editavel);

            btnIniciar.Text = "Iniciar";
            btnIniciar.Enabled = (_os.Status == StatusOS.Aberta);
            btnConcluir.Enabled = editavel && _os.ValorTotal > 0;
            btnCancelar.Enabled = editavel;
        }

        private void HabilitarOperacoes(bool habilitado)
        {
            gbAdicionarItem.Enabled = habilitado;
            btnRemoverItem.Enabled = habilitado;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (_modoNova)
            {
                CriarNovaOS();
            }
            else
            {
                AlterarStatusInterno(StatusOS.EmAndamento, "iniciar");
            }
        }

        private void CriarNovaOS()
        {
            if (cmbCliente.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var clienteId = ((ComboItemCliente)cmbCliente.SelectedItem).Id;
                var id = _osService.Abrir(clienteId, txtObservacao.Text.Trim());
                _os = _osService.ObterCompleta(id);
                ExibirOS();

                MessageBox.Show("OS #" + id + " criada com sucesso. Adicione os itens.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao criar OS", ex, "FrmOrdemEdit.CriarNovaOS");
                MessageBox.Show("Erro ao criar OS: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            if (_os == null) return;

            if (cmbServico.SelectedItem == null)
            {
                MessageBox.Show("Selecione um servico.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numQuantidade.Value <= 0)
            {
                MessageBox.Show("Quantidade deve ser maior que zero.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var servicoId = ((ComboItemServico)cmbServico.SelectedItem).Id;
                _osService.AdicionarItem(_os.Id, servicoId, numQuantidade.Value, _os.Versao);
                _os = _osService.ObterCompleta(_os.Id);
                ExibirOS();
                numQuantidade.Value = 1;
            }
            catch (ConcorrenciaException ex)
            {
                MessageBox.Show(ex.Message + "\n\nA OS sera recarregada.",
                    "Conflito de Concorrência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CarregarOS();
            }
            catch (RegraNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao adicionar item", ex, "FrmOrdemEdit.AdicionarItem");
                MessageBox.Show("Erro: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (_os == null || dgvItens.CurrentRow == null)
            {
                MessageBox.Show("Selecione um item para remover.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = dgvItens.CurrentRow.DataBoundItem as ItemOrdemServico;
            if (item == null) return;

            var resp = MessageBox.Show(
                "Remover o item '" + item.ServicoNome + "'?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                _osService.RemoverItem(_os.Id, item.Id, _os.Versao);
                _os = _osService.ObterCompleta(_os.Id);
                ExibirOS();
            }
            catch (ConcorrenciaException ex)
            {
                MessageBox.Show(ex.Message + "\n\nA OS sera recarregada.",
                    "Conflito de Concorrência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CarregarOS();
            }
            catch (RegraNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao remover item", ex, "FrmOrdemEdit.RemoverItem");
                MessageBox.Show("Erro: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show(
                "Concluir esta OS? Apos concluida nao podera ser alterada.",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            AlterarStatusInterno(StatusOS.Concluida, "concluir");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show(
                "Cancelar esta OS? Apos cancelada nao podera ser alterada.",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            AlterarStatusInterno(StatusOS.Cancelada, "cancelar");
        }

        private void AlterarStatusInterno(StatusOS novoStatus, string acao)
        {
            if (_os == null) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                _osService.AlterarStatus(_os.Id, novoStatus, _os.Versao);
                _os = _osService.ObterCompleta(_os.Id);
                ExibirOS();
                MessageBox.Show("OS atualizada com sucesso.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ConcorrenciaException ex)
            {
                MessageBox.Show(ex.Message + "\n\nA OS sera recarregada.",
                    "Conflito de Concorrência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CarregarOS();
            }
            catch (RegraNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao " + acao + " OS", ex, "FrmOrdemEdit.AlterarStatus");
                MessageBox.Show("Erro: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private static string StatusTexto(StatusOS s)
        {
            switch (s)
            {
                case StatusOS.Aberta: return "Aberta";
                case StatusOS.EmAndamento: return "Em Andamento";
                case StatusOS.Concluida: return "Concluída";
                case StatusOS.Cancelada: return "Cancelada";
                default: return s.ToString();
            }
        }

        // CLASSES AUXILIARES PARA OS COMBOS

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

        private class ComboItemServico
        {
            public long Id { get; set; }
            public string Nome { get; set; }
            public decimal Valor { get; set; }

            public ComboItemServico(long id, string nome, decimal valor)
            {
                Id = id;
                Nome = nome;
                Valor = valor;
            }

            public override string ToString()
            {
                return Nome + " (R$ " + Valor.ToString("N2") + ")";
            }
        }
    }
}