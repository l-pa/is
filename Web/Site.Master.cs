using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DomainLayer;

namespace Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["reader"] = new Reader(1);
            readerName.InnerText = ((Reader) Session["reader"]).FirstName + " " + ((Reader) Session["reader"]).LastName;
        }
    }
}