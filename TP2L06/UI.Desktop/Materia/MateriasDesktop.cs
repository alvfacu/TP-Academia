using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business.Entities;
using Negocio;
using Data.Database;
using Util;

namespace UI.Desktop
{
    public partial class MateriasDesktop : ApplicationForm
    {
        #region Variables

        private Materia _materiaActual;

        #endregion

        #region Constructores

        public MateriasDesktop()
        {
            InitializeComponent();
            InicializarComboBox();
        }
        

        public MateriasDesktop(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public MateriasDesktop(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            MateriaActual = new MateriaLogic().GetOne(ID);
            MapearDeDatos();
            switch (modo)
            {
                case (ModoForm.Alta): this.btnAceptar.Text = "Guardar";
                    break;
                case (ModoForm.Modificacion): this.btnAceptar.Text = "Guardar";
                    break;
                case (ModoForm.Baja): this.btnAceptar.Text = "Eliminar";
                    break;
                case (ModoForm.Consulta): this.btnAceptar.Text = "Aceptar";
                    break;
            }
        }

        #endregion

        #region Propiedades

        public Materia MateriaActual
        {
            get { return _materiaActual; }
            set { _materiaActual = value; }
        }

        #endregion

        #region Metodos

        private void InicializarComboBox()
        {
            cmbIDPlan.DisplayMember = "descripcion";
            cmbIDPlan.DataSource = new PlanLogic().GetAll();
        }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.MateriaActual.ID.ToString();
            this.txtDescripcion.Text = this.MateriaActual.Descripcion.ToString();
            this.txtHSSemanales.Text = this.MateriaActual.HSSemanales.ToString();
            this.txtHSTotales.Text = this.MateriaActual.HSTotales.ToString();
            this.cmbIDPlan.SelectedIndex = new PlanLogic().DameIndex(this.MateriaActual.IDPlan);
        }

        public virtual void GuardarCambios()
        {
            try
            {
                MapearADatos();
                new MateriaLogic().Save(MateriaActual);
            }
            catch (ErrorEliminar ex)
            {
                Notificar("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public virtual void MapearADatos()
        {
            Business.Entities.Plan planActual = this.DevolverPlan();
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        MateriaActual = new Materia();
                        this.MateriaActual.Descripcion = this.txtDescripcion.Text;
                        this.MateriaActual.HSSemanales = int.Parse(this.txtHSSemanales.Text);
                        this.MateriaActual.HSTotales = int.Parse(this.txtHSTotales.Text);
                        this.MateriaActual.IDPlan = planActual.ID;
                        this.MateriaActual.State = BusinessEntity.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.MateriaActual.Descripcion = this.txtDescripcion.Text;
                        this.MateriaActual.HSSemanales = int.Parse(this.txtHSSemanales.Text);
                        this.MateriaActual.HSTotales = int.Parse(this.txtHSTotales.Text);
                        this.MateriaActual.IDPlan = planActual.ID;
                        this.MateriaActual.State = BusinessEntity.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.MateriaActual.State = BusinessEntity.States.Deleted;
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.MateriaActual.State = BusinessEntity.States.Unmodified;
                        break;
                    }
            }
        }

        private Plan DevolverPlan()
        {
            return new PlanLogic().GetOne(((Business.Entities.Plan)this.cmbIDPlan.SelectedValue).ID);
        }
        
        public virtual bool Validar()
        {
            Boolean estado = true;
            try
            {
                if (!(this.Modo == ModoForm.Baja))
                {
                    foreach (Control control in this.tableLayoutPanel1.Controls)
                    {
                        if (!(control == txtID))
                        {
                            if (control is TextBox && control.Text == String.Empty)
                            {
                                estado = false;
                            }
                        }
                    }
                    if (estado == false)
                    {
                        Notificar("Campos vacíos", "Existen campos sin completar.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (int.Parse(this.txtHSSemanales.Text) > int.Parse(this.txtHSTotales.Text))
                    {
                        estado = false;
                        Notificar("Ingreso incorrecto de horas", "Verifique el ingreso del nro de horas:\nEl nro de horas semanales es mayor que el nro de horas totales.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                return estado;
            }
            catch (FormatException fe)
            {
                Notificar("ERROR", "Formato de hora incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                estado = false;
            }
            catch (Exception e)
            {
                Notificar("ERROR", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                estado = false;
            }
            return estado;
        }

        #endregion

        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                GuardarCambios();
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           Close();
        }

        
        #endregion

    }
}
