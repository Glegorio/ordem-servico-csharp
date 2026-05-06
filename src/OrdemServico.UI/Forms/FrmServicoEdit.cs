using System;
using System.Windows.Forms;
using OrdemServico.Entities;
using OrdemServico.Infra.Logging;
using OrdemServico.Services.Exceptions;
using OrdemServico.Services.Implementations;

namespace OrdemServico.UI.Forms
{
    public partial class FrmServicoEdit : Form
    {
        private readonly ServicoService _service = new ServicoService();
        private Servico _servico;
        private readonly bool _modoEdicao;

        public FrmServicoEdit()
        {
            InitializeComponent();
            _servico = new Servico { Ativo = true };
            _modoEdicao = false;
            this.Text = "Cadastro de Serviço - Novo";
        }

        public FrmServicoEdit(Servico servico)
        {
            InitializeComponent();
            _servico = servico;
            _modoEdicao = true;
            this.Text = "Cadastro de Serviço - Edição";
        }

        private void FrmServicoEdit_Load(object sender, EventArgs e)
        {
            if (_modoEdicao)
            {
                txtNome.Text = _servico.Nome;
                numValorBase.Value = _servico.ValorBase;
                numImposto.Value = _servico.PercentualImposto;
                chkAtivo.Checked = _servico.Ativo;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _servico.Nome = txtNome.Text.Trim();
                _servico.ValorBase = numValorBase.Value;
                _servico.PercentualImposto = numImposto.Value;
                _servico.Ativo = chkAtivo.Checked;

                Cursor = Cursors.WaitCursor;

                if (_modoEdicao)
                    _service.Atualizar(_servico);
                else
                    _servico.Id = _service.Cadastrar(_servico);

                MessageBox.Show("Serviço salvo com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (RegraNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao salvar servico", ex, "FrmServicoEdit.Salvar");
                MessageBox.Show("Erro ao salvar servico. Verifique o log.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}