using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GramaticApp
{
    public partial class GramaticApp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public new string ResolveUrl(string link)
        {
            return this.ResolveClientUrl(link);
        }

    }
}