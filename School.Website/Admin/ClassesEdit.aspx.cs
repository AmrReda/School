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

public partial class ClassesEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "ClassesEdit.aspx?{0}", ClassesDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "ClassesEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Classes.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewStudents1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewStudents1.SelectedDataKey.Values[0]);
		Response.Redirect("StudentsEdit.aspx?" + urlParams, true);		
	}	
}


