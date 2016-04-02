using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Negocio;

namespace UI.Web
{
    public partial class Login1 : System.Web.UI.UserControl
    {
        protected void ingresarButton_Click(object sender, EventArgs e)
        {
            Usuario usuarioActual = new UsuarioLogic().ValidarUsuario(this.usuarioTextBox.Text, this.claveTextBox.Text);

            if (usuarioActual == null)
            {
                this.errorLabel.Visible = true;
                this.errorLabel.Text = "Ingreso incorrecto. Verifique haber ingresado correctamente los datos del login.";
            }
            else if(!(usuarioActual.Habilitado))
            {
                this.errorLabel.Visible = true;
                this.errorLabel.Text = "Usuario no habilitado. Contacte al Administrador.";
            }
            else
            {
                Session["logueo"] = usuarioActual;  //crea una variable de sesion (visible en todas las webs)
                Response.Redirect("Default.aspx");
            }
        }

    }
}