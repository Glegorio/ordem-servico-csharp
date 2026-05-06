using System;
using System.Windows.Forms;

namespace OrdemServico.UI.Forms
{
    public partial class FrmOrdemEdit : Form
    {
        public FrmOrdemEdit()
        {
            InitializeComponent();
            this.Text = "Nova OS - em construção";
        }

        public FrmOrdemEdit(long osId)
        {
            InitializeComponent();
            this.Text = "Editar OS #" + osId + " - em construção";
        }
    }
}