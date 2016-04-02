using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Business.Entities;

namespace UI.Web
{
    public partial class Login : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DeshabilitarOpciones();
        }

    }
}
