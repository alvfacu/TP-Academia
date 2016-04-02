using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Business.Entities;
using Util;

namespace UI.Web
{
    public partial class ModulosUsuarios : Base
    {
        #region Variables

        ModuloUsuarioLogic _logic;
        
        #endregion

        #region Propiedades

        private ModuloUsuarioLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new ModuloUsuarioLogic();
                }
                return _logic;
            }
        }

        private ModuloUsuario Entity
        {
            get;
            set;
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
            this.modulosList.SelectedIndex = new ModuloLogic().DameIndex(this.Entity.IdModulo);
            this.usuariosList.SelectedIndex = new ModuloLogic().DameIndex(this.Entity.IdUsuario);
            this.altaCheck.Checked = this.Entity.PermiteAlta;
            this.bajaCheck.Checked = this.Entity.PermiteBaja;
            this.modificacionCheck.Checked = this.Entity.PermiteModificacion;
            this.consultaCheck.Checked = this.Entity.PermiteConsulta;
        }

        private void EnableForm(bool enable)
        {
            this.modulosList.Enabled = enable;
            this.usuariosList.Enabled = enable;
            this.altaCheck.Enabled = enable;
            this.bajaCheck.Enabled = enable;
            this.modificacionCheck.Enabled = enable;
            this.consultaCheck.Enabled = enable;
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

        private void CargarListas()
        {
            this.modulosList.DataSource = new ModuloLogic().GetAll();
            this.usuariosList.DataSource = new UsuarioLogic().GetAll();

            this.modulosList.DataTextField = "descripcion";
            this.usuariosList.DataTextField = "nombreusuario";

            this.modulosList.DataBind();
            this.usuariosList.DataBind();
        }

        private void LoadEntity(ModuloUsuario modusr)
        {
            modusr.IdModulo = new ModuloLogic().DameIDModulo(this.modulosList.SelectedIndex);
            modusr.IdUsuario = new UsuarioLogic().DameIDUsuario(this.usuariosList.SelectedIndex);
            modusr.PermiteAlta = this.altaCheck.Checked;
            modusr.PermiteBaja = this.bajaCheck.Checked;
            modusr.PermiteModificacion = this.modificacionCheck.Checked;
            modusr.PermiteConsulta = this.consultaCheck.Checked;
        }

        private void SaveEntity(ModuloUsuario modusr)
        {
            this.Logic.Save(modusr);
        }
        
        private void ClearForm()
        {
            this.modulosList.SelectedIndex = 0;
            this.usuariosList.SelectedIndex = 0;
            this.consultaCheck.Checked = false;
            this.altaCheck.Checked = false;
            this.bajaCheck.Checked = false;
            this.modificacionCheck.Checked = false;
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
                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                    LoadGrid();
                    CargarListas();
                }
                else
                {
                    LoadGrid();
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
                        this.Entity = new ModuloUsuario();
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
                        this.Entity = new ModuloUsuario();
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                default:
                    break;
            }

            Response.Redirect("ModulosUsuarios.aspx");
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
                Response.Redirect("ModulosUsuarios.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        #endregion
    }
}