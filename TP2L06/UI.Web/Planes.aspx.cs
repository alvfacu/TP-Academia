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
    public partial class Planes : Base
    {
        #region Variables

        PlanLogic _logic;
        
        #endregion

        #region Propiedades

        private PlanLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new PlanLogic();
                }
                return _logic;
            }
        }
        
        private Plan Entity
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
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.especiliadadesList.SelectedIndex = new EspecialidadLogic().DameIndex(this.Entity.IDEspecialidad);
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.especiliadadesList.Enabled = enable;
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

        private void CargarPlanes()
        {
            this.especiliadadesList.DataTextField = "descripcion";
            this.especiliadadesList.DataSource = new EspecialidadLogic().GetAll();
            this.especiliadadesList.DataBind();
        }

        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.especiliadadesList.SelectedIndex = 0;
        }
        
        private void LoadEntity(Plan plan)
        {            
            plan.Descripcion = this.descripcionTextBox.Text;
            int idEsp = new EspecialidadLogic().DameIDEspecialidad(this.especiliadadesList.SelectedIndex);
            plan.IDEspecialidad = idEsp;            
        }

        private void SaveEntity(Plan plan)
        {
            this.Logic.Save(plan);
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
                    LoadGrid();
                    this.gridView.SelectedIndex = new PlanLogic().DameIndex(((Usuario)Session["logueo"]).Per.IDPlan);
                    this.adminPanel.Visible = true;
                    this.usuarioPanel.Visible = false;
                    this.gridPanel.Visible = true;
                    if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
                    {
                        this.gridActionPanel.FindControl("nuevoLinkButton").Visible = false;
                        this.gridActionPanel.FindControl("editarLinkButton").Visible = false;
                        this.gridActionPanel.FindControl("eliminarLinkButton").Visible = false;
                    }
                    else
                    {
                        this.gridActionPanel.Visible = false;
                    }
                    this.gridView.Columns[1].Visible = false;
                }
                else
                {
                    this.errorPanel.Visible = false;
                    this.aceptarLinkButton.Enabled = true;
                    this.usuarioPanel.Visible = false;
                }
            }
            else
            {

                if (!Page.IsPostBack)
                {
                    LoadGrid();
                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                    CargarPlanes();
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
                        this.Entity = new Plan();
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
                        this.Entity = new Plan();
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                default:
                    break;
            }

            Response.Redirect("Planes.aspx");
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
                Response.Redirect("Planes.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void reporteLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportePlanes.aspx");
        }

        #endregion


    }
}