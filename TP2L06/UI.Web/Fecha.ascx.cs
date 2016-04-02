using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Fecha : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fechaTextBox.Text = DateTime.Now.ToShortDateString();
                calendario.SelectedDate = DateTime.Today;
            }
        }

        public DateTime FechaElegida
        {
            get
            {
                if (!string.IsNullOrEmpty(fechaTextBox.Text))
                {
                    return Convert.ToDateTime(fechaTextBox.Text);
                }
                return DateTime.MinValue;
            }
        }

        protected void calendario_SelectionChanged(object sender, EventArgs e)
        {
            fechaTextBox.Text = calendario.SelectedDate.ToShortDateString();
        }
    }
}