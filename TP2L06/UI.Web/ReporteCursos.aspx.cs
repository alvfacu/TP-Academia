﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class ReporteCursos : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DeshabilitarOpciones();
        }
    }
}