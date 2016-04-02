using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class formPrincipal : Form
    {
        #region Variables

        Login formLogin;

        #endregion

        #region Constructores

        public formPrincipal()
        {
            InitializeComponent();
        }

        #endregion
                     
        #region Eventos

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formPrincipal_Load(object sender, EventArgs e)
        {
            formLogin = new Login();
            if (formLogin.ShowDialog() != DialogResult.OK)
            {
                this.Dispose();
            }
            else
            {
                HabilitarOpciones(Login.UsuarioLogueado.Per.TipoPersona);
                this.Text = "Sistema de Gestión Académica (SGA) - " + Login.UsuarioLogueado.Nombre + " " + Login.UsuarioLogueado.Apellido + " - [" + Login.UsuarioLogueado.Per.TipoPersona + "]";
            }
        }

        private void HabilitarOpciones(Business.Entities.Personas.TiposPersonas tipo)
        {
            if (tipo != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                this.alumnoInscripcionToolStripMenuItem.Visible = true;
                this.comisionesToolStripMenuItem.Visible = true;
                this.cursosToolStripMenuItem.Visible = true;
                this.especialidadToolStripMenuItem.Visible = true;
                this.materiasToolStripMenuItem.Visible = true;
                this.modulosToolStripMenuItem.Visible = false;
                this.moduloUsuariosToolStripMenuItem1.Visible = false;
                this.personasToolStripMenuItem.Visible = true;
                this.planesToolStripMenuItem.Visible = true;
                this.usuariosToolStripMenuItem.Visible = true;
                this.docenteCursoToolStripMenuItem.Visible = true;
            }
            else
            {
                this.alumnoInscripcionToolStripMenuItem.Visible = true;
                this.comisionesToolStripMenuItem.Visible = true;
                this.cursosToolStripMenuItem.Visible = true;
                this.docenteCursoToolStripMenuItem.Visible = true;
                this.especialidadToolStripMenuItem.Visible = true;
                this.materiasToolStripMenuItem.Visible = true;
                this.modulosToolStripMenuItem.Visible = true;
                this.moduloUsuariosToolStripMenuItem1.Visible = true;
                this.personasToolStripMenuItem.Visible = true;
                this.planesToolStripMenuItem.Visible = true;
                this.usuariosToolStripMenuItem.Visible = true;
            }
        }
                
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login.UsuarioLogueado.Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                UsuarioDesktop formUsuarioActual = new UsuarioDesktop(Login.UsuarioLogueado.ID, ApplicationForm.ModoForm.Modificacion);
                formUsuarioActual.ShowDialog();
            }
            else
            {
                Usuarios formUsuario = new Usuarios();
                formUsuario.ShowDialog();
            }
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursos formCurso = new Cursos();
            formCurso.ShowDialog();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materias formMateria = new Materias();
            formMateria.ShowDialog();
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login.UsuarioLogueado.Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                PersonaDesktop formPersonaActual = new PersonaDesktop(Login.UsuarioLogueado.Per.ID, ApplicationForm.ModoForm.Modificacion);
                formPersonaActual.ShowDialog();
            }
            else
            {
                Personas formPersona = new Personas();
                formPersona.ShowDialog();
            }
            
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comisiones formComisiones = new Comisiones();
            formComisiones.ShowDialog();
        }

        private void planesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Planes formPlanes = new Planes();
            formPlanes.ShowDialog();
        }

        private void modulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modulos formModulos = new Modulos();
            formModulos.ShowDialog();
        }

        private void especialidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Especialidades formEspecialidad = new Especialidades();
            formEspecialidad.ShowDialog();
        }

        private void alumnoInscripcionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlumnosInscripciones formAluInscripcion = new AlumnosInscripciones();
            formAluInscripcion.ShowDialog();
        }

        private void moduloUsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ModulosUsuarios formModulosUsr = new ModulosUsuarios();
            formModulosUsr.ShowDialog();
        }

        private void docenteCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocentesCursos formDocCursos = new DocentesCursos();
            formDocCursos.ShowDialog();
        }             
        
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.HabilitarOpciones(Business.Entities.Personas.TiposPersonas.Administrador);
            this.Text = "Sistema de Gestión Académica (SGA)";
            formLogin = new Login();
            if (formLogin.ShowDialog() != DialogResult.OK)
            {
                this.Dispose();
            }
            else
            {
                HabilitarOpciones(Login.UsuarioLogueado.Per.TipoPersona);
                this.Text = "Sistema de Gestión Académica (SGA) - " + Login.UsuarioLogueado.Nombre + " " + Login.UsuarioLogueado.Apellido + " - [" + Login.UsuarioLogueado.Per.TipoPersona + "]";
            }
        }

        #endregion

       
    }
}
