using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;

namespace UI.Web
{
    public partial class Base : System.Web.UI.Page
    {
        #region Variables

        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }

        #endregion

        #region Propiedades

        protected FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }

        protected int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }

        protected bool IsEntitySelected
        {
            get
            {
                return this.SelectedID != 0;
            }
        }

        #endregion

        #region Metodos

        public void DeshabilitarOpciones()
        {
            var menu = (Menu)this.Page.Master.FindControl("menu");
            if (menu != null && (Session["logueo"] == null))
            {
                HabilitarOpciones(menu, false);
            }
            else if ((Session["logueo"] != null))
            {
                HabilitarOpciones(menu, true);
            }
        }

        public void HabilitarOpciones(Menu menu, bool enable)
        {
            if ((Session["logueo"] == null))
            {

            }
            else if ((menu.Items.Count != 10 && ((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador) || (menu.Items.Count != 12 && ((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)) //Para no duplicar el menu
            {
                MenuItem it = new MenuItem("Usuarios", " ", null, "~/Usuarios.aspx", " ");
                it.ToolTip = "Usuarios";
                menu.Items.Add(it);

                it = new MenuItem("Comisiones", " ", null, "~/Comisiones.aspx", " ");
                it.ToolTip = "Comisiones";
                menu.Items.Add(it);

                it = new MenuItem("Planes", " ", null, "~/Planes.aspx", " ");
                it.ToolTip = "Planes";
                menu.Items.Add(it);

                it = new MenuItem("Especialidades", " ", null, "~/Especialidades.aspx", " ");
                it.ToolTip = "Especialidades";
                menu.Items.Add(it);

                it = new MenuItem("Cursos", " ", null, "~/Cursos.aspx", " ");
                it.ToolTip = "Cursos";
                menu.Items.Add(it);

                it = new MenuItem("Materias", " ", null, "~/Materias.aspx", " ");
                it.ToolTip = "Materias";
                menu.Items.Add(it);

                it = new MenuItem("Personas", " ", null, "~/Personas.aspx", " ");
                it.ToolTip = "Personas";
                menu.Items.Add(it);

                it = new MenuItem("Dictados", " ", null, "~/DocentesCursos.aspx", " ");
                it.ToolTip = "Dictados";
                menu.Items.Add(it);

                it = new MenuItem("Inscripciones", " ", null, "~/Inscripciones.aspx", " ");
                it.ToolTip = "Inscripciones";
                menu.Items.Add(it);

                if (((Business.Entities.Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
                {
                    it = new MenuItem("Modulos Usuario", " ", null, "~/ModulosUsuarios.aspx", " ");
                    it.ToolTip = "Modulos Usuario";
                    menu.Items.Add(it);

                    it = new MenuItem("Modulos", " ", null, "~/Modulos.aspx", " ");
                    it.ToolTip = "Modulos";
                    menu.Items.Add(it);
                }
            }
        }

        #endregion
    }
}