using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using OrdemServico.Infra.Logging;
using OrdemServico.Infra.Session;

namespace OrdemServico.UI.Forms
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Preenche status bar
            lblUsuario.Text = "Usuário: " + (SessionContext.UsuarioAtual ?? "-");

            var connStr = ConfigurationManager.ConnectionStrings["PostgreSql"];
            if (connStr != null)
            {
                // Extrai apenas o nome do banco da connection string para nao expor senha
                var partes = connStr.ConnectionString.Split(';');
                foreach (var p in partes)
                {
                    if (p.TrimStart().StartsWith("Database=", StringComparison.OrdinalIgnoreCase))
                    {
                        lblBanco.Text = "Banco: " + p.Split('=')[1];
                        break;
                    }
                }
            }

            AtualizarRelogio();
            CentralizarLogo();
            Logger.Info("FrmPrincipal aberto", "FrmPrincipal.Load");
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CentralizarLogo();
        }

        private void CentralizarLogo()
        {
            if (lblLogo != null && pnlFundo != null)
            {
                lblLogo.Left = (pnlFundo.Width - lblLogo.Width) / 2;
                lblLogo.Top = (pnlFundo.Height - lblLogo.Height) / 2;
            }
        }

        private void timerRelogio_Tick(object sender, EventArgs e)
        {
            AtualizarRelogio();
        }

        private void AtualizarRelogio()
        {
            lblDataHora.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm:ss");
        }

        // -----------------------------------------------------------------
        // EVENTOS DOS MENUS — placeholders por enquanto
        // Cada um vai abrir o Form correspondente quando criarmos.
        // -----------------------------------------------------------------

        private void miClientes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FrmClientesLista — proxima etapa.",
                "Em construção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miServicos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FrmServicosLista — proxima etapa.",
                "Em construção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miOrdensListar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FrmOrdensLista — proxima etapa.",
                "Em construção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miOrdemNova_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FrmOrdemEdit (modo novo) — proxima etapa.",
                "Em construção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miRelatorioOS_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FrmRelatorio — proxima etapa.",
                "Em construção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miSair_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show("Deseja realmente sair do sistema?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == DialogResult.Yes)
            {
                Logger.Info("Logout do usuario " + SessionContext.UsuarioAtual,
                    "FrmPrincipal.Sair");
                SessionContext.Limpar();
                Application.Exit();
            }
        }
    }
}