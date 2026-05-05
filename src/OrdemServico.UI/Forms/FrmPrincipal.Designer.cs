namespace OrdemServico.UI.Forms
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.tsPrincipal = new System.Windows.Forms.ToolStrip();
            this.tsbCadastros = new System.Windows.Forms.ToolStripDropDownButton();
            this.miClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.miServicos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOrdens = new System.Windows.Forms.ToolStripDropDownButton();
            this.miOrdensListar = new System.Windows.Forms.ToolStripMenuItem();
            this.miOrdemNova = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbRelatorios = new System.Windows.Forms.ToolStripDropDownButton();
            this.miRelatorioOS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSair = new System.Windows.Forms.ToolStripDropDownButton();
            this.miSair = new System.Windows.Forms.ToolStripMenuItem();
            this.ssRodape = new System.Windows.Forms.StatusStrip();
            this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblBanco = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDataHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlFundo = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.timerRelogio = new System.Windows.Forms.Timer(this.components);
            this.tsPrincipal.SuspendLayout();
            this.ssRodape.SuspendLayout();
            this.pnlFundo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsPrincipal
            // 
            this.tsPrincipal.AutoSize = false;
            this.tsPrincipal.BackColor = System.Drawing.Color.White;
            this.tsPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsPrincipal.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCadastros,
            this.tsbOrdens,
            this.tsbRelatorios,
            this.tsbSair});
            this.tsPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tsPrincipal.Name = "tsPrincipal";
            this.tsPrincipal.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsPrincipal.Size = new System.Drawing.Size(800, 100);
            this.tsPrincipal.TabIndex = 1;
            this.tsPrincipal.Text = "toolStrip1";
            // 
            // tsbCadastros
            // 
            this.tsbCadastros.AutoSize = false;
            this.tsbCadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClientes,
            this.miServicos});
            this.tsbCadastros.Image = ((System.Drawing.Image)(resources.GetObject("tsbCadastros.Image")));
            this.tsbCadastros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCadastros.Name = "tsbCadastros";
            this.tsbCadastros.ShowDropDownArrow = false;
            this.tsbCadastros.Size = new System.Drawing.Size(90, 90);
            this.tsbCadastros.Text = "Cadastros";
            this.tsbCadastros.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // miClientes
            // 
            this.miClientes.Name = "miClientes";
            this.miClientes.Size = new System.Drawing.Size(224, 26);
            this.miClientes.Text = "Clientes";
            this.miClientes.Click += new System.EventHandler(this.miClientes_Click);
            // 
            // miServicos
            // 
            this.miServicos.Name = "miServicos";
            this.miServicos.Size = new System.Drawing.Size(224, 26);
            this.miServicos.Text = "Serviços";
            this.miServicos.Click += new System.EventHandler(this.miServicos_Click);
            // 
            // tsbOrdens
            // 
            this.tsbOrdens.AutoSize = false;
            this.tsbOrdens.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOrdensListar,
            this.miOrdemNova});
            this.tsbOrdens.Image = ((System.Drawing.Image)(resources.GetObject("tsbOrdens.Image")));
            this.tsbOrdens.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOrdens.Name = "tsbOrdens";
            this.tsbOrdens.ShowDropDownArrow = false;
            this.tsbOrdens.Size = new System.Drawing.Size(90, 90);
            this.tsbOrdens.Text = "OS";
            this.tsbOrdens.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // miOrdensListar
            // 
            this.miOrdensListar.Name = "miOrdensListar";
            this.miOrdensListar.Size = new System.Drawing.Size(250, 26);
            this.miOrdensListar.Text = "Listar";
            this.miOrdensListar.Click += new System.EventHandler(this.miOrdensListar_Click);
            // 
            // miOrdemNova
            // 
            this.miOrdemNova.Name = "miOrdemNova";
            this.miOrdemNova.Size = new System.Drawing.Size(250, 26);
            this.miOrdemNova.Text = "Nova Ordem de Serviço";
            this.miOrdemNova.Click += new System.EventHandler(this.miOrdemNova_Click);
            // 
            // tsbRelatorios
            // 
            this.tsbRelatorios.AutoSize = false;
            this.tsbRelatorios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRelatorioOS});
            this.tsbRelatorios.Image = ((System.Drawing.Image)(resources.GetObject("tsbRelatorios.Image")));
            this.tsbRelatorios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRelatorios.Name = "tsbRelatorios";
            this.tsbRelatorios.ShowDropDownArrow = false;
            this.tsbRelatorios.Size = new System.Drawing.Size(90, 90);
            this.tsbRelatorios.Text = "Relatórios";
            this.tsbRelatorios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // miRelatorioOS
            // 
            this.miRelatorioOS.Name = "miRelatorioOS";
            this.miRelatorioOS.Size = new System.Drawing.Size(263, 26);
            this.miRelatorioOS.Text = "Relatório Gerencial de OS";
            this.miRelatorioOS.Click += new System.EventHandler(this.miRelatorioOS_Click);
            // 
            // tsbSair
            // 
            this.tsbSair.AutoSize = false;
            this.tsbSair.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSair});
            this.tsbSair.ForeColor = System.Drawing.Color.Maroon;
            this.tsbSair.Image = ((System.Drawing.Image)(resources.GetObject("tsbSair.Image")));
            this.tsbSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSair.Name = "tsbSair";
            this.tsbSair.ShowDropDownArrow = false;
            this.tsbSair.Size = new System.Drawing.Size(90, 90);
            this.tsbSair.Text = "Sair";
            this.tsbSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // miSair
            // 
            this.miSair.Name = "miSair";
            this.miSair.Size = new System.Drawing.Size(224, 26);
            this.miSair.Text = "Sair do Sistema";
            this.miSair.Click += new System.EventHandler(this.miSair_Click);
            // 
            // ssRodape
            // 
            this.ssRodape.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssRodape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUsuario,
            this.lblBanco,
            this.lblDataHora});
            this.ssRodape.Location = new System.Drawing.Point(0, 420);
            this.ssRodape.Name = "ssRodape";
            this.ssRodape.Size = new System.Drawing.Size(800, 30);
            this.ssRodape.SizingGrip = false;
            this.ssRodape.TabIndex = 2;
            this.ssRodape.Text = "statusStrip1";
            // 
            // lblUsuario
            // 
            this.lblUsuario.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(76, 24);
            this.lblUsuario.Text = "Usuário: -";
            // 
            // lblBanco
            // 
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(640, 24);
            this.lblBanco.Spring = true;
            this.lblBanco.Text = "Banco: -";
            this.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataHora
            // 
            this.lblDataHora.Name = "lblDataHora";
            this.lblDataHora.Size = new System.Drawing.Size(69, 24);
            this.lblDataHora.Text = "--/--/----";
            // 
            // pnlFundo
            // 
            this.pnlFundo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(140)))));
            this.pnlFundo.Controls.Add(this.lblLogo);
            this.pnlFundo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFundo.Location = new System.Drawing.Point(0, 0);
            this.pnlFundo.Name = "pnlFundo";
            this.pnlFundo.Size = new System.Drawing.Size(800, 450);
            this.pnlFundo.TabIndex = 3;
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(110, 184);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(466, 106);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "Sistema OS";
            // 
            // timerRelogio
            // 
            this.timerRelogio.Enabled = true;
            this.timerRelogio.Interval = 1000;
            this.timerRelogio.Tick += new System.EventHandler(this.timerRelogio_Tick);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ssRodape);
            this.Controls.Add(this.tsPrincipal);
            this.Controls.Add(this.pnlFundo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestão de OS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.tsPrincipal.ResumeLayout(false);
            this.tsPrincipal.PerformLayout();
            this.ssRodape.ResumeLayout(false);
            this.ssRodape.PerformLayout();
            this.pnlFundo.ResumeLayout(false);
            this.pnlFundo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsPrincipal;
        private System.Windows.Forms.ToolStripDropDownButton tsbCadastros;
        private System.Windows.Forms.ToolStripDropDownButton tsbOrdens;
        private System.Windows.Forms.ToolStripDropDownButton tsbRelatorios;
        private System.Windows.Forms.ToolStripDropDownButton tsbSair;
        private System.Windows.Forms.ToolStripMenuItem miClientes;
        private System.Windows.Forms.ToolStripMenuItem miServicos;
        private System.Windows.Forms.ToolStripMenuItem miOrdensListar;
        private System.Windows.Forms.ToolStripMenuItem miOrdemNova;
        private System.Windows.Forms.ToolStripMenuItem miRelatorioOS;
        private System.Windows.Forms.ToolStripMenuItem miSair;
        private System.Windows.Forms.StatusStrip ssRodape;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuario;
        private System.Windows.Forms.ToolStripStatusLabel lblBanco;
        private System.Windows.Forms.ToolStripStatusLabel lblDataHora;
        private System.Windows.Forms.Panel pnlFundo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Timer timerRelogio;
    }
}