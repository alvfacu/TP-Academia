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
    public partial class ModuloUsuarioDesktop : ApplicationForm
    {
        #region Variables

        private ModuloUsuario _moduloUsrActual;

        #endregion

        #region Constructores

        public ModuloUsuarioDesktop()
        {
            InitializeComponent();
            InicializarComboBox();
        }        

        public ModuloUsuarioDesktop(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public ModuloUsuarioDesktop(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            ModuloUsrActual = new ModuloUsuarioLogic().GetOne(ID);
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
        
        public ModuloUsuario ModuloUsrActual
        {
            get { return _moduloUsrActual; }
            set { _moduloUsrActual = value; }
        }

        #endregion

        #region Metodos

        private void InicializarComboBox()
        {
            cmbModulos.DisplayMember = "descripcion";
            cmbUsuarios.DisplayMember = "nombreusuario";
            cmbModulos.DataSource = new ModuloLogic().GetAll();
            cmbUsuarios.DataSource = new UsuarioLogic().GetAll();
        }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.ModuloUsrActual.ID.ToString();
            this.cmbModulos.SelectedIndex = new ModuloLogic().DameIndex(this.ModuloUsrActual.IdModulo);
            this.cmbUsuarios.SelectedIndex = new UsuarioLogic().DameIndex(this.ModuloUsrActual.IdUsuario);
            this.chkAlta.Checked = this.ModuloUsrActual.PermiteAlta;
            this.chkBaja.Checked = this.ModuloUsrActual.PermiteBaja;
            this.chkConsulta.Checked = this.ModuloUsrActual.PermiteConsulta;
            this.chkMod.Checked = this.ModuloUsrActual.PermiteModificacion;
        }

        public virtual void GuardarCambios()
        {
            try
            {
                MapearADatos();
                new ModuloUsuarioLogic().Save(ModuloUsrActual);
            }
            catch (ErrorEliminar ex)
            {
                //ErrorEliminar miExcep = new ErrorEliminar("No se puede eliminar el módulo.");
                Notificar("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public virtual void MapearADatos()
        {
            Business.Entities.Modulo moduloActual = this.DevolverModulo();
            Business.Entities.Usuario usuarioActual = this.DevolverUsuario();
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        ModuloUsrActual = new ModuloUsuario();
                        this.ModuloUsrActual.IdUsuario = usuarioActual.ID;
                        this.ModuloUsrActual.IdModulo = moduloActual.ID;
                        this.ModuloUsrActual.PermiteAlta = this.chkAlta.Checked;
                        this.ModuloUsrActual.PermiteBaja = this.chkBaja.Checked;
                        this.ModuloUsrActual.PermiteConsulta = this.chkConsulta.Checked;
                        this.ModuloUsrActual.PermiteModificacion = this.chkMod.Checked;
                        this.ModuloUsrActual.State = BusinessEntity.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.ModuloUsrActual.IdUsuario = usuarioActual.ID;
                        this.ModuloUsrActual.IdModulo = moduloActual.ID;
                        this.ModuloUsrActual.PermiteAlta = this.chkAlta.Checked;
                        this.ModuloUsrActual.PermiteBaja = this.chkBaja.Checked;
                        this.ModuloUsrActual.PermiteConsulta = this.chkConsulta.Checked;
                        this.ModuloUsrActual.PermiteModificacion = this.chkMod.Checked;
                        this.ModuloUsrActual.State = BusinessEntity.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.ModuloUsrActual.State = BusinessEntity.States.Deleted;
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.ModuloUsrActual.State = BusinessEntity.States.Unmodified;
                        break;
                    }
            }
        }

        private Usuario DevolverUsuario()
        {
            return new UsuarioLogic().GetOne(((Business.Entities.Usuario)this.cmbUsuarios.SelectedValue).ID); ;
        }

        private Modulo DevolverModulo()
        {
            return new ModuloLogic().GetOne(((Business.Entities.Modulo)this.cmbModulos.SelectedValue).ID);
        }

        public virtual bool Validar()
        {
            return true;
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
