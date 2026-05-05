using System;
using System.Windows.Forms;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Logging;
using OrdemServico.Services.Exceptions;
using OrdemServico.Services.Implementations;

namespace OrdemServico.UI.Forms
{
    public partial class FrmClienteEdit : Form
    {
        private readonly ClienteService _service = new ClienteService();
        private Cliente _cliente;
        private readonly bool _modoEdicao;

        public FrmClienteEdit()
        {
            InitializeComponent();
            _cliente = new Cliente { Ativo = true, DataCadastro = DateTime.Now };
            _modoEdicao = false;
            this.Text = "Cadastro de Cliente - Novo";
        }

        public FrmClienteEdit(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            _modoEdicao = true;
            this.Text = "Cadastro de Cliente - Edição";
        }

        private void FrmClienteEdit_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.Add(new ComboItem("Pessoa Física", TipoCliente.Fisica));
            cmbTipo.Items.Add(new ComboItem("Pessoa Jurídica", TipoCliente.Juridica));
            cmbTipo.SelectedIndex = 0;

            if (_modoEdicao)
            {
                txtNome.Text = _cliente.Nome;
                txtDocumento.Text = _cliente.Documento;
                txtEmail.Text = _cliente.Email ?? "";
                txtTelefone.Text = _cliente.Telefone ?? "";
                chkAtivo.Checked = _cliente.Ativo;
                cmbTipo.SelectedIndex = _cliente.Tipo == TipoCliente.Fisica ? 0 : 1;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _cliente.Nome = txtNome.Text.Trim();
                _cliente.Documento = txtDocumento.Text.Trim();
                _cliente.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                _cliente.Telefone = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text.Trim();
                _cliente.Ativo = chkAtivo.Checked;
                _cliente.Tipo = ((ComboItem)cmbTipo.SelectedItem).Tipo;

                Cursor = Cursors.WaitCursor;

                if (_modoEdicao)
                    _service.Atualizar(_cliente);
                else
                    _cliente.Id = _service.Cadastrar(_cliente);

                MessageBox.Show("Cliente salvo com sucesso!", "Sucesso",
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
                Logger.Error("Erro ao salvar cliente", ex, "FrmClienteEdit.Salvar");
                MessageBox.Show("Erro ao salvar cliente. Verifique o log para detalhes.",
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

        // Classe interna para usar no ComboBox de tipo de cliente
        private class ComboItem
        {
            public string Texto { get; set; }
            public TipoCliente Tipo { get; set; }

            public ComboItem(string texto, TipoCliente tipo)
            {
                Texto = texto;
                Tipo = tipo;
            }

            public override string ToString() { return Texto; }
        }
    }
}