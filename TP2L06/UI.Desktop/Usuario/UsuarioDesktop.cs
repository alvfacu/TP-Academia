﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Negocio;
using Business.Entities;
using Util;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        #region Variables

        private Usuario _usuarioActual;
        bool est = true;

        #endregion

        #region Constructores

        public UsuarioDesktop()
        {
            InitializeComponent();
            InicializarCombox();
            if (Login.UsuarioLogueado.Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                this.cmbPersonas.Enabled = false;
                this.chkHabilitado.Enabled = false;
            }
        }

        public UsuarioDesktop(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public UsuarioDesktop(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            UsuarioActual = new UsuarioLogic().GetOne(ID);
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

        public Usuario UsuarioActual
        {
            get { return _usuarioActual; }
            set { _usuarioActual = value; }
        }

        #endregion

        #region Metodos
        
        private void InicializarCombox()
        {
            cmbPersonas.DataSource = new PersonaLogic().GetAll();
            cmbPersonas.DisplayMember = "completo";
        }

        public virtual void MapearDeDatos() 
        { 
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
            this.cmbPersonas.SelectedIndex = new PersonaLogic().DameIndex(UsuarioActual.Per);
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtClave.Text = this.UsuarioActual.Clave;
            this.txtConfirmarClave.Text = this.UsuarioActual.Clave;
            this.txtEmail.Text = this.UsuarioActual.EMail;
        }

        public virtual void GuardarCambios()
        {
            try
            {
                MapearADatos();
                new UsuarioLogic().Save(UsuarioActual);
            }
            catch (ErrorEliminar ex)
            {
                Notificar("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public virtual void MapearADatos() 
        {
            Business.Entities.Personas personaActual = this.ObtenerPersona();
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    { 
                        UsuarioActual = new Usuario();
                        this.UsuarioActual.Per = personaActual;
                        this.UsuarioActual.Apellido = personaActual.Apellido;
                        this.UsuarioActual.Nombre = personaActual.Nombre;
                        this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                        this.UsuarioActual.Clave = this.txtClave.Text;
                        this.UsuarioActual.EMail = this.txtEmail.Text;
                        this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                        this.UsuarioActual.State = BusinessEntity.States.New;
                        break; 
                    }
                case (ModoForm.Modificacion):
                    {
                        this.UsuarioActual.Apellido = personaActual.Apellido;
                        this.UsuarioActual.Nombre = personaActual.Nombre;
                        this.UsuarioActual.Per = personaActual;                        
                        this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                        this.UsuarioActual.Clave = this.txtClave.Text;
                        this.UsuarioActual.EMail = this.txtEmail.Text;
                        this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                        this.UsuarioActual.State = BusinessEntity.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.UsuarioActual.State = BusinessEntity.States.Deleted;
                        break;
                        
                    }
                case (ModoForm.Consulta):
                    {
                        this.UsuarioActual.State = BusinessEntity.States.Unmodified;
                        break;
                    }
            }
        }

        private Business.Entities.Personas ObtenerPersona()
        {
            return new PersonaLogic().GetOne(((Business.Entities.Personas)this.cmbPersonas.SelectedValue).ID);
        }
       
        public virtual bool Validar() 
        { 
            Boolean estado = true;
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
                else if (!(Util.ValidarEMails.esMailValido(this.txtEmail.Text)))
                {
                    estado = false;
                    Notificar("Mail no valido", "Mail no valido. Escribe una dirección de correo electrónico con el formato alguien@ejemplo.com.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(String.Equals(this.txtClave.Text, this.txtConfirmarClave.Text)))
                {
                    estado = false;
                    Notificar("Contraseñas incorrectas", "Las contraseñas no coinciden.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!((this.txtClave.Text).Length >= 8))
                {
                    estado = false;
                    Notificar("Clave corta", "La contraseña debe contener al menos 8 caracteres.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        
        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (est == true)
            {
                est = false;
                this.txtClave.PasswordChar = '\0';
                this.txtConfirmarClave.PasswordChar = '\0';
            }
            else
            {
                est = true;
                this.txtClave.PasswordChar = '•';
                this.txtConfirmarClave.PasswordChar = '•';
            }
        }

        private void cmbPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtEmail.Text = ObtenerPersona().Email;
        }

        #endregion
    }
}