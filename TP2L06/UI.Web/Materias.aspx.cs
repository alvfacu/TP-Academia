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
    public partial class Materias : Base
    {
        #region Variables

        MateriaLogic _logic;
        
        #endregion

        #region Propiedades

        private MateriaLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new MateriaLogic();
                }
                return _logic;
            }
        }

        private Materia Entity
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
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.semanalesTextBox.Text = this.Entity.HSSemanales.ToString();
            this.totalesTextBox.Text = this.Entity.HSTotales.ToString();
            this.planesList.SelectedIndex = new PlanLogic().DameIndex(this.Entity.IDPlan);
        }

        private void EnableForm(bool enable)
        {
            this.planesList.Enabled = enable;
            this.totalesTextBox.Enabled = enable;
            this.semanalesTextBox.Enabled = enable;
            this.descripcionTextBox.Enabled = enable;
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
            this.planesList.DataSource = new PlanLogic().GetAll();
            this.planesList.DataTextField = "descripcion";
            this.DataBind();
        }

        private void LoadEntity(Materia mat)
        {
            mat.IDPlan = new PlanLogic().DameIDPlan(this.planesList.SelectedIndex);
            mat.Descripcion = this.descripcionTextBox.Text;
            mat.HSSemanales = Convert.ToInt32(this.semanalesTextBox.Text);
            mat.HSTotales = Convert.ToInt32(this.totalesTextBox.Text);            
        }

        private void SaveEntity(Materia mat)
        {
            this.Logic.Save(mat);
        }
        
        private bool ValidarHs()
        {
            if (Convert.ToInt32(this.totalesTextBox.Text) > Convert.ToInt32(this.semanalesTextBox.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private void ClearForm()
        {
            this.totalesTextBox.Text = string.Empty;
            this.semanalesTextBox.Text = string.Empty;
            this.descripcionTextBox.Text = string.Empty;
            this.planesList.SelectedIndex = 0;
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
                    this.adminPanel.Visible = true;
                    this.usuarioPanel.Visible = false;
                    this.gridPanel.Visible = true;
                    this.gridActionPanel.Visible = false;
                    this.gridView.Columns[4].Visible = false;
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
            if (ValidarHs())
            {
                switch (this.FormMode)
                {
                    case (FormModes.Modificacion):
                        {
                            this.Entity = new Materia();
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
                            this.Entity = new Materia();
                            this.LoadEntity(this.Entity);
                            this.SaveEntity(this.Entity);
                            this.LoadGrid();
                            break;
                        }
                    default:
                        break;
                }                
                Response.Redirect("Materias.aspx");
            }
            else
            {
                this.errorPanel.Visible = true;
                this.mensajeError.Visible = true;
                this.mensajeError.Text = "Las Hs Semanales deben ser menores que las Hs Totales.\n";
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
                Response.Redirect("Materias.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        #endregion
    }
}