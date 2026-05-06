namespace OrdemServico.UI.Forms
{
    partial class FrmServicosLista
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
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.chkSomenteAtivos = new System.Windows.Forms.CheckBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.dgvServicos = new System.Windows.Forms.DataGridView();
            this.pnlAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAcoes
            // 
            this.pnlAcoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlAcoes.Controls.Add(this.lblTotal);
            this.pnlAcoes.Controls.Add(this.chkSomenteAtivos);
            this.pnlAcoes.Controls.Add(this.btnEditar);
            this.pnlAcoes.Controls.Add(this.btnNovo);
            this.pnlAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcoes.Location = new System.Drawing.Point(0, 453);
            this.pnlAcoes.Name = "pnlAcoes";
            this.pnlAcoes.Size = new System.Drawing.Size(882, 100);
            this.pnlAcoes.TabIndex = 0;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(700, 18);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(123, 20);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total: 0 serviço(s)";
            // 
            // chkSomenteAtivos
            // 
            this.chkSomenteAtivos.AutoSize = true;
            this.chkSomenteAtivos.Checked = true;
            this.chkSomenteAtivos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSomenteAtivos.Location = new System.Drawing.Point(240, 18);
            this.chkSomenteAtivos.Name = "chkSomenteAtivos";
            this.chkSomenteAtivos.Size = new System.Drawing.Size(176, 24);
            this.chkSomenteAtivos.TabIndex = 2;
            this.chkSomenteAtivos.Text = "Mostrar apenas ativos";
            this.chkSomenteAtivos.UseVisualStyleBackColor = true;
            this.chkSomenteAtivos.CheckedChanged += new System.EventHandler(this.chkSomenteAtivos_CheckedChanged);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(120, 12);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 32);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(10, 12);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(100, 32);
            this.btnNovo.TabIndex = 0;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // dgvServicos
            // 
            this.dgvServicos.AllowUserToAddRows = false;
            this.dgvServicos.AllowUserToDeleteRows = false;
            this.dgvServicos.AllowUserToResizeRows = false;
            this.dgvServicos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServicos.BackgroundColor = System.Drawing.Color.White;
            this.dgvServicos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServicos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServicos.GridColor = System.Drawing.SystemColors.Control;
            this.dgvServicos.Location = new System.Drawing.Point(0, 0);
            this.dgvServicos.MultiSelect = false;
            this.dgvServicos.Name = "dgvServicos";
            this.dgvServicos.ReadOnly = true;
            this.dgvServicos.RowHeadersVisible = false;
            this.dgvServicos.RowHeadersWidth = 51;
            this.dgvServicos.RowTemplate.Height = 24;
            this.dgvServicos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServicos.Size = new System.Drawing.Size(882, 453);
            this.dgvServicos.TabIndex = 1;
            this.dgvServicos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServicos_CellDoubleClick);
            // 
            // FrmServicosLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.dgvServicos);
            this.Controls.Add(this.pnlAcoes);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmServicosLista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Serviços";
            this.Load += new System.EventHandler(this.FrmServicosLista_Load);
            this.pnlAcoes.ResumeLayout(false);
            this.pnlAcoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAcoes;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox chkSomenteAtivos;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.DataGridView dgvServicos;
    }
}