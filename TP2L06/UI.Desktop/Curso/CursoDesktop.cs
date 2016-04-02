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
using Util;

namespace UI.Desktop
{
    public partial class CursoDesktop : ApplicationForm
    {
        #region Variables

        private Curso _cursoActual;

        #endregion

        #region Constructores

        public CursoDesktop()
        {
            InitializeComponent();
            this.Modo = ModoForm.Baja;
            InicializarComboBox();
        }

        public CursoDesktop(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public CursoDesktop(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            CursoActual = new CursoLogic().GetOne(ID);
            MapearDeDatos();
            switch (modo)
            {
                case (ModoForm.Alta):
                    {
                        this.btnAceptar.Text = "Guardar";
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.btnAceptar.Text = "Guardar";
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.btnAceptar.Text = "Eliminar";
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.btnAceptar.Text = "Aceptar";
                        break;
                    }
            }
        }

        #endregion

        #region Propiedades

        public Curso CursoActual
        {
            get { return _cursoActual; }
            set { _cursoActual = value; }
        }

        #endregion

        #region Metodos
        
        private void InicializarComboBox()
        {
            cmbComisiones.DisplayMember = "descripcion";
            cmbMaterias.DisplayMember = "descripcion";
            cmbComisiones.DataSource = new ComisionLogic().GetAll();
            cmbMaterias.DataSource = new MateriaLogic().GetAll();
        }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.CursoActual.ID.ToString();
            this.txtAnio.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();
            this.txtDescripcion.Text = this.CursoActual.Descripcion;

            if (this.Modo != ModoForm.Baja)
            {
                this.cmbComisiones.SelectedIndex = new ComisionLogic().DameIndex(this.CursoActual.IDComision);
                this.cmbMaterias.SelectedIndex = new MateriaLogic().DameIndex(this.CursoActual.IDMateria);
            }
        }

        public virtual void GuardarCambios()
        {
            try
            {
                MapearADatos();
                new CursoLogic().Save(CursoActual);
            }
            catch (ErrorEliminar ex)
            {
                Notificar("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public virtual void MapearADatos()
        {
            Business.Entities.Materia materiaActual = this.DevolverMateria();
            Business.Entities.Comision comisionActual = this.DevolverComision(); 

            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        CursoActual = new Curso();
                        this.CursoActual.AnioCalendario = int.Parse(this.txtAnio.Text);
                        this.CursoActual.Cupo = int.Parse(this.txtCupo.Text);
                        this.CursoActual.Descripcion =  this.txtDescripcion.Text;
                        this.CursoActual.IDComision = comisionActual.ID;
                        this.CursoActual.IDMateria = materiaActual.ID;
                        this.CursoActual.State = BusinessEntity.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.CursoActual.AnioCalendario = int.Parse(this.txtAnio.Text);
                        this.CursoActual.Cupo = int.Parse(this.txtCupo.Text);
                        this.CursoActual.Descripcion = this.txtDescripcion.Text; 
                        this.CursoActual.IDComision = comisionActual.ID;
                        this.CursoActual.IDMateria = materiaActual.ID;
                        this.CursoActual.State = BusinessEntity.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.CursoActual.State = BusinessEntity.States.Deleted;
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.CursoActual.State = BusinessEntity.States.Unmodified;
                        break;
                    }
            }
        }

        private Comision DevolverComision()
        {
            return new ComisionLogic().GetOne(((Business.Entities.Comision)this.cmbComisiones.SelectedValue).ID);
        }

        private Materia DevolverMateria()
        {
            return new MateriaLogic().GetOne(((Business.Entities.Materia)this.cmbMaterias.SelectedValue).ID);
        }

        public virtual bool Validar()
        {
            int nro;
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
                    else if (!(int.TryParse(this.txtAnio.Text, out nro)))
                    {
                        estado = false;
                        Notificar("Error en el ingreso del Año", "Verifique que el Año del Calendario sea un numero entero positivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(int.TryParse(this.txtCupo.Text, out nro)))
                    {
                        estado = false;
                        Notificar("Error en el ingreso del Cupo", "Verifique que el Cupo sea un numero entero positivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return estado;
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
        
        private void cmbMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(this.Modo == ModoForm.Baja || this.Modo == ModoForm.Modificacion))
            {
                cmbComisiones.DisplayMember = "descripcion";
                cmbComisiones.DataSource = new ComisionLogic().GetAllXPlan(DevolverMateria().IDPlan);
            }
        }

        #endregion

    }
}
