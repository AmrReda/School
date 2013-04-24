<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ClassesEdit.aspx.cs" Inherits="ClassesEdit" Title="Classes Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Classes - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="ClassesDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ClassesFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ClassesFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Classes not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ClassesDataSource ID="ClassesDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:ClassesDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewStudents1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewStudents1_SelectedIndexChanged"			 			 
			DataSourceID="StudentsDataSource1"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Students.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="[Name]" />				
				<asp:BoundField DataField="Address" HeaderText="Address" SortExpression="[Address]" />				
				<data:HyperLinkField HeaderText="Class Id" DataNavigateUrlFormatString="ClassesEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ClassIdSource" DataTextField="ClassNumber" />
				<asp:BoundField DataField="Birthdate" HeaderText="Birthdate" SortExpression="[Birthdate]" />				
				<asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="[Gender]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Students Found! </b>
				<asp:HyperLink runat="server" ID="hypStudents" NavigateUrl="~/admin/StudentsEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:StudentsDataSource ID="StudentsDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:StudentsProperty Name="Classes"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:StudentsFilter  Column="ClassId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:StudentsDataSource>		
		
		<br />
		

</asp:Content>

