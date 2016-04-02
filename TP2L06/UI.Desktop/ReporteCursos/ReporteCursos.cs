using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.Desktop.ReporteCursos
{
    public partial class ReporteCursos : Form
    {
        #region Metodos

        public ReporteCursos()
        {
            InitializeComponent();
        }


        #endregion

        #region Eventos

        private void ReporteCursos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Cursos.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.Cursos.DataTable1);

            this.reportViewer1.RefreshReport();
        }

        #endregion
    }
}
