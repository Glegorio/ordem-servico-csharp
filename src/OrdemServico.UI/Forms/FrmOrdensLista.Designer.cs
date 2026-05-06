namespace OrdemServico.UI.Forms
{
    partial class FrmOrdensLista
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
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtFiltroFim = new System.Windows.Forms.DateTimePicker();
            this.lblDataFim = new System.Windows.Forms.Label();
            this.dtFiltroInicio = new System.Windows.Forms.DateTimePicker();
            this.lblDataInicio = new System.Windows.Forms.Label();
            this.cmbFiltroStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbFiltroCliente = new System.Windows.Forms.ComboBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPaginaProxima = new System.Windows.Forms.Button();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnPaginaAnterior = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.btnNova = new System.Windows.Forms.Button();
            this.dgvOrdens = new System.Windows.Forms.DataGridView();
            this.pnlFiltros.SuspendLayout();
            this.pnlAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdens)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlFiltros.Controls.Add(this.btnLimpar);
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.dtFiltroFim);
            this.pnlFiltros.Controls.Add(this.lblDataFim);
            this.pnlFiltros.Controls.Add(this.dtFiltroInicio);
            this.pnlFiltros.Controls.Add(this.lblDataInicio);
            this.pnlFiltros.Controls.Add(this.cmbFiltroStatus);
            this.pnlFiltros.Controls.Add(this.lblStatus);
            this.pnlFiltros.Controls.Add(this.cmbFiltroCliente);
            this.pnlFiltros.Controls.Add(this.lblCliente);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 0);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1082, 90);
            this.pnlFiltros.TabIndex = 0;
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.Silver;
            this.btnLimpar.ForeColor = System.Drawing.Color.White;
            this.btnLimpar.Location = new System.Drawing.Point(830, 28);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(90, 28);
            this.btnLimpar.TabIndex = 9;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(735, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 28);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtFiltroFim
            // 
            this.dtFiltroFim.Checked = false;
            this.dtFiltroFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFiltroFim.Location = new System.Drawing.Point(595, 30);
            this.dtFiltroFim.Name = "dtFiltroFim";
            this.dtFiltroFim.ShowCheckBox = true;
            this.dtFiltroFim.Size = new System.Drawing.Size(125, 27);
            this.dtFiltroFim.TabIndex = 7;
            // 
            // lblDataFim
            // 
            this.lblDataFim.AutoSize = true;
            this.lblDataFim.Location = new System.Drawing.Point(595, 10);
            this.lblDataFim.Name = "lblDataFim";
            this.lblDataFim.Size = new System.Drawing.Size(70, 20);
            this.lblDataFim.TabIndex = 6;
            this.lblDataFim.Text = "Data fim:";
            // 
            // dtFiltroInicio
            // 
            this.dtFiltroInicio.Checked = false;
            this.dtFiltroInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFiltroInicio.Location = new System.Drawing.Point(460, 30);
            this.dtFiltroInicio.Name = "dtFiltroInicio";
            this.dtFiltroInicio.ShowCheckBox = true;
            this.dtFiltroInicio.Size = new System.Drawing.Size(125, 27);
            this.dtFiltroInicio.TabIndex = 5;
            // 
            // lblDataInicio
            // 
            this.lblDataInicio.AutoSize = true;
            this.lblDataInicio.Location = new System.Drawing.Point(460, 10);
            this.lblDataInicio.Name = "lblDataInicio";
            this.lblDataInicio.Size = new System.Drawing.Size(84, 20);
            this.lblDataInicio.TabIndex = 4;
            this.lblDataInicio.Text = "Data início:";
            // 
            // cmbFiltroStatus
            // 
            this.cmbFiltroStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroStatus.FormattingEnabled = true;
            this.cmbFiltroStatus.Location = new System.Drawing.Point(300, 30);
            this.cmbFiltroStatus.Name = "cmbFiltroStatus";
            this.cmbFiltroStatus.Size = new System.Drawing.Size(150, 28);
            this.cmbFiltroStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(300, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // cmbFiltroCliente
            // 
            this.cmbFiltroCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroCliente.FormattingEnabled = true;
            this.cmbFiltroCliente.Location = new System.Drawing.Point(10, 30);
            this.cmbFiltroCliente.Name = "cmbFiltroCliente";
            this.cmbFiltroCliente.Size = new System.Drawing.Size(280, 28);
            this.cmbFiltroCliente.TabIndex = 1;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(10, 10);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(58, 20);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            // 
            // pnlAcoes
            // 
            this.pnlAcoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlAcoes.Controls.Add(this.lblTotal);
            this.pnlAcoes.Controls.Add(this.btnPaginaProxima);
            this.pnlAcoes.Controls.Add(this.lblPagina);
            this.pnlAcoes.Controls.Add(this.btnPaginaAnterior);
            this.pnlAcoes.Controls.Add(this.btnAbrir);
            this.pnlAcoes.Controls.Add(this.btnNova);
            this.pnlAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcoes.Location = new System.Drawing.Point(0, 548);
            this.pnlAcoes.Name = "pnlAcoes";
            this.pnlAcoes.Size = new System.Drawing.Size(1082, 55);
            this.pnlAcoes.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(900, 20);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(80, 20);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total: 0 OS";
            // 
            // btnPaginaProxima
            // 
            this.btnPaginaProxima.BackColor = System.Drawing.Color.Silver;
            this.btnPaginaProxima.ForeColor = System.Drawing.Color.White;
            this.btnPaginaProxima.Location = new System.Drawing.Point(630, 12);
            this.btnPaginaProxima.Name = "btnPaginaProxima";
            this.btnPaginaProxima.Size = new System.Drawing.Size(90, 32);
            this.btnPaginaProxima.TabIndex = 4;
            this.btnPaginaProxima.Text = "Próxima >";
            this.btnPaginaProxima.UseVisualStyleBackColor = false;
            this.btnPaginaProxima.Click += new System.EventHandler(this.btnPaginaProxima_Click);
            // 
            // lblPagina
            // 
            this.lblPagina.Location = new System.Drawing.Point(500, 20);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(120, 20);
            this.lblPagina.TabIndex = 3;
            this.lblPagina.Text = "Página 1 de 1";
            this.lblPagina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPaginaAnterior
            // 
            this.btnPaginaAnterior.BackColor = System.Drawing.Color.Silver;
            this.btnPaginaAnterior.ForeColor = System.Drawing.Color.White;
            this.btnPaginaAnterior.Location = new System.Drawing.Point(400, 12);
            this.btnPaginaAnterior.Name = "btnPaginaAnterior";
            this.btnPaginaAnterior.Size = new System.Drawing.Size(90, 32);
            this.btnPaginaAnterior.TabIndex = 2;
            this.btnPaginaAnterior.Text = "< Anterior";
            this.btnPaginaAnterior.UseVisualStyleBackColor = false;
            this.btnPaginaAnterior.Click += new System.EventHandler(this.btnPaginaAnterior_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAbrir.ForeColor = System.Drawing.Color.White;
            this.btnAbrir.Location = new System.Drawing.Point(120, 12);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(100, 32);
            this.btnAbrir.TabIndex = 1;
            this.btnAbrir.Text = "Abrir OS";
            this.btnAbrir.UseVisualStyleBackColor = false;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnNova
            // 
            this.btnNova.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNova.ForeColor = System.Drawing.Color.White;
            this.btnNova.Location = new System.Drawing.Point(10, 12);
            this.btnNova.Name = "btnNova";
            this.btnNova.Size = new System.Drawing.Size(100, 32);
            this.btnNova.TabIndex = 0;
            this.btnNova.Text = "Nova OS";
            this.btnNova.UseVisualStyleBackColor = false;
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            // 
            // dgvOrdens
            // 
            this.dgvOrdens.AllowUserToAddRows = false;
            this.dgvOrdens.AllowUserToDeleteRows = false;
            this.dgvOrdens.AllowUserToResizeRows = false;
            this.dgvOrdens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdens.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrdens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrdens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdens.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrdens.Location = new System.Drawing.Point(0, 90);
            this.dgvOrdens.MultiSelect = false;
            this.dgvOrdens.Name = "dgvOrdens";
            this.dgvOrdens.ReadOnly = true;
            this.dgvOrdens.RowHeadersVisible = false;
            this.dgvOrdens.RowHeadersWidth = 51;
            this.dgvOrdens.RowTemplate.Height = 24;
            this.dgvOrdens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdens.Size = new System.Drawing.Size(1082, 458);
            this.dgvOrdens.TabIndex = 2;
            this.dgvOrdens.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdens_CellDoubleClick);
            // 
            // FrmOrdensLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1082, 603);
            this.Controls.Add(this.dgvOrdens);
            this.Controls.Add(this.pnlAcoes);
            this.Controls.Add(this.pnlFiltros);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmOrdensLista";
            this.Text = "Ordens de Serviço";
            this.Load += new System.EventHandler(this.FrmOrdensLista_Load);
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlAcoes.ResumeLayout(false);
            this.pnlAcoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.DateTimePicker dtFiltroInicio;
        private System.Windows.Forms.Label lblDataInicio;
        private System.Windows.Forms.ComboBox cmbFiltroStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbFiltroCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtFiltroFim;
        private System.Windows.Forms.Label lblDataFim;
        private System.Windows.Forms.Panel pnlAcoes;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnPaginaProxima;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.Button btnPaginaAnterior;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.DataGridView dgvOrdens;
    }
}