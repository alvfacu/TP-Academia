using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.Desktop.ReportePlanes
{
    public partial class ReportePlanes : Form
    {
        #region Metodos

        public ReportePlanes()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void ReportePlanes_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Planes.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.Planes.DataTable1);

            this.reportViewer1.RefreshReport();
        }

        #endregion
    }
}
