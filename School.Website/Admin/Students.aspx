<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Students.aspx.cs" Inherits="Students" Title="Students List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Students List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="StudentsDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Students.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="[Name]"  />
				<asp:BoundField DataField="Address" HeaderText="Address" SortExpression="[Address]"  />
				<data:HyperLinkField HeaderText="Class Id" DataNavigateUrlFormatString="ClassesEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ClassIdSource" DataTextField="ClassNumber" />
				<asp:BoundField DataField="Birthdate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Birthdate" SortExpression="[Birthdate]"  />
				<asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="[Gender]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Students Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnStudents" OnClientClick="javascript:location.href='StudentsEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:StudentsDataSource ID="StudentsDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:StudentsProperty Name="Classes"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:StudentsDataSource>
	    		
</asp:Content>



