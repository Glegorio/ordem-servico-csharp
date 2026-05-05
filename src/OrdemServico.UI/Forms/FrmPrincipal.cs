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
        private MdiClient _mdiClient;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Usuário: " + (SessionContext.UsuarioAtual ?? "-");

            var connStr = ConfigurationManager.ConnectionStrings["PostgreSql"];
            if (connStr != null)
            {
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

            ConfigurarMdiClient();
            AtualizarRelogio();
            Logger.Info("FrmPrincipal aberto", "FrmPrincipal.Load");
        }

        private void ConfigurarMdiClient()
        {
            foreach (Control c in this.Controls)
            {
                if (c is MdiClient)
                {
                    _mdiClient = (MdiClient)c;
                    _mdiClient.BackColor = Color.FromArgb(0, 122, 140);
                    _mdiClient.Paint += MdiClient_Paint;
                    _mdiClient.Resize += (s, ev) => _mdiClient.Invalidate();
                    this.MdiChildActivate += (s, ev) => _mdiClient.Invalidate();
                    break;
                }
            }
        }

        private void MdiClient_Paint(object sender, PaintEventArgs e)
        {
            if (MdiChildren.Length > 0) return;

            const string texto = "Sistema OS";
            using (var font = new Font("Segoe UI", 48F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            {
                var size = e.Graphics.MeasureString(texto, font);
                var x = (_mdiClient.ClientSize.Width - size.Width) / 2;
                var y = (_mdiClient.ClientSize.Height - size.Height) / 2;
                e.Graphics.DrawString(texto, font, brush, x, y);
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

        private void miClientes_Click(object sender, EventArgs e)
        {
            AbrirMdiChild<FrmClientesLista>();
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

        private void AbrirMdiChild<T>() where T : Form, new()
        {
            foreach (var f in MdiChildren)
            {
                if (f is T)
                {
                    f.BringToFront();
                    f.Focus();
                    return;
                }
            }

            var novo = new T();
            novo.MdiParent = this;
            novo.Show();
            novo.WindowState = FormWindowState.Maximized;
        }
    }
}