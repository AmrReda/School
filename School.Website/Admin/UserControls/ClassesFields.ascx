<%@ Control Language="C#" ClassName="ClassesFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataClassNumber" runat="server" Text="Class Number:" AssociatedControlID="dataClassNumber" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataClassNumber" Text='<%# Bind("ClassNumber") %>' MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataClassNumber" runat="server" Display="Dynamic" ControlToValidate="dataClassNumber" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


