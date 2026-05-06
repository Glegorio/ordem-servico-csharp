namespace OrdemServico.UI.Forms
{
    partial class FrmServicoEdit
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
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblValorBase = new System.Windows.Forms.Label();
            this.numValorBase = new System.Windows.Forms.NumericUpDown();
            this.lblImposto = new System.Windows.Forms.Label();
            this.numImposto = new System.Windows.Forms.NumericUpDown();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numValorBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImposto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(20, 25);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(53, 20);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(20, 45);
            this.txtNome.MaxLength = 150;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(440, 27);
            this.txtNome.TabIndex = 1;
            // 
            // lblValorBase
            // 
            this.lblValorBase.AutoSize = true;
            this.lblValorBase.Location = new System.Drawing.Point(20, 85);
            this.lblValorBase.Name = "lblValorBase";
            this.lblValorBase.Size = new System.Drawing.Size(112, 20);
            this.lblValorBase.TabIndex = 2;
            this.lblValorBase.Text = "Valor Base (R$):";
            // 
            // numValorBase
            // 
            this.numValorBase.DecimalPlaces = 2;
            this.numValorBase.Location = new System.Drawing.Point(20, 105);
            this.numValorBase.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numValorBase.Name = "numValorBase";
            this.numValorBase.Size = new System.Drawing.Size(200, 27);
            this.numValorBase.TabIndex = 3;
            this.numValorBase.ThousandsSeparator = true;
            // 
            // lblImposto
            // 
            this.lblImposto.AutoSize = true;
            this.lblImposto.Location = new System.Drawing.Point(240, 85);
            this.lblImposto.Name = "lblImposto";
            this.lblImposto.Size = new System.Drawing.Size(93, 20);
            this.lblImposto.TabIndex = 4;
            this.lblImposto.Text = "Imposto (%):";
            // 
            // numImposto
            // 
            this.numImposto.DecimalPlaces = 2;
            this.numImposto.Location = new System.Drawing.Point(240, 105);
            this.numImposto.Name = "numImposto";
            this.numImposto.Size = new System.Drawing.Size(220, 27);
            this.numImposto.TabIndex = 5;
            this.numImposto.ThousandsSeparator = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Location = new System.Drawing.Point(20, 160);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(116, 24);
            this.chkAtivo.TabIndex = 6;
            this.chkAtivo.Text = "Serviço ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(280, 230);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(90, 32);
            this.btnSalvar.TabIndex = 7;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Maroon;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(380, 230);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 32);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 267);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro";
            // 
            // FrmServicoEdit
            // 
            this.AcceptButton = this.btnSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(482, 273);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.numImposto);
            this.Controls.Add(this.lblImposto);
            this.Controls.Add(this.numValorBase);
            this.Controls.Add(this.lblValorBase);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmServicoEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cadastro de Serviço";
            this.Load += new System.EventHandler(this.FrmServicoEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numValorBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImposto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblValorBase;
        private System.Windows.Forms.NumericUpDown numValorBase;
        private System.Windows.Forms.Label lblImposto;
        private System.Windows.Forms.NumericUpDown numImposto;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}