using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Logging;
using OrdemServico.Repositories.Common;
using OrdemServico.Reports.Dtos;
using OrdemServico.Reports.Filters;
using OrdemServico.Reports.Services;
using OrdemServico.Services.Implementations;

namespace OrdemServico.UI.Forms
{
    public partial class FrmRelatorio : Form
    {
        private readonly RelatorioService _relatorioService = new RelatorioService();
        private readonly ClienteService _clienteService = new ClienteService();
        private List<RelatorioOrdemDto> _ultimoResultado;

        public FrmRelatorio()
        {
            InitializeComponent();
        }

        private void FrmRelatorio_Load(object sender, EventArgs e)
        {
            CarregarFiltroClientes();
            CarregarFiltroStatus();
            ConfigurarReportViewer();
        }

        private void CarregarFiltroClientes()
        {
            cmbCliente.Items.Clear();
            cmbCliente.Items.Add(new ComboItemCliente(0, "(Todos os clientes)"));

            var resultado = _clienteService.Buscar(new ClienteFiltro { Ativo = true }, 1, 1000);
            foreach (var c in resultado.Items)
                cmbCliente.Items.Add(new ComboItemCliente(c.Id, c.Nome));

            cmbCliente.SelectedIndex = 0;
        }

        private void CarregarFiltroStatus()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add(new ComboItemStatus(null, "(Todos)"));
            cmbStatus.Items.Add(new ComboItemStatus(StatusOS.Aberta, "Aberta"));
            cmbStatus.Items.Add(new ComboItemStatus(StatusOS.EmAndamento, "Em andamento"));
            cmbStatus.Items.Add(new ComboItemStatus(StatusOS.Concluida, "Concluída"));
            cmbStatus.Items.Add(new ComboItemStatus(StatusOS.Cancelada, "Cancelada"));
            cmbStatus.SelectedIndex = 0;
        }

        private void ConfigurarReportViewer()
        {
            reportViewer.ProcessingMode = ProcessingMode.Local;

            // Carrega o .rdlc do assembly OrdemServico.Reports (nao do UI).
            // Usar typeof de uma classe do projeto Reports garante o assembly correto.
            var assembly = typeof(RelatorioOrdemDto).Assembly;
            var resourceName = "OrdemServico.Reports.RelatorioOrdens.rdlc";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    // Lista os recursos disponiveis para debug
                    var disponiveis = string.Join("\n", assembly.GetManifestResourceNames());
                    throw new Exception("Recurso nao encontrado: " + resourceName +
                        "\n\nRecursos disponiveis:\n" + disponiveis);
                }
                reportViewer.LocalReport.LoadReportDefinition(stream);
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var filtro = new RelatorioFiltro
                {
                    ClienteId = ((ComboItemCliente)cmbCliente.SelectedItem).Id == 0
                        ? (long?)null
                        : ((ComboItemCliente)cmbCliente.SelectedItem).Id,
                    Status = ((ComboItemStatus)cmbStatus.SelectedItem).Status,
                    DataInicio = dtInicio.Checked ? (DateTime?)dtInicio.Value.Date : null,
                    DataFim = dtFim.Checked ? (DateTime?)dtFim.Value.Date.AddDays(1).AddSeconds(-1) : null
                };

                _ultimoResultado = _relatorioService.GerarDadosOrdens(filtro);

                // Monta texto do periodo para o cabecalho
                string periodoTexto = MontarTextoPeriodo(filtro);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(
                    new ReportDataSource("dsOrdens", _ultimoResultado));

                reportViewer.LocalReport.SetParameters(new[]
                {
                    new ReportParameter("PeriodoTexto", periodoTexto)
                });

                reportViewer.RefreshReport();

                Logger.Info("Relatorio gerado com " + _ultimoResultado.Count + " registros",
                    "FrmRelatorio.btnGerar");
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao gerar relatorio", ex, "FrmRelatorio.btnGerar");
                MessageBox.Show("Erro ao gerar relatorio: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            if (_ultimoResultado == null || _ultimoResultado.Count == 0)
            {
                MessageBox.Show("Gere o relatorio antes de exportar.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "PDF (*.pdf)|*.pdf";
                dlg.FileName = "RelatorioOS_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";

                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    byte[] pdfBytes = reportViewer.LocalReport.Render("PDF");
                    File.WriteAllBytes(dlg.FileName, pdfBytes);

                    MessageBox.Show("PDF exportado com sucesso:\n" + dlg.FileName,
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.Error("Erro ao exportar PDF", ex, "FrmRelatorio.ExportarPdf");
                    MessageBox.Show("Erro ao exportar PDF: " + ex.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private string MontarTextoPeriodo(RelatorioFiltro f)
        {
            string parteData;
            if (f.DataInicio.HasValue && f.DataFim.HasValue)
                parteData = "de " + f.DataInicio.Value.ToString("dd/MM/yyyy") +
                           " até " + f.DataFim.Value.ToString("dd/MM/yyyy");
            else if (f.DataInicio.HasValue)
                parteData = "a partir de " + f.DataInicio.Value.ToString("dd/MM/yyyy");
            else if (f.DataFim.HasValue)
                parteData = "até " + f.DataFim.Value.ToString("dd/MM/yyyy");
            else
                parteData = "todos os períodos";

            return parteData;
        }

        // Classes auxiliares
        private class ComboItemCliente
        {
            public long Id { get; set; }
            public string Nome { get; set; }
            public ComboItemCliente(long id, string nome) { Id = id; Nome = nome; }
            public override string ToString() { return Nome; }
        }

        private class ComboItemStatus
        {
            public StatusOS? Status { get; set; }
            public string Texto { get; set; }
            public ComboItemStatus(StatusOS? s, string t) { Status = s; Texto = t; }
            public override string ToString() { return Texto; }
        }
    }
}