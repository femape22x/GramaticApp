using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GramaticApp.Pages
{
    public partial class GramaticApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCodigo.Attributes.Add("placeholder", "Ingresar código...");
            lblResultado.Text = "Resultado...";
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = String.Empty;
            txtCodigo.Attributes.Add("placeholder", "Ingresar código...");
            lblResultado.Text = "Resultado...";
        }

        protected void btnCompilar_Click(object sender, EventArgs e)
        {

        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {

        }
    }
}