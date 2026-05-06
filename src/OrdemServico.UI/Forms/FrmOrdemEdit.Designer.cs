namespace OrdemServico.UI.Forms
{
    partial class FrmOrdemEdit
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
            this.pnlCabecalho = new System.Windows.Forms.Panel();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.lblDataConclusao = new System.Windows.Forms.Label();
            this.lblConclusao = new System.Windows.Forms.Label();
            this.lblDataAbertura = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblVersao = new System.Windows.Forms.Label();
            this.lblVerf = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblNumeroOS = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.gbAdicionarItem = new System.Windows.Forms.GroupBox();
            this.btnAdicionarItem = new System.Windows.Forms.Button();
            this.numQuantidade = new System.Windows.Forms.NumericUpDown();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.cmbServico = new System.Windows.Forms.ComboBox();
            this.lblServico = new System.Windows.Forms.Label();
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConcluir = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnRemoverItem = new System.Windows.Forms.Button();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.lblValorT = new System.Windows.Forms.Label();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.pnlCabecalho.SuspendLayout();
            this.gbAdicionarItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantidade)).BeginInit();
            this.pnlAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCabecalho
            // 
            this.pnlCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlCabecalho.Controls.Add(this.txtObservacao);
            this.pnlCabecalho.Controls.Add(this.lblObservacao);
            this.pnlCabecalho.Controls.Add(this.lblDataConclusao);
            this.pnlCabecalho.Controls.Add(this.lblConclusao);
            this.pnlCabecalho.Controls.Add(this.lblDataAbertura);
            this.pnlCabecalho.Controls.Add(this.lblData);
            this.pnlCabecalho.Controls.Add(this.cmbCliente);
            this.pnlCabecalho.Controls.Add(this.lblCliente);
            this.pnlCabecalho.Controls.Add(this.lblVersao);
            this.pnlCabecalho.Controls.Add(this.lblVerf);
            this.pnlCabecalho.Controls.Add(this.lblStatus);
            this.pnlCabecalho.Controls.Add(this.lblStats);
            this.pnlCabecalho.Controls.Add(this.lblNumeroOS);
            this.pnlCabecalho.Controls.Add(this.lblNumero);
            this.pnlCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecalho.Location = new System.Drawing.Point(0, 0);
            this.pnlCabecalho.Name = "pnlCabecalho";
            this.pnlCabecalho.Size = new System.Drawing.Size(982, 147);
            this.pnlCabecalho.TabIndex = 0;
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(10, 116);
            this.txtObservacao.MaxLength = 500;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(970, 27);
            this.txtObservacao.TabIndex = 13;
            // 
            // lblObservacao
            // 
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.Location = new System.Drawing.Point(10, 95);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Size = new System.Drawing.Size(90, 20);
            this.lblObservacao.TabIndex = 12;
            this.lblObservacao.Text = "Observação:";
            // 
            // lblDataConclusao
            // 
            this.lblDataConclusao.Location = new System.Drawing.Point(630, 65);
            this.lblDataConclusao.Name = "lblDataConclusao";
            this.lblDataConclusao.Size = new System.Drawing.Size(200, 23);
            this.lblDataConclusao.TabIndex = 11;
            this.lblDataConclusao.Text = "-";
            // 
            // lblConclusao
            // 
            this.lblConclusao.AutoSize = true;
            this.lblConclusao.Location = new System.Drawing.Point(630, 45);
            this.lblConclusao.Name = "lblConclusao";
            this.lblConclusao.Size = new System.Drawing.Size(80, 20);
            this.lblConclusao.TabIndex = 10;
            this.lblConclusao.Text = "Conclusão:";
            // 
            // lblDataAbertura
            // 
            this.lblDataAbertura.Location = new System.Drawing.Point(420, 65);
            this.lblDataAbertura.Name = "lblDataAbertura";
            this.lblDataAbertura.Size = new System.Drawing.Size(200, 23);
            this.lblDataAbertura.TabIndex = 9;
            this.lblDataAbertura.Text = "-";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(420, 45);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(70, 20);
            this.lblData.TabIndex = 8;
            this.lblData.Text = "Abertura:";
            // 
            // cmbCliente
            // 
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(10, 65);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(400, 28);
            this.cmbCliente.TabIndex = 7;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(10, 45);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(58, 20);
            this.lblCliente.TabIndex = 6;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.Location = new System.Drawing.Point(510, 10);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(15, 20);
            this.lblVersao.TabIndex = 5;
            this.lblVersao.Text = "-";
            // 
            // lblVerf
            // 
            this.lblVerf.AutoSize = true;
            this.lblVerf.Location = new System.Drawing.Point(450, 10);
            this.lblVerf.Name = "lblVerf";
            this.lblVerf.Size = new System.Drawing.Size(56, 20);
            this.lblVerf.TabIndex = 4;
            this.lblVerf.Text = "Versão:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblStatus.Location = new System.Drawing.Point(260, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(19, 25);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "-";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(200, 10);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(52, 20);
            this.lblStats.TabIndex = 2;
            this.lblStats.Text = "Status:";
            // 
            // lblNumeroOS
            // 
            this.lblNumeroOS.AutoSize = true;
            this.lblNumeroOS.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroOS.Location = new System.Drawing.Point(60, 8);
            this.lblNumeroOS.Name = "lblNumeroOS";
            this.lblNumeroOS.Size = new System.Drawing.Size(68, 25);
            this.lblNumeroOS.TabIndex = 1;
            this.lblNumeroOS.Text = "(nova)";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(10, 10);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(49, 20);
            this.lblNumero.TabIndex = 0;
            this.lblNumero.Text = "OS n°:";
            // 
            // gbAdicionarItem
            // 
            this.gbAdicionarItem.Controls.Add(this.btnAdicionarItem);
            this.gbAdicionarItem.Controls.Add(this.numQuantidade);
            this.gbAdicionarItem.Controls.Add(this.lblQuantidade);
            this.gbAdicionarItem.Controls.Add(this.cmbServico);
            this.gbAdicionarItem.Controls.Add(this.lblServico);
            this.gbAdicionarItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAdicionarItem.Location = new System.Drawing.Point(0, 147);
            this.gbAdicionarItem.Name = "gbAdicionarItem";
            this.gbAdicionarItem.Size = new System.Drawing.Size(982, 90);
            this.gbAdicionarItem.TabIndex = 1;
            this.gbAdicionarItem.TabStop = false;
            this.gbAdicionarItem.Text = "Adicionar Item";
            // 
            // btnAdicionarItem
            // 
            this.btnAdicionarItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAdicionarItem.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarItem.Location = new System.Drawing.Point(540, 44);
            this.btnAdicionarItem.Name = "btnAdicionarItem";
            this.btnAdicionarItem.Size = new System.Drawing.Size(120, 28);
            this.btnAdicionarItem.TabIndex = 4;
            this.btnAdicionarItem.Text = "+ Adicionar";
            this.btnAdicionarItem.UseVisualStyleBackColor = false;
            this.btnAdicionarItem.Click += new System.EventHandler(this.btnAdicionarItem_Click);
            // 
            // numQuantidade
            // 
            this.numQuantidade.DecimalPlaces = 3;
            this.numQuantidade.Location = new System.Drawing.Point(420, 45);
            this.numQuantidade.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numQuantidade.Name = "numQuantidade";
            this.numQuantidade.Size = new System.Drawing.Size(100, 27);
            this.numQuantidade.TabIndex = 3;
            this.numQuantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Location = new System.Drawing.Point(420, 25);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(90, 20);
            this.lblQuantidade.TabIndex = 2;
            this.lblQuantidade.Text = "Quantidade:";
            // 
            // cmbServico
            // 
            this.cmbServico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServico.FormattingEnabled = true;
            this.cmbServico.Location = new System.Drawing.Point(10, 45);
            this.cmbServico.Name = "cmbServico";
            this.cmbServico.Size = new System.Drawing.Size(400, 28);
            this.cmbServico.TabIndex = 1;
            // 
            // lblServico
            // 
            this.lblServico.AutoSize = true;
            this.lblServico.Location = new System.Drawing.Point(10, 25);
            this.lblServico.Name = "lblServico";
            this.lblServico.Size = new System.Drawing.Size(60, 20);
            this.lblServico.TabIndex = 0;
            this.lblServico.Text = "Serviço:";
            // 
            // pnlAcoes
            // 
            this.pnlAcoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlAcoes.Controls.Add(this.btnFechar);
            this.pnlAcoes.Controls.Add(this.btnCancelar);
            this.pnlAcoes.Controls.Add(this.btnConcluir);
            this.pnlAcoes.Controls.Add(this.btnIniciar);
            this.pnlAcoes.Controls.Add(this.btnRemoverItem);
            this.pnlAcoes.Controls.Add(this.lblValorTotal);
            this.pnlAcoes.Controls.Add(this.lblValorT);
            this.pnlAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcoes.Location = new System.Drawing.Point(0, 593);
            this.pnlAcoes.Name = "pnlAcoes";
            this.pnlAcoes.Size = new System.Drawing.Size(982, 60);
            this.pnlAcoes.TabIndex = 2;
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(880, 14);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(90, 32);
            this.btnFechar.TabIndex = 6;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(700, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 32);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar OS";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConcluir
            // 
            this.btnConcluir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnConcluir.ForeColor = System.Drawing.Color.White;
            this.btnConcluir.Location = new System.Drawing.Point(600, 14);
            this.btnConcluir.Name = "btnConcluir";
            this.btnConcluir.Size = new System.Drawing.Size(90, 32);
            this.btnConcluir.TabIndex = 4;
            this.btnConcluir.Text = "Concluir";
            this.btnConcluir.UseVisualStyleBackColor = false;
            this.btnConcluir.Click += new System.EventHandler(this.btnConcluir_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Gold;
            this.btnIniciar.ForeColor = System.Drawing.Color.White;
            this.btnIniciar.Location = new System.Drawing.Point(500, 14);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(90, 32);
            this.btnIniciar.TabIndex = 3;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnRemoverItem
            // 
            this.btnRemoverItem.BackColor = System.Drawing.Color.Maroon;
            this.btnRemoverItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoverItem.Location = new System.Drawing.Point(300, 14);
            this.btnRemoverItem.Name = "btnRemoverItem";
            this.btnRemoverItem.Size = new System.Drawing.Size(120, 32);
            this.btnRemoverItem.TabIndex = 2;
            this.btnRemoverItem.Text = "Remover Item";
            this.btnRemoverItem.UseVisualStyleBackColor = false;
            this.btnRemoverItem.Click += new System.EventHandler(this.btnRemoverItem_Click);
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblValorTotal.Location = new System.Drawing.Point(100, 12);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(93, 31);
            this.lblValorTotal.TabIndex = 1;
            this.lblValorTotal.Text = "R$ 0,00";
            // 
            // lblValorT
            // 
            this.lblValorT.AutoSize = true;
            this.lblValorT.Location = new System.Drawing.Point(10, 18);
            this.lblValorT.Name = "lblValorT";
            this.lblValorT.Size = new System.Drawing.Size(83, 20);
            this.lblValorT.TabIndex = 0;
            this.lblValorT.Text = "Valor Total:";
            // 
            // dgvItens
            // 
            this.dgvItens.AllowUserToAddRows = false;
            this.dgvItens.AllowUserToDeleteRows = false;
            this.dgvItens.AllowUserToResizeRows = false;
            this.dgvItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItens.BackgroundColor = System.Drawing.Color.White;
            this.dgvItens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItens.GridColor = System.Drawing.SystemColors.Control;
            this.dgvItens.Location = new System.Drawing.Point(0, 237);
            this.dgvItens.MultiSelect = false;
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.ReadOnly = true;
            this.dgvItens.RowHeadersVisible = false;
            this.dgvItens.RowHeadersWidth = 51;
            this.dgvItens.RowTemplate.Height = 24;
            this.dgvItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItens.Size = new System.Drawing.Size(982, 356);
            this.dgvItens.TabIndex = 3;
            // 
            // FrmOrdemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.dgvItens);
            this.Controls.Add(this.pnlAcoes);
            this.Controls.Add(this.gbAdicionarItem);
            this.Controls.Add(this.pnlCabecalho);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOrdemEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ordem de Serviço";
            this.Load += new System.EventHandler(this.FrmOrdemEdit_Load);
            this.pnlCabecalho.ResumeLayout(false);
            this.pnlCabecalho.PerformLayout();
            this.gbAdicionarItem.ResumeLayout(false);
            this.gbAdicionarItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantidade)).EndInit();
            this.pnlAcoes.ResumeLayout(false);
            this.pnlAcoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCabecalho;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblNumeroOS;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label lblDataAbertura;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Label lblVerf;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.Label lblDataConclusao;
        private System.Windows.Forms.Label lblConclusao;
        private System.Windows.Forms.GroupBox gbAdicionarItem;
        private System.Windows.Forms.Label lblServico;
        private System.Windows.Forms.NumericUpDown numQuantidade;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.ComboBox cmbServico;
        private System.Windows.Forms.Button btnAdicionarItem;
        private System.Windows.Forms.Panel pnlAcoes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConcluir;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnRemoverItem;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.Label lblValorT;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.DataGridView dgvItens;
    }
}