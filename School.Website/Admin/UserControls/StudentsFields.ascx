<%@ Control Language="C#" ClassName="StudentsFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataName" runat="server" Text="Name:" AssociatedControlID="dataName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataName" Text='<%# Bind("Name") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataName" runat="server" Display="Dynamic" ControlToValidate="dataName" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAddress" runat="server" Text="Address:" AssociatedControlID="dataAddress" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAddress" Text='<%# Bind("Address") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataAddress" runat="server" Display="Dynamic" ControlToValidate="dataAddress" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataClassId" runat="server" Text="Class Id:" AssociatedControlID="dataClassId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataClassId" DataSourceID="ClassIdClassesDataSource" DataTextField="ClassNumber" DataValueField="Id" SelectedValue='<%# Bind("ClassId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ClassesDataSource ID="ClassIdClassesDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataBirthdate" runat="server" Text="Birthdate:" AssociatedControlID="dataBirthdate" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataBirthdate" Text='<%# Bind("Birthdate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataBirthdate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataBirthdate" runat="server" Display="Dynamic" ControlToValidate="dataBirthdate" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataGender" runat="server" Text="Gender:" AssociatedControlID="dataGender" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataGender" Text='<%# Bind("Gender") %>' MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataGender" runat="server" Display="Dynamic" ControlToValidate="dataGender" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


