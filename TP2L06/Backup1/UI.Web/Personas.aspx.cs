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
    public partial class Personas : Base
    {
        #region Variables

        PersonaLogic _logic;

        #endregion

        #region Propiedades

        private PersonaLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new PersonaLogic();
                }
                return _logic;
            }
        }
        
        private Business.Entities.Personas Entity
        {
            get;
            set;
        }
                
        private int Day
        {
            get
            {
                if (Request.Form[this.diasList.UniqueID] != null)
                {
                    return int.Parse(Request.Form[this.diasList.UniqueID]);
                }
                else
                {
                    return int.Parse(this.diasList.SelectedItem.Value);
                }
            }
            set
            {
                this.PopulateDay();
                this.diasList.ClearSelection();
                this.diasList.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        private int Month
        {
            get
            {
                return int.Parse(this.mesesList.SelectedItem.Value);
            }
            set
            {
                this.PopulateMonth();
                this.mesesList.ClearSelection();
                this.mesesList.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        private int Year
        {
            get
            {
                return int.Parse(this.aniosList.SelectedItem.Value);
            }
            set
            {
                this.PopulateYear();
                this.aniosList.ClearSelection();
                this.aniosList.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.Month + "/" + this.Day + "/" + this.Year);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                {
                    this.Year = value.Year;
                    this.Month = value.Month;
                    this.Day = value.Day;
                }
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
            this.planesList.SelectedIndex = new PlanLogic().DameIndex(this.Entity.IDPlan);
            this.tiposList.SelectedIndex = new PersonaLogic().DameIndexTipo(this.Entity.TipoPersona);
            this.aniosList.SelectedIndex = DameIndexAnio(this.Entity.FechaNacimiento.Year);
            this.diasList.SelectedIndex = DameIndexDia(this.Entity.FechaNacimiento.Day);
            this.mesesList.SelectedIndex = DameIndexMes(this.Entity.FechaNacimiento.Month);
            this.apellidoTextBox.Text = this.Entity.Apellido;
            this.nombreTextBox.Text = this.Entity.Nombre;
            this.direccionTextBox.Text = this.Entity.Direccion;
            this.emailTextBox.Text = this.Entity.Email;
            this.legajoTextBox.Text = this.Entity.Legajo.ToString();
            this.telefonoTextBox.Text = this.Entity.Telefono;
        }

        private int DameIndexMes(int mes)
        {
            int index = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (mes == i)
                {
                    break;
                }
                ++index;
            }
            return index+1;
        }

        private int DameIndexDia(int dia)
        {
            int index = 0;
            for (int i = 1; i < 32;++i)
            {
                if (i == dia)
                {
                    break;
                }
                ++index;
            }
            return index+1;
        }

        private int DameIndexAnio(int anio)
        {
            int index = 0;
            for (int i = DateTime.Now.Year; i >= 1950; i--)
            {
                if (i == anio)
                {
                    break;
                }
                ++index;
            }
            return index+1;
        }

        private void EnableForm(bool enable)
        {
            this.planesList.Enabled = enable;
            this.tiposList.Enabled = enable;
            this.apellidoTextBox.Enabled = enable;
            this.nombreTextBox.Enabled = enable;
            this.direccionTextBox.Enabled = enable;
            this.emailTextBox.Enabled = enable;
            this.legajoTextBox.Enabled = enable;
            this.telefonoTextBox.Enabled = enable;
            this.diasList.Enabled = enable;
            this.mesesList.Enabled = enable;
            this.aniosList.Enabled = enable;
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
            this.tiposList.DataSource = new PersonaLogic().DevolverTiposPersonas();
            this.planesList.DataSource = new PlanLogic().GetAll();

            this.planesList.DataTextField = "descripcion";
            this.planesList.DataValueField = "id";

            this.tiposList.DataBind();
            this.planesList.DataBind();
        }
        
        private void LoadEntity(Business.Entities.Personas per)
        {
            per.IDPlan = new PlanLogic().DamePlan(this.planesList.SelectedIndex);
            per.Apellido = this.apellidoTextBox.Text;
            per.Nombre = this.nombreTextBox.Text;
            DateTime fechaNac = new DateTime(Convert.ToInt32(this.aniosList.SelectedValue.ToString()), Convert.ToInt32(this.mesesList.SelectedValue.ToString()), Convert.ToInt32(this.diasList.SelectedValue.ToString()), 0, 0, 0);
            per.FechaNacimiento = fechaNac;
            per.Email = this.emailTextBox.Text;
            per.Direccion = this.direccionTextBox.Text;
            per.Legajo = Convert.ToInt32(this.legajoTextBox.Text);
            per.Telefono = this.telefonoTextBox.Text;
            per.TipoPersona = (Business.Entities.Personas.TiposPersonas)Enum.Parse(typeof(Business.Entities.Personas.TiposPersonas), this.tiposList.SelectedValue);
        }
        
        private void SaveEntity(Business.Entities.Personas per)
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                ((Usuario)Session["logueo"]).Per = per;
            }
            this.Logic.Save(per);
        }

        private void PopulateDay()
        {
            this.diasList.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "DD";
            lt.Value = "0";
            this.diasList.Items.Add(lt);
            if (this.Year != 0 && this.Month != 0)
            {
                int days = DateTime.DaysInMonth(this.Year, this.Month);
                for (int i = 1; i <= days; i++)
                {
                    lt = new ListItem();
                    lt.Text = i.ToString();
                    lt.Value = i.ToString();
                    this.diasList.Items.Add(lt);
                }
                this.diasList.Items.FindByValue(DateTime.Now.Day.ToString()).Selected = true;
            }
        }

        private void PopulateMonth()
        {
            this.mesesList.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "MM";
            lt.Value = "0";
            this.mesesList.Items.Add(lt);
            for (int i = 1; i <= 12; i++)
            {
                lt = new ListItem();
                lt.Text = Convert.ToDateTime("1/" + i.ToString() + "/1900").ToString("MMMM");
                lt.Value = i.ToString();
                this.mesesList.Items.Add(lt);
            }
            this.mesesList.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }

        private void PopulateYear()
        {
            this.aniosList.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "YYYY";
            lt.Value = "0";
            this.aniosList.Items.Add(lt);
            for (int i = DateTime.Now.Year; i >= 1950; i--)
            {
                lt = new ListItem();
                lt.Text = i.ToString();
                lt.Value = i.ToString();
                this.aniosList.Items.Add(lt);
            }
            this.aniosList.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }

        private void ClearForm()
        {
            this.apellidoTextBox.Text = string.Empty;
            this.nombreTextBox.Text = string.Empty;
            this.direccionTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.legajoTextBox.Text = string.Empty;
            this.telefonoTextBox.Text = string.Empty;
            this.diasList.SelectedIndex = 0;
            this.aniosList.SelectedIndex = 0;
            this.mesesList.SelectedIndex = 0;
            this.planesList.SelectedIndex = 0;
            this.tiposList.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            Business.Entities.Personas perActual = ((Usuario)Session["logueo"]).Per;
            this.SelectedID = perActual.ID;
            this.FormMode = FormModes.Modificacion;

            this.nombreTextBox.Text = perActual.Nombre;
            this.apellidoTextBox.Text = perActual.Apellido;
            this.direccionTextBox.Text = perActual.Direccion;
            this.emailTextBox.Text = perActual.Email;
            this.legajoTextBox.Text = perActual.Legajo.ToString();
            this.telefonoTextBox.Text = perActual.Telefono;
            this.planesList.SelectedIndex = new PlanLogic().DameIndex(perActual.IDPlan);
            this.tiposList.SelectedIndex = new PersonaLogic().DameIndexTipo(perActual.TipoPersona);
            this.aniosList.SelectedIndex = DameIndexAnio(perActual.FechaNacimiento.Year);
            this.diasList.SelectedIndex = DameIndexDia(perActual.FechaNacimiento.Day);
            this.mesesList.SelectedIndex = DameIndexMes(perActual.FechaNacimiento.Month);
            this.planesList.Enabled = false;
            this.tiposList.Enabled = false;
            this.legajoTextBox.Enabled = false;
        }
        
        private bool LegajoRepetido()
        {
            return new PersonaLogic().LegajoRepetido(Convert.ToInt32(this.legajoTextBox.Text));
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
                    CargarListas();
                    if (this.SelectedDate == DateTime.MinValue)
                    {
                        this.PopulateYear();
                        this.PopulateMonth();
                        this.PopulateDay();
                    }
                    this.adminPanel.Visible = false;
                    this.usuarioPanel.Visible = true;
                    
                    CargarListas();
                    CargarDatos();
                }
                else
                {
                    if (Request.Form[this.diasList.UniqueID] != null)
                    {
                        this.PopulateDay();
                        this.diasList.ClearSelection();
                        this.diasList.Items.FindByValue(Request.Form[this.diasList.UniqueID]).Selected = true;
                    }
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
                    CargarListas();
                    if (this.SelectedDate == DateTime.MinValue)
                    {
                        this.PopulateYear();
                        this.PopulateMonth();
                        this.PopulateDay();
                    }
                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                }
                else
                {
                    LoadGrid();
                    if (Request.Form[this.diasList.UniqueID] != null)
                    {
                        this.PopulateDay();
                        this.diasList.ClearSelection();
                        this.diasList.Items.FindByValue(Request.Form[this.diasList.UniqueID]).Selected = true;
                    }
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
            bool est = false;
            switch (this.FormMode)
            {
                case (FormModes.Modificacion):
                    {
                        this.Entity = new Business.Entities.Personas();
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
                        if (!(LegajoRepetido()))
                        {
                            this.Entity = new Business.Entities.Personas();
                            this.LoadEntity(this.Entity);
                            this.SaveEntity(this.Entity);
                            this.LoadGrid();
                        }
                        else
                        {
                            est = true;
                            this.errorPanel.Visible = true;
                            this.mensajeError.Visible = true;
                            this.mensajeError.Text = "Legajo repetido";
                        }
                        break;
                    }
                default:
                    break;
            }
            if (!est && ((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
            {
                Response.Redirect("Personas.aspx");
            }
            else if(((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
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
                Response.Redirect("Personas.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        #endregion

    }
}