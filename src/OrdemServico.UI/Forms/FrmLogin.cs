using System;
using System.Windows.Forms;
using OrdemServico.Infra.Logging;
using OrdemServico.Infra.Session;

namespace OrdemServico.UI.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Autenticar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Autenticar();
                e.Handled = true;
            }
        }

        private void Autenticar()
        {
            var usuario = txtUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("Informe o nome de usuario.",
                    "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (usuario.Length < 3)
            {
                MessageBox.Show("Nome de usuario deve ter pelo menos 3 caracteres.",
                    "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            // Login mock — apenas grava no SessionContext para uso na auditoria
            // Em produção, aqui seria validacao real de credenciais
            SessionContext.UsuarioAtual = usuario;
            Logger.Info("Login efetuado: " + usuario, "FrmLogin.Autenticar");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}