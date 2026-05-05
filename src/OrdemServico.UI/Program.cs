using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OrdemServico.Infra.Logging;
using OrdemServico.UI.Forms;

namespace OrdemServico.UI
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main()
        {
            // Força DPI awareness antes de qualquer renderizacao
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ... resto do código permanece igual
            try
            {
                using (var login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        return;
                }

                MessageBox.Show("Login OK! Proxima etapa: criar FrmPrincipal.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("Erro fatal na aplicacao", ex, "Program.Main");
                MessageBox.Show("Erro fatal: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}