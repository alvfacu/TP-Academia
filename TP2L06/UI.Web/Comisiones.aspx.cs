﻿using System;
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
    public partial class Comisiones : Base
    {
        #region Variables

        ComisionLogic _logic;
        
        #endregion

        #region Propiedades

        private ComisionLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new ComisionLogic();
                }
                return _logic;
            }
        }
                
        private Comision Entity
        {
            get;
            set;
        }
                
        #endregion

        #region Metodos

        private void LoadGrid()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                this.gridView.DataSource = this.Logic.GetAllXPlan(((Usuario)Session["logueo"]).Per.IDPlan);
            }
            else
            {
                this.gridView.DataSource = this.Logic.GetAll();
            }
            this.gridView.DataBind();
        }

        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.anioTextBox.Text = this.Entity.AnioEspecialidad.ToString();
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.planesList.SelectedIndex = new PlanLogic().DameIndex(this.Entity.IDPlan);
        }

        private void EnableForm(bool enable)
        {
            this.anioTextBox.Enabled = enable;
            this.descripcionTextBox.Enabled = enable;
            this.planesList.Enabled = enable;
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
            this.planesList.DataSource = new PlanLogic().GetAll();

            this.planesList.DataTextField = "descripcion";

            this.planesList.DataBind();
        }

        private void ClearForm()
        {
            this.anioTextBox.Text = string.Empty;
            this.descripcionTextBox.Text = string.Empty;
            this.planesList.SelectedIndex = 0;
        }

        private void LoadEntity(Comision comision)
        {
            comision.IDPlan = new PlanLogic().DameIDPlan(this.planesList.SelectedIndex);
            comision.AnioEspecialidad = Convert.ToInt32(this.anioTextBox.Text);
            comision.Descripcion = this.descripcionTextBox.Text;
        }

        private void SaveEntity(Comision comision)
        {
            this.Logic.Save(comision);
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            DeshabilitarOpciones();
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                if (!(Page.IsPostBack))
                {
                    LoadGrid();
                    this.adminPanel.Visible = true;
                    this.usuarioPanel.Visible = false;
                    this.gridPanel.Visible = true;
                    this.gridActionPanel.Visible = false;
                    this.gridView.Columns[3].Visible = false;
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
                    CargarPlanes();
                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                }
                else
                {
                    LoadGrid();
                    this.errorPanel.Visible = false;
                    this.aceptarLinkButton.Enabled = true;
                    this.usuarioPanel.Visible = true;
                }
            }
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.editarLinkButton.Enabled = true;
            this.eliminarLinkButton.Enabled = true;
            this.SelectedID = (int)this.gridView.SelectedValue;
            this.usuarioPanel.Visible = false;
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.EnableForm(true);
                this.formPanel.Visible = true;
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
                        this.Entity = new Comision();
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
                        this.Entity = new Comision();
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                    default:
                        break;
            }

            Response.Redirect("Comisiones.aspx");
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.formPanel.Visible = true;
                this.EnableForm(false);
                this.aceptarLinkButton.CausesValidation = false;
                this.LoadForm(this.SelectedID);
                this.FormMode = FormModes.Baja;
            }
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.formPanel.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
            {
                //this.usuarioPanel.Visible = false;
                Response.Redirect("Comisiones.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        #endregion
    }
}