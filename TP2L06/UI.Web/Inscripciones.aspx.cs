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
    public partial class Inscripciones : Base
    {
        #region Variables

        AlumnoInsLogic _logic;

        #endregion

        #region Propiedades

        private AlumnoInsLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new AlumnoInsLogic();
                }
                return _logic;
            }
        }

        private AlumnoInscripcion Entity
        {
            get;
            set;
        }
        
        #endregion

        #region Metodos

        private void LoadGrid()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Alumno)
            {
                this.gridView.DataSource = this.Logic.GetAllXAlumno(((Usuario)Session["logueo"]).Per.ID);
            }
            else if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
            {
                this.gridView.DataSource = this.Logic.GetAllAlumnosXDocente(((Usuario)Session["logueo"]).Per.ID);
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
            this.condicionTextBox.Text = this.Entity.Condicion;
            this.notaTextBox.Text = this.Entity.Nota.ToString();
            this.alumnosList.SelectedIndex = new PersonaLogic().DameIndex(this.Entity.IDAlumno, Business.Entities.Personas.TiposPersonas.Alumno);
            CargarCursos();
            this.cursoList.SelectedIndex = new CursoLogic().DameIndex(this.Entity.IDCurso);
                        
        }

        private void EnableForm(bool enable)
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Alumno)
            {
                this.alumnosList.Enabled = !enable;
                this.alumnosList.SelectedIndex = new PersonaLogic().DameIndex(((Usuario)Session["logueo"]).Per.ID, Business.Entities.Personas.TiposPersonas.Alumno);
                this.condicionTextBox.Visible = !enable;
                this.notaTextBox.Visible = !enable;
                this.condicionLabel.Visible = !enable;
                this.notaLabel.Visible = !enable;
            }
            else if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
            {
                this.alumnosList.Enabled = !enable;
                this.cursoList.Enabled = !enable;
            }
            else
            {
                this.condicionTextBox.Enabled = enable;
                this.notaTextBox.Enabled = enable;
                this.alumnosList.Enabled = enable;
                this.cursoList.Enabled = enable;
            }
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
            this.alumnosList.DataSource = new PersonaLogic().DevolverAlumnos();
            
            this.alumnosList.DataTextField = "nombre";
            
            this.alumnosList.DataBind();
        }

        private void ClearForm()
        {
            this.condicionTextBox.Text = string.Empty;
            this.notaTextBox.Text = string.Empty;
            this.alumnosList.SelectedIndex = -1;
            this.cursoList.SelectedIndex = -1;
        }
        
        private void LoadEntity(AlumnoInscripcion inscripcion)
        {
            inscripcion.IDAlumno = new PersonaLogic().DameIDPersona(this.alumnosList.SelectedIndex, Business.Entities.Personas.TiposPersonas.Alumno);
            if (this.FormMode == FormModes.Alta)
            {
                inscripcion.IDCurso = new CursoLogic().DameIDCurso(this.cursoList.SelectedIndex, inscripcion.IDAlumno);
            }
            else
            {
                inscripcion.IDCurso = new CursoLogic().DameIDCurso(this.cursoList.SelectedIndex);
            }
            if (String.IsNullOrEmpty(this.notaTextBox.Text))
            {
                inscripcion.Nota = 0;
            }
            else
            {
                inscripcion.Nota = Convert.ToInt32(this.notaTextBox.Text);
            }
            if (String.IsNullOrEmpty(this.condicionTextBox.Text))
            {
                inscripcion.Condicion = "INSCRIPTO";
            }
            else
            {
                inscripcion.Condicion = this.condicionTextBox.Text;
            }
        }
        
        private void SaveEntity(AlumnoInscripcion curso)
        {
            this.Logic.Save(curso);
        }
        
        private bool EstaInscripto()
        {
            int idAlu = new PersonaLogic().DameIDPersona(this.alumnosList.SelectedIndex,Business.Entities.Personas.TiposPersonas.Alumno);

            return new AlumnoInsLogic().AlumnoEstaInscripto(idAlu,new CursoLogic().DameIDCurso(this.cursoList.SelectedIndex,idAlu));
        }
        
        private void CargarCursos()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador && this.FormMode == FormModes.Alta)
            {
                this.cursoList.DataSource = new CursoLogic().GetAllXPlan(new PersonaLogic().DameIDPlanPersona(this.alumnosList.SelectedIndex, Business.Entities.Personas.TiposPersonas.Alumno));

            }
            else if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador && (this.FormMode == FormModes.Baja || this.FormMode == FormModes.Modificacion))
            {
                this.cursoList.DataSource = new CursoLogic().GetAll();
            }
            else if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
            {
                this.cursoList.DataSource = new CursoLogic().GetAll();
            }
            else
            {
                this.cursoList.DataSource = new CursoLogic().GetAllXPlan(((Usuario)Session["logueo"]).Per.IDPlan);
            }
            this.cursoList.DataTextField = "descripcion";
            this.cursoList.DataBind();
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
                    this.FormMode = FormModes.Baja;
                    LoadGrid();
                    CargarListas();
                    CargarCursos();
                    this.eliminarLinkButton.Visible = false;
                    if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
                    {
                        this.editarLinkButton.Visible = false;
                        this.nuevoLinkButton.Visible = false;
                    }
                    else
                    {
                        this.editarLinkButton.Visible = false;
                        this.gridView.Columns[4].Visible = false;
                    }
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
                    this.FormMode = FormModes.Baja;
                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                    LoadGrid();
                    CargarListas();
                    CargarCursos();
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
            if (this.errorPanel.Visible != true)
            {
                switch (this.FormMode)
                {
                    case (FormModes.Modificacion):
                        {
                            this.Entity = new AlumnoInscripcion();
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
                            if (!EstaInscripto())
                            {
                                if (new AlumnoInsLogic().HayCupo(this.cursoList.SelectedValue))
                                {
                                    this.Entity = new AlumnoInscripcion();
                                    this.LoadEntity(this.Entity);
                                    this.SaveEntity(this.Entity);
                                    this.LoadGrid();
                                }
                                else
                                {
                                    this.errorPanel.Visible = true;
                                    this.mensajeError.Text = "No hay posibilidad de inscripción: No hay cupo.";
                                }
                            }
                            else
                            {
                                this.errorPanel.Visible = true;
                                this.mensajeError.Text = "Inscripción repetida: Alumno ya se encuentra registrado a dicho Curso.";
                            }
                            break;
                        }
                    default:
                        break;
                }
                if (!(this.errorPanel.Visible))
                {
                    Response.Redirect("Inscripciones.aspx");
                }
            }
            else
            {
                this.errorPanel.Visible = false;
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
                Response.Redirect("Inscripciones.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void alumnosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FormMode != FormModes.Baja || this.FormMode != FormModes.Modificacion)
            {
                CargarCursos();
            }
            
        }

        #endregion
                
    }
}