#region Imports...
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using School.Web.UI;
#endregion

public partial class StudentsEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "StudentsEdit.aspx?{0}", StudentsDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "StudentsEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Students.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


