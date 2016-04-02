using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Negocio;
using Util;

namespace UI.Web
{
    public partial class Usuarios : Base
    {
        #region Variables

        UsuarioLogic _logic;
                
        #endregion

        #region Propiedades

        private Usuario Entity
        {
            get;
            set;
        }

        private UsuarioLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new UsuarioLogic();
                }
                return _logic;
            }
        }

        #endregion

        #region Metodos

        private void LoadGrid()
        {
            this.gridView.DataSource = this.Logic.GetAll();
            this.gridView.DataBind();            
        }

        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.emailTextBox.Text = this.Entity.EMail;
            this.personasList.SelectedIndex = new PersonaLogic().DameIndex(Entity.Per);
            this.habilitadoCheckBox.Checked = this.Entity.Habilitado;
            this.nombreUsuarioTextBox.Text = this.Entity.NombreUsuario;
            this.claveTextBox.Text = this.Entity.Clave;
            this.repetirClaveTextBox.Text = this.Entity.Clave;
        }

        private void EnableForm(bool enable)
        {
            this.emailTextBox.Enabled = enable;
            this.personasList.Enabled = enable;
            this.habilitadoCheckBox.Enabled = enable;
            this.nombreUsuarioTextBox.Enabled = enable;
            this.claveLabel.Visible = enable;
            this.claveTextBox.Visible = enable;
            this.repetirClaveLabel.Visible = enable;
            this.repetirClaveTextBox.Visible = enable;
        }

        private void DeleteEntity(int id)
        {
            try
            {
                this.Logic.Delete(id);
            }
            catch (ErrorEliminar ex)
            {
                this.errorPanel.Visible = true;
                this.usuarioPanel.Visible = false;
                this.mensajeError.Text = ex.Message;
                this.aceptarLinkButton.Enabled = false;
            }
        }

        private void CargarPersonas()
        {            
            this.personasList.DataSource = new PersonaLogic().GetAll();

            this.personasList.DataTextField = "completo";

            this.personasList.DataBind();
        }

        private void ClearForm()
        {
            this.nombreUsuarioTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.habilitadoCheckBox.Checked = false;
            this.claveTextBox.Text = string.Empty;
            this.repetirClaveTextBox.Text = string.Empty;
            this.personasList.SelectedIndex = 0;
        }

        private void LoadEntity(Usuario usuario)
        {
            usuario.EMail = this.emailTextBox.Text;
            usuario.NombreUsuario = this.nombreUsuarioTextBox.Text;
            // !!!!!!!!!!!
            Business.Entities.Personas pActual = new PersonaLogic().DamePersona(this.personasList.SelectedIndex);
            usuario.Apellido = pActual.Apellido;
            usuario.Nombre = pActual.Nombre;
            usuario.Per = pActual;
            usuario.Clave = this.claveTextBox.Text;
            usuario.Habilitado = this.habilitadoCheckBox.Checked;            
        }

        private void SaveEntity(Usuario usuario)
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                Session["logueo"] = usuario;
            }
            this.Logic.Save(usuario);
        }

        private void CargarDatos()
        {
            Usuario usrActual = ((Usuario)Session["logueo"]);
            this.SelectedID = usrActual.ID;
            this.FormMode = FormModes.Modificacion;

            this.nombreUsuarioTextBox.Text = usrActual.NombreUsuario;
            this.personasList.SelectedIndex = new PersonaLogic().DameIndex(usrActual.Per);
            this.personasList.Enabled = false;
            this.emailTextBox.Text = usrActual.EMail;
            this.habilitadoCheckBox.Checked = usrActual.Habilitado;
            this.habilitadoCheckBox.Enabled = false;
            this.claveTextBox.Text = usrActual.Clave.ToString();
            this.repetirClaveTextBox.Text = usrActual.Clave.ToString();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            DeshabilitarOpciones();
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                if (!Page.IsPostBack)
                {
                    this.adminPanel.Visible = false;
                    this.usuarioPanel.Visible = true;
                    CargarPersonas();
                    CargarDatos();
                }
                else
                {
                    this.errorPanel.Visible = false;
                    this.aceptarLinkButton.Enabled = true;
                    this.usuarioPanel.Visible = true;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    LoadGrid();
                    //!!!!!!!!!!!
                    CargarPersonas();

                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                }
                else
                {
                    LoadGrid();
                    string nombre = this.personasList.SelectedValue;
                    this.errorPanel.Visible = false;
                    this.aceptarLinkButton.Enabled = true;
                }
            }
        }
        
        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.SelectedID = (int)this.gridView.SelectedValue;
            this.usuarioPanel.Visible = false;
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.EnableForm(true);
                this.usuarioPanel.Visible = true;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
            }
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case (FormModes.Modificacion):
                    {
                        this.Entity = new Usuario();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                case (FormModes.Baja):
                    {
                        this.DeleteEntity(this.SelectedID);
                        this.LoadGrid();
                        break;
                    }
                case (FormModes.Alta):
                    {
                        this.Entity = new Usuario();
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                default:
                    break;
            }

            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
            {
                Response.Redirect("Usuarios.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.usuarioPanel.Visible = true;
                this.EnableForm(false);
                this.aceptarLinkButton.CausesValidation = false;
                this.LoadForm(this.SelectedID);
                this.FormMode = FormModes.Baja;
            }
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.usuarioPanel.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
            {
                //this.usuarioPanel.Visible = false;
                Response.Redirect("Usuarios.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void personasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.emailTextBox.Text = new PersonaLogic().DamePersona(this.personasList.SelectedIndex).Email;
        }
        
        #endregion
  
    }
}