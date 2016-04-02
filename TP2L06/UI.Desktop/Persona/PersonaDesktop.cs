﻿using System;
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
    public partial class PersonaDesktop : ApplicationForm
    {
        #region Variables

        private Business.Entities.Personas _personaActual;

        #endregion

        #region Constructores
 
        public PersonaDesktop()
        {
            InitializeComponent();
            InicializarComboBox();
            if (Login.UsuarioLogueado.Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                this.cmbTiposPersonas.Enabled = false;
                this.cmbPlan.Enabled = false;
                this.txtLegajo.Enabled = false;
            }
        }       

        public PersonaDesktop(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public PersonaDesktop(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            PersonaActual = new PersonaLogic().GetOne(ID);
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

        public Business.Entities.Personas PersonaActual
        {
            get { return _personaActual; }
            set { _personaActual = value; }
        }

        #endregion
        
        #region Metodos

        private void InicializarComboBox()
        {
            List<String> listaTipos = new PersonaLogic().DevolverTiposPersonas();
            cmbTiposPersonas.DataSource = listaTipos;

            cmbPlan.DisplayMember = "descripcion";
            cmbPlan.DataSource = new PlanLogic().GetAll();
        }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.PersonaActual.ID.ToString();
            this.txtNombre.Text = this.PersonaActual.Nombre;
            this.txtApellido.Text = this.PersonaActual.Apellido;
            this.txtEmail.Text = this.PersonaActual.Email;
            this.txtDia.Text = this.PersonaActual.FechaNacimiento.Day.ToString();
            this.txtMes.Text = this.PersonaActual.FechaNacimiento.Month.ToString();
            this.txtAño.Text = this.PersonaActual.FechaNacimiento.Year.ToString();
            this.txtDireccion.Text = this.PersonaActual.Direccion;
            this.txtLegajo.Text = this.PersonaActual.Legajo.ToString();
            this.txtTelefono.Text = this.PersonaActual.Telefono;
            this.cmbTiposPersonas.SelectedIndex = new PersonaLogic().DameIndexTipo(this.PersonaActual.TipoPersona);
            this.cmbPlan.SelectedIndex = new PlanLogic().DameIndex(this.PersonaActual.IDPlan);
        }

        public virtual void GuardarCambios()
        {
            try
            {
                MapearADatos();
                new PersonaLogic().Save(PersonaActual);
            }
            catch (ErrorEliminar ex)
            {
                //ErrorEliminar miExcep = new ErrorEliminar("No se puede eliminar la persona.");
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
                        PersonaActual = new Business.Entities.Personas();
                        this.PersonaActual.IDPlan = planActual.ID;
                        this.PersonaActual.Nombre = this.txtNombre.Text;
                        this.PersonaActual.Apellido = this.txtApellido.Text;
                        this.PersonaActual.Email = this.txtEmail.Text;
                        this.PersonaActual.Direccion = this.txtDireccion.Text;
                        DateTime fechaNac = new DateTime(int.Parse(this.txtAño.Text), int.Parse(this.txtMes.Text), int.Parse(this.txtDia.Text), 0, 0, 0);
                        this.PersonaActual.FechaNacimiento = fechaNac;
                        this.PersonaActual.Legajo = int.Parse(this.txtLegajo.Text);
                        this.PersonaActual.Telefono = this.txtTelefono.Text;
                        this.PersonaActual.TipoPersona = (Business.Entities.Personas.TiposPersonas)Enum.Parse(typeof(Business.Entities.Personas.TiposPersonas), this.cmbTiposPersonas.SelectedValue.ToString());
                        this.PersonaActual.State = BusinessEntity.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.PersonaActual.IDPlan = planActual.ID;
                        this.PersonaActual.Nombre = this.txtNombre.Text;
                        this.PersonaActual.Apellido = this.txtApellido.Text;
                        this.PersonaActual.Email = this.txtEmail.Text;
                        this.PersonaActual.Direccion = this.txtDireccion.Text;
                        DateTime fechaNac = new DateTime(int.Parse(this.txtAño.Text), int.Parse(this.txtMes.Text), int.Parse(this.txtDia.Text), 0, 0, 0);
                        this.PersonaActual.FechaNacimiento = fechaNac;
                        this.PersonaActual.Legajo = int.Parse(this.txtLegajo.Text);
                        this.PersonaActual.Telefono = this.txtTelefono.Text;
                        this.PersonaActual.TipoPersona = (Business.Entities.Personas.TiposPersonas)Enum.Parse(typeof(Business.Entities.Personas.TiposPersonas), this.cmbTiposPersonas.SelectedValue.ToString());
                        this.PersonaActual.State = BusinessEntity.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.PersonaActual.State = BusinessEntity.States.Deleted;
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.PersonaActual.State = BusinessEntity.States.Unmodified;
                        break;
                    }
            }
        }

        private Plan DevolverPlan()
        {
            return new PlanLogic().GetOne(((Business.Entities.Plan)this.cmbPlan.SelectedValue).ID); ;
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
                    else if (!(ValidarEMails.esMailValido(this.txtEmail.Text)))
                    {
                        estado = false;
                        Notificar("Mail no valido", "Mail no valido. Escribe una dirección de correo electrónico con el formato alguien@ejemplo.com.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(int.TryParse(this.txtAño.Text, out nro)) || !(int.TryParse(this.txtMes.Text, out nro)) || !(int.TryParse(this.txtDia.Text, out nro)))
                    {
                        estado = false;
                        Notificar("Error en la fecha de nacimiento", "Verifique haber ingresado una fecha correcta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(0 < int.Parse(this.txtAño.Text)))
                    {
                        estado = false;
                        Notificar("Error en la fecha de nacimiento", "Verifique haber ingresado para el año un número positivo.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!((0 < int.Parse(this.txtMes.Text)) && (int.Parse(this.txtMes.Text) < 13)))
                    {
                        estado = false;
                        Notificar("Error en la fecha de nacimiento", "Verifique haber ingresado para el mes un número positivo menor a 13.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!((0 < int.Parse(this.txtDia.Text)) && (int.Parse(this.txtDia.Text) < 32)))
                    {
                        estado = false;
                        Notificar("Error en la fecha de nacimiento", "Verifique haber ingresado para el día un número positivo menor a 32.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if(!(int.TryParse(this.txtLegajo.Text, out nro)))
                    {
                        estado = false;
                        Notificar("Formato de legajo incorrecto", "Verifique haber ingresado un legajo correcto:\n El legajo debe contener solo números.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        
        private bool LegajoRepetido()
        {
            bool estado = new PersonaLogic().LegajoRepetido(Convert.ToInt32(this.txtLegajo.Text));
            if (estado)
            {
                Notificar("Legajo duplicado", "El legajo ingresado ya existe.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return estado;
        }
        
        #endregion

        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                if (this.Modo != ModoForm.Alta || !(LegajoRepetido()))
                {
                    GuardarCambios();
                    Close();
                }
            }
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
